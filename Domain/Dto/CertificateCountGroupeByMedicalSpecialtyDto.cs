using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CertificateCountGroupeByMedicalSpecialtyDto
    {
        public long SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public int CountCert { get; set; }
    }
}
