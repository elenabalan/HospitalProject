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
    public class PatientsController : Controller
    {
        private readonly IRepositoryPatient _repositoryPatient;

        
        public PatientsController(IRepositoryPatient repositoryPatient)
        {
            _repositoryPatient = repositoryPatient;
        }

        // GET: Patient
        public ActionResult Index()
        {
            var dtos = _repositoryPatient.ShortInfoPatient();
            return View(dtos);
        }

        [HttpGet]
       public ActionResult AboutPatient()
        {

            var patients = _repositoryPatient.AllPatients();
            //var patientModel = new PatientDetailViewModel { Id = patient.Id, IDNP=patient.IDNP, Name = patient.Name, Surname = patient.Surname, BirthDate = patient.BirthDate, Gender = patient.Gender, AdressHome = patient.AdressHome, PhoneNumber = patient.PhoneNumber };
            IList<PatientDetailViewModel> patientModel = new List<PatientDetailViewModel>();
            foreach (var patient in patients)
            {
                var patientViewTemp = new PatientDetailViewModel();
                patientModel.Add(patientViewTemp.GetPatientViewModel(patient));
            }
            
            return View(patientModel);
        }

        [HttpGet]
        public ActionResult CreatePatient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePatient(CreatePatientModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var patient = model.MapToPatient();
            _repositoryPatient.Save(patient);

            return RedirectToAction("AboutPatient");
        }


        [HttpGet]
        public ActionResult EditPatient(long id)
        {
            var patient = _repositoryPatient.Get<Patient>(id);
            var patientModel = new EditPatientModel { Id = patient.Id, Name = patient.Name, Surname = patient.Surname, Birthday = patient.BirthDate, Gender=patient.Gender, AdressHome = patient.AdressHome, PhoneNumber = patient.PhoneNumber };
            return View(patientModel);
        }

        [HttpPost]
        public ActionResult EditPatient(EditPatientModel patient)
        {
            var existingPatient = _repositoryPatient.Get<Patient>(patient.Id);

            existingPatient.Name = patient.Name;
            existingPatient.Surname = patient.Surname;
            existingPatient.AdressHome = patient.AdressHome;
            existingPatient.Gender = patient.Gender;
            existingPatient.PhoneNumber = patient.PhoneNumber;
            existingPatient.BirthDate = patient.Birthday;

            _repositoryPatient.Save(existingPatient);


            return RedirectToAction("AboutPatient");
        }

        public ActionResult DetailsPatient(long id)
        {
            Patient patient = _repositoryPatient.Get<Patient>(id);
            PatientDetailViewModel patientModel = new PatientDetailViewModel();
            patientModel.GetPatientViewModel(patient);
            return View(patientModel);
        }

        //Get Patient's details
        public ActionResult DeletePatient(long id)
        {
            Patient patient = _repositoryPatient.Get<Patient>(id);
            if (patient == null)
                return HttpNotFound();
            PatientDetailViewModel patientModel = new PatientDetailViewModel();
            patientModel.GetPatientViewModel(patient);
            return View(patientModel);
        }

        public ActionResult DeletePatientConfirm(long id)
        {
            var patient = _repositoryPatient.Get<Patient>(id);
            _repositoryPatient.Delete(patient);

            return RedirectToAction("AboutPatient");
        }
    }
}