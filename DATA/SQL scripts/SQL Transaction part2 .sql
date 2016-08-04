
----------------   LOCK   -----------------

begin tran
update Phone set Prefix= 222 
where id=6

commit tran

---------------   DEADLOCK   -----------------
begin tran

update PersonCopy set [Address]='tran 222222222' 
where IdPerson = 1;



update Phone set prefix=222
where id = 3;

commit tran

rollback
select @@TRANCOUNT
------------------------   VIEW THE LOCKS    ----------------------
select  
    object_name(p.object_id) as TableName, 
    resource_type, resource_description
from
    sys.dm_tran_locks l
    join sys.partitions p on l.resource_associated_entity_id = p.hobt_id

--------------   DIRTY READ   -----------------
set tran isolation level read uncommitted

select * from Phone where id=3

---- OR
set transaction isolation level read committed
select * from Phone (NOLOCK) where id=3

--Fixam problema
set transaction isolation level read committed
select * from Phone where id=3


--------------------   NON-REPEATEBLE READ   -------------------
begin tran

update Phone set prefix=8
where id=2

commit tran

--------------   PHANTOM READS   ---------------
begin transaction

insert into Phone values (2,8)

--delete from Phone where id=4

commit tran
