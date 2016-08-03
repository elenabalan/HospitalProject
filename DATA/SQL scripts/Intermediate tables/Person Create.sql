create table [dbo].[Person]
(
[Id]		BIGINT	IDENTITY(1,1)	NOT NULL,
[Name]		VARCHAR(50)				NOT NULL,
[Surname]	VARCHAR(50)				NOT NULL,
[Birthday]	DATE				NOT NULL,
[Gender]	CHAR(1)					NOT NULL,
CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id]),
CONSTRAINT [chk_Gender] CHECK (Gender = 'M' OR Gender='F')
);