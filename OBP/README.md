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

#### Pogledi

* Pogledi (engl. views) su **objekti** u bazi podataka
	* Često se nazivaju i virtualne tablice
* **Ne sadrže podatke, već samo SQL upite**
* Svaki pogled sadrži točno jedan SELECT upit (koji može sadržavati i podupite, spajanja, grupiranja, ...)
	* Dodaju razinu apstrakcije nad tablice
	* Koriste se (skoro) na isti način kao i tablice, primjerice:
```sql
SELECT * FROM neki_pogled
```
* Razlog za postojanje pogleda su smanjenje kompleksnosti dijelova baze prema korisnicima
* Zastita podataka
* Implementacija sucelja prema korisnicima baze

```
Primjeri od 1-11 u practice.sql
```


##### Izmjena podataka kroz poglede

Podatke je moguce mijenjati samo ako se radi o tablici uz sljedece uvijete:
* Sve promjene moraju referencirati stupce iz jedne tablice
* Referencirani stupci ne smiju biti rezultat podupita, agregatnih funkcija niti kombinacija drugih stupaca
* Referencirani stupci ne smiju biti dijelom GROUP BY, HAVING niti DISTINCT ključnih riječi
* Pogled sadržava sve neophodne stupce
```
Primjeri od 12-17 u practice.sql
```

##### Dodatne opcije pogleda

* Izradom pogleda se ne uspostavljaju čvrste veze na tablice koje pogled koristi
	* Promjena strukture tablica može pogled učiniti neispravnim
* Moguće zahtijevati od [RDBMS](https://en.wikipedia.org/wiki/Relational_database_management_system)-a da napravi čvrste veze 
	* Korištenjem opcije WITH SCHEMABINDING
```sql
CREATE VIEW naziv_pogleda
WITH SCHEMABINDING
AS
SELECT_naredba
```

###### Zastita pogleda
* Sadržaj pogleda je moguće vidjeti kroz GUI ili pomoću sistemske procedure **sp_helptext**
	* `EXECUTE sp_helptext naziv_pogleda`
* Moguće je sadržaj pogleda zaštititi:
	* Korištenjem opcije `WITH ENCRYPTION`
```sql
CREATE VIEW naziv_pogleda
WITH ENCRYPTION
AS
SELECT_naredba
```
	* U slučaju korištenja zaštite potrebno čuvati originalnu 
	  definiciju pogleda (najbolje u SQL skripti)
	* Nije prava e

```
Primjeri 18,19.. u practice.sql
```