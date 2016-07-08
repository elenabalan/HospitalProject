using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace TempHospitalApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Doctori");
            List<Doctor> doct = new List<Doctor> { new Doctor("Bordea", "Boris", Gender.M, new DateTime(1958, 1, 1),"Stefan cel mare 2","123456987", TipDoctor.CHIRURG),
                                    new Doctor("Albu", "Ana", Gender.F, new DateTime(1965, 5, 9), "Aleco Russo 3", "77755566", TipDoctor.THERAPIST) };
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
            //Console.WriteLine(pacient1.ToString());
            //Console.WriteLine(pacient2.ToString());
            //Console.WriteLine(pacient3.ToString());

            doct[1].ViewPatients();
            Console.WriteLine("************************");
            doct[1].PatientsExtern(new DateTime(2016, 6, 25));
            Console.WriteLine("************************");
            doct[0].listPatients.Sort(Patient.SortByFullName);
            doct[0].ViewPatients();

            doct[0].listPatients.Sort(Patient.SortByDateIn);
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
            SicknessHistory[] sicknessHistories = new SicknessHistory[]
            {
                new SicknessHistory( patients.ElementAt(0), doct .ElementAt(0), "Angina", SicknessStatusEnum.ACTIV),
                new SicknessHistory( patients.ElementAt(1), doct .ElementAt(0), "Hipertensiune", SicknessStatusEnum.CHRONIC),
                new SicknessHistory( patients.ElementAt(2), doct .ElementAt(1), "Otita", SicknessStatusEnum.ACTIV ),
                new SicknessHistory( patients.ElementAt(2), doct .ElementAt(1), "Gastrita", SicknessStatusEnum.CHRONIC),
                new SicknessHistory( patients.ElementAt(1), doct .ElementAt(1), "InfGastr", SicknessStatusEnum.OFF)
            };
            Console.WriteLine("************************");

            Console.WriteLine($"Print all SicknessHistories for patient {patients.ElementAt(1)}");
            foreach (Patient pat in patients)
            {


                var result = sicknessHistories.Where(x => x.IdPatient.Name  == pat.Name).Select(x => x.ToString());

                Console.WriteLine(pat.Name + "probolel:");

                foreach (var rez in result)
                {
                    Console.WriteLine(rez);
                }

            }
            Console.WriteLine("************************");
            //-------------------------------
            Console.ReadKey();
        }
    }
}
