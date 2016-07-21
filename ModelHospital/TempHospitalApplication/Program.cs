using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.PersonInHospital;
using Domain.Sicknesses;
using Factories;
using Hospital.Interfaces;

namespace TempHospitalApplication
{
    delegate SicknessHistory StartSicknessHandler<Patient, NewStartSicknessEventArgs>(Patient p, NewStartSicknessEventArgs a);
    class Program
    {
        private static IPersonManagement _personManagement;

        public Program(IPersonManagement personManagement)
        {
            _personManagement = personManagement;
        }

        static void Main(string[] args)
        {
            #region MockData
            Console.WriteLine("Doctori");
            List<Doctor> doct = new List<Doctor>
            {
                new Doctor(name: "Bordea", surname: "Boris", gender: Gender.M, phone: "123456987",
                    adress: "Stefan cel mare 2",
                    tipDoc: TipDoctor.CHIRURG, birthDay: new DateTime(1983, 05, 09)),
                new Doctor(surname: "Ana", name: "Albu", gender: Gender.F, adress: "Aleco Russo 3",
                    phone: "77755566", tipDoc: TipDoctor.THERAPIST,
                    birthDay: new DateTime(1965, 5, 9),
                    dateIn: new DateTime(1980, 08, 09))
            };
            Console.WriteLine(doct[0].ToString());
            Console.WriteLine(doct[1].ToString());

            Console.WriteLine("\nPatienti");
            List<Patient> patients = new List<Patient>
            {
                new Patient("Pupkin", "Vasile", Gender.M, new DateTime(1987, 4, 8), "Negruzzi, 67", "654789159",
                    new DateTime(2016, 6, 1, 0, 0, 0)),
                new Patient("Dorofei", "Anatol", Gender.M, new DateTime(1975, 5, 9), "Budeci, 45", null,
                    new DateTime(2016, 6, 15, 0, 0, 0)),
                new Patient("Pupkin", "Ludmila", Gender.F, new DateTime(1980, 10, 18), "Decebal, 3 ap.5", "69634856",
                    new DateTime(2016, 5, 20, 0, 0, 0))
            };


            patients.ElementAt(0).AssignDoctor(doct[0]);
            patients.ElementAt(1).AssignDoctor(doct[0]);
            patients.ElementAt(2).AssignDoctor(doct[1]);
            patients.ElementAt(2).AssignDoctor(doct[0]);

            patients.ElementAt(1).Externarea(new DateTime(2019, 6, 20));

            doct[1].ViewPatients();
            Console.WriteLine("************************");
            doct[1].PatientsExtern(new DateTime(2016, 6, 25));
            Console.WriteLine("************************");
            doct[0].ListPatients.Sort(Patient.SortByFullName);
            doct[0].ViewPatients();

            doct[0].ListPatients.Sort(Patient.SortByDateIn);
            doct[0].ViewPatients();

            Console.WriteLine();
            Console.WriteLine();
            DateTime d = new DateTime(2016, 4, 22);
            Console.WriteLine($"{d.Day}.{d.Month,2}.{d.Year}");

            Console.WriteLine($"Doctors that treated of {patients.ElementAt(2)}:");

            foreach (Doctor doc in doct)
                if (doc.ITreatPerson(patients.ElementAt(2))) Console.WriteLine(doc.ToString());


            //-------------------------------
            List<string> realSymptomsList = new List<string>
            {
                "DurCap",
                "Voma",
                "Tensiune",
                "DurBurta",
                "Anemie",
                "Convulsii",
                "Fiebra39+",
                "Ferba38+",
                "Fiebra37+"
            };
            List<SicknessHistory> sicknessHistories = new List<SicknessHistory>
            {
                new SicknessHistory("Angina", SicknessStateEnum.ACTIV, patients.ElementAt(0), doct.ElementAt(0)),
                new SicknessHistory("Hipertensiune", SicknessStateEnum.CHRONIC, patients.ElementAt(1), doct.ElementAt(0),
                    new DateTime(2014, 04, 23)),
                new SicknessHistory("Otita", SicknessStateEnum.ACTIV, patients.ElementAt(2), doct.ElementAt(1),
                    new DateTime(2016, 03, 30)),
                new SicknessHistory("Gastrita", SicknessStateEnum.CHRONIC, patients.ElementAt(2), doct.ElementAt(1),
                    new DateTime(2015, 11, 07)),
                new SicknessHistory("InfGastr", SicknessStateEnum.OFF, patients.ElementAt(1), doct.ElementAt(1),
                    new DateTime(2016, 01, 20))
            };
            Console.WriteLine("************************");


            Utils.Utils.PrintAllSicknessHistories(patients, sicknessHistories);

            Utils.Utils.PrintAllSicknessHistoriesForAPacient(patients.ElementAt(1), sicknessHistories);
            Console.WriteLine("**************  EVENTS  **************");

            //Apare event DoctorQuit
            // Utils.Utils.PreluareaPacientilor(doct[0], new DateTime(2016, 07, 12), doct[1], sicknessHistories);

            Console.WriteLine("Doctori");
            Console.WriteLine(doct[0].ToString());
            Console.WriteLine(doct[1].ToString());
            Console.WriteLine("************************");
            //-------------------------------

            Utils.Utils.SicknessHistoryToTxtFile(sicknessHistories[0]);
            Utils.Utils.SicknessHistoryToTxtFile(sicknessHistories[1]);
            Utils.Utils.SicknessHistoryToFileEncodigUtf8(sicknessHistories[0]);
            Utils.Utils.SicknessHistoryToFileEncodigAscii(sicknessHistories[0]);

            Utils.Utils.AllSicknessHistoryToTxtFile(sicknessHistories);

            Console.WriteLine("************************");
            Console.WriteLine("************************");
            Console.WriteLine("Create a List of Patients using Factory CreatorPersonInHospital");
            // Create a List of Patients using Factory CreatorPersonInHospital
            //  CreatorDoctor creatorDoctor = new CreatorDoctor();
            CreatorPatient creatorPacient = new CreatorPatient();


            List<Patient> patientsFabric = new List<Patient>();

            patientsFabric.Add((Patient)creatorPacient.Create(name: "Pupkin", surname: "Vasile", gender: Gender.M,
                birthDay: new DateTime(1987, 4, 8), adressHome: "Negruzzi, 67", phoneNumber: "654789159", dateIn: new DateTime(2016, 6, 1)));
            patientsFabric.Add((Patient)creatorPacient.Create(name: "Dorofei", surname: "Anatol", gender: Gender.M,
                birthDay: new DateTime(1975, 5, 9), adressHome: "Budeci, 45", phoneNumber: null, dateIn: new DateTime(2016, 6, 15)));
            patientsFabric.Add((Patient)creatorPacient.Create(name: "Balan", surname: "Ludmila", gender: Gender.F,
                birthDay: new DateTime(1980, 10, 18), adressHome: "Decebal, 3 ap.5", phoneNumber: "69634856", dateIn: new DateTime(2016, 5, 20)));

            //  var convertedPatients = patientsFabric.ConvertAll(new Converter<Person,Patient>());

            patientsFabric[0].AssignDoctor(doct[0]);
            patientsFabric[1].AssignDoctor(doct[0]);
            patientsFabric[2].AssignDoctor(doct[1]);

            Console.WriteLine(patientsFabric[0].ToString());
            Console.WriteLine(patientsFabric[1].ToString());
            Console.WriteLine(patientsFabric[2].ToString());

            Console.WriteLine("************************");
            Console.WriteLine("************************");
            Console.WriteLine("Create a List of Doctors using SingletonFactory");
            PersonFactory factoryPersonInHospital;

            // var qw = new SingletonFactory();
            List<Doctor> doctorsFabric = new List<Doctor>();

            try
            {

                doctorsFabric.Add(PersonFactory.Instance.CreateDoctor(
                    name: "Bordea", surname: "Boris", gender: Gender.M, phoneNumber: "123456987",
                    adressHome: "Stefan cel mare 2", tipDoc: TipDoctor.CHIRURG, birthDay: new DateTime(1983, 05, 09)));

            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            try
            {
                doctorsFabric.Add(PersonFactory.Instance.CreateDoctor(
                surname: "Ana", name: "Albu", gender: Gender.F, adressHome: "Aleco Russo 3",
                phoneNumber: "77755566", tipDoc: TipDoctor.THERAPIST,
                birthDay: new DateTime(1965, 5, 9), dateIn: new DateTime(1980, 08, 09)));
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            try
            {
                doctorsFabric.Add(PersonFactory.Instance.CreateDoctor(
                surname: "Mariuta", name: "Bejenari", gender: Gender.F, adressHome: "Aleco Russo 3",
                phoneNumber: "25478934", tipDoc: TipDoctor.THERAPIST,
                birthDay: new DateTime(2000, 5, 9)));

            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine("Doctori");
            foreach (Doctor item in doctorsFabric)
                Console.WriteLine(item.ToString());

            Console.WriteLine("************************");
            //-------------------------------


            Console.ReadKey();
#endregion

        //  _personManagement.InHospital()

           //var updatedPatient = _personManagement.InHospital(DateTime.Now, new Patient("q"));

        }
    }
}
