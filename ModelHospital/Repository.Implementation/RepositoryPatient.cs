using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using NHibernate.Criterion;
using NHibernate.Transform;
using Domain.Dto;
using NHibernate;
//=============     Write Queries, using AliasToBean and DistinctRootEntity transformer     ==================



//----------            Write Queries using queryover with different Subquery types – WhereProperty EXISTS, IN, SOME(ANY)
//----------            SELECTSUBQUERY           -------------------
//----------            Integrate a SQL function.


namespace Repository.Implementation
{
    class RepositoryPatient : BaseRepository, IRepositoryPatient
    {
        public IList<Patient> AllPatients()
        {
            return _session.QueryOver<Patient>().List();
        }

        //***************     Using queryover with WhereProperty SOME  (ANY)     ***************
        public IList<Patient> PatientsRepeated()
        {
            Patient patient = null;
            SicknessHistory sh = null;
            var rez = _session.QueryOver(() => patient)
                .WithSubquery
                .WhereSome(() => patient.LastDateInHospital > (QueryOver.Of(() => sh)
                                                                    .Where(() => sh.Patient.Id == patient.Id)
                                                                    .Select(x => x.StartDate)).As<DateTime>())
                .List();

            return rez;
        }

        //***************     Using queryover with WhereProperty EXISTS     ***************
        public IList<Patient> PatientsSickWith(long sicknessId)
        {
 
            //            --Selectam toti pacienti care au fost sau sunt bolnavi cu ANGINA (toate SicknessHistories cu Diagnoza ANGINA)
            //select* from SicknessHistory
            //select* from sickness
            //--
            //SELECT p.IdPatient,
            //       (SELECT pp.Name + ' ' + pp.Surname
            //        FROM Person AS pp
            //        WHERE pp.IdPerson = p.IdPatient) AS FullName
            //FROM Patient AS p
            //WHERE EXISTS (SELECT *
            //               FROM   SicknessHistory AS sh
            //               WHERE  IdSickness = 1--5 Nu exista nici o history cu OTITA
            //                      AND p.IdPatient = sh.IdPatient)

            Patient patientAlias = null;
            SicknessHistory sicknessHistoryAlias = null;
                
            var rez = _session.QueryOver(() => patientAlias)
                .WithSubquery.WhereExists(QueryOver.Of(() => sicknessHistoryAlias)
                                            .Where(() => sicknessHistoryAlias.NameSickness.Id == sicknessId 
                                                         && patientAlias.Id == sicknessHistoryAlias.Patient.Id)
                                             .Select(x=>x.Id))
                .List();
            return rez;
        }

        //===============     DistinctRootEntity transformer     ==================
        //***************     Using queryover with WhereProperty IN     ***************
        public IList<Patient> PatientsTraitedByDoctor(long doctorId)
        {
            Patient patientAlias = null;
         
            var subquery = QueryOver.Of<SicknessHistory>()
                                    .Where(x => x.Doctor.Id == doctorId)
                                    .Select(s => s.Patient.Id);

            var rez = _session.QueryOver(() => patientAlias)
                .WithSubquery
                .WhereProperty(x => x.Id).In(subquery)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
            return rez;
        }

        //===============     AliasToBean  transformer     ==================
        //***************     SELECTSUBQUERY     ***************
        //***************     Integrate a SQL function     ***************
        public IList<PatientRedusDto> ShortInfoPatient()
        {
            Patient patient = null;
            SicknessHistory sicknessHistory = null;
            PatientRedusDto patientRedusDto = null;

            var countSicknessHistory = QueryOver.Of(() => sicknessHistory)
                                                .Where(() => sicknessHistory.Patient.Id == patient.Id)
                                                .SelectList(list1 => list1
                                                         .SelectCount(x => x.Id));

            var rez = _session.QueryOver(() => patient)
                .SelectList(list => list
                .Select(x => x.Id).WithAlias(() => patientRedusDto.IdPatient)
                .Select(x => x.Name).WithAlias(() => patientRedusDto.Name)
                .Select(x => x.Surname).WithAlias(() => patientRedusDto.Surname)
                .SelectSubQuery(countSicknessHistory).WithAlias(() => patientRedusDto.CountSicknessHistories)
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
                .List<PatientRedusDto>();
            return rez;
        }
    }
}
