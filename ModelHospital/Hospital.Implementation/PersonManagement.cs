using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ChangeDoctor;
using Domain.PersonInHospital;
using Hospital.Interfaces;

namespace Hospital.Implementation
{
    public class PersonManagement : IPersonManagement
    {
       
        public Person InHospital(DateTime dateIn, Person person)
        {
            person.DateInHospital = dateIn;
            return person;
        }

        public Person OutHospital(DateTime dateOut, Person person)
        {
            person.DateInHospital = dateOut;
            return person;
        }
    }
}
