using Domain;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepositoryDoctor : IRepository
    {
        IList<Doctor> GetDoctorsWithSpecialty(long idMedicalSpecialty);
        void ModifySpecialty(long idDoctor, long newProfession);

        //Doctorii care au certificatele eliberate pina la data indicata
        IList<Doctor> DoctorsWithCertificatesReceivingDateBefore(DateTime date);

        //Toti doctori care au tratat bolnavul indicat
        IList<Doctor> DoctorsTratePatient(long patientId);

        //Doctorii care au certificatele expirate
        IList<Doctor> DoctorsWithAllExpiredCertificates();

        IList<DoctorRedusDto> ShotInfoDoctor();

        Doctor OldestEmployeeDoctor();

        // Doctori si count de SH a lor
        IList<DoctorCountSicknessHistoriesDto> DoctorSicknessHistoryCount();

        //Doctorii care au una din specialitatile specificate
        IList<Doctor> DoctorsHavingCertificatesForOneOf2Specialty(long specialty1, long specialty2);

        //Doctori cu experienta (au mai mult de 3 SicknessHistories)
        IList<DoctorsWithExperienceDto> DoctorsWithExperience();
    }
}
