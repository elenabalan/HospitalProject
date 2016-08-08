using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Repository.Implementation;
using NHibernate;

namespace Repository.Implementation
{


    class RepositoryDoctor : BaseRepository, IRepositoryDoctor
    {
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
    }
}
