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
            var doct1 = new Doctor("Bordea", "Boris", Gender.M, new DateTime(1958, 1, 1, 0, 0, 0),"Stefan cel mare 2","123456987", TipDoctor.CHIRURG);
            Console.WriteLine(doct1.ToString());

            Console.WriteLine("\nPatienti");
            var pacient1 = new Patient("Pupkin", "Vasea", Gender.M, new DateTime(1987, 4, 8, 0, 0, 0), "Negruzzi, 67", "654789159", new DateTime(2016, 6, 1, 0, 0, 0));
            var pacient2 = new Patient ("Dorofei", "Anatol", Gender.M, new DateTime(1975, 5, 9, 0, 0, 0), "Budeci, 45", null, new DateTime(2016, 6, 15, 0, 0, 0));
            var pacient3 = new Patient("Pupkin", "Ludmila", Gender.F, new DateTime(1980, 10, 18, 0, 0, 0), "Decebal, 3 ap.5", "69634856", new DateTime(2016, 5, 20, 0, 0, 0));
            

            pacient1.AssignDoctor(doct1);
            pacient2.AssignDoctor(doct1);
            pacient3.AssignDoctor(doct1);

            //Console.WriteLine(pacient1.ToString());
            //Console.WriteLine(pacient2.ToString());
            //Console.WriteLine(pacient3.ToString());

            doct1.ViewPatients();
            doct1.listPatients.Sort(Patient.SortByFullName);
            doct1.ViewPatients();

            doct1.listPatients.Sort(Patient.SortByDateIn);
            doct1.ViewPatients();

            DateTime d = new DateTime(2016, 4, 22);
            Console.WriteLine($"{d.Day}.{d.Month,2}.{d.Year}");
            
            Console.ReadKey();
        }
    }
}
