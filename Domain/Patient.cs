using System;
using System.Collections.Generic;
using Domain.ChangeDoctor;
using Domain.PersonComparers;
using Domain.Sicknesses;

namespace Domain.PersonInHospital
{
    delegate void FinishSicknessHandler(DateTime d);
    public class Patient : Person, IComparable<Patient>
    {
        
        private static int countPacient = 0;
        #region FILDS AND PROPERTIES

     //   public int IdPacient { get; set; }
        Doctor DoctorResponsible=null;
       // public DateTime DateInHospital { get; set; }

//        public DateTime? DateOutHospital { get; set; } = null;

        List<SicknessHistory> SickHistories= new List<SicknessHistory>();
        #region Static Properties for Sorting
        public static IComparer<Patient> SortByFullName { get; } = new NameComparer();
        //{
        //    get { return (IComparer<Patient>)new NameComparer(); }
        //}
        public static IComparer<Patient> SortByDateIn
        {
            get { return new DateInComparer(); }
        }
        #endregion
        #endregion
        #region CONSTRUCTORS
        public Patient(string name, string surname, Gender gender, DateTime birthD, string adress, string phone,
                       DateTime? dInHospital):base ( name,  surname,  gender, adress,  phone, birthD)
        {
            if(dInHospital ==null) dInHospital =DateTime.Now;
            DateInHospital = (DateTime)dInHospital;
           
      
        }
        #endregion
        #region METHODS
        public void InHospital(DateTime dateIn) => DateInHospital = dateIn;
        
        public void OutHospital(DateTime dateOut)
        {

            if (dateOut.Equals(DateTime.Today) || (dateOut == null))
                DateOutHospital = dateOut;
            else
                return;

        }
        public override string ToString()=> $"{Name} {Surname}  Data internarii {DateInHospital.Day}.{DateInHospital.Month,2 }.{DateInHospital.Year} Doctor {DoctorResponsible.Name} {DoctorResponsible.Surname}";
        
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
            string fullNameThis = this.Name + " " + this.Surname;
            string fullNameObj = other.Name + " " + other.Surname;
            return fullNameThis.CompareTo(fullNameObj);
        }
        public void Externarea(DateTime dExtrn) => DateOutHospital = dExtrn;
        public void AddSickHistory(SicknessHistory sh) => SickHistories.Add(sh);
        
        //public SicknessHistory CreateNewSicknessHistory(Person p, NewSicknessEventArgs arg)
        //{

        //    var temp = new SicknessHistory(arg.NameSickness, arg.State, p, arg.StartDate);
        //    EventFinishSickHandler.EventFinishSick += temp.CloseSicknessHistory;
        //    return temp;
        //}
        #endregion
    }
}