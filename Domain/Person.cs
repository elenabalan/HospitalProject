using System;

namespace Domain
{

    public abstract class Person : Entity
    {

        public virtual long IDNP { get; set; }
        public virtual string Name { get; } = "Default";

        public virtual string Surname { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual string AdressHome { get; set; }
        public virtual string PhoneNumber { get; set; }

        //it is calculable properties with Expression Body Members
        public virtual int Age => (int)(DateTime.Now - BirthDate).Days / 365;

        protected Person(long idnp, string name, string surname, Gender gender, string adressHome,
            string phoneNumber, DateTime birthDay)
        {
            if (idnp.ToString().Length != 13)
            {
                throw new ArgumentException("Idnp inadmisibil");
            }
            IDNP = idnp;
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException($"{nameof(name)} is null");
            if (String.IsNullOrEmpty(surname)) throw new ArgumentNullException($"{nameof(surname)} is null");

            Name = name;
            Surname = surname;
            Gender = gender;
            BirthDate = birthDay;
            AdressHome = adressHome;
            PhoneNumber = phoneNumber;

        }

    }
}