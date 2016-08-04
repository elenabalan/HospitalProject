using System;
using System.Collections.Generic;
using System.Linq;
using Domain.ChangeDoctor;

namespace Domain
{
    public delegate void DoctorQuit(NewDoctorQuitArgs dateQuit);

    public class Doctor : Person
    {
       // public static int CountDoctors;
        #region FILDS AND PROPERTIES

        public virtual TipDoctor? TipDoctor { get;}
        public List<Patient> ListPatients = new List<Patient>();
        #endregion

        #region CONSTRUCTORS

        public Doctor(string name, string surname, Gender gender, string adress, string phone,
                      TipDoctor? tipDoc, DateTime birthDay, DateTime? dateIn = null):base( name,  surname,  gender,  adress,  phone, birthDay, dateIn)
        {
            if (tipDoc == null)
                throw new Exception($"{nameof(tipDoc)} is not specified");
            TipDoctor = tipDoc;
        }

        #endregion
        #region METHODS
        public override string ToString() => $"\n{Name } {Surname } {TipDoctor } {nameof (BirthDate)} is in {BirthDate:d}. Age is {Age} \nComes in hospital in {DateInHospital:d} Leave the hospital in {DateOutHospital:d}";

        public void ViewPatients()
        {
            Console.WriteLine($"\nLista patientilor tratati de doctorul {Name} {Surname}");
            foreach (Patient p in ListPatients)
                Console.WriteLine(p.ToString());
        }
        #endregion

        public bool ITreatPerson(Person p) => ListPatients.Contains(p);
        public void PatientsExtern(DateTime date)
        {
            Console.WriteLine($"Pacientii tratati de doctorul {Name} {Surname} externati pina la data de {date.ToShortDateString()}");
            var pExtern = (from pat in ListPatients
                           where DateOutHospital < date
                           select Name + Surname).ToList();
            foreach (var p in pExtern)
            {
                Console.WriteLine(p);
            }
        }
        public void DocQuit(NewDoctorQuitArgs e)
        {
            Console.WriteLine($"Doctorul {e.QuitDoctor} se elibereaza. Doctorul {e.NewDoctor} ii preia bolnavii");
            DateOutHospital = e.DateOut;
            //SicknessHistory.ChangeDoctors(this, e.NewDoctor, doct);
        }
        
    }
}
