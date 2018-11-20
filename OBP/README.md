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

Dohvatiti sve kupce iz tablice Kupac koji se zovu Ana ili
Tamara i iz Osijeka su.
```sql
SELECT * FROM Kupac as k
WHERE (k.Ime='Ana' or k.Ime='Tamara') and(
        SELECT g.Naziv FROM Grad as g
        WHERE g.IDGrad=k.GradID
      )='Osijek'
-- moze se i kracim putem napravit ako prvo nademo IDGrad od Osijeka
SELECT * FROM Grad
-- vidimo da Osijek ima GradID=2
-- pa skratimo cijeli unutarnji select
SELECT * FROM Kupac as k
WHERE (k.Ime='Ana' or k.Ime='Tamara') and k.GradID=2
```
