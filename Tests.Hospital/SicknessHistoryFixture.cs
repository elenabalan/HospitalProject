using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Domain;
using Domain.ChangeDoctor;

namespace Tests.Hospital
{
    [TestFixture]
    class SicknessHistoryFixture
    {
        private Patient _patient;
        private Doctor _doctor;
        private Doctor _doctorActiv, _doctorQuit, _doctorNew;
        [SetUp]
        public void Setup()
        {
            _patient = new Patient("Pupkin", "Vasile", Gender.M, new DateTime(1987, 4, 8), "Negruzzi, 67", "654789159", new DateTime(2016, 6, 1, 0, 0, 0));
            _doctor = new Doctor(name: "Bordea", surname: "Boris", gender: Gender.M, phone: "123456987", adress: "Stefan cel mare 2",
                tipDoc: TipDoctor.CHIRURG, birthDay: new DateTime(1983, 05, 09));
           _doctorActiv = new Doctor(name: "Kilicik", surname: "Tatiana", gender: Gender.F, phone: "128956987",
                    adress: "Stefan cel mare 2",
                    tipDoc: TipDoctor.CHIRURG, birthDay: new DateTime(1983, 05, 09));

            _doctorQuit = new Doctor(name: "Bordea", surname: "Boris", gender: Gender.M, phone: "123456987",
                    adress: "Stefan cel mare 2",
                    tipDoc: TipDoctor.CHIRURG, birthDay: new DateTime(1983, 05, 09));
            _doctorNew = new Doctor(surname: "Ana", name: "Albu", gender: Gender.F, adress: "Aleco Russo 3",
                phone: "77755566", tipDoc: TipDoctor.THERAPIST,
                birthDay: new DateTime(1965, 5, 9),
                dateIn: new DateTime(1980, 08, 09));
        }

        [Test]
        public void WhenCreateSicknessHistoryWithoutDateStartApplyDateNow()
        {
            //Arrange
            
            //Act
            SicknessHistory sicknessHistory = new SicknessHistory("Angina", SicknessStateEnum.ACTIV, _patient, _doctor, null);
            //Assert
            Assert.AreEqual(sicknessHistory.SicknessDateStart.Date, DateTime.Now.Date);
        }

        [Test]
        public void WhenCloseSicknessHistoryFinishDateIsSetAndStateIsOff()
        {
            //Arrange

            SicknessHistory sicknessHistory = new SicknessHistory("Angina", SicknessStateEnum.ACTIV, _patient, _doctor, null);
            //Act

            sicknessHistory.CloseSicknessHistory(DateTime.Now);

            //Assert
            Assert.IsNotNull(sicknessHistory.SicknessDateFinish);
            Assert.AreEqual(sicknessHistory.SicknessState, SicknessStateEnum.OFF);

        }

        [Test]
        public void IfDoctorQuitHeIsChangedWithNew()
        {
            //arrange

            SicknessHistory sicknessHistory = new SicknessHistory("Angina", SicknessStateEnum.ACTIV, _patient, _doctorQuit);
            var newDoctorQuitArgs = new NewDoctorQuitArgs(_doctorQuit, new DateTime(2016, 07, 21), _doctorNew);
            //Act
            sicknessHistory.ChangeDoctors(newDoctorQuitArgs);
            var rez = $"{sicknessHistory.Doctor.Name} {sicknessHistory .Doctor .Surname}";
            //Assert
            Assert.AreEqual(rez,"Albu Ana");
        }

        [Test]

        public void IfDoctorisNotThatQuitHeIsNotChangedWithNew()
        {
            //arrange
            SicknessHistory sicknessHistory = new SicknessHistory("Angina", SicknessStateEnum.ACTIV, _patient, _doctorActiv);
            var newDoctorQuitArgs = new NewDoctorQuitArgs(_doctorQuit, new DateTime(2016, 07, 21), _doctorNew);
            //Act
            sicknessHistory.ChangeDoctors(newDoctorQuitArgs);
            var rez = $"{sicknessHistory.Doctor.Name} {sicknessHistory.Doctor.Surname}";
            //Assert
            Assert.AreNotEqual(rez, "Albu Ana");
        }
    }
}
