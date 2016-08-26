using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Repository.Implementation;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Domain.Dto;
//=============     Write Queries, using AliasToBean and DistinctRootEntity transformer     ==================
//=============     Write query using SelectCount and Having     ===============



//----------            SELECTSUBQUERY           -------------------
//----------            Write Queries using queryover with different Subquery types – WhereProperty Exists, In, All          ----------------
//----------            Integrate a SQL function.

//Distinct in SQL


namespace Repository.Implementation
{
    class RepositoryDoctor : BaseRepository, IRepositoryDoctor
    {
        //***************     Using queryover with WhereProperty EXISTS     ***************
        public IList<Doctor> DoctorsHavingCertificatesForOneOf2Specialty(long specialty1, long specialty2)
        {
            Doctor doctor = null;

            var rez = _session.QueryOver(()=>doctor)
                .WithSubquery
                .WhereExists(QueryOver.Of<Certificate>()
                                      .Where(c=>(c.Specialty.Id == specialty1 || c.Specialty.Id == specialty2) && c.Doctor.Id == doctor.Id)
                                      .Select(c=>c.Doctor.Id))
                .List();

            return rez;
        }

        //=============    DistinctRootEntity transformer     ==================
        //***************     Using queryover with WhereProperty IN     ***************
        public IList<Doctor> DoctorsWithCertificatesReceivingDateBefore(DateTime date)
        {
            Certificate certificateAlias = null;
            Doctor doctorAlias = null;

            var queryCertReceivingDateBefore = QueryOver.Of(() => certificateAlias)
                                                        .Where(x => x.DataOfReceiving < date)
                                                        .Select(x => x.Doctor.Id);

            var rez = _session.QueryOver(() => doctorAlias)
                              .WithSubquery
                              .WhereProperty(d => d.Id).In(queryCertReceivingDateBefore)
                              .TransformUsing(Transformers.DistinctRootEntity)
                              .List();
            return rez;
        }

        //***************     Using queryover with WhereProperty ALL     ***************
        public Doctor OldestEmployeeDoctor() //+++++++++++++++++++
        {

            //            --Selectam cel mai tirziu angajat doctor
            //select DateofStart
            //FROM   Doctor AS d
            //where DateOfStart >= ALL(select DateofStart
            //                            FROM   Doctor AS d2)

            var rez = _session.QueryOver<Doctor>()
                .WithSubquery
                .WhereProperty(x => x.DateOfStart).LeAll(QueryOver.Of<Doctor>()
                                                               .Select(x => x.DateOfStart))
                .SingleOrDefault();

            var rez1 = _session.QueryOver<Doctor>()
                .WithSubquery
                .WhereAll(x => x.DateOfStart <= (QueryOver.Of<Doctor>()
                                                       .Select(d => d.DateOfStart).As<DateTime>()))
                .SingleOrDefault();

            return rez;
        }

        //===============     AliasToBean  transformer     ==================
        //***************     SELECTSUBQUERY     ***************
        //***************     Integrate a SQL function     ***************
        public IList<DoctorRedusDto> ShortInfoDoctor()  //Short info + count Certificates
        {
            Doctor doctorAlias = null;
            MedicalSpecialty medicalSpecialtyAlias = null;
            DoctorRedusDto doctorRedusDtoAlias = null;
            Certificate certificate = null;

            var subquery = QueryOver.Of(() => certificate)
                                           .Where(() => certificate.Doctor.Id == doctorAlias.Id)
                                           .SelectList(list1 => list1
                                                    .SelectCount(x => x.Id));

            var rez = _session.QueryOver(() => doctorAlias)
                .SelectList(list => list
                .Select(x => x.Id).WithAlias(() => doctorRedusDtoAlias.IdDoctor)
                .Select(x => x.Name).WithAlias(() => doctorRedusDtoAlias.Name)
                .Select(x => x.Surname).WithAlias(() => doctorRedusDtoAlias.Surname)
                .SelectSubQuery(QueryOver.Of(() => medicalSpecialtyAlias)
                                        .Where(() => medicalSpecialtyAlias.Id == doctorAlias.Profession.Id)
                                        .Select(x => x.MedicalSpecialtyName)).WithAlias(() => doctorRedusDtoAlias.ProfessionName)
                .SelectSubQuery(subquery).WithAlias(() => doctorRedusDtoAlias.CountCertificate)
                .Select(Projections.SqlFunction("concat"
                                                , NHibernateUtil.String
                                                , Projections.Property(() => doctorAlias.Name)
                                                , Projections.SqlFunction("concat"
                                                                         , NHibernateUtil.String
                                                                         , Projections.Constant(" ", NHibernateUtil.String)
                                                                         , Projections.Property(() => doctorAlias.Surname))))
                        .WithAlias(() => doctorRedusDtoAlias.FullName))

                .TransformUsing(Transformers.AliasToBean<DoctorRedusDto>())
                .OrderByAlias(() => doctorRedusDtoAlias.CountCertificate)
                .Asc
                .List<DoctorRedusDto>();
            return rez;
        }

        //=============     Write query using SelectCount and Having     ===============
        public IList<DoctorsWithExperienceDto> DoctorsWithExperience()
        {
            SicknessHistory sicknessHistory = null;
            DoctorsWithExperienceDto doctorWithExperienceDto = null;

            var rez = _session.QueryOver<Doctor>()
               .JoinQueryOver(d => d.ListSikcnessHistories, () => sicknessHistory)
               .SelectList(list => list
                   .SelectGroup(d => d.Id).WithAlias(()=> doctorWithExperienceDto.IdDoctor)
                   .SelectCount(() => sicknessHistory.Id).WithAlias(() => doctorWithExperienceDto.CountSicknessHistory))
               .Where(Restrictions.Gt(Projections.Count(Projections.Property(() => sicknessHistory.Id)), 3))
               .TransformUsing(Transformers.AliasToBean<DoctorsWithExperienceDto>())
               .List<DoctorsWithExperienceDto>();
            
            Doctor doc = null;
            var rez1 = _session.QueryOver(() => doc)
               .JoinQueryOver(() => doc.ListSikcnessHistories, () => sicknessHistory)
               .SelectList(list => list
                   .SelectGroup(d => d.Id).WithAlias(() => doctorWithExperienceDto.IdDoctor)
                   .SelectCount(() => sicknessHistory.Id).WithAlias(() => doctorWithExperienceDto.CountSicknessHistory))
               .Where(Restrictions.Between(Projections.Group(() => doc.Id), 101, 104))
               .TransformUsing(Transformers.AliasToBean<DoctorsWithExperienceDto>())
               .List<DoctorsWithExperienceDto>();

            var rez2 = _session.QueryOver(() => doc)
               .JoinQueryOver(() => doc.ListSikcnessHistories, () => sicknessHistory)
               .SelectList(list => list
                   .SelectGroup(d => d.Id).WithAlias(() => doctorWithExperienceDto.IdDoctor)
                   .SelectCount(() => sicknessHistory.Id).WithAlias(() => doctorWithExperienceDto.CountSicknessHistory))
               .Where(Restrictions.Between(Projections.GroupProperty(Projections.Property(() => doc.Id)), 101, 104))
               .TransformUsing(Transformers.AliasToBean<DoctorsWithExperienceDto>())
               .List<DoctorsWithExperienceDto>();

            return rez;
        }

        //=============     Select Count     ==================
        //===============     AliasToBean  transformer     ==================
        public IList<DoctorCountSicknessHistoriesDto> DoctorSicknessHistoryCount()
        {
            DoctorCountSicknessHistoriesDto doctorCountSickness = null;
            var rez = _session.QueryOver<SicknessHistory>()
                .SelectList(list => list
                    .SelectGroup(x => x.Doctor.Id).WithAlias(() => doctorCountSickness.DoctorId)
                    .SelectCount(x => x.Id).WithAlias(() => doctorCountSickness.CountSH))
                .TransformUsing(Transformers.AliasToBean<DoctorCountSicknessHistoriesDto>())
                .List<DoctorCountSicknessHistoriesDto>();
            return rez;
        }

        //=============    DistinctRootEntity transformer     ==================
        //                 Distinct in SQL
        public IList<Doctor> DoctorsTratePatient(long patientId)
        {
            SicknessHistory sicknessHistoryAlias = null;
            Doctor doctorAlias = null;

            var rez = _session.QueryOver<Doctor>()
                .JoinAlias(x => x.ListSikcnessHistories, () => sicknessHistoryAlias)
                .Where(() => sicknessHistoryAlias.Patient.Id == patientId)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
            
            // using Alias
            var rez1 = _session.QueryOver(() => doctorAlias)
                               .JoinAlias(() => doctorAlias.ListSikcnessHistories, () => sicknessHistoryAlias)
                               .Where(() => sicknessHistoryAlias.Patient.Id == patientId)
                               .TransformUsing(Transformers.DistinctRootEntity)
                               .List();
            //distinct in SQL
            var rez2 = _session.QueryOver(() => doctorAlias)
                              .JoinAlias(() => doctorAlias.ListSikcnessHistories, () => sicknessHistoryAlias)
                              
                              .Where(() => sicknessHistoryAlias.Patient.Id == patientId)
                              .Select(Projections.Distinct
                                (Projections.ProjectionList()
                                    .Add(Projections.Property(() => doctorAlias.Id))
                                    .Add(Projections.Property(() => doctorAlias.Name))
                                    .Add(Projections.Property(() => doctorAlias.Surname))
                                    ))
                              .TransformUsing(Transformers.AliasToBean<Doctor>())
                              .List();

            return rez;
        }

        public IList<Doctor> GetDoctorsWithSpecialty(long idMedicalSpecialty)
        {
            var doctors = _session.QueryOver<Doctor>()
                            .Where(x => x.Profession.Id == idMedicalSpecialty).List<Doctor>();
            return doctors;
        }

        public void ModifySpecialty(long idDoctor, long newProfession)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                var doctor = _session.Load<Doctor>(idDoctor);

                var profession = _session.Load<MedicalSpecialty>(newProfession);

                if (profession != null)
                    doctor.Profession = profession;

                _session.Update(doctor);

                transaction.Commit();
            }
        }

        public IList<Doctor> DoctorsWithAllExpiredCertificates()
        {
            //?????????????????????????????????????????????????????  ADD DAYS to Date
            //Doctorii care au certificate valide in ziua de azi
            //  Console.WriteLine(new DateTime(2012, 12, 31).AddDays(365));
            Certificate certificateAlias = null;
            Doctor doctorAlias = null;

            var rez = _session.QueryOver(() => certificateAlias)
                              .JoinAlias(() => certificateAlias.Doctor, () => doctorAlias)
                              .Where(() => certificateAlias.DataOfReceiving.AddDays((double)certificateAlias.ValidFor) >= DateTime.Now)
                              .List();

            return null;
        }

        public IList<MedicalSpecialtyForCreationDoctorDto> ListMedicalSpecialtyFoCreationDoctor()
        {
            MedicalSpecialtyForCreationDoctorDto msfcd = null;
            MedicalSpecialty ms = null;
            var rez = _session.QueryOver(() => ms)
                .SelectList(list => list
                .Select(() => ms.Id).WithAlias(() => msfcd.Id)
                .Select(() => ms.MedicalSpecialtyName).WithAlias(() => msfcd.MedicalSpecialtyName))
                .TransformUsing(Transformers.AliasToBean<MedicalSpecialtyForCreationDoctorDto>())
                .List<MedicalSpecialtyForCreationDoctorDto>();
            return rez;
        }
    }
}
