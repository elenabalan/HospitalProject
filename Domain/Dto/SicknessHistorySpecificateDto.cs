using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class SicknessHistorySpecificateDto
    {
        public long Id { get; set; }
        public  long SicknessId { get; set; }
     
        public  long PatientId { get; set; }
        public  long DoctorId { get; protected set; }
        public  string State { get; set; }
        
    }
}
