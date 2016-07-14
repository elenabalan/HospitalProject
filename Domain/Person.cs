using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;


namespace Domain
{
    
    public abstract class Person
    {
        public string Name { get; } = "Default";

        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string AdressHome { get; set; }
        public string PhoneNumber { get; set; }

        //it is calculable properties with Expression Body Members
        public int Age => (int)(DateTime.Now - BirthDate).Days / 365;
        
        protected Person(string name, string surname, Gender gender,  string adressHome,
            string phoneNumber, DateTime birthDay)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException($"{nameof(name)} is null");
            Name = name;
        
                
            if (String.IsNullOrEmpty(surname)) throw new ArgumentNullException($"{nameof(surname)} is null");
            Surname = surname;
           
               

            Gender = gender;
            BirthDate = birthDay;
            AdressHome = adressHome;
            PhoneNumber = phoneNumber;

        }

    }
}