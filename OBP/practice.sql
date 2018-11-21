use AdventureWorksOBP

SELECT TOP 3 * FROM Kupac
GO

DECLARE @Ime nvarchar(50),@Prezime nvarchar(50)
SELECT @Ime = Ime, @Prezime = Prezime
FROM Kupac
WHERE IDKupac = 9821

PRINT @Ime
PRINT @Prezime
GO

PRINT @Ime -- Greska

select * from Grad

-- Dohvatiti sve kupce iz tablice Kupac koji se 
-- zovu Ana ili Tamara i iz Osijeka su.
SELECT * FROM Kupac as k
WHERE (k.Ime='Ana' or k.Ime='Tamara') and GradID=2

-- Umetnite proizvod "Sony Player" cijene 985,50 kuna. 
-- Potkategorija je "Playeri", kategorija "Razno". 
-- Podatke koji nisu zadani izmislite.
INSERT INTO Kategorija (Naziv) VALUES('Razno')
select k.IDKategorija from Kategorija as k
where k.Naziv='Razno'

INSERT INTO Potkategorija(KategorijaID,Naziv) VALUES (5,'Playeri')
select IDPotkategorija from Potkategorija
where Naziv='Playeri'
select * from Proizvod

INSERT INTO Proizvod(Naziv,BrojProizvoda,Boja,MinimalnaKolicinaNaSkladistu,CijenaBezPDV,PotkategorijaID) 
VALUES ('Sony Player','XY-1234','Crvena',20,980.50,38)
select * from Proizvod
where Naziv='Sony Player'

--Napravite tablicu KupacVIP sa stupcima ime i prezime.
--Umetnite u nju sve kupce koji se zove Karen, Mary ili Jimmy.
CREATE TABLE KupacVIP
(
	IDKupacVIP int CONSTRAINT PK_KupacVIP PRIMARY KEY IDENTITY,
	Ime nvarchar(50) NOT NULL,
	Prezime nvarchar(50) NOT NULL
)
INSERT INTO KupacVIP(Ime,Prezime)
SELECT Ime,Prezime FROM Kupac
WHERE Ime IN ('Karen','Mary','Jimmy')
select * from KupacVIP


