using Repository.Interfaces;
using System;
using System.Collections.Generic;
using Domain;

using NHibernate.Criterion;
using Domain.Dto;
using NHibernate;
using NHibernate.Transform;
//=============     Query using FUTURE and FUTURE.VALUE     =================
//=============     Query using AliasToBean     =================
//=============     QueryOver with JoinAlias that returns 2 properties     =================



//---------           PROJECTIONS.CONDITIONAL method       -------------

namespace Repository.Implementation
{
    public class RepositorySicknessHistory : BaseRepository, IRepositorySicknessHistory
    {
        //===========     AliasToBean transformer    =================
        //***********     PROJECTIONS.CONDITIONAL method     *******************
        public IList<SicknessHistorySpecificateDto> SicknessHistoryWithSpecification()
        {
            SicknessHistory sicknessHistoryAlias = null;
            SicknessHistorySpecificateDto shSpecificateDtoAlias = null;

            var rez = _session.QueryOver(() => sicknessHistoryAlias)
                .SelectList(list =>
                list.Select(x => x.Id).WithAlias(() => shSpecificateDtoAlias.Id)
                    .Select(x => x.NameSickness.Id).WithAlias(() => shSpecificateDtoAlias.SicknessId)
                    .Select(x => x.Patient.Id).WithAlias(() => shSpecificateDtoAlias.PatientId)
                    .Select(x => x.Doctor.Id).WithAlias(() => shSpecificateDtoAlias.DoctorId)
                    .Select(Projections.Conditional(
                                        Restrictions.Where<SicknessHistory>(x => x.FinishDate != null),
                                        Projections.Constant("Actuala", NHibernateUtil.String),
                                        Projections.Constant("OFF", NHibernateUtil.String)))
                                        .WithAlias(() => shSpecificateDtoAlias.State)
                    )
                    .TransformUsing(Transformers.AliasToBean<SicknessHistorySpecificateDto>())
                    .List<SicknessHistorySpecificateDto>();
            return rez;
        }

        //=============     QueryOver with JoinAlias that returns 2 properties     =================
        public void CorespondencePatientDoctor()
        {
            SicknessHistory sh = null;
            Patient patient = null;
            PatientDoctorDto patientDoctorDto = null;

            var rez = _session.QueryOver(() => patient)
                .JoinAlias(() => patient.SickHistories, () => sh,NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                .SelectList(list => list
                    .Select(() => patient.Id).WithAlias(() => patientDoctorDto.PatientId)
                    .Select(() => sh.Doctor.Id).WithAlias(() => patientDoctorDto.DoctorId))
                .TransformUsing(Transformers.AliasToBean<PatientDoctorDto>())
                .OrderByAlias(() => patientDoctorDto.PatientId)
                .Asc
                .OrderByAlias(() => patientDoctorDto.DoctorId)
                .Asc
                .List<PatientDoctorDto>();
        }

        //=============     Query using FUTURE     =================
        //=============     Query using FUTURE.VALUE     =================
        public int CountSicknessHistoriesAndShortInfoAboutPersonsInHospital()
        {
            Doctor doctorAlias = null;
            MedicalSpecialty medicalSpecialtyAlias = null;
            DoctorRedusDto doctorRedusDtoAlias = null;
            Certificate certificate = null;

            Patient patient = null;
            SicknessHistory sicknessHistory = null;
            PatientRedusDto patientRedusDto = null;

            var doctorsList = _session.QueryOver(() => doctorAlias)
                .SelectList(list => list
                .Select(x => x.Id).WithAlias(() => doctorRedusDtoAlias.IdDoctor)
                .Select(x => x.Name).WithAlias(() => doctorRedusDtoAlias.Name)
                .Select(x => x.Surname).WithAlias(() => doctorRedusDtoAlias.Surname)
                .SelectSubQuery(QueryOver.Of(() => medicalSpecialtyAlias)
                                        .Where(() => medicalSpecialtyAlias.Id == doctorAlias.Profession.Id)
                                        .Select(x => x.MedicalSpecialtyName)).WithAlias(() => doctorRedusDtoAlias.ProfessionName)
                .SelectSubQuery(QueryOver.Of(() => certificate)
                                           .Where(() => certificate.Doctor.Id == doctorAlias.Id)
                                           .SelectList(list1 => list1
                                                    .SelectCount(x => x.Id)))
                        .WithAlias(() => doctorRedusDtoAlias.CountCertificate)
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
                .Future<DoctorRedusDto>();

            
            var patientList = _session.QueryOver(() => patient)
                .SelectList(list => list
                .Select(x => x.Id).WithAlias(() => patientRedusDto.IdPatient)
                .Select(x => x.Name).WithAlias(() => patientRedusDto.Name)
                .Select(x => x.Surname).WithAlias(() => patientRedusDto.Surname)
                .SelectSubQuery(QueryOver.Of(() => sicknessHistory)
                                                .Where(() => sicknessHistory.Patient.Id == patient.Id)
                                                .SelectList(list1 => list1
                                                         .SelectCount(x => x.Id)))
                        .WithAlias(() => patientRedusDto.CountSicknessHistories)
                .Select(Projections.SqlFunction("concat"
                                                , NHibernateUtil.String
                                                , Projections.Property(() => patient.Name)
                                                , Projections.SqlFunction("concat"
                                                                         , NHibernateUtil.String
                                                                         , Projections.Constant(" ", NHibernateUtil.String)
                                                                         , Projections.Property(() => patient.Surname))))
                        .WithAlias(() => patientRedusDto.FullName))

                .TransformUsing(Transformers.AliasToBean<PatientRedusDto>())
                .OrderByAlias(() => patientRedusDto.FullName)
                .Asc
                .Future<PatientRedusDto>();

            var countSicknessHistory = _session.QueryOver<SicknessHistory>()
                .SelectList(list=>list
                    .SelectCount(x=>x.Id))
                .FutureValue<int>();

            return countSicknessHistory.Value;
        }



        public IList<SicknessHistory> ClosedSicknessHistoryBeforeData(DateTime date)
        {
            var rez = _session.QueryOver<SicknessHistory>().Where(x => x.FinishDate < date).List();
            return rez;
        }

        public IList<SicknessHistory> SicknessHistoriesClosedOrderByDoctors()
        {
            // SicknessHistory sicknessHistoryAlias = null;
            return _session.QueryOver<SicknessHistory>()
                .WhereNot(x => x.FinishDate == null)
                .OrderBy(x => x.Doctor)
                .Asc
                .List();
        }

        public IList<SicknessHistory> SicknessHistoriesOrderByDoctors()
        {
            var rez = _session.QueryOver<SicknessHistory>()
                              .OrderBy(x => x.Doctor)
                              .Asc
                              .List();
            return rez;
        }


    }
}
