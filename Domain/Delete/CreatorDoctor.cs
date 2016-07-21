using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PersonInHospital;

namespace Domain
{
    public class CreatorDoctor : CreatorPersonInHospital
    {

        public override Person Create(string name, string surname, Gender gender, string adressHome,
            string phoneNumber, DateTime birthDay, TipDoctor? tipDoc,  DateTime? dateIn = null)
        {
            if ((DateTime.Today - birthDay).Days/365 < 18)
            {
                //Console.WriteLine("Persoana e minora si nu poate fi incadrata in cimpul muncii");
                throw new Exception($"Persoana {name} {surname} e minora si nu poate fi incadrata in cimpul muncii");
            }
            
            return new Doctor(name: name, surname: surname, gender: gender, adress: adressHome, phone: phoneNumber,
                    tipDoc: tipDoc, birthDay: birthDay, dateIn: dateIn);

        }


        //public CreatorDoctor(string name, string surname, Gender gender, string adressHome, string phoneNumber, DateTime birthDay) : base(name, surname, gender, adressHome, phoneNumber, birthDay)
        public CreatorDoctor() : base()
        {
        }
    }
}
