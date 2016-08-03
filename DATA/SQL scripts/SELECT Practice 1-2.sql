
SELECT * FROM Doctor

SELECT * FROM Patient

SELECT * FROM Person

SELECT * FROM [Certificate]

SELECT * FROM Sickness

SELECT * FROM SicknessHistory

SELECT * FROM SicknessSymptoms

--Select All Patient Activi in Hospital (Patient.State=0)
SELECT * FROM Patient

SELECT * FROM Patient
WHERE [State]=0

--Select sickness histories deschise
SELECT * FROM SicknessHistory

SELECT * FROM SicknessHistory
WHERE FinishDate IS NULL

--All persons with name start with 'A'

SELECT * FROM Person
WHERE Name like 'A%'


--Specialistii in PEDIATRIE

SELECT p.Name + ' ' + p.Surname
FROM   [Person] AS p
       INNER JOIN
       Doctor AS d
       ON d.IdProfession = 1
          AND p.IdPerson = d.IdDoctor;


select * from MedicalSpecialty

SELECT *
FROM   person AS p
       LEFT OUTER JOIN
       patient AS pt
       ON p.IdPerson = pt.IdPatient;

--Doctorii care au tratat exact 2 persoane
select * from SicknessHistory

SELECT   sh.IdDoctor
FROM     SicknessHistory AS sh
GROUP BY IdDoctor
HAVING   count(IdPatient) = 2;

--Doctorii care au mai mult de un certificat
select * from [Certificate]

SELECT   idDoctor,
         count(idDoctor) AS NumberOfCertificates
FROM     [Certificate]
GROUP BY IdDoctor
HAVING   count(IdDoctor) > 1;

--Pacientii tratati de mai multi doctori
select * from SicknessHistory

select IdPatient from SicknessHistory
group by IdPatient
having count(IdDoctor)>1

--Nr de simptome la toate bolile
select * from SicknessSymptoms

SELECT   ss.IdSickness,
         s.SicknessName,
         COUNT(ss.IdSickness) AS NumberOfSymptoms
FROM     SicknessSymptoms AS ss
         LEFT OUTER JOIN
         Sickness AS s
         ON ss.IdSickness = s.IdSickness
GROUP BY ss.IdSickness, s.SicknessName;


SELECT t.IdSickness,
       s.SicknessName,
       t.NumberOfSymptoms
FROM   Sickness AS s
       INNER JOIN
       (SELECT   IdSickness,
                 COUNT(IdSickness) AS NumberOfSymptoms
        FROM     SicknessSymptoms AS ss
        GROUP BY ss.IdSickness) AS t
       ON t.IdSickness = s.IdSickness;

----------------------------------
----------------------------------
----------------------------------

--Try Out truncate statement.

select * from Doctor
select * from ManagementDoctor

--STAREA INITIALA A TABELELOR
--truncate table Doctor  --Nu merge pentru ca are FOREIGN KEY
--delete from Doctor   --Nu merge pentru ca are foreign key in Certificate


truncate table ManagementDoctor  -- A MERS
select * from ManagementDoctor

INSERT INTO [dbo].[ManagementDoctor]
           ([IdDoctor]
           ,[IDNP]
           ,[DateOfEmployment]
           ,[DateOfDimissal])
     VALUES
           (1,1236547896542,'1980-09-01',null),
		   (4,4569875211459,'1999-01-20',null),
		   (2,9854236157894,'1980-10-25',null)

-----------------------------------------------------------
--Try out Insert Select statement.
-- Create PersonDetail table 
CREATE TABLE [dbo].[PersonDetail](
	[IdPerson] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
	[Birthday] [date] NOT NULL,
	[Gender] [char](1) NOT NULL,
	[IDNP] [bigint] NOT NULL,
 CONSTRAINT [PK_PersonDetail] PRIMARY KEY CLUSTERED (	[IdPerson] ASC))
 
 ALTER TABLE Person
 Add CONSTRAINT [ck_BirthdayPersonDetail] CHECK (Year([Birthday]) > YEAR(GETDATE()) - 100 )
 
 ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [chk_GenderPersonDetail] CHECK  (([Gender]='M' OR [Gender]='F'))

 ALTER TABLE Person ADD CONSTRAINT [unq_IDNPPersonDetail] UNIQUE(IDNP)

 


 SET IDENTITY_INSERT PersonDetail ON;
 INSERT INTO [dbo].[PersonDetail](
	[IdPerson],
	[FullName],
	[Birthday],
	[Gender],
	[IDNP])
	SELECT [IdPerson],[Name]+' '+[Surname],[Birthday],[Gender],[IDNP]
	FROM [dbo].[Person];

SET IDENTITY_INSERT PersonDetail ON

SELECT * FROM Person;
SELECT * FROM PersonDetail
------------------------------------------

SELECT * FROM ManagementDoctor
-- Doctorul cu ID=1 s-a eliberat de la serviciu
UPDATE ManagementDoctor
SET DateOFDimissal = '2016-07-29'
WHERE idDoctor = 1
SELECT * FROM ManagementDoctor

SELECT * FROM [Certificate]
DELETE FROM [Certificate]
WHERE IdDoctor = 1
SELECT * FROM [Certificate]



--Try Out a Delete based on Join.
--In Certificate adaugam rinduri cu certificate pentru IdDoctor = 1 Care va urma sa fie sterse folosind join

INSERT INTO [dbo].[Certificate]
           ([CertificateNumber]
           ,[IdSpecialty]
           ,[IdDoctor]
           ,[DateOfReceiving])
VALUES
('SERT9', 1,1,'1970-01-10'),
('SERT10', 1,1,'2009-11-26'),
('SERT12', 4,1,'2007-09-12'),
('SERT11', 2,1,'2016-04-24')
select * from Certificate

--Stregem toate certificate ale doctorilor eliberati
SELECT * FROM [Certificate]  --Vor fi sterse Certificatele Doctorului cu Id=1. 
DELETE c FROM [Certificate] c
INNER JOIN ManagementDoctor md on c.IdDoctor=md.IdDoctor
WHERE md.DateOfDimissal IS NOT NULL
SELECT * FROM [Certificate] 
---------------------------------------------

--Try Out an Update based on Join 

--De la Registru a venit info referitoare la datele persoanelor (diferita de cea pastrata in Hospital.Person)

CREATE TABLE [dbo].[PersonRegistru](
	--[IdPerson] [bigint] IDENTITY(1,1) PRIMARY KEY,
	[IDNP] [bigint] PRIMARY KEY,
	[Name] [varchar](50) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[Birthday] [date] NOT NULL,
	[Gender] [char](1) NOT NULL,
	[Address] [varchar](100) NULL,
	[Phone] [varchar](25) NULL,
	
	CONSTRAINT [unq_IDNPPersonDetailR] UNIQUE NONCLUSTERED ([IDNP]),
	CONSTRAINT [chk_GenderR] CHECK  (([Gender]='M' OR [Gender]='F')),
	CONSTRAINT [chk_GenderPersonDetailR] CHECK  (([Gender]='M' OR [Gender]='F')),
	CONSTRAINT [ck_BirthdayR] CHECK  ((datepart(year,[Birthday])>(datepart(year,getdate())-(100)))),
	CONSTRAINT [ck_BirthdayPersonDetailR] CHECK  ((datepart(year,[Birthday])>(datepart(year,getdate())-(100))))
)
truncate table PersonRegistru

INSERT INTO [dbo].[PersonRegistru]
           ([Name]
           ,[Surname]
           ,[Birthday]
           ,[Gender]
           ,[Address]
           ,[Phone]
           ,[IDNP])
VALUES
('Bordea','Galina',	'1967-05-18','F',	'bd.Negruzzi, 3',	'25496547',	1236547896542),
('!!!Aparece','Olga','1983-11-19','F',	'Ungheni, Magurele, 7',	NULL,	9654723684569),
('Tataru','Vasile','1984-08-09','M'	,'!!!str. Traian, 9, ap. 29',null,9854236157894),
('Palici','Mariana',	'1983-12-07',	'F',	'!!!or. Stefan-Voda',	'054369871',3697452186549),
('Albu','Adrian','1981-12-11','M',	'!!!Str.Grenoble'	,'026975463',4569875211459),
('Andreev','Gheorghe','1965-06-29','M',	NULL,	NULL,5763145514654),
('Afanasii','Denis','1971-09-21','M','str. Mioritei, 56',	NULL,4645464231643),
('Roman','Angela','1976-08-01','F',	'str. Bucuresti, 65',	'0245963574',	4421385262585),
('!!!Roman','Victor','1981-03-19','M',	'str. Bucuresti, 65',	NULL,	9654782145698),
('!!!Bolocan','Viorel','1972-04-29','M',	NULL,	NULL,	9873654125863)

select * from PersonRegistru



--Update based on Join            Ajustam Datele din Hospital.Person cu cele venite de la registru cu ajutorul IDNP
drop table PersonCopy
SELECT * INTO PersonCopy FROM Person

UPDATE p
SET p.[Address] = pr.Address
FROM PersonCopy p
JOIN PersonRegistru pr on p.IDNP = pr.IDNP

SELECT * FROM PersonCopy


--Reintorcem Person la situatie initiala

--UPDATE [dbo].[PersonCopy]
--           SET [Address]='str. Bucovinei, 9, ap. 29' 
--		   OUTPUT  inserted.IdPerson, inserted.[Address] as NewAdrdress, deleted.[Address] as OldAddress
--		   WHERE IdPerson=2
--UPDATE [dbo].[PersonCopy]
--           SET [Address]='Stefan cel Mare,89, ap.7'
--		   OUTPUT  inserted.IdPerson, inserted.[Address] as NewAdrdress, deleted.[Address] as OldAddress
--		   WHERE IdPerson=3
--UPDATE [dbo].[PersonCopy]
--           SET [Address]=NULL
--		   OUTPUT  inserted.IdPerson, inserted.[Address] as NewAdrdress, deleted.[Address] as OldAddress
--		   WHERE IdPerson=4

--DELETE FROM Person
--where IDNP in (9873654125863,9654782145698,9654723684569)


---------------------------------------
--Rewriting an Update based on join with MERGE COMAND


--**************************
--Reintorcem PersonCopy la situatie initiala

drop table PersonCopy

SELECT * INTO PersonCopy FROM Person
--**************************

select * from PersonRegistru

select * from PersonCopy

MERGE PersonCopy AS p 
USING PersonRegistru AS pr 
  ON p.IDNP=pr.IDNP 
WHEN MATCHED  AND ((p.[Address]<>pr.[Address] OR p.Phone <> pr.Phone) OR (p.[Address] IS NULL AND pr.[Address] IS NOT NULL))
   THEN UPDATE SET p.[Address]=pr.[Address],p.Phone = pr.Phone
WHEN NOT MATCHED BY TARGET
   THEN INSERT (Name,Surname,Birthday,Gender,[Address],Phone,IDNP) 
		VALUES (pr.Name,pr.Surname,pr.Birthday,pr.Gender,pr.[Address],pr.Phone,pr.IDNP)                                              
OUTPUT $action, inserted.*;
  
 select * from personCopy
 ------------------------------------------------




