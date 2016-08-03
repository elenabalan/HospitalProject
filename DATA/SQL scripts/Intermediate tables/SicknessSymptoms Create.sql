CREATE TABLE SicknessSymptoms
(
SicknessName VARCHAR(50) NOT NULL,
Symptom VARCHAR(100) NOT NULL,
CONSTRAINT FK_SicknessName FOREIGN KEY (SicknessName) REFERENCES Sickness(Name)
);