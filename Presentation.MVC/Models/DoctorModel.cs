using Common.Hospital;
using Domain;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.MVC.Models
{
    public class DoctorModel : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string ProfessionName { get; set; }
        public int CountCertificate { get; set; }
        
        public DoctorModel GetDoctorModel(DoctorRedusDto doctor)
        {
            Id = doctor.IdDoctor;
            Name = doctor.Name;
            Surname = doctor.Surname;
            FullName = doctor.FullName;
            ProfessionName = doctor.ProfessionName;
            CountCertificate = doctor.CountCertificate;
            return this;
        }
    }
}