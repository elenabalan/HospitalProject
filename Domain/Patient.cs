using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    delegate void FinishSicknessHandler(DateTime d);
    public class Patient : Person, IPersonInOutHospital, IComparable<Patient>
    {
        
        private static int countPacient = 0;
        #region FILDS AND PROPERTIES

     //   public int IdPacient { get; set; }
        Doctor DoctorResponsible;
        public DateTime DateIn { get; set; }

        public DateTime? DateOut { get; set;}

        List<SicknessHistory> SickHistories;
        #region Static Properties for Sorting
        public static IComparer<Patient> SortByFullName
        {
            get { return (IComparer<Patient>)new NameComparer(); }
        }
        public static IComparer<Patient> SortByDateIn
        {
            get { return new DateInComparer(); }
        }
        #endregion
        #endregion
        #region CONSTRUCTORS
        public Patient(string name, string surname, Gender g, DateTime birthD, string adress, string phone, DateTime dIn)
        {
  //          IdPacient = countPacient++;
            Name = name;
            Surname = surname;
            Gender = g;
            BirthDate = birthD;
            AdressHome = adress;
            PhoneNumber = phone;
            //  DoctorResponsible = null;
            //DateIn = DateTime.Today;
            DateIn = dIn;
            DateOut = null;
            SickHistories = new List<SicknessHistory>();
        }
        #endregion
        #region METHODS
        public void InHospital(DateTime dateIn)
        {
            DateIn = dateIn;
        }

        public void OutHospital(DateTime dateOut)
        {

            if (dateOut.Equals(DateTime.Today) || (dateOut == null))
                DateOut = dateOut;
            else
                return;

        }
        public override string ToString()
        {
            //return $"{Name} {Surname}  Data internarii {DateIn .ToShortDateString ()} Doctor {DoctorResponsible.Name} {DoctorResponsible .Surname}";
            return $"{Name} {Surname}  Data internarii {DateIn.Day}.{DateIn.Month,2 }.{DateIn.Year} Doctor {DoctorResponsible.Name} {DoctorResponsible.Surname}";
        }

        public void AssignDoctor(Doctor doc)
        {
            DoctorResponsible = doc;
            Patient p = this;
            if (p == null)
                throw (new ArgumentNullException());
            (doc.ListPatients).Add(p);
        }

        public int CompareTo(Patient other)
        {
            //Patient pTmp = (Patient)other;

            string fullNameThis = this.Name + " " + this.Surname;
            string fullNameObj = other.Name + " " + other.Surname;
            return fullNameThis.CompareTo(fullNameObj);
        }
        public void Externarea(DateTime dExtrn)
        {
            DateOut = dExtrn;
        }
        public void AddSickHistory(SicknessHistory sh)
        {
            SickHistories.Add(sh);
        }
        //public SicknessHistory CreateNewSicknessHistory(Person p, NewSicknessEventArgs arg)
        //{

        //    var temp = new SicknessHistory(arg.NameSickness, arg.State, p, arg.StartDate);
        //    EventFinishSickHandler.EventFinishSick += temp.CloseSicknessHistory;
        //    return temp;
        //}
        #endregion
    }
}