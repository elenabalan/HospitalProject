using Common.Hospital;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.MVC.Models
{
    public class CreateDoctorModel : BaseModel
    {
        [Required]
        public long IDNP { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string AdressHome { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public MedicalSpecialty Profession { get; set; }
        public DateTime? DateOfStart { get; set; }
        public int ProfessionalGrade { get; set; }

        public IList<SelectListItem> Professions { get; set; }

        public CreateDoctorModel(IList<SelectListItem> professions)
        {
            Professions = professions;
        }
        public CreateDoctorModel(Doctor doctor)
        {
            Id = doctor.Id;
            IDNP = doctor.IDNP;
            Name = doctor.Name;
            Surname = doctor.Surname;
            Gender = doctor.Gender;
            BirthDate = doctor.BirthDate;
            AdressHome = doctor.AdressHome;
            PhoneNumber = doctor.PhoneNumber;
            DateOfStart = doctor.DateOfStart;
            ProfessionalGrade = doctor.ProfessionalGrade;

        }

        public CreateDoctorModel(Doctor doctor, IList<SelectListItem> professions)
        {
            Id = doctor.Id;
            IDNP = doctor.IDNP;
            Name = doctor.Name;
            Surname = doctor.Surname;
            Gender = doctor.Gender;
            BirthDate = doctor.BirthDate;
            AdressHome = doctor.AdressHome;
            PhoneNumber = doctor.PhoneNumber;
            DateOfStart = doctor.DateOfStart;
            ProfessionalGrade = doctor.ProfessionalGrade;
            Professions = professions;
        }

        [Obsolete]
        public CreateDoctorModel()
        { }
        public Doctor MapToDoctor()
        {
            return new Doctor(IDNP, Name, Surname, Gender, AdressHome, PhoneNumber, Profession, BirthDate, DateOfStart, ProfessionalGrade);

        }
    }
}