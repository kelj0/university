--------------------------------------------------------------------
-----------------------PREDAVANJE1----------------------------------
--------------------------------------------------------------------

-- 1. Dohvatiti sve kupce iz tablice Kupac koji se zovu Ana ili Tamara i iz Osijeka su.
SELECT * FROM Kupac WHERE Ime IN ('Ana', 'Tamara') AND GradID = 2
-- ili 
SELECT * FROM Kupac WHERE (Ime = 'Ana' OR Ime = 'Tamara') AND GradID = 2

-- 2. Umetnite proizvod "Sony Player" cijene 985,50 kuna. Potkategorija je "Playeri", kategorija "Razno". Podatke koji nisu zadani izmislite.
INSERT INTO Kategorija (Naziv) VALUES ('Razno') 
-- Umeæe kategoriju s ID-em 9 (kod vas može biti drukèije).

INSERT INTO Potkategorija (KategorijaID, Naziv) VALUES (9, 'Playeri') 
-- Umeæe potkategoriju s ID-em 38 (kod vas može biti drukèije).

INSERT INTO Proizvod (Naziv, BrojProizvoda, Boja, MinimalnaKolicinaNaSkladistu, CijenaBezPDV, PotkategorijaID) 
VALUES ('Sony Player', 'XX-0001', 'Trula višnja', 20, 985.50, 38)

-- 3. Napravite tablicu KupacVIP sa stupcima ime i prezime. Umetnite u nju sve kupce koji se zove Karen, Mary ili Jimmy.
CREATE TABLE KupacVIP
(
	IDKupacVIP int CONSTRAINT PK_KupacVIP PRIMARY KEY IDENTITY,
	Ime nvarchar(50) NOT NULL,
	Prezime nvarchar(50) NOT NULL
)

INSERT INTO KupacVIP (Ime, Prezime)
SELECT Ime, Prezime FROM Kupac WHERE Ime IN ('Karen', 'Mary', 'Jimmy')

SELECT * FROM KupacVIP

DROP TABLE KupacVIP

-- 4. Kupcima s ID-evima 40, 41 i 42 promijenite e-mail u nepoznato@nepoznato.com
UPDATE Kupac
SET Email = 'nepoznato@nepoznato.com'
WHERE IDKupac IN (40, 41, 42)

SELECT * FROM Kupac WHERE IDKupac IN (40, 41, 42)

-- 5. Obrišite sve kupce koji se prezivaju Trtimiroviæ. Je li se dogodila pogreška? Koliko ih je obrisano?
DELETE FROM Kupac WHERE Prezime = 'Trtimiroviæ'

-- 6. Dohvatiti imena i prezimena svih kupaca i uz svakog ispisati naziv grada i države.
SELECT 
	k.Ime,
	k.Prezime,
	g.Naziv AS NazivGrada,
	d.Naziv AS NazivDrzave
FROM Kupac AS k
INNER JOIN Grad AS g ON k.GradID = g.IDGrad
INNER JOIN Drzava AS d ON g.DrzavaID = d.IDDrzava

-- 7. Ispisati nazive proizvoda koji su na nekoj stavci raèuna prodani u više od 35 komada. Svaki proizvod navesti samo jednom.
SELECT DISTINCT
	p.Naziv
FROM Stavka AS s
INNER JOIN Proizvod AS p ON s.ProizvodID = p.IDProizvod
WHERE s.Kolicina > 35

-- 8. Koristeæi lijevo vanjsko spajanje dohvatiti sve proizvode koji nisu nikad prodani.
SELECT *
FROM Proizvod AS p
LEFT OUTER JOIN Stavka AS s ON s.ProizvodID = p.IDProizvod
WHERE s.ProizvodID IS NULL

-- 9. Koristeæi puno vanjsko spajanje ispisati nazive država i nazive gradova. Ispisati samo one gradove koji nemaju definiranu državu i one države koji nemaju upisanih gradova.
SELECT 
	g.Naziv AS NazivGrada,
	d.Naziv AS NazivDrzave
FROM Grad AS g
FULL OUTER JOIN Drzava AS d ON g.DrzavaID = d.IDDrzava
WHERE g.IDGrad IS NULL OR d.IDDrzava IS NULL

-- 10. Vratite nazive svih proizvoda i uz svaki ispišite boju ako je definirana, odnosno "NIJE DEFINIRANA" ako nije.
SELECT 
	Naziv,
	ISNULL(Boja, 'NIJE DEFINIRANA') AS Boja
FROM Proizvod

-- 11. Vratite prosjeènu cijenu proizvoda iz potkategorije 16.
SELECT AVG(CijenaBezPdv)
FROM Proizvod
WHERE PotkategorijaID = 16

-- 12. Vratite datume najstarijeg i najnovijeg raèuna izdanog kupcu 131.
SELECT 
	MIN(DatumIzdavanja) AS NajstarijiRacun,
	MAX(DatumIzdavanja) AS NajnovijiRacun
FROM Racun
WHERE KupacID = 131

-- 13. Grupiranjem ispišite sve boje proizvoda i pokraj svake napišite koliko proizvoda ima tu boju.
SELECT 
	Boja,
	COUNT(*) AS BrojPoizvodaKojiImaTuBoju
FROM Proizvod
GROUP BY Boja

-- 14. Grupiranjem ispišite koliko je raèuna izdano koje godine.
SELECT 
	YEAR(DatumIzdavanja) AS GodinaIzdavanja,
	COUNT(*) AS IzdanoTeGodine
FROM Racun
GROUP BY YEAR(DatumIzdavanja)

-- 15. Grupiranjem ispišite ukupno zaraðene iznose za svaki od proizvoda koji je prodan u više od 2000 primjeraka.
SELECT 
	p.Naziv,
	SUM(s.UkupnaCijena) AS UkupnoZaradjeno
FROM Stavka AS s
INNER JOIN Proizvod AS p ON s.ProizvodID = p.IDProizvod
GROUP BY p.Naziv
HAVING SUM(s.Kolicina) > 2000

-- 16. Koristeæi podupite, dohvatite imena i prezimena 5 komercijalista koji su izdali najviše raèuna.
SELECT TOP 5
    k.Ime, 
    k.Prezime, 
    (SELECT COUNT(r.IDRacun) FROM Racun r WHERE k.IDKomercijalist = r.KomercijalistID) AS UkupnoRacuna 
FROM Komercijalist k
ORDER BY UkupnoRacuna DESC

-- 17. Dohvatite sve boje proizvoda. Uz svaku boju pomoæu podupita dohvatite broj proizvoda u toj boji.
SELECT DISTINCT
	p.Boja,
	(SELECT COUNT(*)
	FROM Proizvod AS p1
	WHERE ISNULL(p1.Boja, 'NEMA') = ISNULL(p.Boja, 'NEMA')) AS BrojProizvodaTeBoje
FROM Proizvod AS p

-- 18. Vratite sve proizvode koji nikad nisu prodani:
-- a)Pomoæu IN ili NOT IN:
SELECT *
FROM Proizvod AS p
WHERE 
	p.IDProizvod NOT IN (SELECT s.ProizvodID FROM Stavka AS s)

-- b) Pomoæu EXISTS ili NOT EXISTS:
SELECT *
FROM Proizvod AS p
WHERE 
	NOT EXISTS(SELECT * FROM Stavka AS s WHERE s.ProizvodID = p.IDProizvod)

--------------------------------------------------------------------
-----------------------PREDAVANJE2/3--------------------------------
--------------------------------------------------------------------
/*
1. Napraviti tablicu Klijent(IDKlijent, Ime, Prezime, Tel1, Tel2,Tel3)
2. Napuniti tablicu s nekim podacima
3. Neka aplikacije koriste tablicu (npr. SELECT Ime, Prezime,Tel1, Tel2, Tel3 FROM Klijent) 
*/

-- 1. Kreiranje baze.
CREATE DATABASE Predavanje02Demo
GO

USE Predavanje02Demo
GO

-- 2. Kreiranje osnovne tablice i punjenje podacima.
CREATE TABLE Klijent
(
	IDKlijent int CONSTRAINT PK_Klijent PRIMARY KEY IDENTITY, 
	Ime nvarchar(50) NOT NULL, 
	Prezime nvarchar(50) NOT NULL, 
	Tel1 nvarchar(50) NULL, 
	Tel2 nvarchar(50) NULL, 
	Tel3 nvarchar(50) NULL
)

INSERT INTO Klijent (Ime, Prezime, Tel1, Tel2, Tel3) VALUES ('Miro', 'Miriæ', '095/111-222', null, null)
INSERT INTO Klijent (Ime, Prezime, Tel1, Tel2, Tel3) VALUES ('Ana', 'Aniæ', '091/222-333', '098/999-555', null)
INSERT INTO Klijent (Ime, Prezime, Tel1, Tel2, Tel3) VALUES ('Juro', 'Juriæ', '099/999-222', null, null)
GO

-- 3. Normalizacija. Prvo kreiramo dvije nove tablice.
CREATE TABLE KlijentOsoba
(
	IDKlijentOsoba int CONSTRAINT PK_KlijentOsoba PRIMARY KEY IDENTITY, 
	Ime nvarchar(50) NOT NULL, 
	Prezime nvarchar(50) NOT NULL
)

CREATE TABLE KlijentTelefon
(
	IDKlijentTelefon int CONSTRAINT PK_KlijentTelefon PRIMARY KEY IDENTITY, 
	KlijentOsobaID int CONSTRAINT FK_KlijentTelefon_KlijentOsoba FOREIGN KEY REFERENCES KlijentOsoba(IDKlijentOsoba) NOT NULL,
	BrojTelefona nvarchar(50) NOT NULL, 
	Rbr int NOT NULL
)
GO

-- 4. Migriramo podatke (ignorirajuæi potrebnu eksplicitnog umetanja vrijednosti primarnog kljuèa).
INSERT INTO KlijentOsoba (Ime, Prezime)
SELECT Ime, Prezime
FROM Klijent

INSERT INTO KlijentTelefon (KlijentOsobaID, BrojTelefona, Rbr)
SELECT IDKlijent, Tel1, 1 AS Rbr FROM Klijent WHERE Tel1 IS NOT NULL

INSERT INTO KlijentTelefon (KlijentOsobaID, BrojTelefona, Rbr)
SELECT IDKlijent, Tel2, 2 AS Rbr FROM Klijent WHERE Tel2 IS NOT NULL

INSERT INTO KlijentTelefon (KlijentOsobaID, BrojTelefona, Rbr)
SELECT IDKlijent, Tel3, 3 AS Rbr FROM Klijent WHERE Tel3 IS NOT NULL
GO

-- 5. Uklanjamo staru tablicu (i time "skršimo" sve njene korisnike)
DROP TABLE Klijent
GO

-- 6. Izraðujemo pogled istog naziva.
CREATE VIEW Klijent
AS
SELECT 
	ko.IDKlijentOsoba AS IDKlijent,
	ko.Ime,
	ko.Prezime,
	(SELECT kt.BrojTelefona FROM KlijentTelefon AS kt WHERE kt.KlijentOsobaID = ko.IDKlijentOsoba AND kt.Rbr = 1) AS Tel1,
	(SELECT kt.BrojTelefona FROM KlijentTelefon AS kt WHERE kt.KlijentOsobaID = ko.IDKlijentOsoba AND kt.Rbr = 2) AS Tel2,
	(SELECT kt.BrojTelefona FROM KlijentTelefon AS kt WHERE kt.KlijentOsobaID = ko.IDKlijentOsoba AND kt.Rbr = 3) AS Tel3
FROM KlijentOsoba AS ko
GO

-- Aplikacija sad može i dalje koristiti objekt naziva Klijent (ali samo za dohvaæanje podataka; INSERT/UPDATE/DELETE je druga prièa koja æe biti objašnjena u nastavku)
SELECT * FROM Klijent
GO

USE master
DROP DATABASE Predavanje02Demo
GO

/*
1. Situacija: neki korisnik nema pravo koristiti tablicu
Proizvod, a treba određene podatke. Pomognite mu tako
što ćete napraviti pogled koji će dohvaćati sve proizvode.
2. Iskoristite pogled za dohvaćanje svih zapisa
a. Iskoristite pogled za dohvaćanje zapisa u potkategoriji 13
b. Iskoristite pogled za ispis boja proizvoda i broja proizvoda u
svakoj boji, padajuće prema broju proizvoda
c. Iskoristite pogled i pokraj naziva proizvoda ispišite i naziv
potkategorije
3. Promijenite pogled tako da preimenujete stupac Naziv u
NazivProizvoda. Dohvatite podatke kroz pogled.
4. Uklonite pogled
*/
CREATE VIEW p1 
AS
SELECT * FROM Proizvod
GO

SELECT * FROM p1 
SELECT * FROM p1 WHERE PotkategorijaID = 13
SELECT Boja, COUNT(*) AS BrojProizvoda FROM p1 GROUP BY Boja ORDER BY BrojProizvoda DESC

SELECT p1.Naziv, pk.Naziv AS PotkategorijaNaziv
FROM p1
INNER JOIN Potkategorija AS pk ON p1.PotkategorijaID = pk.IDPotkategorija
GO

ALTER VIEW p1 
AS
SELECT IDProizvod, Naziv AS NazivProizvoda, BrojProizvoda, Boja, MinimalnaKolicinaNaSkladistu, CijenaBezPDV, PotkategorijaID FROM Proizvod
GO

SELECT * FROM p1 

DROP VIEW p1
GO

/*
5. Vašeg šefa (koji voli Management Studio) zanimaju
podaci: ime i prezime komercijalista i koliko je ukupno
prodao proizvoda. Rezultate želi sortirane opadajuće
prema broju prodanih proizvoda.
a. Objasnite šefu kako da napiše upit 
b. Napravite pogled i objasnite šefu kako da ga koristi 
6. Obrišite pogled.
*/

-- Primjeri 5, 6.
CREATE VIEW p2
AS
SELECT
	k.Ime,
	k.Prezime,
	SUM(s.Kolicina) AS KolicinaProizvoda
FROM Komercijalist AS k
INNER JOIN Racun AS r on k.IDKomercijalist = r.KomercijalistID
INNER JOIN Stavka AS s ON r.IDRacun = s.RacunID
GROUP BY k.Ime, k.Prezime
GO

SELECT * FROM p2 ORDER BY KolicinaProizvoda DESC
GO

DROP VIEW p2
GO

/*
7. Gospođa iz prodaje treba pristup podacima o kreditnim
karticama, ali samo za kartice tipa Diners. Napravite
pogled. Pomoću pogleda dohvatite sve podatke o
karticama tipa Diners, a zatim o karticama tipa Visa.
8. Obrišite pogled.
*/
-- Primjeri 7, 8.
CREATE VIEW p3
AS
SELECT *
FROM KreditnaKartica AS kk
WHERE kk.Tip = 'Diners'
GO

SELECT * FROM p3
SELECT * FROM p3 WHERE Tip = 'Diners'
SELECT * FROM p3 WHERE Tip = 'Visa'
GO

DROP VIEW p3
GO


/*
9. Marketing treba podatke o svim proizvodima koji su
prodani u više od 2000 primjeraka. Napravite pogled koji
će to omogućiti.
10. Dodajte u pogled informaciju o broj prodanih proizvoda.
11. Uklonite pogled.
*/
-- Primjeri 9, 10, 11.
CREATE VIEW p4
AS
SELECT *
FROM Proizvod AS p
WHERE (SELECT SUM(Kolicina) FROM Stavka AS s WHERE s.ProizvodID = p.IDProizvod) > 2000
GO

SELECT * FROM p4
GO

ALTER VIEW p4
AS
SELECT 
	*,
	(SELECT SUM(Kolicina) FROM Stavka AS s WHERE s.ProizvodID = p.IDProizvod) AS Prodano
FROM Proizvod AS p
WHERE (SELECT SUM(Kolicina) FROM Stavka AS s WHERE s.ProizvodID = p.IDProizvod) > 2000
GO

SELECT * FROM p4
GO

DROP VIEW p4
GO

/*
12. Napravite pogled koji će dohvaćati sve iz tablice Kupac.
Možete li napraviti INSERT, UPDATE i DELETE nekog
kupca?
*/
CREATE VIEW p5
AS
SELECT * FROM Kupac
GO

SELECT * FROM p5

INSERT INTO p5 (Ime, Prezime, Email, Telefon, GradID) VALUES ('Miro', 'Miriæ', NULL, '042/111-222', 9)

UPDATE p5 SET Email = 'miro@miro.com' WHERE IDKupac = 19993

DELETE FROM p5 WHERE IDKupac = 19993
GO

/*
13. Promijenite prethodni pogled tako da dohvaća sve
stupce osim Prezime. Možete li napraviti INSERT,
UPDATE i DELETE nekog kupca? Obrišite pogled.
*/
ALTER VIEW p5
AS
SELECT IDKupac, Ime, Email, Telefon, GradID FROM Kupac
GO

INSERT INTO p5 (Ime, Email, Telefon, GradID) VALUES ('Janko', NULL, '042/222-333', 8)

UPDATE p5 SET Email = 'ana@ana.com' WHERE IDKupac = 19992

DELETE FROM p5 WHERE IDKupac = 19992
GO

DROP VIEW p5
GO

/*
14. Napravite pogled koji će dohvaćati ime i prezime kupca te
sve podatke o gradu.
a. Možete li napraviti INSERT grada kroz pogled. Vidi li se kroz
pogled?
b. Možete li napraviti UPDATE ili DELETE grada kroz pogled?
c. Obrišite pogled.
*/
CREATE VIEW p6
AS
SELECT 
	k.Ime,
	k.Prezime,
	g.*
FROM Kupac AS k
INNER JOIN Grad AS g ON k.GradID = g.IDGrad
GO

SELECT * FROM p6

INSERT INTO p6 (Naziv, DrzavaID) VALUES ('Velika Gorica', 1)

SELECT * FROM p6
SELECT * FROM Grad

UPDATE p6 SET Naziv = 'Velika Gorica PROMIJENJENO!' WHERE IDGrad = 69

DELETE FROM p6 WHERE IDGrad = 69
GO

DROP VIEW p6
GO

/*
15. Napravite pogled koji će dohvaćati sve kupce iz Sarajeva.
Pomoću tog pogleda umetnite kupca iz Zagreba. Vidi li se
kupac kroz pogled?
16. Promijenite pogled tako da ne dopušta umetanje/izmjenu
redaka koji neće biti vidljivi kroz njega. Probajte umetnuti
novog kupca.
17. Obrišite pogled.
*/
-- Primjeri 15, 16, 17.
CREATE VIEW p7
AS
SELECT * FROM Kupac WHERE GradID = 9
GO

SELECT * FROM p7

INSERT INTO p7 (Ime, Prezime, Email, Telefon, GradID) VALUES ('Lana', 'Laniæ', NULL, NULL, 1)

SELECT * FROM p7
GO

ALTER VIEW p7
AS
SELECT * FROM Kupac WHERE GradID = 9
WITH CHECK OPTION
GO

INSERT INTO p7 (Ime, Prezime, Email, Telefon, GradID) VALUES ('Vana', 'Vaniæ', NULL, NULL, 1)
GO

DROP VIEW p7
GO



/*
18. Napravite tablicu Osoba sa stupcima IDOsoba, Ime,
Prezime, OdjelID i Placa i umetnite nekoliko redaka.
a. Napravite pogled koji dohvaća sve iz tablice
b. Iskoristite pogled za dohvaćanje podataka
c. Uklonite stupac OdjelID iz tablice
d. Možete li iskoristiti pogled za dohvaćanje podataka?
e. Promijenite definiciju pogleda tako da bude čvrsto vezan uz objekte koje koristi
f. Uklonite stupac Placa iz tablice
g. Uklonite pogled
*/
CREATE TABLE Osoba
(
	IDOsoba int CONSTRAINT PK_Osoba PRIMARY KEY IDENTITY,
	Ime nvarchar(50),
	Prezime nvarchar(50),
	OdjelID int,
	Placa money 
)
GO
INSERT INTO Osoba (Ime, Prezime, OdjelID, Placa) VALUES ('Miro', 'Miriæ', 1, 5000)
INSERT INTO Osoba (Ime, Prezime, OdjelID, Placa) VALUES ('Ana', 'Aniæ', 1, 8500)
INSERT INTO Osoba (Ime, Prezime, OdjelID, Placa) VALUES ('Juro', 'Juriæ', 2, 3850)
GO

CREATE VIEW p8
AS
SELECT IDOsoba, Ime, Prezime, OdjelID, Placa FROM Osoba
GO

SELECT * FROM p8
GO

ALTER TABLE Osoba DROP COLUMN OdjelID
GO

SELECT * FROM p8
GO

ALTER VIEW p8 WITH SCHEMABINDING
AS
SELECT IDOsoba, Ime, Prezime, Placa FROM dbo.Osoba
GO

ALTER TABLE Osoba DROP COLUMN Placa
GO

DROP VIEW p8
GO


/*
19. Napravite pogled koji dohvaća ime i prezime kupca, te naziv države.
1. Pogledajte SELECT upit pogleda kroz sučelje i pomoću sistemske procedure sp_helptext
2. Zaštitite pogled
3. Pogledajte SELECT upit pogleda kroz sučelje i pomoću sistemske procedure sp_helptext
4. Promijenite pogled tako da bude zaštićen i čvrsto vezan uz tablice
5. Promijenite pogled tako da bude zaštićen, čvrsto vezan uz tablice i da ne 
   dopušta izmjene koje neće biti vidljive kroz pogled
6. Uklonite pogled
*/
CREATE VIEW p9
AS
SELECT
	k.Ime,
	k.Prezime,
	d.Naziv AS DrzavaNaziv
FROM Kupac AS k
INNER JOIN Grad AS g ON k.GradID = g.IDGrad
INNER JOIN Drzava AS d ON d.IDDrzava = g.DrzavaID
GO

SELECT * FROM p9
GO

EXECUTE sp_helptext p9
GO

ALTER VIEW p9 WITH ENCRYPTION
AS
SELECT
	k.Ime,
	k.Prezime,
	d.Naziv AS DrzavaNaziv
FROM Kupac AS k
INNER JOIN Grad AS g ON k.GradID = g.IDGrad
INNER JOIN Drzava AS d ON d.IDDrzava = g.DrzavaID
GO

SELECT * FROM p9
GO

EXECUTE sp_helptext p9
GO

ALTER VIEW p9 WITH ENCRYPTION, SCHEMABINDING
AS
SELECT
	k.Ime,
	k.Prezime,
	d.Naziv AS DrzavaNaziv
FROM dbo.Kupac AS k
INNER JOIN dbo.Grad AS g ON k.GradID = g.IDGrad
INNER JOIN dbo.Drzava AS d ON d.IDDrzava = g.DrzavaID
GO

ALTER VIEW p9 WITH ENCRYPTION, SCHEMABINDING
AS
SELECT
	k.Ime,
	k.Prezime,
	d.Naziv AS DrzavaNaziv
FROM dbo.Kupac AS k
INNER JOIN dbo.Grad AS g ON k.GradID = g.IDGrad
INNER JOIN dbo.Drzava AS d ON d.IDDrzava = g.DrzavaID
WITH CHECK OPTION
GO

SELECT * FROM p9
GO

DROP VIEW p9
GO


--------------------------------------------------------------------
--TODO----------------------PREDAVANJE4-----------------------------
--------------------------------------------------------------------

--------------------------------------------------------------------
--TODO----------------------PREDAVANJE5-----------------------------
--------------------------------------------------------------------

--------------------------------------------------------------------
--TODO----------------------PREDAVANJE6-----------------------------
--------------------------------------------------------------------




