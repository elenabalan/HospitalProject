using Domain;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepositoryPatient : IRepository
    {
        IList<Patient> PatientsTraitedByDoctor(long doctorId);

        IList<Patient> PatientsSickWith(long sicknessId);

        //Pacienti fideli (care se trateaza nu prima oara  sau s-au tratat de >2 ori)
        IList<Patient> PatientsRepeated();

        IList<PatientRedusDto> ShotInfoPatient();
    }
}
