CREATE TABLE SicknessHistory
(
Id  BIGINT IDENTITY(1,1) NOT NULL,
SicknessName  VARCHAR(50) NOT NULL,
IdPatient BIGINT NOT NULL,
IdDoctor BIGINT NOT NULL,
StartDate DATE NOT NULL,
FinishDate DATE ,

CONSTRAINT PK_IdSickness PRIMARY KEY (Id),
CONSTRAINT FK_Patient_SicknessHistory FOREIGN KEY (IdPatient ) REFERENCES Patient(Id),
CONSTRAINT FK_Doctor_SicknessHistory FOREIGN KEY (IdDoctor ) REFERENCES Doctor(Id),
CONSTRAINT FK_SicknessType_SicknessHistory FOREIGN KEY (SicknessName ) REFERENCES Sickness(Name)

);