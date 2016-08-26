using Common.Hospital;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.MVC.Models
{
    public class PatientDetailViewModel:BaseModel
    {
        public long IDNP { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string AdressHome { get; set; }
        public string PhoneNumber { get; set; }

        public PatientDetailViewModel GetPatientViewModel(Patient patient)
        {
            Id = patient.Id;
            IDNP = patient.IDNP;
            Name = patient.Name;
            Surname = patient.Surname;
            BirthDate = patient.BirthDate;
            Gender = patient.Gender; 
            AdressHome = patient.AdressHome; 
            PhoneNumber = patient.PhoneNumber;

            return this;
        }
    }
}