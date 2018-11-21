# Notes for OBP

Primjer Skripte sa skupinama naredbi
```sql
SELECT TOP 3 * FROM Kupac
GO

DECLARE @Ime nvarchar(50)  --ako bi vise htio vise varijabli samo stavi , i 
SELECT @Ime = Ime
FROM Kupac
WHERE IDKupac = 9821

PRINT @Ime
GO

PRINT @Ime -- Greska jer nema varijable @Ime (varijeble zive samo do GO)
```
Drugi primjer
```sql
SELECT TOP 3 * FROM Kupac
SELECT TOP 3 * FROM Racun
GO
SELECT TOP 3 * FROM Kupac
SELECT TOP 3 Nepostojece FROM Kupac
GO
DELETE FROM Racun
SELECT TOP 3 * FROM Racun
-- Prva skupina je prosla ok
-- Druga skupina je selectala top 3 kupca ali je error zbog drugog krivog selecta
-- Treca skupina nije obrisala racun ali je selectala top 3 iz racuna
-- ispod je link zasto nije obrisala
-- https://www.codeproject.com/Answers/677285/I-am-getting-error-while-delete-entry#answer3
```

```sql
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
```