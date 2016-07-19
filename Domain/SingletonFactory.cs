using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class SingletonFactory
    {
        private static readonly SingletonFactory instance = new SingletonFactory();

        static SingletonFactory()
        {
            Console.WriteLine("Static constructor worked");
        }

        private SingletonFactory()
        {
            Console.WriteLine("Private constructor ");
        }

        public static SingletonFactory Instance => instance;
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
