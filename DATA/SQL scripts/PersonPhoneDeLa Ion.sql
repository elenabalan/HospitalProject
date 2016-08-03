create table phone(id bigint primary key, personId bigint, prefix int)

insert into phone values
(1, 1, 1),
(2, 1, 2),
(3, 2, 3)


select * from Person p 
left join Phone ph on p.IdPerson=ph.personId and ph.prefix = 1
where ph.id is null


select * from Person p 
left join Phone ph on p.IdPerson=ph.personId
where ph.id !=ANY (select ph.id from Phone where ph.prefix!=1) or Ph.id is null

