
----------------------correlated Subqueries: in Select

--Doctori impreuna cu numarul de certificate

SELECT d.IdDoctor,
       p.Name + ' ' + p.Surname AS FullName,
       (SELECT count(*)
        FROM   [Certificate] AS c
        WHERE  c.IdDoctor = d.IdDoctor) AS CountOfCertification
FROM   Doctor AS d
       INNER JOIN
       Person AS p
       ON d.IdDoctor = p.IdPerson;

-------------------------------------------------------------------------
-----------------------correlated Subqueries: in Where

--Combine queries using IN
--Selectam Doctori care trateaza exact 2 persoane

SELECT * FROM   SicknessHistory

SELECT d.IdDoctor,
       p.Name + ' ' + p.Surname AS FullName,
       ms.SpecialtyName
FROM   Doctor AS d
       INNER JOIN
       Person AS p
       ON d.IdDoctor = p.IdPerson
       INNER JOIN
       MedicalSpecialty AS ms
       ON ms.IdSpecialty = d.IdProfession
WHERE  d.IdDoctor IN (SELECT   IdDoctor
                      FROM     SicknessHistory AS sh
                      GROUP BY IdDoctor
                      HAVING   COUNT(IdDoctor) = 2);

------------------------------------------------------------------------
-----------------------------correlated Subqueries: in Having.

SELECT * FROM   SicknessHistory;
SELECT * FROM   ManagementDoctor;

--Selectam SicknessHistories finisati pentru doctorii activi in Hospital 
SELECT   sh.IdDoctor,
         p.Name + ' ' + p.Surname AS FullName,
         count(FinishDate)
FROM     SicknessHistory AS sh
         INNER JOIN
         Person AS p
         ON p.IdPerson = sh.IdDoctor
GROUP BY sh.IdDoctor, p.Name + ' ' + p.Surname
HAVING   sh.IdDoctor IN (SELECT md.IdDoctor
                         FROM   ManagementDoctor AS md
                         WHERE  md.DateOfDimissal IS NULL);

----------------------------------------------------------------------------
-------------------------------Combine queries using All.
--Cautam doctorii care au toate certifictaele expirate
select *,DATEADD(day,c.ValidFor,c.DateOfReceiving) from [Certificate] c


-- De ce selecteaza si doctorul care are certificate expirate si certificat valid
SELECT *
FROM   Doctor AS d
WHERE  GETDATE() > ALL (SELECT DATEADD(day, c.ValidFor, c.DateOfReceiving)
                        FROM   [Certificate] AS c
                        WHERE  d.IdDoctor = c.IdDoctor);

SELECT DISTINCT d.IdDoctor,
                p.Name + ' ' + p.Surname AS FullName,
                (SELECT count(cc.IdCertificate)
                 FROM   [Certificate] AS cc
                 WHERE  cc.IdDoctor = d.IdDoctor) AS NrExpiredCertificates
FROM   Doctor AS d
       INNER JOIN
       [Certificate] AS c
       ON d.IdDoctor = c.IdDoctor
       INNER JOIN
       Person AS p
       ON d.IdDoctor = p.IdPerson
WHERE  GETDATE() > ALL (SELECT DATEADD(day, c.ValidFor, c.DateOfReceiving)
                        FROM   [Certificate] AS c
                        WHERE  d.IdDoctor = c.IdDoctor)
-------------------------------------
--Selectam cel mai tirziu angajat doctor
select DateofStart
FROM   Doctor AS d
where DateOfStart >= ALL (select DateofStart
FROM   Doctor AS d2)
------------------------------------------------------------
---------------------------------Combine queries using EXISTS

--Selectam toti pacienti care au fost sau sunt bolnavi cu ANGINA (toate SicknessHistories cu Diagnoza ANGINA)
select * from SicknessHistory
select * from sickness
--
SELECT p.IdPatient,
       (SELECT pp.Name + ' ' + pp.Surname
        FROM   Person AS pp
        WHERE  pp.IdPerson = p.IdPatient) AS FullName
FROM   Patient AS p
WHERE  EXISTS (SELECT *
               FROM   SicknessHistory AS sh
               WHERE  IdSickness = 1			--5 Nu exista nici o history cu OTITA
                      AND p.IdPatient = sh.IdPatient)


--varianta JOIN
SELECT p.IdPatient,
       pp.Name + ' ' + pp.Surname AS FullName
FROM   Patient AS p
       INNER JOIN
       Person AS pp
       ON p.IdPatient = pp.IdPerson
WHERE  EXISTS (SELECT *
               FROM   SicknessHistory AS sh
               WHERE  IdSickness = 1 --5 Nu exista nici o history cu OTITA
                      AND p.IdPatient = sh.IdPatient);
--


------------------------------------------------------------

--------------------------Combine queries using ANY

--Se gasesc toate bolile care au 'Dureri de cap' in lista simptomilor

select * from SicknessSymptoms

SELECT *
FROM   Sickness AS s
WHERE  s.IdSickness = ANY (SELECT IdSickness
                           FROM   SicknessSymptoms AS ss
                           WHERE  ss.Symptom = 'Dureri de cap');



----------------------------------------------------						   
-----------------------------Write one query using case in select.	

--Selectam toate istoriile cu specificarea daca este deschisa sau data inchiderii istoriei
SELECT sh.IdPatient,
       p.Name + ' ' + p.Surname AS FullName,
       s.SicknessName,
       (CASE 
			WHEN sh.FinishDate IS NULL THEN 'deschisa' 
			ELSE 'Inchisa la ' + CONVERT (VARCHAR (10), sh.FinishDate, 10) 
			END) AS SicknessHistoryState
FROM   SicknessHistory AS sh
       INNER JOIN
       Person AS p
       ON p.IdPerson = sh.IdPatient
       INNER JOIN
       Sickness AS s
       ON s.IdSickness = sh.IdSickness;


-----------------------------------------------------

--------------------------------Doctorii care au fost si pacientii spitaluilui

--Numaram pacientii care sunt si doctorii, si pur si simplu pacienti
SELECT count(CASE 
			 WHEN d.IdDoctor IS NOT NULL THEN 1 
			 END) AS PatientIsADoctor,
       count(CASE 
			 WHEN d.IdDoctor IS NULL THEN 1 
			 END) AS OnlyPatient
FROM   SicknessHistory AS sh
       LEFT OUTER JOIN
       Doctor AS d
       ON d.IdDoctor = sh.IdPatient;

--Adaugam o coloana cu specificare
SELECT *,
       (CASE 
		WHEN sh.IdPatient IN (SELECT IdDoctor FROM   Doctor) THEN 'Patient IS a doctor' 
		ELSE 'Patient IS NOT a doctor' 
		END) as IsPatientADoctor
FROM   SicknessHistory AS sh;

-------------------------------------------------------------------

--------------------------------Write one query using Relational Division.

select IdPatient , IdSIckness , count(IdSickness) from SicknessHistory
--where IdSickness in (1,2)
group by IdPatient,IdSickness
order by IdPatient

--Selectam toti pacienti care au fost bolnavi si de ANGINA si de APENDICITA
SELECT   sh.IdPatient,
         p.Name + ' ' + p.Surname AS FullName
FROM     SicknessHistory AS sh
         INNER JOIN
         Person AS p
         ON p.IdPerson = sh.IdPatient
WHERE    sh.IdSickness IN (1, 2)
GROUP BY sh.IdPatient, p.Name + ' ' + p.Surname
HAVING   count(DISTINCT sh.IdSickness) = 2
         --Pentru divizarea exacta
         AND count(sh.IdSickness) = (SELECT count(*)
                                     FROM   SicknessHistory AS x
                                     WHERE  x.IdPatient = sh.IdPatient);
