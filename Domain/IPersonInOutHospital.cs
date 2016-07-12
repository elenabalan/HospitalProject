using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    interface IPersonInOutHospital
    {
        DateTime DateInHospital { get; set; }
        DateTime? DateOutHospital { get; set; }
        void InHospital(DateTime dateIn);
        void OutHospital(DateTime dateOut);
    }
}
