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

        private TipDoctor _TipDoctor { get;}
        public List<Patient> ListPatients = new List<Patient>();
        
        #endregion

        #region CONSTRUCTORS

        public Doctor(string name, string surname, Gender gender, string adress, string phone,
                      TipDoctor tipDoc, DateTime birthDay = new DateTime(), 
                      DateTime dateIn = new DateTime()) //DateTime dateIn = BirthDayHospital)
        {
            DateTime tempBirthDay = birthDay == new DateTime() ? DateTime.Now.AddYears(-35) : birthDay; 
     //       Doc = CountDoctors++;
            Name = name;
            Surname = surname;
            Gender = gender;
            BirthDate = tempBirthDay;
            AdressHome = adress;
            PhoneNumber = phone;
            _TipDoctor = tipDoc;
            DateIn = dateIn;
        }

        #endregion
        #region METHODS
        public override string ToString() => $"\n{Name } {Surname } {_TipDoctor } birthDay is in {BirthDate:d} \nComes in hospital in {DateIn:d} Leave the hospital in {DateOut:d}";

        public void ViewPatients()
        {
            Console.WriteLine($"\nLista patientilor tratati de doctorul {Name} {Surname}");
            foreach (Patient p in ListPatients)
                Console.WriteLine(p.ToString());
        }
        #endregion

        public bool ITreatPerson(Person p)
        {
            return ListPatients.Contains(p);
        }
        public void PatientsExtern(DateTime date)
        {
            Console.WriteLine($"Pacientii tratati de doctorul {Name} {Surname} externati pina la data de {date.ToShortDateString()}");
            var pExtern = (from pat in ListPatients
                           where DateOut < date
                           select Name + Surname).ToList();
            foreach (var p in pExtern)
            {
                Console.WriteLine(p.ToString());
            }
        }

        public void DocQuit(NewDoctorQuitArgs e)
        {
            Console.WriteLine($"Doctorul {e.QuitDoctor} se elibereaza. Doctorul {e.NewDoctor} ii preia bolnavii");
            this.DateOut = e.DateOut;

            //SicknessHistory.ChangeDoctors(this, e.NewDoctor, doct);
        }
        
    }
}
