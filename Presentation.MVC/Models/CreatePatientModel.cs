using Common.Hospital;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.MVC.Models
{
    public class CreatePatientModel : BaseModel
    {
        public long IDNP { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string AdressHome { get; set; }
        public string PhoneNumber { get; set; }

        public Patient MapToPatient()
        {
            return new Patient(IDNP,Name, Surname, Gender, BirthDate, AdressHome, PhoneNumber, null, Common.Hospital.StatePatient.IsSick);
        }
    }
}