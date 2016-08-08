using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Hospital.Mapping
{
    class CertificateMap : EntityMap<Certificate>
    {
        public CertificateMap()
        {
            References(x => x.Doctor).Not.Nullable();
            Map(x => x.CertificateNumber).Not.Nullable();
            Map(x => x.DataOfReceiving).Not.Nullable();
            References(x => x.Specialty).Not.Nullable();
            Map(x => x.ValidFor).Not.Nullable();
        }
    }
}
