using System;
using System.Collections.Generic;
using Common.Hospital;

namespace Domain
{
    delegate void FinishSicknessHandler(DateTime d);

    public class Patient : Person, IComparable<Patient>
    {
        #region FILDS AND PROPERTIES
        //    private static int countPacient = 0;
        public virtual DateTime LastDateInHospital { get; set; }
        public virtual StatePatient State { get; set; }
        public virtual Doctor DoctorResponsible { set; get; }
        public virtual IList<SicknessHistory> SickHistories { get; set; }

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
        public Patient(long idnp, string name, string surname, Gender gender, DateTime birthD, string adress, string phone,
                       DateTime? lastDateInHospital, StatePatient state = StatePatient.IsChronically) : base(idnp, name, surname, gender, adress, phone, birthD)
        {
            if (lastDateInHospital == null) LastDateInHospital = DateTime.Now;
            LastDateInHospital = (DateTime)lastDateInHospital;
            State = state;
        }

        [Obsolete]
        protected Patient()
        {
        }

        #endregion
        #region METHODS
        //public void InHospital(DateTime dateIn) => DateInHospital = dateIn;

        //public void OutHospital(DateTime dateOut)
        //{

        //    if (dateOut.Equals(DateTime.Today) || (dateOut == null))
        //        DateOutHospital = dateOut;
        //    else
        //        return;

        //}
        public override string ToString() => $"{Name} {Surname}  Data internarii {LastDateInHospital.Day}.{LastDateInHospital.Month,2 }.{LastDateInHospital.Year} Doctor {DoctorResponsible.Name} {DoctorResponsible.Surname}";

        public virtual void AssignDoctor(Doctor doc)
        {
            DoctorResponsible = doc;
            if (doc == null)
                throw (new ArgumentNullException());
           // (doc.ListPatients).Add(this);
        }

        public virtual int CompareTo(Patient other)
        {
            string fullNameThis = this.Name + " " + this.Surname;
            string fullNameObj = other.Name + " " + other.Surname;
            return fullNameThis.CompareTo(fullNameObj);
        }
        //        public void Externarea(DateTime dExtrn) => DateOutHospital = dExtrn;

        //  public void AddSickHistory(SicknessHistory sh) => SickHistories.Add(sh);

        //public SicknessHistory CreateNewSicknessHistory(Person p, NewSicknessEventArgs arg)
        //{

        //    var temp = new SicknessHistory(arg.NameSickness, arg.State, p, arg.StartDate);
        //    EventFinishSickHandler.EventFinishSick += temp.CloseSicknessHistory;
        //    return temp;
        //}
        #endregion
    }
}