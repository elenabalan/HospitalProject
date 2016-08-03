CREATE TABLE StateDoctor
(
Id bigint not null,
IDNP bigint not null,
DateOfEmployment date Not null,
DateOfDimissal date Not null,
[State] bit Not null,

Constraint [FK_StateDoctor_Doctor] FOREIGN KEY (id) REFERENCES Doctor(id)
--Constraint [FK_StateDoctor_Person] FOREIGN KEY (IDNP) REFERENCES Person(IDNP)
)