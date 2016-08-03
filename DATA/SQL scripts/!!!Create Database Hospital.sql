
CREATE DATABASE Hospital
GO
--USE [HospitalVersion2]

CREATE TABLE [dbo].[Person](
	[IdPerson] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[Birthday] [date] NOT NULL,
	[Gender] [char](1) NOT NULL,
	[Address] [varchar](100) NULL,
	[Phone] [varchar](25) NULL,
	[IDNP] [bigint] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED (	[IdPerson] ASC))
 
 ALTER TABLE Person
 Add CONSTRAINT [ck_Birthday] CHECK (Year([Birthday]) > YEAR(GETDATE()) - 100 )
 
 ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [chk_Gender] CHECK  (([Gender]='M' OR [Gender]='F'))

 ALTER TABLE Person ADD CONSTRAINT [unq_IDNP] UNIQUE(IDNP)


 CREATE TABLE [dbo].[MedicalSpecialty](
	[IdSpecialty] [bigint] IDENTITY(1,1) NOT NULL,
	[SpecialtyName] [varchar](100) NOT NULL,
	CONSTRAINT [PK_IdSpecialty] PRIMARY KEY CLUSTERED (	[IdSpecialty] ASC)
 )

 CREATE TABLE [dbo].[Doctor](
	[IdDoctor] [bigint] NOT NULL,
	[IdProfession] [bigint] NOT NULL,
	[DateOfStart] [date] NOT NULL,
--	[Expirience] [bigint] AS (datediff(DAY, ([DateOfStart], GETDATE)))
 CONSTRAINT [PK_IdDoctor] PRIMARY KEY CLUSTERED ([IdDoctor] ASC))
 


 CREATE TABLE [dbo].[Patient](
	[IdPatient] [bigint] NOT NULL,
	[LastDateInHospital] [date] NOT NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_IdPatient] PRIMARY KEY CLUSTERED (	[IdPatient] ASC))

 CREATE TABLE [dbo].[Certificate](
	[IdCertificate] [bigint] IDENTITY(1,1) NOT NULL,
	[CertificateNumber] [varchar](50) not null,
	[IdSpecialty] [bigint] NOT NULL,
	[IdDoctor] [bigint] NOT NULL,
	[DateOfReceiving] [date] NOT NULL,
	[ValidFor] bigint not null,
 CONSTRAINT [PK_Id] PRIMARY KEY CLUSTERED (	[IdCertificate] ASC))
 

 ALTER TABLE [Certificate] 
 ADD CONSTRAINT [uqn_CertificateNumber] UNIQUE (CertificateNumber)
 
 
 CREATE TABLE [dbo].[Sickness](
	[IdSickness] BIGINT IDENTITY(1,1) NOT NULL,
	[SicknessName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_IdSickness] PRIMARY KEY CLUSTERED ([IdSickness] ASC))


 CREATE TABLE [dbo].[SicknessSymptoms](
	[IdSickness] BIGINT NOT NULL,
	[Symptom] [varchar](100) NOT NULL
)

CREATE TABLE [dbo].[ManagementDoctor](
	[IdDoctor] [bigint] NOT NULL,
	[IDNP] [bigint] NOT NULL,
	[DateOfEmployment] [date] NOT NULL,
	[DateOfDimissal] [date] NULL,
--	[State] [bit] NOT NULL
) 
ALTER TABLE [ManagementDoctor]
 ADD CONSTRAINT [ck_DateOfDimissal] CHECK ([DateOfDimissal] IS NOT NULL AND [DateOfEmployment]<[DateOfDimissal] OR [DateOfDimissal] IS NULL)
   --ALTER TABLE [ManagementDoctor]
   --     ADD CONSTRAINT [ck_DateOfEmployment] CHECK (CAST (datediff(DAY, (SELECT p.Birthday
   --                                                                     FROM   Person AS p, ManagementDoctor AS d
   --                                                                      WHERE  p.IdPerson = d.IdDoctor), [DateOfEmployment]) / (365.23076923074) AS INT) >= 18);  


CREATE TABLE [dbo].[SicknessHistory](
	[IdSicknessHistory] [bigint] IDENTITY(1,1) NOT NULL,
	[IdSickness] [bigint] NOT NULL,
--	[SicknessName] [varchar](50) NOT NULL DEFAULT (SELECT s.SicknessName from Sickness s where s.IdSickness=IdSickness),
	[SicknessName] [varchar](50) NOT NULL,
	[IdPatient] [bigint] NOT NULL,
	[IdDoctor] [bigint] NOT NULL,
	[StartDate] [date] NOT NULL,
	[FinishDate] [date] NULL,
 CONSTRAINT [PK_IdSicknessHistory] PRIMARY KEY CLUSTERED  ([IdSicknessHistory] ASC))
 
 ALTER TABLE [SicknessHistory]
 ADD CONSTRAINT [ck_FinishDate] CHECK ([FinishDate] IS NOT NULL AND [StartDate]<[FinishDate] OR [FinishDate] IS NULL)




--ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [chk_Gender]

 ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Person] FOREIGN KEY([IdPatient])
REFERENCES [dbo].[Person] ([IdPerson])


--ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [FK_Patient_Person]



 ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Person] FOREIGN KEY([IdDoctor])
REFERENCES [dbo].[Person] ([IdPerson])

ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_IdProfession] FOREIGN KEY([IdProfession])
REFERENCES [dbo].[MedicalSpecialty] ([IdSpecialty])

--ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_Person]


 ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD  CONSTRAINT [FK_Certificate_Doctor] FOREIGN KEY([IdDoctor])
REFERENCES [dbo].[Doctor] ([IdDoctor])

ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD  CONSTRAINT [FK_IdSpeciality] FOREIGN KEY([IdSpecialty])
REFERENCES [dbo].[MedicalSpecialty] ([IdSpecialty])

--ALTER TABLE [dbo].[Certificate] CHECK CONSTRAINT [FK_Certificate_Doctor]


ALTER TABLE [dbo].[SicknessSymptoms]  WITH CHECK ADD  CONSTRAINT [FK_IdSickness] FOREIGN KEY([IdSickness])
REFERENCES [dbo].[Sickness] ([IdSickness])


--ALTER TABLE [dbo].[SicknessSymptoms] CHECK CONSTRAINT [FK_SicknessName]

ALTER TABLE [dbo].[ManagementDoctor]  WITH CHECK ADD  CONSTRAINT [FK_StateDoctor_Doctor] FOREIGN KEY([IdDoctor])
REFERENCES [dbo].[Doctor] ([IdDoctor])


--ALTER TABLE [dbo].[StateDoctor] CHECK CONSTRAINT [FK_StateDoctor_Doctor]

ALTER TABLE [dbo].[SicknessHistory]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_SicknessHistory] FOREIGN KEY([IdDoctor])
REFERENCES [dbo].[Doctor] ([IdDoctor])


--ALTER TABLE [dbo].[SicknessHistory] CHECK CONSTRAINT [FK_Doctor_SicknessHistory]


ALTER TABLE [dbo].[SicknessHistory]  WITH CHECK ADD  CONSTRAINT [FK_SicknessHistory_Patient] FOREIGN KEY([IdPatient])
REFERENCES [dbo].[Patient] ([IdPatient])


--ALTER TABLE [dbo].[SicknessHistory] CHECK CONSTRAINT [FK_Patient_SicknessHistory]


ALTER TABLE [dbo].[SicknessHistory]  WITH CHECK ADD  CONSTRAINT [FK_SicknessHistory_Sickness] FOREIGN KEY([IdSickness])
REFERENCES [dbo].[Sickness] ([IdSickness])

--ALTER TABLE [dbo].[SicknessHistory] CHECK CONSTRAINT [FK_SicknessType_SicknessHistory]



