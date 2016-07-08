using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class Sickness
    {
        string NameSickness { get; set; }
        List<Symptom> symptoms;
        string Diagnosis { get; set; }
         
        public Sickness(string nS, List <Symptom > listSympt,string Diagn)
        {
            NameSickness = nS;
            symptoms = listSympt;
            Diagnosis = Diagn;
        }

        public void AddSymptom(Symptom symp)
        {
            symptoms.Add(symp);
        }


    }
}
