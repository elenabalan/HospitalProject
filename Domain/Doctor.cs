using System;
using System.Collections.Generic;
using System.Linq;
using Common.Hospital;
//using Common.Hospital;
using Domain.ChangeDoctor;

namespace Domain
{
    public delegate void DoctorQuit(NewDoctorQuitArgs dateQuit);

    public class Doctor : Person
    {
        // public static int CountDoctors;
        #region FILDS AND PROPERTIES

        public virtual MedicalSpecialty Profession { get; set; }
        public virtual DateTime DateOfStart { get; set; }
        public virtual int ProfessionalGrade { get; set; }

        public  virtual IList<SicknessHistory> ListSikcnessHistories {get;set;}

        #endregion

        #region CONSTRUCTORS

        public Doctor(long idnp, string name, string surname, Gender gender, string adress, string phone,
                      MedicalSpecialty profession, DateTime birthDay, DateTime? dateOfStart = null, int professionalGrade = 1) : base(idnp, name, surname, gender, adress, phone, birthDay)
        {
           
            Profession = profession;
            if (dateOfStart == null)
                DateOfStart = DateTime.Now;
            else DateOfStart = (DateTime)dateOfStart;
            ProfessionalGrade = professionalGrade;
        }

        [Obsolete]
        protected Doctor()
        {
        }

        #endregion
        #region METHODS
        public override string ToString() => $"\n{Name } {Surname } {Profession.Id } - {Profession.MedicalSpecialtyName} {nameof(BirthDate)} is in {BirthDate:d}. Age is {Age} \nBecame practice in {DateOfStart:d}";

        public virtual void ViewPatients()
        {
            Console.WriteLine($"\nLista patientilor tratati de doctorul {Name} {Surname}");
            //foreach (Patient p in ListPatients)
            //    Console.WriteLine(p.ToString());
        }
        #endregion

        //public bool ITreatPerson(Person p) => ListPatients.Contains(p);
        public virtual void PatientsExtern(DateTime date)
        {
            Console.WriteLine($"Pacientii tratati de doctorul {Name} {Surname} externati pina la data de {date.ToShortDateString()}");
            //var pExtern = (from pat in ListPatients
            //               where DateOutHospital < date
            //               select Name + Surname).ToList();
            //foreach (var p in pExtern)
            //{
            //    Console.WriteLine(p);
            //}
        }
        public virtual void DocQuit(NewDoctorQuitArgs e)
        {
            Console.WriteLine($"Doctorul {e.QuitDoctor} se elibereaza. Doctorul {e.NewDoctor} ii preia bolnavii");
            //DateOutHospital = e.DateOut;
            //SicknessHistory.ChangeDoctors(this, e.NewDoctor, doct);
        }

    }
}
