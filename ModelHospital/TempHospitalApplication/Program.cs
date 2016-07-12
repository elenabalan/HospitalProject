using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Utils;

namespace TempHospitalApplication
{
    delegate SicknessHistory StartSicknessHandler<Patient, NewStartSicknessEventArgs>(Patient p, NewStartSicknessEventArgs a);
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Doctori");
            List<Doctor> doct = new List<Doctor>
            {
                new Doctor(name: "Bordea",surname: "Boris",gender: Gender.M,phone: "123456987", adress: "Stefan cel mare 2",
                            tipDoc: TipDoctor.CHIRURG),
                new Doctor(surname: "Ana",name: "Albu",  gender: Gender.F, adress: "Aleco Russo 3",
                           phone: "77755566", tipDoc: TipDoctor.THERAPIST,
                           birthDay: new DateTime(1965, 5, 9) ,
                           dateIn: new DateTime( 1980,08,09))
            };
            Console.WriteLine(doct[0].ToString());
            Console.WriteLine(doct[1].ToString());

            Console.WriteLine("\nPatienti");
            List<Patient> patients = new List<Patient>
            {
                new Patient("Pupkin", "Vasea", Gender.M, new DateTime(1987, 4, 8), "Negruzzi, 67", "654789159", new DateTime(2016, 6, 1, 0, 0, 0)),
                new Patient ("Dorofei", "Anatol", Gender.M, new DateTime(1975, 5, 9), "Budeci, 45", null, new DateTime(2016, 6, 15, 0, 0, 0)),
                new Patient("Pupkin", "Ludmila", Gender.F, new DateTime(1980, 10, 18), "Decebal, 3 ap.5", "69634856", new DateTime(2016, 5, 20, 0, 0, 0))
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
            List<string> realSymptomsList = new List<string> { "DurCap", "Voma", "Tensiune", "DurBurta", "Anemie", "Convulsii", "Fiebra39+", "Ferba38+", "Fiebra37+" };
            List<SicknessHistory> sicknessHistories = new List<SicknessHistory>
            {
                new SicknessHistory( "Angina", SicknessStateEnum.ACTIV,patients.ElementAt(0), doct .ElementAt(0)),
                new SicknessHistory( "Hipertensiune", SicknessStateEnum.CHRONIC,patients.ElementAt(1), doct .ElementAt(0), new DateTime( 2014,04,23)),
                new SicknessHistory( "Otita", SicknessStateEnum.ACTIV, patients.ElementAt(2), doct .ElementAt(1), new DateTime( 2016,03,30)),
                new SicknessHistory( "Gastrita", SicknessStateEnum.CHRONIC,patients.ElementAt(2), doct .ElementAt(1), new DateTime( 2015,11,07)),
                new SicknessHistory( "InfGastr", SicknessStateEnum.OFF,patients.ElementAt(1), doct .ElementAt(1), new DateTime( 2016,01,20))
            };
            Console.WriteLine("************************");


            Utils.Utils.PrintAllSicknessHistories(patients, sicknessHistories);

            Utils.Utils.PrintAllSicknessHistoriesForAPacient(patients.ElementAt(1), sicknessHistories);
            Console.WriteLine("**************  EVENTS  **************");

            //Apare event DoctorQuit
            Utils.Utils.PreluareaPacientilor(doct[0], new DateTime(2016, 07, 12), doct[1], sicknessHistories);

            Console.WriteLine("Doctori");
            Console.WriteLine(doct[0].ToString());
            Console.WriteLine(doct[1].ToString());
            Console.WriteLine("************************");
            //-------------------------------

            Utils.Utils.SicknessHistoryToTxtFile(sicknessHistories[0]);
            Utils.Utils.SicknessHistoryToTxtFile(sicknessHistories[1]);

            Utils.Utils.AllSicknessHistoryToTxtFile(sicknessHistories);
            Console.ReadKey();
        }
    }
}
