ALTER TABLE Patient
drop FK_Person_Patient;

ALTER TABLE Patient 
ADD CONSTRAINT FK_Patient_Person FOREIGN KEY (Id) REFERENCES Person(Id);