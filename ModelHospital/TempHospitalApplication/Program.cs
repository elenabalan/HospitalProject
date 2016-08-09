using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Common.Hospital;
using Domain;

using Repository.Interfaces;
using Hospital.Infrastructure;
//using MedicalSpecialty = Domain.MedicalSpecialty;
using HibernatingRhinos.Profiler.Appender.NHibernate;

namespace TempHospitalApplication
{
    delegate SicknessHistory StartSicknessHandler<TPatient, NewStartSicknessEventArgs>(TPatient p, NewStartSicknessEventArgs a);
    class Program
    {

        private static IRepository _repository;

        static Program()
        {
            ServiceLocator.RegisterAll();
            NHibernateProfiler.Initialize();
            _repository = ServiceLocator.Resolver<IRepository>();
        }

        public Program()
        {
        }

        public static void ConsoleIO()
        {
            #region MockData
            //Console.WriteLine("Doctori");
            //List<Doctor> doct = new List<Doctor>
            //{
            //    new Doctor(1236547896325,name: "Bordea", surname: "Boris", gender: Gender.M, phone: "123456987",
            //        adress: "Stefan cel mare 2",
            //        profession: "CHIRURG", birthDay: new DateTime(1983, 05, 09)),
            //    new Doctor(surname: "Ana", name: "Albu", gender: Gender.F, adress: "Aleco Russo 3",
            //        phone: "77755566", profession: "THERAPIST",
            //        birthDay: new DateTime(1965, 5, 9),
            //        dateIn: new DateTime(1980, 08, 09))
            //};
            //Console.WriteLine(doct[0].ToString());
            //Console.WriteLine(doct[1].ToString());

            //Console.WriteLine("\nPatienti");
            //List<Patient> patients = new List<Patient>
            //{
            //    new Patient("Pupkin", "Vasile", Gender.M, new DateTime(1987, 4, 8), "Negruzzi, 67", "654789159",
            //        new DateTime(2016, 6, 1, 0, 0, 0)),
            //    new Patient("Dorofei", "Anatol", Gender.M, new DateTime(1975, 5, 9), "Budeci, 45", null,
            //        new DateTime(2016, 6, 15, 0, 0, 0)),
            //    new Patient("Pupkin", "Ludmila", Gender.F, new DateTime(1980, 10, 18), "Decebal, 3 ap.5", "69634856",
            //        new DateTime(2016, 5, 20, 0, 0, 0))
            //};


            //patients.ElementAt(0).AssignDoctor(doct[0]);
            //patients.ElementAt(1).AssignDoctor(doct[0]);
            //patients.ElementAt(2).AssignDoctor(doct[1]);
            //patients.ElementAt(2).AssignDoctor(doct[0]);

            //patients.ElementAt(1).Externarea(new DateTime(2019, 6, 20));

            //doct[1].ViewPatients();
            //Console.WriteLine("************************");
            //doct[1].PatientsExtern(new DateTime(2016, 6, 25));
            //Console.WriteLine("************************");
            //doct[0].ListPatients.Sort(Patient.SortByFullName);
            //doct[0].ViewPatients();

            //doct[0].ListPatients.Sort(Patient.SortByDateIn);
            //doct[0].ViewPatients();

            //Console.WriteLine();
            //Console.WriteLine();
            //DateTime d = new DateTime(2016, 4, 22);
            //Console.WriteLine($"{d.Day}.{d.Month,2}.{d.Year}");

            //Console.WriteLine($"Doctors that treated of {patients.ElementAt(2)}:");

            //foreach (Doctor doc in doct)
            //    if (doc.ITreatPerson(patients.ElementAt(2))) Console.WriteLine(doc.ToString());


            ////-------------------------------
            //List<string> realSymptomsList = new List<string>
            //{
            //    "DurCap",
            //    "Voma",
            //    "Tensiune",
            //    "DurBurta",
            //    "Anemie",
            //    "Convulsii",
            //    "Fiebra39+",
            //    "Ferba38+",
            //    "Fiebra37+"
            //};
            //List<SicknessHistory> sicknessHistories = new List<SicknessHistory>
            //{
            //    new SicknessHistory("Angina", SicknessStateEnum.ACTIV, patients.ElementAt(0), doct.ElementAt(0)),
            //    new SicknessHistory("Hipertensiune", SicknessStateEnum.CHRONIC, patients.ElementAt(1), doct.ElementAt(0),
            //        new DateTime(2014, 04, 23)),
            //    new SicknessHistory("Otita", SicknessStateEnum.ACTIV, patients.ElementAt(2), doct.ElementAt(1),
            //        new DateTime(2016, 03, 30)),
            //    new SicknessHistory("Gastrita", SicknessStateEnum.CHRONIC, patients.ElementAt(2), doct.ElementAt(1),
            //        new DateTime(2015, 11, 07)),
            //    new SicknessHistory("InfGastr", SicknessStateEnum.OFF, patients.ElementAt(1), doct.ElementAt(1),
            //        new DateTime(2016, 01, 20))
            //};
            //Console.WriteLine("************************");


            //Utils.Utils.PrintAllSicknessHistories(patients, sicknessHistories);

            //Utils.Utils.PrintAllSicknessHistoriesForAPacient(patients.ElementAt(1), sicknessHistories);
            //Console.WriteLine("**************  EVENTS  **************");

            ////Apare event DoctorQuit
            //// Utils.Utils.PreluareaPacientilor(doct[0], new DateTime(2016, 07, 12), doct[1], sicknessHistories);

            //Console.WriteLine("Doctori");
            //Console.WriteLine(doct[0].ToString());
            //Console.WriteLine(doct[1].ToString());
            //Console.WriteLine("************************");
            ////-------------------------------

            //Utils.Utils.SicknessHistoryToTxtFile(sicknessHistories[0]);
            //Utils.Utils.SicknessHistoryToTxtFile(sicknessHistories[1]);
            //Utils.Utils.SicknessHistoryToFileEncodigUtf8(sicknessHistories[0]);
            //Utils.Utils.SicknessHistoryToFileEncodigAscii(sicknessHistories[0]);

            //Utils.Utils.AllSicknessHistoryToTxtFile(sicknessHistories);

            //Console.WriteLine("************************");
            //Console.WriteLine("************************");
            //Console.WriteLine("Create a List of Patients using Factory CreatorPersonInHospital");
            //// Create a List of Patients using Factory CreatorPersonInHospital
            ////  CreatorDoctor creatorDoctor = new CreatorDoctor();
            //CreatorPatient creatorPacient = new CreatorPatient();


            //List<Patient> patientsFabric = new List<Patient>();

            //patientsFabric.Add((Patient)creatorPacient.Create(name: "Pupkin", surname: "Vasile", gender: Gender.M,
            //    birthDay: new DateTime(1987, 4, 8), adressHome: "Negruzzi, 67", phoneNumber: "654789159", dateIn: new DateTime(2016, 6, 1)));
            //patientsFabric.Add((Patient)creatorPacient.Create(name: "Dorofei", surname: "Anatol", gender: Gender.M,
            //    birthDay: new DateTime(1975, 5, 9), adressHome: "Budeci, 45", phoneNumber: null, dateIn: new DateTime(2016, 6, 15)));
            //patientsFabric.Add((Patient)creatorPacient.Create(name: "Balan", surname: "Ludmila", gender: Gender.F,
            //    birthDay: new DateTime(1980, 10, 18), adressHome: "Decebal, 3 ap.5", phoneNumber: "69634856", dateIn: new DateTime(2016, 5, 20)));

            ////  var convertedPatients = patientsFabric.ConvertAll(new Converter<Person,Patient>());

            //patientsFabric[0].AssignDoctor(doct[0]);
            //patientsFabric[1].AssignDoctor(doct[0]);
            //patientsFabric[2].AssignDoctor(doct[1]);

            //Console.WriteLine(patientsFabric[0].ToString());
            //Console.WriteLine(patientsFabric[1].ToString());
            //Console.WriteLine(patientsFabric[2].ToString());

            //Console.WriteLine("************************");
            //Console.WriteLine("************************");
            //Console.WriteLine("Create a List of Doctors using SingletonFactory");
            ////       PersonFactory factoryPersonInHospital;

            //// var qw = new SingletonFactory();
            //List<Doctor> doctorsFabric = new List<Doctor>();

            //try
            //{

            //    doctorsFabric.Add(PersonFactory.Instance.CreateDoctor(
            //        name: "Bordea", surname: "Boris", gender: Gender.M, phoneNumber: "123456987",
            //        adressHome: "Stefan cel mare 2", tipDoc: MedicalSpecialty.CHIRURG, birthDay: new DateTime(1983, 05, 09)));

            //}
            //catch (ArgumentException exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}
            //try
            //{
            //    doctorsFabric.Add(PersonFactory.Instance.CreateDoctor(
            //    surname: "Ana", name: "Albu", gender: Gender.F, adressHome: "Aleco Russo 3",
            //    phoneNumber: "77755566", tipDoc: MedicalSpecialty.THERAPIST,
            //    birthDay: new DateTime(1965, 5, 9), dateIn: new DateTime(1980, 08, 09)));
            //}
            //catch (ArgumentException exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}
            //try
            //{
            //    doctorsFabric.Add(PersonFactory.Instance.CreateDoctor(
            //    surname: "Mariuta", name: "Bejenari", gender: Gender.F, adressHome: "Aleco Russo 3",
            //    phoneNumber: "25478934", tipDoc: MedicalSpecialty.THERAPIST,
            //    birthDay: new DateTime(2000, 5, 9)));

            //}
            //catch (ArgumentException exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}

            //Console.WriteLine("Doctori");
            //foreach (Doctor item in doctorsFabric)
            //    Console.WriteLine(item.ToString());

            //Console.WriteLine("************************");
            ////-------------------------------


            //Console.ReadKey();
            #endregion
        }


        static void Main(string[] args)
        {
            var repozitory = ServiceLocator.Resolver<IRepository>();
            //GenerateData(repozitory);
            //GenerateDataSicknessHistory(repozitory);

            ////var doctor = repozDoctor.Get<Doctor>(15015);
            //repozDoctor.ModifySpecialty(15015, 12012);

            //foreach (Doctor doc in repozDoctor.GetDoctorsWithSpecialty(12012))
            //    Console.WriteLine($"{doc}");


            Console.ReadKey();
        }

        private static void GenerateData(IRepository repoz)
        {
           // var repoz = ServiceLocator.Resolver<IRepository>();

            var medicalSpec1 = new MedicalSpecialty("TERAPEUT", DificultyLevel.EASY, 365);
            var medicalSpec2 = new MedicalSpecialty("REANIMATOLOG", DificultyLevel.HARD, 365 * 3);
            var medicalSpec3 = new MedicalSpecialty("PEDIATOR", DificultyLevel.HARD, 365 * 3);
            var medicalSpec4 = new MedicalSpecialty("CHIRURG", DificultyLevel.HARD, 365 * 3);
            var medicalSpec5 = new MedicalSpecialty("PATOLOGOANATOM", DificultyLevel.EASY, 365);
            var medicalSpec6 = new MedicalSpecialty("PSIHIATOR", DificultyLevel.HARD, 365 * 4);
            var medicalSpec7 = new MedicalSpecialty("LOR", DificultyLevel.MEDIUM, 365 * 2);
            var medicalSpec8 = new MedicalSpecialty("OFTALMOLOG", DificultyLevel.MEDIUM, 365 * 2);
            var medicalSpec9 = new MedicalSpecialty("CARDIOLOG", DificultyLevel.HARD, 365 * 3);



            var doc1 = new Doctor(idnp: 1236547896325, name: "Terapeut", surname: "Boris", gender: Gender.M, phone: "123456987",
                    adress: "Stefan cel mare 2", profession: medicalSpec1, birthDay: new DateTime(1983, 05, 09), dateOfStart: new DateTime(2005, 09, 01), professionalGrade: 2);
            var doc2 = new Doctor(idnp: 9875146328763, name: "Reanimatolog", surname: "Ana", gender: Gender.F, phone: "77755566",
                    adress: "Aleco Russo 3", profession: medicalSpec2, birthDay: new DateTime(1965, 5, 9), dateOfStart: new DateTime(1980, 08, 09), professionalGrade: 2);
            var doc3 = new Doctor(idnp: 6548321987259, name: "Oftalmolog", surname: "Viorel", gender: Gender.M, phone: "65478219",
                    adress: "str. Bucurasti, 8 ap. 9", profession: medicalSpec8, birthDay: new DateTime(1970, 7, 29), dateOfStart: new DateTime(1997, 10, 15), professionalGrade: 3);
            var doc4 = new Doctor(idnp: 2569874123658, name: "Chirurg", surname: "Alexei", gender: Gender.M, phone: "11111111",
                    adress: "str. Ismail, 22 ap. 6", profession: medicalSpec8, birthDay: new DateTime(1963, 9, 21), dateOfStart: new DateTime(1988, 09, 5), professionalGrade: 3);
           

            var certificat1 = new Certificate("cert 2", medicalSpec8, doc3, new DateTime(2009, 09, 01), 3 * 365);
            var certificat2 = new Certificate("cert 1", medicalSpec8, doc3, new DateTime(2012, 09, 01), 3 * 365);
            var certificat3 = new Certificate("cert 1.1", medicalSpec3, doc3, new DateTime(2000, 01, 15), 3 * 365);
            var certificat4 = new Certificate("cert 4", medicalSpec5, doc2, new DateTime(1995, 08, 15), 3 * 365);
            var certificat5 = new Certificate("cert 5", medicalSpec5, doc2, new DateTime(1998, 08, 15), 3 * 365);
            var certificat6 = new Certificate("cert 6", medicalSpec2, doc2, new DateTime(2000, 12, 01), 3 * 365);
            var certificat7 = new Certificate("cert 7", medicalSpec2, doc2, new DateTime(2003, 11, 15), 5 * 365);
            var certificat8 = new Certificate("cert 8", medicalSpec2, doc2, new DateTime(2010, 10, 11), 5 * 365);
            var certificat9 = new Certificate("cert 9", medicalSpec2, doc2, new DateTime(2015, 10, 5), 5 * 365);
            var certificat10 = new Certificate("cert 10", medicalSpec1, doc1, new DateTime(2013, 08, 25), 3 * 365);
            var certificat11 = new Certificate("cert 11", medicalSpec1, doc1, new DateTime(2016, 05, 10), 3 * 365);
            var certificat12 = new Certificate("cert 12", medicalSpec4, doc4, new DateTime(2016, 4, 20), 3 * 365);
       

            var patient1 = new Patient(5698741236589, "Ursu", "Iana", Gender.F, new DateTime(2007, 05, 18), "Decebal,8/2, ap.42", "12256357", null, StatePatient.IsSick);
            var patient2 = new Patient(1236547896325, "Bordea", "Boris", Gender.M, new DateTime(1983, 05, 09), "Stefan cel mare 2", "123456987", new DateTime(2015, 12, 24), StatePatient.IsHealthy);
            var patient3 = new Patient(2546387412596, "Coliban", "Alla", Gender.F, new DateTime(1954, 09, 28), "bd. Mircea cel Batrin, 45,ap. 9", "23657954", new DateTime(2010, 03, 11));
            

            var symptom1 = new SicknessSymptom("Fiebra ushor ridicata", "Fiebra intre 37 si 38", SymptomSeverity.LIGHT);
            var symptom2 = new SicknessSymptom("Fiebra mediu ridicata", "Fiebra intre 38 si 40", SymptomSeverity.GRAVE);
            var symptom3 = new SicknessSymptom("Fiebra extrem ridicat", "Fiebra mai mare de 40", SymptomSeverity.LETAL);
            var symptom4 = new SicknessSymptom("Dureri de cap", "Dureri la mishcarea capului", SymptomSeverity.LIGHT);
            var symptom5 = new SicknessSymptom("Dureri in ghit", "Dureri in ghit", SymptomSeverity.LIGHT);
            var symptom6 = new SicknessSymptom("Lipsa de pofta", "Lipsa de pofta", SymptomSeverity.LIGHT);
            var symptom7 = new SicknessSymptom("Voma", "Voma", SymptomSeverity.GRAVE);
            var symptom8 = new SicknessSymptom("Dureri abdominale", "Dureri abdominale", SymptomSeverity.GRAVE);
            var symptom9 = new SicknessSymptom("Hemorogie in creer", "Hemorogie in creer", SymptomSeverity.LETAL);
            var symptom10 = new SicknessSymptom("Dureri abdominale la apasare", "Dureri abdominale specifice la apasare", SymptomSeverity.GRAVE);
            var symptom11 = new SicknessSymptom("Imaginea neclara", "Imaginea neclara", SymptomSeverity.LIGHT);
            var symptom12 = new SicknessSymptom("Spazmuri abdominale alimentare", "Dureri pronuntate dupa administrarea anumitor produse alimentare", SymptomSeverity.GRAVE);
            var symptom13 = new SicknessSymptom("Eruptii cutanate", "Eruptii cutanate", SymptomSeverity.LIGHT);
            var symptom14 = new SicknessSymptom("Tensiune", "Tensiune ridicata", SymptomSeverity.GRAVE);
          

            var sickness1 = new Sickness("ANGINA", new List<SicknessSymptom> { symptom2, symptom8, symptom6, symptom5 });
            var sickness2 = new Sickness("APENDICITA", new List<SicknessSymptom> { symptom7, symptom10, symptom1 });
            var sickness3 = new Sickness("CATARACTA", new List<SicknessSymptom> { symptom4, symptom11 });
            var sickness4 = new Sickness("GASTRITA", new List<SicknessSymptom> { symptom6, symptom8, symptom12 });
            var sickness5 = new Sickness("OTITA", new List<SicknessSymptom> { symptom2, symptom4 });
            var sickness6 = new Sickness("VARICEL", new List<SicknessSymptom> { symptom1, symptom13 });
            var sickness7 = new Sickness("TENSIUNE", new List<SicknessSymptom> { symptom4, symptom14 });
         

           

            repoz.Save(medicalSpec1);
            repoz.Save(medicalSpec2);
            repoz.Save(medicalSpec3);
            repoz.Save(medicalSpec4);
            repoz.Save(medicalSpec5);
            repoz.Save(medicalSpec6);
            repoz.Save(medicalSpec7);
            repoz.Save(medicalSpec8);
            repoz.Save(medicalSpec9);

            repoz.Save(doc1);
            repoz.Save(doc2);
            repoz.Save(doc3);
            repoz.Save(doc4);

            repoz.Save(certificat1);
            repoz.Save(certificat2);
            repoz.Save(certificat3);
            repoz.Save(certificat4);
            repoz.Save(certificat5);
            repoz.Save(certificat6);
            repoz.Save(certificat7);
            repoz.Save(certificat8);
            repoz.Save(certificat9);
            repoz.Save(certificat10);
            repoz.Save(certificat11);
            repoz.Save(certificat12);

            repoz.Save(patient1);
            repoz.Save(patient2);
            repoz.Save(patient3);

            repoz.Save(symptom1);
            repoz.Save(symptom2);
            repoz.Save(symptom3);
            repoz.Save(symptom4);
            repoz.Save(symptom5);
            repoz.Save(symptom6);
            repoz.Save(symptom7);
            repoz.Save(symptom8);
            repoz.Save(symptom9);
            repoz.Save(symptom10);
            repoz.Save(symptom11);
            repoz.Save(symptom12);
            repoz.Save(symptom13);
            repoz.Save(symptom14);

            repoz.Save(sickness1);
            repoz.Save(sickness2);
            repoz.Save(sickness3);
            repoz.Save(sickness4);
            repoz.Save(sickness5);
            repoz.Save(sickness6);
            repoz.Save(sickness7);


        }
        private static void GenerateDataSicknessHistory(IRepository repoz)
        {
            var patient1 = repoz.Get<Patient>(106);
            var patient2 = repoz.Get<Patient>(107);
            var patient3 = repoz.Get<Patient>(108);

            var doc1 = repoz.Get<Doctor>(102);
            var doc2 = repoz.Get<Doctor>(103);
            var doc3 = repoz.Get<Doctor>(104);
            var doc4 = repoz.Get<Doctor>(105);

            var sickness1 = repoz.Get<Sickness>(255);
            var sickness2 = repoz.Get<Sickness>(256);
            var sickness3 = repoz.Get<Sickness>(257);
            var sickness4 = repoz.Get<Sickness>(258);
            var sickness5 = repoz.Get<Sickness>(259);
            var sickness6 = repoz.Get<Sickness>(261);
            var sickness7 = repoz.Get<Sickness>(260);

            var sicknessHistory1 = new SicknessHistory(sickness1, patient1, doc1, new DateTime(2012, 11, 23), new DateTime(2012, 12, 07));
            var sicknessHistory2 = new SicknessHistory(sickness6, patient1, doc1, new DateTime(2016, 08, 08));
            patient1.AssignDoctor(doc1);
            patient1.SickHistories.Add(sicknessHistory1);
            patient1.SickHistories.Add(sicknessHistory2);
            var sicknessHistory3 = new SicknessHistory(sickness2, patient2, doc4, new DateTime(2004, 12, 3), new DateTime(2004, 12, 27));
            var sicknessHistory4 = new SicknessHistory(sickness5, patient2, doc1, new DateTime(2012, 3, 11), new DateTime(2012, 3, 27));
            var sicknessHistory5 = new SicknessHistory(sickness3, patient2, doc3, new DateTime(2015, 01, 20), new DateTime(2015, 1, 23));
            patient2.AssignDoctor(doc3);
            patient2.SickHistories.Add(sicknessHistory3);
            patient2.SickHistories.Add(sicknessHistory4);
            patient2.SickHistories.Add(sicknessHistory5);
            var sicknessHistory6 = new SicknessHistory(sickness3, patient3, doc3, new DateTime(2012, 1, 23), new DateTime(2012, 1, 25));
            var sicknessHistory7 = new SicknessHistory(sickness1, patient3, doc1, new DateTime(2012, 11, 23), new DateTime(2012, 12, 07));
            var sicknessHistory8 = new SicknessHistory(sickness7, patient3, doc1, new DateTime(2002, 05, 8));
            patient3.AssignDoctor(doc1);
            var sicknessHistory9 = new SicknessHistory(sickness2, patient3, doc4, new DateTime(1975, 02, 9), new DateTime(1975, 2, 15));
            patient3.SickHistories.Add(sicknessHistory6);
            patient3.SickHistories.Add(sicknessHistory7);
            patient3.SickHistories.Add(sicknessHistory8);
            patient3.SickHistories.Add(sicknessHistory9);

            doc1.ListSikcnessHistories.Add(sicknessHistory1);
            doc1.ListSikcnessHistories.Add(sicknessHistory2);
            doc1.ListSikcnessHistories.Add(sicknessHistory4);
            doc1.ListSikcnessHistories.Add(sicknessHistory7);
            doc1.ListSikcnessHistories.Add(sicknessHistory8);

            doc3.ListSikcnessHistories.Add(sicknessHistory5);
            doc3.ListSikcnessHistories.Add(sicknessHistory6);

            doc4.ListSikcnessHistories.Add(sicknessHistory3);
            doc4.ListSikcnessHistories.Add(sicknessHistory9);

            repoz.Save(doc1);
            repoz.Save(doc2);
            repoz.Save(doc3);
            repoz.Save(doc4);

            repoz.Save(patient1);
            repoz.Save(patient2);
            repoz.Save(patient3);

        }
    }
}
