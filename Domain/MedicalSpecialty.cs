using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Hospital;

namespace Domain
{
    public  class MedicalSpecialty : Entity
    {
        public virtual string MedicalSpecialtyName { get; set; }
        public virtual DificultyLevel Level { get; set; }
        public virtual int MinimumExperiencePeriod { get; set; }

//        public List<string> MedicalSpecialtyList = new List<string>() { "PEDIATOR" , "CHIRURG ", "REANIMATOLOG", "LOR", "CARDIOLOG", "GASTROLOG" };

        public MedicalSpecialty(string medicalSpecialtyName, DificultyLevel level, int minimumExperiencePeriod)
        {
            if(String.IsNullOrEmpty(medicalSpecialtyName)) throw new ArgumentNullException();
            MedicalSpecialtyName = medicalSpecialtyName;
            Level = level;
            if(minimumExperiencePeriod<0) throw new Exception();
            MinimumExperiencePeriod = minimumExperiencePeriod;

        }
        [Obsolete]
        protected MedicalSpecialty()
        {
        }
    }
}
