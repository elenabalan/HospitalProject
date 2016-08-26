    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class MedicalSpecialtyForCreationDoctorDto
    {
        public virtual long Id { get; set; }
        public virtual string MedicalSpecialtyName { get; set; }
    }
}
