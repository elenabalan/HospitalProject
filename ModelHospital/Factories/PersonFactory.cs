using System;
using Domain;
using Domain.PersonInHospital;

namespace Factories
{
    public sealed class PersonFactory
    {
        private static readonly PersonFactory instance = new PersonFactory();

        static PersonFactory()
        {
            Console.WriteLine("Static constructor worked");
        }

        private PersonFactory()
        {
            Console.WriteLine("Private constructor ");
        }

        public static PersonFactory Instance => instance;
        public Doctor CreateDoctor(string name, string surname, Gender gender, string adressHome,
            string phoneNumber, DateTime birthDay, TipDoctor tipDoc, DateTime? dateIn = null )
        {
           
            if ((DateTime.Today - birthDay).Days / 365 < 18)
            {
               // Console.WriteLine("Persoana e minora si nu poate fi incadrata in cimpul muncii");
                throw new ArgumentException($"Persoana {name} {surname} e minora si nu poate fi incadrata in cimpul muncii") ;
                
            }
            return new Doctor(name: name, surname: surname, gender: gender, adress: adressHome, phone: phoneNumber,
                    tipDoc: tipDoc, birthDay: birthDay, dateIn: dateIn);

        }
        public  Patient CreatePatient(string name, string surname, Gender gender, string adressHome, string phoneNumber, DateTime birthDay,
             DateTime? dateIn = null)
        {
            return new Patient(name, surname, gender, birthDay, adressHome, phoneNumber, dateIn);
        }
    }
}
