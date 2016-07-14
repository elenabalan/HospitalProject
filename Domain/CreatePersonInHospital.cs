using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class CreatorPersonInHospital
    {
        //public string Name { get; }

        //public string Surname { get; set; }
        //public Gender Gender { get; set; }
        //public DateTime BirthDate { get; set; }
        //public string AdressHome { get; set; }
        //public string PhoneNumber { get; set; }

        ////it is calculable properties with Expression Body Members
        //public int Age => (int)(DateTime.Now - BirthDate).Days / 365;

        public CreatorPersonInHospital()
        {
        }

        protected CreatorPersonInHospital(string name, string surname, Gender gender, string adressHome,
            string phoneNumber, DateTime birthDay)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException($"{nameof(name)} is null");
            if (String.IsNullOrEmpty(surname)) throw new ArgumentNullException($"{nameof(surname)} is null");
        }

        public abstract Person Create(string name, string surname, Gender gender, string adressHome,
            string phoneNumber, DateTime birthDay,  DateTime? dateIn = null ,TipDoctor tipDoc = TipDoctor.GENERAL);

    }
}
