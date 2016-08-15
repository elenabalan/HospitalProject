using Domain;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepositoryCertificate :IRepository
    {
        IList<Certificate> AllCertificatesForDoctor(long doctorId);

        //Certificate valabile cel putin 3 ani si eliberati dupa data de DATA
        IList<Certificate> CertificatesValidFor3YearRecevedAfter(DateTime date);

        //Count of certificate expirate a unui doctor

        int CountExpiredCertificateForDoctor(long doctor);

        //Certificate grupate dupa Specialty
        IList<Certificate> CertificateGroupBySpecialty();

        //Certificate conform Denumirii a Specialitatii
        IList<Certificate> CertificateConformSpacialtyName(string specialtyName);

        //Count Certificate grupate dupa MedicalSpecialty
        IList<CertificateCountGroupeByMedicalSpecialtyDto> CertificateCountByMedicalSpecialty();


    }

}
