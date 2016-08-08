using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Hospital.Mapping
{
    class SicknessSymptomMap : EntityMap<SicknessSymptom>
    {
        public SicknessSymptomMap()
        {
            Map(x => x.SymptomName).Not.Nullable();
            Map(x => x.Description).Not.Nullable();
            Map(x => x.Severity).Not.Nullable();
        }
    }
}
