using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Sickness : Entity
    {
        public virtual string SicknessName { get; set; }

        public virtual List<SicknessSymptom> SicknessSymptoms { get; set; }
        //new List<string> { "ANGINA", "APENDICITA", "CATARACTA", "GASTRITA", "OTITA", "VARICEL"};

        
        public Sickness(string sicknessName,List<SicknessSymptom> symptoms)
        {
            if (String.IsNullOrEmpty(sicknessName)) throw new ArgumentNullException();
            SicknessName = sicknessName;
            SicknessSymptoms = symptoms;
        }
        [Obsolete]
        protected Sickness()
        {
        }

        public virtual void AddSymptoms(SicknessSymptom newSymptoms)
        {
            if (newSymptoms == null) throw new ArgumentNullException($"{nameof(newSymptoms)}");
            SicknessSymptoms.Add(newSymptoms);
        }

    }
}
