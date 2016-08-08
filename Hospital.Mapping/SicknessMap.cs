using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Hospital.Mapping
{
    class SicknessMap : EntityMap<Sickness>
    {
        public SicknessMap()
        {
            Map(x => x.SicknessName).Not.Nullable();
            HasMany(x => x.SicknessSymptoms);
        }
    }
}
