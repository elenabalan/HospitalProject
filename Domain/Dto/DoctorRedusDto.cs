using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class DoctorRedusDto
    {
        public long IdDoctor { get; set; }
        public  string Name { get; set; }
        public  string Surname { get; set; }
        public string FullName { get; set; }
        public  string ProfessionName { get; set; }
        public int CountCertificate { get; set; }
    }
}
