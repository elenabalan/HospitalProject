using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Tests.Hospital
{

    [TestFixture]
    class PatientFixture
    {
        private Patient _patient;

        [SetUp]
        public void Setup()
        {
            _patient= new Patient("Pupkin", "Vasile", Gender.M, new DateTime(1987, 4, 8), "Negruzzi, 67", "654789159", new DateTime(2016, 6, 1, 0, 0, 0));
        }

        [Test]
        public void WhenPatientIsCreatedWithoutDateInHospitalItIsSetNow()
        {
            //Arrange

            //Act
            _patient = new Patient("Pupkin", "Vasile", Gender.M, new DateTime(1987, 4, 8), "Negruzzi, 67", "654789159",
                null);
            //Assert
            Assert.AreEqual(_patient .DateInHospital .Date,DateTime.Now.Date);
            Assert.IsNull(_patient.DoctorResponsible);
        }
        [Test]
        public void WhenPatientIsCreatedWithDateInHospitalSpecified()
        {
            //Arrange

            //Act
            _patient = new Patient("Pupkin", "Vasile", Gender.M, new DateTime(1987, 4, 8), "Negruzzi, 67", "654789159",
                new DateTime(2016, 6, 1));
            //Assert
            Assert.AreEqual(_patient.DateInHospital.Date, new DateTime(2016,6,1));
            Assert.IsNull(_patient.DoctorResponsible, null);
        }

        [Test]
        public void AssighDoctorToAPatient()
        {
            //Arrange
            Doctor doctor = new Doctor(name: "Bordea", surname: "Boris", gender: Gender.M, phone: "123456987", adress: "Stefan cel mare 2",
                tipDoc: TipDoctor.CHIRURG, birthDay: new DateTime(1983, 05, 09));
            //Act
            _patient.AssignDoctor(doctor);
            //Assert
            Assert.AreEqual(_patient .DoctorResponsible,doctor);
            
        }
        [Test]
        public void AssighNullDoctorToAPatient()
        {
            //Arrange
            Doctor doc = null;
            //Act
            //Assert
            Assert.Throws<ArgumentNullException >(() => _patient.AssignDoctor(doc));

        }
        [Test]
        public void AssignDoctorAddPatientIntoDoctorSList()
        {
            //Arrange
            Doctor doctor = new Doctor(name: "Bordea", surname: "Boris", gender: Gender.M, phone: "123456987", adress: "Stefan cel mare 2",
                tipDoc: TipDoctor.CHIRURG, birthDay: new DateTime(1983, 05, 09));
            //Act
            _patient.AssignDoctor(doctor);
            //Assert
            Assert.Contains(_patient ,doctor .ListPatients );
        }

        [Test]
        public void WhenExternareaDateOutHospitalIsNotNull()
        {
            //Arrange

            //Act
            _patient.Externarea(new DateTime( 2016,07,22));
            //Assert
            Assert.AreNotEqual(_patient.DateOutHospital, null);
        }
        [Test]
        public void WhenExternareaDateOutHospitalIsIndicatedDate()
        {
            //Arrange

            //Act
            _patient.Externarea(new DateTime(2016, 07, 22));
            //Assert
            Assert.AreEqual(((DateTime )_patient.DateOutHospital).Date, new DateTime(2016, 07, 22).Date );
        }

        [Test]
        public void T()
        {
            //Arrange
            
            //Act

            //Assert
        }
    }
}
