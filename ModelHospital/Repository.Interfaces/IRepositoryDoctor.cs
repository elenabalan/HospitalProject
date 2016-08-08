using Domain;
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
    }
}
