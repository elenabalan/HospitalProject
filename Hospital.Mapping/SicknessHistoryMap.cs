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
            References(x => x.NameSickness); 
            References(x => x.Doctor);
            References(x => x.Patient);
            Map(x => x.StartDate);
            Map(x => x.FinishDate);
        }
    }
}
