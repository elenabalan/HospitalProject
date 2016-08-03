
CREATE TABLE dbo.Patient
	(
	Id BIGINT NOT NULL,
	DateOfAdmission date NOT NULL,
	DateOfDimission date NULL
	CONSTRAINT  PK_IdPatient PRIMARY KEY CLUSTERED(Id)
	CONSTRAINT	FK_Person_Patient FOREIGN KEY(Id) REFERENCES dbo.Person(Id) ON UPDATE  NO ACTION 
																			ON DELETE  NO ACTION 
	) 
