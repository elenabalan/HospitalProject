using System.Collections.Generic;
using Domain.PersonInHospital;

namespace Domain.PersonComparers
{
    class NameComparer  : IComparer<Patient >
    {
        public int Compare(Patient x, Patient y)
        {
            string fullNameX = x.Name + " " + x.Surname;
            string fullNameY = y.Name + " " + y.Surname;
            return string.Compare(fullNameX, fullNameY);
        }

        //int IComparer.Compare(object x, object y)
        //{
        //    Person person1 = (Person)x;
        //    Person person2 = (Person)y;
        //    string fullNameX = person1.Name + " " + person1.Surname;
        //    string fullNameY = person2.Name + " " + person2.Surname;
        //    return string.Compare(fullNameX, fullNameY);

        //}
    }
}
