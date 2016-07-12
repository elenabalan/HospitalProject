using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Hospital;


namespace Domain
{
    public delegate void DoctorQuit(NewDoctorQuitArgs dateQuit);

    public class Doctor : OperatingStaff
    {
        public static int CountDoctors;
        #region FILDS AND PROPERTIES

        private TipDoctor TipDoctor { get;}
        public List<Patient> ListPatients = new List<Patient>();
        #endregion

        #region CONSTRUCTORS

        public Doctor(string name, string surname, Gender gender, string adress, string phone,
                      TipDoctor tipDoc, DateTime? birthDay = null, DateTime? dateIn = null)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
            BirthDate = birthDay ?? DateTime.Now.AddYears(-35);
            AdressHome = adress;
            PhoneNumber = phone;
            TipDoctor = tipDoc;
            DateInHospital = dateIn ?? Hospital.BirthDayHospital;
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
