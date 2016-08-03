----------------   AUTOCOMMIT MODE   -------------------
set implicit_transactions OFF   -- it means that I turn the AUTOCOMMIT mode

-- transation BEGIN
select * from PersonCopy
-- transation END

-- transation BEGIN
Update PersonCopy set [Address] = 'AUTOCOMMIT MODE' 
where IdPerson=1
-- transation END


----------------   IMPLICIT TRANSACTION MODE   -------------------
set implicit_transactions ON
select @@trancount
select xact_state()

select * from Person

select @@trancount
Update PersonCopy set [Address] = 'AUTOCOMMIT MODE' 
where IdPerson=1

select @@trancount
--select * from doctor
delete Doctor 
where IdDoctor=1
select xact_state()
commit tran
rollback

---------------   EXPLICIT TRANSACTION MODE   ---------------------------
set implicit_transactions OFF

begin transaction
	select @@trancount
	select xact_state()

	select * from Person

	begin transaction
		select @@trancount
		Update PersonCopy set [Address] = 'EXPLICIT TRNASACTION MODE' 
		where IdPerson=1

	commit tran
	select @@trancount

	select * from PersonCopy
rollback

--============================================================

----------------   LOCK   -----------------

update Phone set Prefix= 2 
where id=6


begin tran
update Phone set Prefix= 111 
where id=6


commit tran

select * from Phone where id=6
---------------   DEADLOCK   -----------------

update Phone set prefix=5
where id = 3;
update PersonCopy set [Address]='bd. Negruzzi,3' 
where IdPerson = 1;

select * from Phone where id=3
select * from PersonCopy where IdPerson=1


begin tran

update Phone set prefix=111
where id = 3;



update PersonCopy set [Address]='tran 11111111' 
where IdPerson = 1;

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


--=========================================================================

--------------   DIRTY READ   -----------------
--Pentru a fixa DIRTY READ folosim Isolation Level READ COMMITTED
begin tran

update Phone set prefix=100 where id = 3

--it is invalid 
waitfor delay '00:00:10'
rollback
select * from Phone where id=3

--------------    NON-REPEATABLE READS   ----------------
SET Transaction isolation level READ COMMITTED
--Pentru a fixa aceasta problema setam
--SET Tran isolation level Repeatable read
begin transaction

select * from phone 
where id=3

waitfor delay '00:00:05'

select * from phone 
where id=3

commit tran


--------------   PHANTOM READS   ---------------
SET Transaction isolation level READ COMMITTED
--Pentru a fixa aceasta problema setam
--SET Tran isolation level SERIALIZABLE

begin transaction
select * from Phone
where id between 1 and 6

waitfor delay '00:00:05'

select * from Phone
where id between 1 and 6

commit tran

