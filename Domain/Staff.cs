using System;

namespace Domain
{
    public abstract class Staff : Person
    {
   //     public DateTime DateInHospital{get; set;}

   //     public DateTime? DateOutHospital { get; set; } = null;

//        internal Department department { get; set; }

        protected Staff(string name, string surname, Gender gender, string adress, string phone, DateTime birthDay, DateTime? dateIn = null)
            : base(name, surname, gender, adress, phone, birthDay)
        {
            DateInHospital = dateIn ?? Hospital.BirthDayHospital;
        }

      //  public abstract void DoWork();

        public void InHospital(DateTime dateIn)
        {
            DateInHospital = dateIn;
        }

        public void OutHospital(DateTime dateOut)
        {
            if (dateOut.Equals(DateTime.Today) || (dateOut == null))
                DateOutHospital = dateOut;
        }
    }
}