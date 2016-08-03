using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using Domain;
using Factories;
using Hospital.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace TempHospitalApplication
{
    delegate SicknessHistory StartSicknessHandler<TPatient, NewStartSicknessEventArgs>(TPatient p, NewStartSicknessEventArgs a);
    class Program
    {
        private static IPersonManagement _personManagement;

        public Program(IPersonManagement personManagement)
        {
            _personManagement = personManagement;
        }

        public static void ConsoleIO()
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
            //       PersonFactory factoryPersonInHospital;

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
        }

        //public class A
        //{
        //    public A()
        //    {
        //    }

        //    public void Do()
        //    {
        //        Console.WriteLine("Do from non static A");
        //    }
        //}

        static void Main(string[] args)
        {
            //A aaa = new A();
            //aaa.Do();

            //ConsoleIO();

            string connectionString = ConfigurationManager.ConnectionStrings["hospital"].ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {

                sqlConnection.Open();

                //// Try SqlCommand ExecuteScalar()
                //var sqlSelectCommandText = "Select Name from Person where IdPerson=1";
                //using (var sqlSelectCommand = new SqlCommand(sqlSelectCommandText, sqlConnection))
                //{
                //    Console.WriteLine(sqlSelectCommand.ExecuteScalar());
                //}


                ////Create table TestTable
                //var sqlCommandText =
                //        "CREATE TABLE TestTable(Id bigint identity(1,1) primary key, Name varchar(50) not null)";
                //using (var sqlCommand = new SqlCommand(sqlCommandText,sqlConnection))
                //{
                //    var rez = sqlCommand.ExecuteNonQuery();
                //    Console.WriteLine($"sqlCommand.ExecuteNonQuery() return the value {rez}");
                //}

                ////Try ExecuteReader()
                //var sqlSelectCommandText = "Select * from Phone";
                //using (var sqlSelectCommand = new SqlCommand(sqlSelectCommandText, sqlConnection))
                //{
                //    SqlDataReader reader = sqlSelectCommand.ExecuteReader();
                //    while (reader.Read())
                //    {
                //        long id = (long) reader["id"];
                //        long personId = (long) reader["personId"];
                //        int prefix = (int) reader["prefix"];
                //        Console.WriteLine($"{id,4}{personId,4}{prefix,6}");
                //    }
                //}

                //Query parametrization

                var sqlSelectParametrizationText = "select * from Phone where id=@id;";
                using (var sqlSelectParam = new SqlCommand(sqlSelectParametrizationText, sqlConnection))
                {
                    sqlSelectParam.Parameters.Add("@id", SqlDbType.BigInt);
                    SqlDataReader reader;
                   
                        sqlSelectParam.Parameters["@id"].Value = 5;
                        reader = sqlSelectParam.ExecuteReader();

                        while (reader.Read())
                        {
                            long id = (long)reader["id"];
                            long personId = (long)reader["personId"];
                            int prefix = (int)reader["prefix"];
                            Console.WriteLine($"{id,4}{personId,4}{prefix,6}");
                            //Console.WriteLine($"{(string)reader["id"],4}{(string)reader["idPerson"],4},{(string)reader["prefix"],6}");
                        }

                    
                  }


                    ////DataTable

                    //    DataTable PhoneTable = new DataTable();
                    //    DataColumn IdColumn = new DataColumn("Id",typeof(long));

                    //    PhoneTable.Columns.Add(IdColumn);
                    //    PhoneTable.Columns.Add("IdPerson", typeof(long));
                    //    PhoneTable.Columns.Add("Prefix", typeof(int));

                    //    DataRow row1 = PhoneTable.NewRow();
                    //    row1["Id"] = 1;
                    //    row1["IdPerson"] = 1;
                    //    row1["Prefix"] = 3;
                    //    PhoneTable.Rows.Add(row1);


                    //???????????????????????????
                    //Using DataAdapter
                //    var sqlSelectCommandText = "Select IdPerson,Name,Surname,Birthday from Person";
                //SqlDataAdapter adapter = new SqlDataAdapter(sqlSelectCommandText, sqlConnection);
                //DataSet dataSet = new DataSet();

                //adapter.SelectCommand = new SqlCommand(sqlSelectCommandText);
                //adapter.UpdateCommand = new SqlCommand("insert into TestTable (Name) values(@Name)");

                //adapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.VarChar, 50, "Name");
                //adapter.Parameters["@Name"] = "Valentin";
                //dataSet.Tables.Add("PersonTable");  //????????????/
                 
                //adapter.Fill(dataSet);
               
                ////    adapter.FillSchema(dataSet, SchemaType.Mapped);
                ////     adapter.Fill(PersonTable);

                //foreach (DataRow row in dataSet.Tables["Table"].Rows)
                ////DataTable PersonTable = dataSet.Tables["Table"];
                ////foreach (DataRow row in PersonTable.Rows)
                //{
                //    Console.WriteLine($"{row["IdPerson"],4}{row["Name"],10}{row["Surname"],10}{row["Birthday"],20}");
                //}

                //////------------------------------------
                ////New row in dataSet
                //DataRow newRow = dataSet.Tables["Table"].NewRow();
                //newRow[0] = 8;
                //newRow[1] = "Balta";
                //newRow[2] = "Eugen";
                //newRow[3] = new DateTime(2010, 08, 21);
                //dataSet.Tables["Table"].Rows.Add(newRow);


                //foreach (DataRow row in dataSet.Tables["Table"].Rows)
                ////DataTable PersonTable = dataSet.Tables["Table"];
                ////foreach (DataRow row in PersonTable.Rows)
                //{
                //    Console.WriteLine($"{row["IdPerson"],4}{row["Name"],10}{row["Surname"],10}{row["Birthday"],20}");
                //}

                //adapter
                //adapter.Update(dataSet);
               
                //???????????????????

                //Create table TestTable and Insert several rows

                //  DataAdapter adapter = new SqlDataAdapter();
                //  var sqlCommandText =
                //         "insert into TestTable (Name) values(@Name)";
                //  SqlCommand insertCommand = new SqlCommand(sqlCommandText, sqlConnection);
                //  insertCommand.Parameters.Add("@Name", SqlDbType.VarChar, 50, "Name");






                //  insertCommand.Parameters["@Name"] = "Valentin";

                ////  adapter.

                //   Console.ReadKey();


            }


            Console.ReadKey();
            //  _personManagement.InHospital()

            //var updatedPatient = _personManagement.InHospital(DateTime.Now, new Patient("q"));

        }
    }
}
