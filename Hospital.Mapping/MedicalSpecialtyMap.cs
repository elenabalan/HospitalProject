using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Hospital.Mapping
{
    class MedicalSpecialtyMap : EntityMap<MedicalSpecialty>
    {
        public MedicalSpecialtyMap()
        {
          
            Map(x => x.MedicalSpecialtyName).Not.Nullable().Unique();
            Map(x => x.Level).Not.Nullable();
            Map(x => x.MinimumExperiencePeriod).Not.Nullable();
        }
    }
}
