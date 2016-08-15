using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Hospital.Mapping
{
    class SicknessHistoryMap : EntityMap<SicknessHistory>
    {
        public SicknessHistoryMap()
        {
            References(x => x.NameSickness).Not.Nullable(); 
            References(x => x.Doctor).Not.Nullable();
            References(x => x.Patient).Not.Nullable();
            Map(x => x.StartDate).Not.Nullable();
            Map(x => x.FinishDate);
        }
    }
}
