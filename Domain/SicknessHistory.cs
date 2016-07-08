using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum SicknessStatusEnum { ACTIV,CHRONIC,OFF}
    public class SicknessHistory
    {
       // public int IdPatient { get; set; }
      //  public int IdDoctor { get; set; }
        public Patient IdPatient;
        public Doctor IdDoctor;
        public string NameSickness { get; set; }
     //   List<string> symptoms;
        SicknessStatusEnum sicknessStatus;
        List<string> Prescription;

        public SicknessHistory (Patient idp,Doctor idd,string ns,SicknessStatusEnum sStatus)
        {
            IdPatient = idp;
            IdDoctor = idd;
            NameSickness = ns;
            sicknessStatus = sStatus;
            Prescription = new List<string>();
     //       symptoms = new List<string> (); //includes only short name of symptoms 
        }

        public void AddPrescription(string newPrescr)
        {
            Prescription.Add(newPrescr);
        }
        //public void AddSymptom (string sym)
        //{
        //    symptoms.Add(sym);
        //}

        public override string ToString()
        {
            var rez = $"Id doctor = {IdDoctor.ToString()}  {NameSickness}  ";
            //foreach (var sym in symptoms)
            //{
            //    rez += $"\n {sym} ";
            //}
            rez += $"\n At the moment the sickness is {sicknessStatus}\n";
            return rez;
        }
    }
}
