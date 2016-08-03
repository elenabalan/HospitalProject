using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.IO;
using Domain.ChangeDoctor;

namespace Utils
{
    public class Utils
    {
        //Print all SicknessHistories for patients in the hole hospital
        public static void PrintAllSicknessHistories(List<Patient> patList, List<SicknessHistory> sicknessHistoriesList)
        {
            Console.WriteLine($"Print all SicknessHistories for patients in the hole hospital");
            foreach (Patient pat in patList)
            {
                var result = sicknessHistoriesList.Where(x => (x.Patient.Name == pat.Name) && (x.Patient.Surname == pat.Surname)).Select(x => x.ToString());
                Console.WriteLine(pat.Name + "  " + pat.Surname + "   probolel:");

                foreach (var rez in result)
                {
                    Console.WriteLine(rez);
                }

            }
        }

        public static void PrintAllSicknessHistoriesForAPacient(Patient pat, List<SicknessHistory> listSicknessHistories)
        {
            //Console.WriteLine($"Print all SicknessHistories for patient {patients.ElementAt(1)}");
            Console.WriteLine($"\n\nPrint all SicknessHistories for patients {pat}");
            var result1 =
                listSicknessHistories
                        .Where(
                    x => (x.Patient.Name == pat.Name) &&
                        (x.Patient.Surname == pat.Surname))
                        .Select(x => x.ToString());
            // Console.WriteLine($"{pat.Name}  {pat.Surname}. His list of sicknessHistories is:");

            foreach (var rez in result1)
            {
                Console.WriteLine(rez);
            }
        }
        public static void SicknessHistoryToTxtFile(SicknessHistory sickHist)
        {
            FileInfo file = new FileInfo(@"D:\Projects\HospitalProject\ModelHospital\DataSicknessHistories\" + sickHist.Patient.Name + sickHist.Patient.Surname + "_" + sickHist.NameSickness + ".txt");

            using (StreamWriter sw = new StreamWriter(file.OpenWrite()))
            {
                Console.WriteLine($"Inscriem info despre istoria bolii {sickHist.NameSickness } a pacientului {sickHist.Patient.Name} {sickHist.Patient.Surname} in txt file");
                sw.WriteLine($"Pacient {sickHist.Patient.Name} {sickHist.Patient.Surname}");
                sw.WriteLine($"Sickness name is {sickHist.NameSickness}");
                sw.WriteLine($"Starea maladiei {sickHist.SicknessState}");
                sw.WriteLine($"Data internarii {sickHist.SicknessDateStart}");
                sw.WriteLine($"Data externarii { sickHist.SicknessDateFinish}");
                sw.WriteLine($"Doctor {sickHist.Doctor.Name} {sickHist.Doctor.Surname}");
            }

        }

        public static void SicknessHistoryToFileEncodigUtf8(SicknessHistory sickHist)
        {
            FileInfo file = new FileInfo(@"D:\Projects\HospitalProject\ModelHospital\DataSicknessHistories\" + sickHist.Patient.Name + sickHist.Patient.Surname + "_" + sickHist.NameSickness + "_UTF8.txt");
            using (StreamWriter sw = new StreamWriter(file.OpenWrite()))
            {
                Console.WriteLine($"Inscriem info codata in UTF8 despre istoria bolii {sickHist.NameSickness } a pacientului {sickHist.Patient.Name} {sickHist.Patient.Surname} in txt file");
                sw.WriteLine($"Pacient {sickHist.Patient.Name} {sickHist.Patient.Surname}", Encoding.UTF8);
                sw.WriteLine($"Sickness name is {sickHist.NameSickness}", Encoding.UTF8);
                sw.WriteLine($"Starea maladiei {sickHist.SicknessState}", Encoding.UTF8);
                sw.WriteLine($"Data internarii {sickHist.SicknessDateStart}", Encoding.UTF8);
                sw.WriteLine($"Data externarii { sickHist.SicknessDateFinish}", Encoding.UTF8);
                sw.WriteLine($"Doctor {sickHist.Doctor.Name} {sickHist.Doctor.Surname}", Encoding.UTF8);
            }
        }
        public static void SicknessHistoryToFileEncodigAscii(SicknessHistory sickHist)
        {
            FileInfo file = new FileInfo(@"D:\Projects\HospitalProject\ModelHospital\DataSicknessHistories\" + sickHist.Patient.Name + sickHist.Patient.Surname + "_" + sickHist.NameSickness + "_ASCII.txt");
            using (StreamWriter sw = new StreamWriter(file.OpenWrite()))
            {
                Console.WriteLine($"Inscriem info codata in ASCII despre istoria bolii {sickHist.NameSickness } a pacientului {sickHist.Patient.Name} {sickHist.Patient.Surname} in txt file");
                sw.WriteLine($"Pacient {sickHist.Patient.Name} {sickHist.Patient.Surname}",Encoding .ASCII);
                sw.WriteLine($"Sickness name is {sickHist.NameSickness}", Encoding.ASCII);
                sw.WriteLine($"Starea maladiei {sickHist.SicknessState}", Encoding.ASCII);
                sw.WriteLine($"Data internarii {sickHist.SicknessDateStart}", Encoding.ASCII);
                sw.WriteLine($"Data externarii { sickHist.SicknessDateFinish}", Encoding.ASCII);
                sw.WriteLine($"Doctor {sickHist.Doctor.Name} {sickHist.Doctor.Surname}", Encoding.ASCII);
            };
        }

    

    public static void AllSicknessHistoryToTxtFile(List<SicknessHistory> listSick)
    {
        Console.WriteLine("Write all sickness histories into file allHistories.txt");
        string[] str = new string[listSick.Count];
        int i = 0;
        foreach (SicknessHistory sh in listSick)
        {
            str[i] = sh.ToString();
            i++;
        }
        File.WriteAllLines(@"D:\Projects\HospitalProject\ModelHospital\DataSicknessHistories\allHistories.txt", str);
    }

    //public static void PreluareaPacientilor(Doctor docQuit, DateTime dquit,Doctor docNew,List<SicknessHistory> listSicknessHistories)
    //{
    //    NewDoctorQuitArgs ndq = new NewDoctorQuitArgs(docQuit, dquit, docNew);
    //    docQuit.DocQuit(ndq);
    //    SicknessHistory.ChangeDoctors(docQuit,docNew ,listSicknessHistories );

    //}
    public static void PreluareaPacientilor(Doctor docQuit, DateTime dquit, Doctor docNew, List<SicknessHistory> listSicknessHistories)
    {
        NewDoctorQuitArgs ndq = new NewDoctorQuitArgs(docQuit, dquit, docNew);
        docQuit.DocQuit(ndq);
        SicknessHistory.QuitDocHandler.OnDoctorQuit(ndq);
    }
}
}
