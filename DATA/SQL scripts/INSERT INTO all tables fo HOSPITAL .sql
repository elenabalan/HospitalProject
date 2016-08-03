USE [Hospital]
GO
--Delete from Person
--DBCC CHECKIDENT ('[Person]', RESEED, 0)

INSERT INTO [dbo].[Person]
           ([Name]
           ,[Surname]
           ,[Birthday]
           ,[Gender]
           ,[Address]
           ,[Phone]
           ,[IDNP])
VALUES
('Bordea','Galina',	'1967-05-18','F',	'bd.Negruzzi, 3',	'25496547',	1236547896542),
('Tataru','Vasile','1984-08-09','M'	,'str. Bucovinei, 9, ap. 29',null,9854236157894),
('Palici','Mariana',	'1983-12-07',	'F',	'Stefan cel Mare,89, ap.7',	'054369871',3697452186549),
('Albu','Adrian','1981-12-11','M',	NULL	,'0569421354',4569875211459),
('Andreev','Gheorghe','1965-06-29','M',	NULL,	NULL,5763145514654),
('Afanasii','Denis','1971-09-21','M','str. Mioritei, 56',	NULL,4645464231643),
('Roman','Angela','1976-08-01','F',	'str. Bucuresti, 65',	'0245963574',	4421385262585)

select * from Person
-------------------------------------
INSERT INTO [dbo].[MedicalSpecialty]
           ([SpecialtyName])
     VALUES
('PEDIATOR'),
('CHIRURG'),
('PATOLOGOANATOM'),
('PSIHIATOR'),
('LOR'),
('OFTALMOLOG')

select * from MedicalSpecialty
--------------------------------------
INSERT INTO [dbo].[Doctor]
           ([IdDoctor]
           ,[IdProfession]
           ,[DateOfStart])
     VALUES
(1,1,'1963-09-01'),
(4,2,'1974-10-15'),
(2,6,'2001-08-20'),
(6,4,'1978-01-01'),
(3,1,'1978-01-01')

select * from Doctor
-------------------------------------

INSERT INTO [dbo].[ManagementDoctor]
           ([IdDoctor]
           ,[IDNP]
           ,[DateOfEmployment]
           ,[DateOfDimissal])
     VALUES
           (1,1236547896542,'1980-09-01',null),
		   (4,4569875211459,'1999-01-20',null),
		   (2,9854236157894,'1980-10-25',null)

select * from ManagementDoctor
-------------------------------------
select * from doctor

INSERT INTO [dbo].[Certificate]
           ([CertificateNumber]
           ,[IdSpecialty]
           ,[IdDoctor]
           ,[DateOfReceiving]
		   ,[ValidFor])
VALUES
('SERT1', 2,4,'1975-04-05',1095),
('SERT2', 1,1,'1970-01-10',365),
('SERT3', 6,2,'2010-07-01',1095),
('SERT4', 1,1,'2009-11-26',550),
('SERT5', 2,4,'2011-06-10',1095),
('SERT6', 4,6,'2007-09-12',1095),
('SERT7', 2,4,'2016-04-24',1095)
select * from Certificate

--SET IDENTITY_INSERT [Certificate] ON
--INSERT INTO [dbo].[Certificate]
--           ([IdCertificate],
--		   [CertificateNumber]
--           ,[IdSpecialty]
--           ,[IdDoctor]
--           ,[DateOfReceiving])
--VALUES
--(1,'SERT8', 2,4,'1995-10-01')
--SET IDENTITY_INSERT [Certificate] OFF
-------------------------------------------

select * from person
insert into [dbo].[Patient]
           ([IdPatient]
           ,[LastDateInHospital]
           ,[State])
values
(3,'20160507',1),
(5,'20160720',0),
(6,'20160715',0),
(7,'20101229',1),
(2,'20160507',0)

select * from patient

----------------------------------

insert into [dbo].[Sickness]
           ([SicknessName]) 
values
('ANGINA'),
('APENDICITA'),
('CATARACTA'),
('GASTRITA')

select * from sickness
-----------------------------------

select * from Doctor
select * from [Certificate]

select * from Patient
select * from Sickness


Insert into [dbo].[SicknessHistory]
           ([IdSickness]
           ,[SicknessName]
           ,[IdPatient]
           ,[IdDoctor]
           ,[StartDate]
           ,[FinishDate]) 
values
(2,'APENDICITA',6,4,'20160507',null),
(3,'CATARACTA',3,2,'20141012','20141111'),
(1,'ANGINA',7,1,'20101229','20110105'),
(4,'GASTRITA',5,4,'20160720',null),
(1,'ANGINA',5,1,'20120514','20120524'),
(2,'APENDICITA',5,4,'2013-07-28','2013-08-05'),
(4,'GASTRITA',7,1,'2015-06-11','2015-06-20'),
(2,'APENDICITA',7,3,'2016-08-01',null),
(1,'APENDICITA',6,1,'2016-07-28',null),
(1,'ANGINA',2,3,'20160507',null),
(1,'ANGINA',2,3,'20141012','20141026')

select * from SicknessHistory

--truncate table SicknessHistory



insert into [dbo].[SicknessSymptoms]
           ([IdSickness]
           ,[Symptom])
values
(1,'Fiebra ridicata'),
(1,'Dureri de cap'),
(1,'Dureri in ghit'),
(1,'Lipsa de pofta'),
(2,'Voma'),
(2,'Dureri abdominale'),
(2,'Dureri abdominale specifice la apasare'),
(3,'Imaginea neclara'),
(3,'Dureri de cap'),
(4,'Spazmuri abdominale'),
(4,'Dureri pronuntate dupa administrarea anumitor produse alimentare')

select * from SicknessSymptoms