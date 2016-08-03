using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CreatorPatient : CreatorPersonInHospital
    {
        //public CreatorPatient(string name, string surname, Gender gender, DateTime birthD, string adress, string phone,
        //    DateTime dInHospital) : base(name, surname, gender, adress, phone, birthD)
        //{

        //}

        public CreatorPatient():base()
        {
        }

        public override Person Create(string name, string surname, Gender gender, string adressHome, string phoneNumber, DateTime birthDay,
             TipDoctor? tipDoc = null, DateTime ? dateIn = null)
        {
            return new Patient(name, surname, gender, birthDay, adressHome, phoneNumber, dateIn);
        }
    }
}
