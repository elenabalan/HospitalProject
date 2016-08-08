using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Hospital;

namespace Domain
{
    public class SicknessSymptom : Entity
    {
        //    public virtual Sickness SicknessName { get; set; }

        public virtual string SymptomName { get; set; }
        public virtual string Description { get; set; }
        public virtual SymptomSeverity Severity { get; set; }

        public SicknessSymptom(string symptomName,string description, SymptomSeverity severity)
        {
            if(String.IsNullOrEmpty(symptomName)) throw new ArgumentNullException();
            SymptomName=symptomName;
            if (String.IsNullOrEmpty(description)) throw new ArgumentNullException();
            Description = description;
            Severity = severity;

        }
       
        [Obsolete]
        protected SicknessSymptom()
        {
        }
    }
}
