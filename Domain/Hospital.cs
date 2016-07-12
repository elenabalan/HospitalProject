using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    public class Hospital
    {
        public static readonly DateTime BirthDayHospital = new DateTime( 1076,06,20);
        internal string Name { get; set; }
        internal string Adress { get; set; } 
        internal string PhoneNumber { get; set; }
    }
}