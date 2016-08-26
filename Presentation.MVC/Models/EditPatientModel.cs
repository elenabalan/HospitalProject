using Common.Hospital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.MVC.Models
{
    public class EditPatientModel:BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string AdressHome { get; set; }
        public string PhoneNumber { get; set; }
    }
}