using System;
using Domain;

namespace Hospital.Interfaces
{           
    public interface IPersonManagement
    {
        Person InHospital(DateTime dateIn, Person person);
        Person OutHospital(DateTime dateOut,Person person);
    }
}
