using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Domain;
using Factories;
using Hospital.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Repository.Interfaces;
using Hospital.Infrastructure;

namespace TempHospitalApplication
{
    delegate SicknessHistory StartSicknessHandler<TPatient, NewStartSicknessEventArgs>(TPatient p, NewStartSicknessEventArgs a);
    class Program
    {
     
        private static IRepository _repository;

        static Program()
        {
            ServiceLocator.RegisterAll();
            _repository = ServiceLocator.Resolver<IRepository>();
        }

        public Program()
        {
          
          
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

 
        static void Main(string[] args)
        {
            //ConsoleIO();

            string connectionString = ConfigurationManager.ConnectionStrings["hospital"].ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {

                sqlConnection.Open();

                #region Delete Table

                using (var sqlComand = new SqlCommand("Drop table Phone", sqlConnection))
                {
                    sqlComand.ExecuteNonQuery();
                }
                using (var sqlComand = new SqlCommand("Drop table TestTable", sqlConnection))
                {
                    sqlComand.ExecuteNonQuery();
                }

                #endregion

                #region Creation of Tables
                //Create table TestTable
                var sqlCommandText =
                        "CREATE TABLE TestTable(Id bigint identity(1,1) primary key, Name varchar(50) not null)";
                using (var sqlCommand = new SqlCommand(sqlCommandText, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("S-a creat tabela TestTable in BD \n");
                }

                //Create table Phone
                //  sqlCommandText = "CREATE TABLE Phone(Id bigint identity(1,1) primary key, personId bigint not null,prefix int not null)";
                using (var sqlCommand = new SqlCommand("CREATE TABLE Phone(Id bigint identity(1,1) primary key, personId bigint not null,prefix int not null)", sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("S-a creat tabela Phone in BD\n");
                }
                #endregion

                #region DataAdapter for Phone

                DataSet phoneDataSet = new DataSet();

                SqlDataAdapter adapterPhone = new SqlDataAdapter();

                //-----------------------   SELECT   -----------------------

                adapterPhone.SelectCommand = new SqlCommand("Select * from Phone",sqlConnection);

                adapterPhone.Fill(phoneDataSet);

                //Console.WriteLine("Informatie din tabelul Phone from DB");
                foreach (DataRow row in phoneDataSet.Tables[0].Rows)
                    Console.WriteLine($"{row[0],4} {row[1],4} {row[2],6}");
                Console.WriteLine();
                Console.WriteLine("-------   Modificari in Data Set   ------");

                //--------------------   INSERT   ------------------------
                //Insert new rows in Phone
                Console.WriteLine("Inseram rinduri in PhoneDataSet\n");

                DataRow newRow = phoneDataSet.Tables[0].NewRow();
                newRow[0] = phoneDataSet.Tables[0].Rows.Count + 1;
                newRow[1] = 1;
                newRow[2] = 1;
                phoneDataSet.Tables[0].Rows.Add(newRow);
                newRow = phoneDataSet.Tables[0].NewRow();
                newRow[0] = phoneDataSet.Tables[0].Rows.Count + 1;
                newRow[1] = 2;
                newRow[2] = 2;
                phoneDataSet.Tables[0].Rows.Add(newRow);
                newRow = phoneDataSet.Tables[0].NewRow();
                newRow[0] = phoneDataSet.Tables[0].Rows.Count + 1;
                newRow[1] = 3;
                newRow[2] = 4;
                phoneDataSet.Tables[0].Rows.Add(newRow);
                newRow = phoneDataSet.Tables[0].NewRow();
                newRow[0] = phoneDataSet.Tables[0].Rows.Count + 1;
                newRow[1] = 4;
                newRow[2] = 5;
                phoneDataSet.Tables[0].Rows.Add(newRow);

                Console.WriteLine("phoneDataSet dupa adaugarea rindurilor");
                foreach (DataRow row in phoneDataSet.Tables[0].Rows)
                    Console.WriteLine($"{row[0],4} {row[1],4} {row[2],6}");
                Console.WriteLine();

                //Setam InsertCommand pentru adapterPhone
                string sqlInsertCommandText = "Insert into Phone values (@IdPerson,@Prefix)";
                SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertCommandText, sqlConnection);
                sqlInsertCommand.Parameters.Add("@IdPerson", SqlDbType.BigInt, Int32.MaxValue, "personId");
                sqlInsertCommand.Parameters.Add("@Prefix", SqlDbType.Int, Int32.MaxValue, "prefix");

                adapterPhone.InsertCommand = sqlInsertCommand;


                //----------------------   UPDATE   ----------------
                //Setam UpdateCommand pentru adapterPhone 
                SqlCommand sqlUpdateCommand = new SqlCommand("Update Phone set prefix=@Prefix where id=@Id", sqlConnection);
                sqlUpdateCommand.Parameters.Add("@Id", SqlDbType.BigInt, Int32.MaxValue, "id");
                sqlUpdateCommand.Parameters.Add("@Prefix", SqlDbType.Int, Int32.MaxValue, "prefix");

                adapterPhone.UpdateCommand = sqlUpdateCommand;

                //Modificam DataSet
                phoneDataSet.Tables[0].Rows[1]["prefix"] = 666;
                Console.WriteLine("phoneDataSet dupa modificarea prefixului in rindul 2");
                foreach (DataRow row in phoneDataSet.Tables[0].Rows)
                    Console.WriteLine($"{row[0],4} {row[1],4} {row[2],6}");

                adapterPhone.Update(phoneDataSet);

                //----------------------   DELETE   --------------------
                // Setam DeleteCommand pentru adapterPhone
                SqlCommand sqlDeleteCommand = new SqlCommand("delete from Phone where id=@id", sqlConnection);
                sqlDeleteCommand.Parameters.Add("@id", SqlDbType.BigInt, 5, "id");

                adapterPhone.DeleteCommand = sqlDeleteCommand;

                //Stergem rindul 3 cu id = 3
                Console.WriteLine("\nStergem rindul cu Id=3");
                phoneDataSet.Tables[0].Rows[2].Delete();

                Console.WriteLine("-----------------------------------------");

                adapterPhone.Update(phoneDataSet);


                phoneDataSet.Clear();
                // adapterPhone.Fill(phoneDataSet);

                DataTable phoneDataTable = new DataTable();
                adapterPhone.Fill(phoneDataTable);
                Console.WriteLine("\nInformatie salvata in BD");
                foreach (DataRow row in phoneDataTable.Rows)
                    Console.WriteLine($"{row[0],4} {row[1],4} {row[2],6}");
                Console.WriteLine();

                #endregion

                #region Insert Into MedicalSpecialty

                ////MedicalSpecialty
                //SqlDataAdapter adapterMedicalSpecialty = new SqlDataAdapter("select * from MedicalSpecialty",sqlConnection);
                //DataSet medicalSpecialtyDataSet = new DataSet();
                //adapterMedicalSpecialty.Fill(medicalSpecialtyDataSet);



                //DataRow newRow = medicalSpecialtyDataSet.Tables[0].NewRow();
                //newRow[0] = medicalSpecialtyDataSet.Tables[0].Rows.Count + 1;
                //newRow[1] = "GASTROINTEROLOG";
                //medicalSpecialtyDataSet.Tables[0].Rows.Add(newRow);

                //foreach (DataRow row in medicalSpecialtyDataSet.Tables[0].Rows)
                //    Console.WriteLine($"{row[0],4}{row[1],25}");

                //SqlCommand insertCommand = new SqlCommand("insert into MedicalSpecialty (SpecialtyName) values (@SpecialtyName)", sqlConnection);
                //insertCommand.Parameters.Add("@SpecialtyName", SqlDbType.VarChar, 50, "SpecialtyName");

                //adapterMedicalSpecialty.InsertCommand = insertCommand;

                //adapterMedicalSpecialty.Update(medicalSpecialtyDataSet);

                #endregion

                #region SQL Commands

                //-----------------------------------
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

                ////Query parametrization

                //var sqlSelectParametrizationText = "select * from Phone where id=@id;";
                //using (var sqlSelectParam = new SqlCommand(sqlSelectParametrizationText, sqlConnection))
                //{
                //    sqlSelectParam.Parameters.Add("@id", SqlDbType.BigInt);
                //    sqlSelectParam.Parameters["@id"].Value = 2;

                //    SqlDataReader reader = sqlSelectParam.ExecuteReader();

                //    while (reader.Read())
                //    {
                //        long id = (long)reader["id"];
                //        long personId = (long)reader["personId"];
                //        int prefix = (int)reader["prefix"];
                //        Console.WriteLine($"{id,4}{personId,4}{prefix,6}");
                //    }
                //}


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


                #endregion
            }

           

            Console.ReadKey();
            //  _personManagement.InHospital()

            //var updatedPatient = _personManagement.InHospital(DateTime.Now, new Patient("q"));
            var doc = new Doctor("Doc 1", "SurName 1 ", Gender.M, "", "", TipDoctor.RESUSCITATOR, DateTime.Now, DateTime.Now);

            _repository.Save(doc);
        }
    }
}
