using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    public enum TipDoctor { THERAPIST, SURGEON, RESUSCITATOR, CHIRURG }
    public class Doctor : OperatingStaff
    {
        public static int countDoctors;
        #region FILDS AND PROPERTIES

   //     private int IdDoctor { get; set; }
        TipDoctor tipDoctor { get; set; }
        public List<Patient> listPatients;
        
        #endregion

        #region CONSTRUCTORS
        public Doctor(string name, string surname, Gender g, DateTime birthD, string adress, string phone,TipDoctor tDoc)
        {
     //       IdDoctor = countDoctors++;
            Name = name;
            Surname = surname;
            Gender = g;
            BirthDate = birthD;
            AdressHome = adress;
            PhoneNumber = phone;
            tipDoctor = tDoc;
            DateIn = DateTime.Today;
            DateOut = null;
            listPatients = new List<Patient>();
        }

        //public Doctor(string v1, string v2, string v3, double v4, double v5, string v6, string v7)
        //{

        //}
        #endregion
        #region METHODS
        public override string ToString()
        {
            return $"{Name } {Surname } {tipDoctor }";
        }
        public void ViewPatients()
        {
            Console.WriteLine($"\nLista patientilor tratati de doctorul {Name} {Surname}");
            foreach (Patient p in listPatients)
                Console.WriteLine(p.ToString());
        }
        #endregion

        public bool ITreatPerson(Person p)
        {
            return listPatients.Contains(p);
        }
        public void PatientsExtern(DateTime date)
        {
            Console.WriteLine($"Pacientii tratati de doctorul {Name} {Surname} externati pina la data de {date.ToShortDateString()}");
            var pExtern = (from pat in listPatients
                           where DateOut < date
                           select Name + Surname).ToList();
            foreach (var p in pExtern)
            {
                Console.WriteLine(p.ToString());
            }
        }
    }
}
