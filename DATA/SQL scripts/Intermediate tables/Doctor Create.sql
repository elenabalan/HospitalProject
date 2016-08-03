CREATE TABLE Doctor
(
	Id	BIGINT NOT NULL,
	DateOfEmployment Date NOT NULL,
	DateOfDismissal DATE,
	IdCertificate VARCHAR(50) NOT NULL,
	CONSTRAINT [PK_IdDoctor] PRIMARY KEY CLUSTERED([Id]),
	CONSTRAINT [FK_IdDoctor]	FOREIGN KEY (Id) REFERENCES Person(Id),
	CONSTRAINT [FK_IdSertificate] FOREIGN KEY(IdCertificate) REFERENCES  Certificate(Id)
);