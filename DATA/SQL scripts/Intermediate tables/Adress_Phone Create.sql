CREATE TABLE AdressPhonePerson
(
IdPerson		BIGINT		NOT NULL,
Adress			VARCHAR(100)		,
PhoneNumber		VARCHAR(25)			,
CONSTRAINT [FK_IDPerson] FOREIGN KEY (IdPerson) REFERENCES Person(Id) ON DELETE CASCADE,
CONSTRAINT [uc_IdPerson] UNIQUE (IdPerson)
			
);