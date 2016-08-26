using Domain;
using Domain.Dto;
using Presentation.MVC.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.MVC.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IRepositoryDoctor _repositoryDoctor;
        public DoctorsController(IRepositoryDoctor repositoryDoctor)
        {
            _repositoryDoctor = repositoryDoctor;
        }

        // GET: Doctors
        public ActionResult AboutDoctors()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ManageDoctors()
        {
            var doctorsDto = _repositoryDoctor.ShortInfoDoctor();

            IList<DoctorModel> doctorModel = new List<DoctorModel>();
            foreach (var doctor in doctorsDto)
            {
                var doctorTemp = new DoctorModel();
                doctorModel.Add(doctorTemp.GetDoctorModel(doctor));
            }
            return View(doctorModel);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}


        // GET: Car/Create
        [HttpGet]
        public PartialViewResult Create()
        {
            var items = new List<SelectListItem>();

            IList<MedicalSpecialtyForCreationDoctorDto> medicalSpecialties = _repositoryDoctor.ListMedicalSpecialtyFoCreationDoctor();

            foreach (var ms in medicalSpecialties)
            {
                items.Add(new SelectListItem { Text = ms.MedicalSpecialtyName, Value = ms.Id.ToString() });
            }

            var createDoctorModel = new CreateDoctorModel(items);
           
            return PartialView(createDoctorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDoctorModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var doctor = model.MapToDoctor();
            _repositoryDoctor.Save(doctor);
            return RedirectToAction("ManageDoctors");

        }

    }
}