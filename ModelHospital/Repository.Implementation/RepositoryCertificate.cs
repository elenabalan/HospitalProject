using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using NHibernate.Criterion;
using Domain.Dto;
using NHibernate.Transform;
//=============     Queries, using AliasToBean transformer     ==================


namespace Repository.Implementation
{
    class RepositoryCertificate : BaseRepository, IRepositoryCertificate
    {
        public IList<Certificate> AllCertificatesForDoctor(long doctorId)
        {
            var rez = _session.QueryOver<Certificate>()
                .Where(cert => cert.Doctor.Id == doctorId)
                .List();

            return rez;
        }

        public IList<Certificate> CertificateConformSpacialtyName(string specialtyName)  //grupate dupa idDoctor
        {
            var subquery = QueryOver.Of<MedicalSpecialty>()
                                    .Where(x => x.MedicalSpecialtyName == specialtyName)
                                    .Select(x => x.Id);
            var rez1 = _session.QueryOver<Certificate>()
                             .WithSubquery
                             .Where(x => x.Specialty.Id == (subquery.As<long>()))
                             .List();
            //------------ OR  ----------------------
            var rez = _session.QueryOver<Certificate>()
                              .WithSubquery
                              .Where(x => x.Specialty.Id == (QueryOver.Of<MedicalSpecialty>()
                                    .Where(t => t.MedicalSpecialtyName == specialtyName)
                                    .Select(t => t.Id)
                                    .As<long>()))
                              .List();
            return rez;
        }

        //===============     AliasToBean  transformer     ==================
        public IList<CertificateCountGroupeByMedicalSpecialtyDto> CertificateCountByMedicalSpecialty()
        {
            Certificate certificate = null;
            MedicalSpecialty medicalSpecialty = null;

            CertificateCountGroupeByMedicalSpecialtyDto certCountByMedicalSpecialty = null;
            var rez = _session.QueryOver(() => certificate)
                .JoinAlias(() => certificate.Specialty, () => medicalSpecialty)
                .SelectList(list => list
                   .SelectGroup(() => certificate.Specialty.Id).WithAlias(() => certCountByMedicalSpecialty.SpecialtyId)
                   .SelectGroup(() => medicalSpecialty.MedicalSpecialtyName).WithAlias(() => certCountByMedicalSpecialty.SpecialtyName)
                   .SelectCount(() => certificate.Id).WithAlias(() => certCountByMedicalSpecialty.CountCert))
                .TransformUsing(Transformers.AliasToBean<CertificateCountGroupeByMedicalSpecialtyDto>())
                .List<CertificateCountGroupeByMedicalSpecialtyDto>();

            return rez;
        }

        public IList<Certificate> CertificateGroupBySpecialty()
        {
            var rez = _session.QueryOver<Certificate>()
                .OrderBy(x => x.Specialty)
                .Asc
                .List();

            return rez;
        }

        public IList<Certificate> CertificatesValidFor3YearRecevedAfter(DateTime date)
        {
            var rez = _session.QueryOver<Certificate>()
                              .Where(new Conjunction().Add(Restrictions.Where<Certificate>(x => x.ValidFor > 3 * 365))
                                                      .Add(Restrictions.Where<Certificate>(x => x.DataOfReceiving > date)))
                              .List();
            //se genereaza acelasi SQL
            rez = _session.QueryOver<Certificate>().Where(x => (x.ValidFor > 3 * 365)).And(x => x.DataOfReceiving > date).List();
            rez = _session.QueryOver<Certificate>().Where(x => x.ValidFor > 3 * 365 && x.DataOfReceiving > date).List();
            return rez;
        }

        public int CountExpiredCertificateForDoctor(long doctor)
        { //?????????   Nu funtioneaza AddDays
            Certificate certificateAlias = null;
            //Count certificatelor unui Doctor
            var rez = _session.QueryOver<Certificate>()
                              .Where(x => x.Doctor.Id == doctor)
                              .RowCount();

            //Toate certificate expirate       ?????????????????????????????????????????????????
            var rez1 = _session.QueryOver(() => certificateAlias).Where(() => certificateAlias.DataOfReceiving.AddDays(certificateAlias.ValidFor) > DateTime.Now).List();

            return 0;
        }
    }
}
