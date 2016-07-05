using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class DateInComparer : IComparer<Patient>

    {
        public int Compare(Patient x, Patient y)
        {
            DateTime d1 = x.DateIn;
            DateTime d2 = y.DateIn;
            return d1.CompareTo(d2);
        }
    }
}
