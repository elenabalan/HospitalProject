using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Symptom
    {
        string NameShort { get; set; }
        string Description { get; set; }

        public Symptom(string sShort, string desc)
        {
            NameShort = sShort;
            Description = desc;
        }
        
    }
}
