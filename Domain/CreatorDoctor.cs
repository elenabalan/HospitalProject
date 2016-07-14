using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CreatorDoctor : CreatorPersonInHospital
    {


        public override Person Create(string name, string surname, Gender gender, string adressHome,
            string phoneNumber, DateTime birthDay,  DateTime? dateIn = null, TipDoctor tipDoc = TipDoctor.GENERAL)
        {
            if (tipDoc == TipDoctor.GENERAL)
                throw new Exception($"{nameof(tipDoc)} is not specified");
            return new Doctor(name: name, surname: surname, gender: gender, adress: adressHome, phone: phoneNumber,
                    tipDoc: tipDoc, birthDay: birthDay, dateIn: dateIn);

        }

        //public CreatorDoctor(string name, string surname, Gender gender, string adressHome, string phoneNumber, DateTime birthDay) : base(name, surname, gender, adressHome, phoneNumber, birthDay)
        public CreatorDoctor() : base()
        {
        }
    }
}
