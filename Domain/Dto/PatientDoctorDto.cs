using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class PatientDoctorDto
    {
        public long DoctorId { get; set; }
        public long PatientId { get; set; }
    }
}
