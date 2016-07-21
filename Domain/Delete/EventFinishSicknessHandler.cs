using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PersonInHospital;

namespace Domain
{
    class EventFinishSicknessHandler
    {
        public static event FinishSicknessHandler EvenFinishSickness;

        public virtual void OnFinishSickness(DateTime d)
        {
            if (EvenFinishSickness != null)
            {
                EvenFinishSickness(d);
            }
        }
    }
}
