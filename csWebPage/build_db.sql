use [master]
go

create database [RWA]
go

use RWA
go

create table [Spol]
(
	IDSpol int constraint PK_Spol primary key identity,
	Spol nvarchar(10) not null
)
go

create table [Aktivnost]
(
	IDAktivnost int constraint PK_Aktivnost primary key identity,
	Razina nvarchar(20) not null
)
go

create table [Korisnik]
(
	IDKorisnik int constraint PK_Korisnik primary key identity,
	Ime nvarchar(50) not null,
	Prezime nvarchar(50) not null,
	DatumRodenja nvarchar(50) not null,
	Visina int not null,
	Tezina int not null,
	AktivnostID int constraint PK_Korisnik_Aktivnost foreign key references [dbo].[Aktivnost](IDAktivnost),
	TipDia int not null,
	Email nvarchar(50) not null unique,
	SpolID int constraint PK_Korisnik_Spol FOREIGN KEY REFERENCES [dbo].[Spol](IDSpol),
	KorisnickoIme nvarchar(30) not null unique,
	Lozinka nvarchar(max) not null,
	Ulogiran nvarchar(10)
)
go

create table [Admin]
(
	IDAdmin int constraint PK_Admin primary key identity,
	KorisnickoIme nvarchar(50) not null unique,
	Lozinka nvarchar(max) not null,
	Ulogiran nvarchar(10)
)
go

create table [Meni]
(
	IDMeni int constraint PK_Meni primary key identity,
	DatumKreiranja Date not null,
	BrojObroka int not null,
	KorisnikID int constraint PK_Meni_Korisnik foreign key references [dbo].[Korisnik](IDKorisnik)
)
go

create table [TipNamirnice]
(
	IDTipNamirnice int constraint PK_TipNamirnice primary key identity,
	Tip nvarchar(50) not null unique
)
go

create table [Jedinica]
(
	IDJedinica int constraint PK_Jedinica primary key identity,
	Jedinica nvarchar(50) not null unique
)
go

create table [Namirnica]
(
	IDNamirnica int constraint PK_Namirnica primary key identity,
	Naziv nvarchar(128) not null unique,
	Energija_kcal nvarchar(10) not null,
	Energija_kJ nvarchar(10) not null,
	TipNamirniceID int constraint PK_Namirnica_TipNamirnice foreign key references [dbo].[TipNamirnice](IDTipNamirnice),
	JedinicaID int constraint PK_Namirnica_Jedinica foreign key references [dbo].[Jedinica](IDJedinica),
	Kolicina int not null
)
go

create table [NazivObroka]
(
	IDNazivObroka int constraint PK_NazivObroka primary key identity,
	Ime nvarchar(50) not null unique
)
go

create table [Obrok]
(
	IDObrok int constraint PK_Obrok primary key identity,
	MeniID int constraint PK_Obrok_Meni foreign key references [dbo].[Meni](IDMeni),
	NazivObrokaID int constraint PK_Obrok_NazivObroka foreign key references [dbo].[NazivObroka](IDNazivObroka),
	NamirnicaID int constraint PK_Obrok_Namirnica foreign key references [dbo].[Namirnica](IDNamirnica)
)
go

create table [Kombinacija]
(
	IDKombinacija int constraint PK_Kombinacija primary key identity,
	BrojObroka int not null,
	DatumKreiranja date not null,
	VrijediDo date
)
go

create table [KombinacijaDetalji]
(
	IDKombinacijaDetalji int constraint PK_KombinacijaDetalji primary key identity,
	NazivObrokaID int constraint PK_KombinacijaDetalji_NazivObroka foreign key references [dbo].[NazivObroka](IDNazivObroka),
	PostotakUgljikohidrata int not null,
	PostotakMasti int not null,
	PostotakProteina int not null,
	PostotakUkupno int not null,
	KombinacijaID int constraint PK_KombinacijaDetalji_Kombinacija foreign key references [dbo].[Kombinacija](IDKombinacija)
)
go

create proc [dbo].[Dohvati_Namirnice_Koristene_U_Meniju]
	@MeniID int
as
	select n.Naziv from Obrok as o
	inner join Namirnica as n on n.IDNamirnica=o.NamirnicaID
	where (select IDMeni from Meni)=@MeniID
go

create proc [dbo].[Dohvati_Sve_Menije_Od_Korisnika]
	@KorisnikID int
as
	select m.IDMeni,m.DatumKreiranja,[no].Ime as 'Obrok', n.Naziv as 'Namirnica' from Meni as m 
	inner join Obrok as o on o.MeniID = m.IDMeni
	inner join NazivObroka as [no] on [no].IDNazivObroka=o.NazivObrokaID
	inner join Namirnica as n on o.NamirnicaID = n.IDNamirnica
	where m.KorisnikID = @KorisnikID
go

select * from Meni
select * from Obrok

create proc [dbo].[Dohvati_Sve_Jedinice]
as
	select * from [Jedinica]
go

create proc [dbo].[Dohvati_Sve_Korisnike]
as
	select k.IDKorisnik,k.Ime,k.Prezime,k.DatumRodenja,k.Visina,k.Tezina,a.Razina as 'Razina aktivnosti',k.Email,sp.Spol as 'Spol',k.KorisnickoIme,k.Lozinka from Korisnik as k 
	inner join Aktivnost as a on a.IDAktivnost = k.AktivnostID
	inner join Spol as sp on sp.IDSpol=k.SpolID
go

create proc [dbo].[Dohvati_Sve_Obroke]
as
	select IDObrok,MeniID,Ime, Naziv from Obrok
	inner join NazivObroka on IDNazivObroka=NazivObrokaID
	inner join Namirnica on NamirnicaID=IDNamirnica
go

create proc [dbo].[Dohvati_Sve_Nazive_Obroka]
as
	select * from NazivObroka
go

create proc [dbo].[Dohvati_Sve_Namirnice]
as
	select n.IDNamirnica, n.Naziv,n.Energija_kcal, n.Energija_kJ,t.Tip, j.Jedinica ,n.Kolicina from Namirnica as n
    inner join TipNamirnice as t on t.IDTipNamirnice = n.TipNamirniceID
    inner join Jedinica as j on j.IDJedinica = n.JedinicaID
go

create proc [dbo].[Dohvati_Sve_Kombinacije]
as
	select * from Kombinacija
go

create proc [dbo].[Dohvati_Sve_KombinacijeDetalji]
as
	select * from KombinacijaDetalji
go

-- admin procedure
create proc [dbo].[Prijavi_Admina]
	@KorisnickoIme nvarchar(50)
as
	if exists(select IDAdmin from [Admin] where KorisnickoIme = @KorisnickoIme) begin
		update [dbo].[Admin]
		set
			Ulogiran = 'd'
		where KorisnickoIme = @KorisnickoIme
	end
go

create proc [dbo].[Odjavi_Admina]
	@KorisnickoIme nvarchar(50)
as
	if exists(select IDAdmin from [Admin] where KorisnickoIme = @KorisnickoIme) begin
		update [dbo].[Admin]
		set
			Ulogiran = 'n'
		where KorisnickoIme = @KorisnickoIme
	end
go

create proc [dbo].[Provjeri_Admina]
	@KorisnickoIme nvarchar(50),
	@Lozinka nvarchar(50)
as
	if exists(select IDAdmin from [Admin] where KorisnickoIme = @KorisnickoIme and Lozinka = @Lozinka) begin
		select 1
	end
	else begin
		select 'Krivo korisnicko ime ili lozinka'		
	end
go

create proc [dbo].[Dohvati_Admina]
	@KorisnickoIme nvarchar(50)
as
	if exists(select IDAdmin from [Admin] where KorisnickoIme = @KorisnickoIme) begin
		select * from [dbo].[Admin] where KorisnickoIme = @KorisnickoIme
	end
go

create proc [dbo].[Provjeri_Korisnika]
	@KorisnickoIme nvarchar(50),
	@Lozinka nvarchar(50)
as
	if exists(select IDKorisnik from [Korisnik] where KorisnickoIme = @KorisnickoIme and Lozinka = @Lozinka) begin
		select 1
	end
	else begin
		select 'Krivo korisnicko ime ili lozinka'		
	end
go	

create proc [dbo].[Dohvati_Korisnik_ID]
	@KorisnickoIme nvarchar(50)
as
	if exists(select IDKorisnik from [Korisnik] where KorisnickoIme = @KorisnickoIme) begin
		select IDKorisnik from [Korisnik] where KorisnickoIme = @KorisnickoIme
	end
	else begin
		select -1
	end
go
	

create proc [dbo].[Prijavi_Korisnika]
	@KorisnickoIme nvarchar(50)
as
	if exists(select IDKorisnik from [Korisnik] where KorisnickoIme = @KorisnickoIme) begin
		update [dbo].[Korisnik]
		set
			Ulogiran = 'd'
		where KorisnickoIme = @KorisnickoIme
	end
go

create proc [dbo].[Odjavi_Korisnika]
	@KorisnickoIme nvarchar(50)
as
	if exists(select IDKorisnik from [Korisnik] where KorisnickoIme = @KorisnickoIme) begin
		update [dbo].[Korisnik]
		set
			Ulogiran = 'n'
		where KorisnickoIme = @KorisnickoIme
	end
go

-- Korisnik CRUD
create proc [dbo].[Dohvati_Korisnika]
	@KorisnikID int
as
	if exists(select IDKorisnik from [Korisnik] where IDKorisnik = @KorisnikID) begin
		select k.IDKorisnik, k.Ime,k.Prezime,k.DatumRodenja,k.Visina,k.Tezina,a.Razina,k.Email,sp.Spol,k.KorisnickoIme,k.Lozinka,k.Ulogiran from Korisnik as k 
		inner join Aktivnost as a on a.IDAktivnost = k.AktivnostID
		inner join Spol as sp on sp.IDSpol=k.SpolID
		where k.IDKorisnik = @KorisnikID
	end
	else begin 
		select -1
	end
go

create proc [dbo].[Dodaj_Korisnika]
	@Korisnicko_ime nvarchar(50),
	@Lozinka nvarchar(max),

	@Email nvarchar(50),
	@Ime nvarchar(50),
	@Prezime nvarchar(50),
	@DatumRodenja date,
	@Visina int,
	@Tezina int,
	@IDAktivnost int,
	@TipDia int,
	@IDSpol int
as
	if exists(select IDKorisnik from Korisnik where [KorisnickoIme] = @Korisnicko_ime) begin
		select -1
	end
	else if exists(select IDKorisnik from Korisnik where [Email] = @Email) begin
		select -2
	end
	else begin
		insert into [dbo].[Korisnik] values(@Ime,@Prezime,@DatumRodenja,@Visina,@Tezina,@IDAktivnost,@TipDia,@Email,@IDSpol,@Korisnicko_ime,@Lozinka,'n')
		select 1
	end
go

create proc [dbo].[Obrisi_Korisnika]
	@IDKorisnik int
as
	if exists(select IDKorisnik from Korisnik where IDKorisnik = @IDKorisnik) begin
		delete from Korisnik where IDKorisnik = @IDKorisnik
		select 'Korisnik uspjesno obrisan'
	end
	else begin
		select 'Korisnik sa tim ID ne postoji'
	end
go

create proc [dbo].[Izmjeni_Korisnika]
	@Korisnicko_ime nvarchar(50),
	@NovoKorisnicko_ime nvarchar(50),
	@NovoLozinka nvarchar(max),
	@NovoEmail nvarchar(50),
	@NovoIme nvarchar(50),
	@NovoPrezime nvarchar(50),
	@NovoDatumRodenja date,
	@NovoVisina int,
	@NovoTezina int,
	@NovoIDAktivnost int,
	@NovoTipDia int,
	@NovoIDSpol int
as
	if exists(select IDKorisnik from Korisnik where [KorisnickoIme] = @Korisnicko_ime) begin
		update [dbo].[Korisnik]
		set
			Ime = @NovoIme,
			Prezime = @NovoPrezime,
			DatumRodenja = @NovoDatumRodenja,
			Visina = @NovoVisina,
			Tezina = @NovoTezina,
			AktivnostID = @NovoIDAktivnost,
			TipDia = @NovoTipDia,
			Email = @NovoEmail,
			SpolID = @NovoIDSpol,
			KorisnickoIme = @NovoKorisnicko_ime,
			Lozinka = @NovoLozinka
		where KorisnickoIme = @Korisnicko_ime
		select 'Korisnik uspjesno izmjenjen'
	end
	else begin
		select 'Korisnik '  + @Korisnicko_ime + ' ne postoji' 
	end
go

-- Jedinica CRUD
create proc [dbo].[Dohvati_Jedinicu]
	@ImeJedinice nvarchar(50)
as 
	if exists(select IDJedinica from Jedinica where Jedinica = @ImeJedinice) begin
		select * from Jedinica where Jedinica = @ImeJedinice
	end
	else begin
		select -1
	end
go

create proc [dbo].[Dodaj_Jedinicu]
	@ImeJedinice nvarchar(50)
as
	if exists(select IDJedinica from Jedinica where Jedinica = @ImeJedinice) begin
		select 'Jedinica vec postoji'
	end
	else begin
		insert into Jedinica values(@ImeJedinice)
		select 'Jedinica uspjesno dodana'
	end
go

create proc [dbo].[Obrisi_Jedinicu]
	@JedinicaID int
as
	if exists(select IDJedinica from Jedinica where IDJedinica = @JedinicaID) begin
		delete from Jedinica where IDJedinica = @JedinicaID
		select 'Jedinica uspjesno obrisana'
	end
	else begin
		select -1
	end
go

create proc [dbo].[Izmjeni_Jedinicu]
	@JedinicaID int,
	@NovoImeJedinice nvarchar(50)
as
	if exists(select IDJedinica from Jedinica where Jedinica = @NovoImeJedinice) begin
		select 'Jedinica sa tim imenom vec postoji'
	end
	else if exists(select IDJedinica from Jedinica where IDJedinica = @JedinicaID) begin
		update [dbo].[Jedinica]
		set
			Jedinica = @NovoImeJedinice
		where IDJedinica = @JedinicaID
	end
	else begin
		select -1
	end
go

-- TipNamirnice CRUD
create proc [dbo].[Dohvati_TipNamirnice]
	@TipNamirnice nvarchar(50)
as 
	if exists(select IDTipNamirnice from TipNamirnice where Tip = @TipNamirnice) begin
		select * from TipNamirnice where Tip = @TipNamirnice
	end
	else begin
		select 'Tip namirnice ' + @TipNamirnice + ' ne postoji' 
	end
go

create proc [dbo].[Dodaj_TipNamirnice]
	@TipNamirnice nvarchar(50)
as
	if exists(select IDTipNamirnice from TipNamirnice where Tip = @TipNamirnice) begin
		select 'Tip namirnice vec postoji'
	end
	else begin
		insert into Jedinica values(@TipNamirnice)
		select 'Tip namirnice uspjesno dodan'
	end
go

create proc [dbo].[Obrisi_TipNamirnice]
	@TipNamirnice nvarchar(50)
as
	if exists(select IDTipNamirnice from TipNamirnice where Tip = @TipNamirnice) begin
		delete from TipNamirnice where Tip = @TipNamirnice
		select 'Tip namirnice uspjesno obrisan'
	end
	else begin
		select 'Tip namirnice ' + @TipNamirnice + ' ne postoji' 
	end
go

create proc [dbo].[Izmjeni_TipNamirnice]
	@TipNamirnice nvarchar(50),
	@NovoTipNamirnice nvarchar(50)
as
		if exists(select IDTipNamirnice from TipNamirnice where tip = @TipNamirnice) begin
		update [dbo].[TipNamirnice]
		set
			Tip = @NovoTipNamirnice
		where Tip = @TipNamirnice
		select SCOPE_IDENTITY()
	end
	else begin
		select 'Tip namirnice '  + @TipNamirnice + ' ne postoji' 
	end
go

-- Namirnica CRUD
create proc [dbo].[Dohvati_Namirnicu]
	@Naziv nvarchar(50)
as
	if exists(select IDNamirnica from Namirnica where Naziv = @Naziv) begin
		 select n.IDNamirnica, n.Naziv,n.Energija_kcal, n.Energija_kJ,t.Tip, j.Jedinica ,n.Kolicina from Namirnica as n
         inner join TipNamirnice as t on t.IDTipNamirnice = n.TipNamirniceID
         inner join Jedinica as j on j.IDJedinica = n.JedinicaID
		 where n.Naziv = @Naziv
	end
	else begin
		select @Naziv + ' ne postoji u tablici Namirnica'
	end
go

create proc [dbo].[Dodaj_Namirnicu]
	@Naziv nvarchar(50),
	@Energija_kcal int,
	@Energija_kJ int,
	@TipNamirniceID int,
	@JedinicaID int,
	@Kolicina int
as
	if not exists(select IDNamirnica from Namirnica where Naziv = @Naziv) begin
		insert into Namirnica values(@Naziv,@Energija_kcal,@Energija_kJ,@TipNamirniceID,@JedinicaID,@Kolicina)
		select 'Namirnica uspjesno dodana'
	end
	else begin
		select @Naziv + ' ne postoji u tablici Namirnica'
	end
go

create proc [dbo].[Obrisi_Namirnicu]
	@IDNamirnica nvarchar(50)
as
	if exists(select IDNamirnica from Namirnica where IDNamirnica = @IDNamirnica) begin
		delete from Namirnica where IDNamirnica = @IDNamirnica
		select 'Namirnica je uspjesno obrisana'
	end
	else begin
		select -1
	end
go

create proc [dbo].[Izmjeni_Namirnicu]
	@IDNamirnica int,
	@NovoNaziv nvarchar(50),
	@NovoEnergija_kcal int,
	@NovoEnergija_kJ int,
	@NovoTipNamirniceID int,
	@NovoJedinicaID int,
	@NovoKolicina int
as
if exists(select IDNamirnica from Namirnica where IDNamirnica = @IDNamirnica) begin
		update Namirnica 
		set
			Naziv = @NovoNaziv,
			Energija_kcal = @NovoEnergija_kcal,
			Energija_kJ = @NovoEnergija_kJ,
			TipNamirniceID = @NovoTipNamirniceID,
			JedinicaID = @NovoJedinicaID,
			Kolicina = @NovoKolicina
		where IDNamirnica = @IDNamirnica
		select 'Namirnica je uspjesno promjenjena'
	end
	else begin
		select -1
	end
go

-- NazivObroka CRUD
create proc [dbo].[Dohvati_NazivObroka]
	@NazivObrokaID nvarchar(50)
as 
	if exists(select IDNazivObroka from NazivObroka where IDNazivObroka = @NazivObrokaID) begin
		select * from NazivObroka where IDNazivObroka = @NazivObrokaID
	end
	else begin
		select -1
	end
go

create proc [dbo].[Dodaj_NazivObroka]
	@NazivObroka nvarchar(50)
as
	if exists(select IDNazivObroka from NazivObroka where Ime = @NazivObroka) begin
		select 'Naziv obroka vec postoji'
	end
	else begin
		insert into NazivObroka values(@NazivObroka)
		select 'Naziv obroka uspjesno dodana'
	end
go

create proc [dbo].[Obrisi_NazivObroka]
	@NazivObroka nvarchar(50)
as
	if exists(select IDNazivObroka from NazivObroka where Ime = @NazivObroka) begin
		delete from NazivObroka where Ime = @NazivObroka
		select 'Naziv obroka uspjesno obrisana'
	end
	else begin
		select 'Naziv obroka ' + @NazivObroka + ' ne postoji' 
	end
go

create proc [dbo].[Izmjeni_NazivObroka]
	@NazivObroka nvarchar(50),
	@NovoNazivObroka nvarchar(50)
as
		if exists(select IDNazivObroka from NazivObroka where Ime = @NazivObroka) begin
		update [dbo].[NazivObroka]
		set
			Ime = @NovoNazivObroka
		where Ime = @NazivObroka
	end
	else begin
		select 'Naziv obroka '  + @NazivObroka + ' ne postoji' 
	end
go

-- Kombinacija CRUD
create proc [dbo].[Dohvati_Kombinaciju]
	@KombinacijaID nvarchar(50)
as 
	if exists(select IDKombinacija from Kombinacija where IDKombinacija = @KombinacijaID) begin
		select * from Kombinacija where IDKombinacija = @KombinacijaID
	end
	else begin
		select -1
	end
go

create proc [dbo].[Dodaj_Kombinaciju]
	@BrojObroka int,
	@DatumKreiranja date,
	@VrijediDo date
as
	insert into Kombinacija values(@BrojObroka,@DatumKreiranja,@VrijediDo)
	select SCOPE_IDENTITY()
go


create proc [dbo].[Obrisi_Kombinaciju]
	@KombinacijaID int
as
	if exists(select IDKombinacija from Kombinacija where IDKombinacija = @KombinacijaID) begin
		delete from KombinacijaDetalji where KombinacijaID = @KombinacijaID
		delete from Kombinacija where IDKombinacija = @KombinacijaID
		select 'Kombinacija uspjesno obrisana'
	end
	else begin
		select -1
	end
go

create proc [dbo].[Izmjeni_Kombinaciju]
	@KombinacijaID int,
	@BrojObroka int,
	@DatumKreiranja date,
	@VrijediDo date
as
	if exists(select IDKombinacija from Kombinacija where IDKombinacija = @KombinacijaID) begin
		update Kombinacija
		set
			BrojObroka = @BrojObroka,
			DatumKreiranja = @DatumKreiranja,
			VrijediDo = @VrijediDo
		where IDKombinacija = @KombinacijaID
	end
	else begin
		select -1
	end
go

-- KombinacijaDetalji CRUD
create proc [dbo].[Dohvati_KombinacijaDetalji]
	@KombinacijaDetaljiID int
as
	if exists(select IDKombinacijaDetalji from KombinacijaDetalji where @KombinacijaDetaljiID = IDKombinacijaDetalji) begin
		select * from KombinacijaDetalji where @KombinacijaDetaljiID = IDKombinacijaDetalji
	end
	else begin
		select -1
	end
go

create proc [dbo].[Dodaj_KombinacijaDetalji]
	@NazivObrokaID int,
	@Uglj int,
	@Mast int,
	@Prot int,
	@Ukup int,
	@KombinacijaID int
as
	insert into KombinacijaDetalji values (@NazivObrokaID,@Uglj,@Mast,@Prot,@Ukup,@KombinacijaID)
	select SCOPE_IDENTITY()
go

create proc [dbo].[Izmjeni_KombinacijaDetalji]
	@KombinacijaDetaljiID int,
	@NazivObrokaID int,
	@Uglj int,
	@Mast int,
	@Prot int,
	@Ukup int,
	@KombinacijaID int
as
	if exists(select IDKombinacijaDetalji from KombinacijaDetalji where @KombinacijaDetaljiID = IDKombinacijaDetalji) begin
		update KombinacijaDetalji
		set 
			NazivObrokaID = @NazivObrokaID,
			PostotakUgljikohidrata = @Uglj,
			PostotakMasti = @Mast,
			PostotakProteina = @Prot,
			PostotakUkupno = @Ukup,
			KombinacijaID = @KombinacijaID
		where IDKombinacijaDetalji = @KombinacijaDetaljiID
	end
	else begin
		select -1
	end
go

create proc [dbo].[Obrisi_KombinacijaDetalji]
	@KombinacijaDetaljiID int
as
	if exists(select IDKombinacijaDetalji from KombinacijaDetalji where @KombinacijaDetaljiID = IDKombinacijaDetalji) begin
		delete from KombinacijaDetalji where IDKombinacijaDetalji = @KombinacijaDetaljiID
	end
	else begin
		select -1
	end
go

-- dummy data
insert into [dbo].[Spol] 
values ('Muskarac'),('Zena')
go

insert into [dbo].[Aktivnost]
values ('Nikakva'),('Umjerena'),('Intenzivna')
go

insert into [dbo].[Korisnik]
values ('TestIme','TestPrezime','1.1.1999',150,50,1,1,'test@testmail.com',1,'test','UjyVAQyYuP7b0BuvvPvdivLx1cb+imw1nq5+5q97rK4e65Sn','n')
go

insert into [dbo].[Admin] 
values ('admin','TgLXfRGBx4LgnJtgB4Umu0yZ/G/n1hcI8u+2PJHE/WmYPQkx','n')
go

insert into [NazivObroka] 
values ('Zajutrak'),('Dorucak'),('Rucak'),('Vecera'),('Marenda')
go

insert into [TipNamirnice]
values ('Ugljikohidrati'),('Bjelancevine'),('Masti')
go


insert into [Jedinica]
values ('g'),('kom'),('salica'),('zlica')
go

insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Mlijeko',167,40,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Jogurt',360,40,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kiselo vrhnje',800,192,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Puding',560,134,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Sirni namaz',480,115,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Topljeni sir',1275,385,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Tvrdi sir',1555,372,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Svježi kravlji sir',101,72,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Zrnati sir',396,92,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Janjetina',875,211,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Jetra',575,137,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kunić',550,132,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Mljeveno, miješano meso',1060,253,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Piletina',600,144,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Puretina',970,231,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Slanina',2530,605,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Srnetina',515,123,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Svinjetina',1445,345,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Šunka dimljena i pršut',1653,385,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Šunka (kuhana)',1145,274,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Šunka pureća/pileća',525,128,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Teletina',390,105,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Bakalar',295,76,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Dagnja',270,66,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Grgeč',295,75,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Haringa',650,155,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Inćun',310,89,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Jastog',305,86,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Lignja',295,77,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Losos',910,217,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Pastrva',470,112,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Sardine',1005,240,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Skuša',820,195,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Šaran',270,65,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Škampi',310,91,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Štuka',305,85,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Tuna',1270,303,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Crni kruh',1046,250,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Dvopek',1590,397,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Griz',1550,370,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kolači od samog tijesta',1315,314,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kokice',1580,376,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kruh sa cijelim zrnima',1004,240,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kukuruzni kruh',915,220,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kukuruzne pahuljice',1625,388,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Musli',1550,371,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Polubijeli kruh',1055,252,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Riža ljuštena',1540,368,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Riža neljuštena',1550,371,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Soja u zrnu',1785,427,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Sojin sir (tofu)',285,72,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Tjestenina sa jajima',1630,390,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Zobene pahuljice',1680,402,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Čips od krumpira',2375,568,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Krumpir',355,85,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Njoki',490,117,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Pomfrit',1130,270,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Ananas',230,56,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Banane',410,99,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Borovnice',260,62,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Breskve',192,46,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Dinje',100,24,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Grožđe',295,70,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Grejp',180,42,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Jabuka',218,52,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Jagode',150,36,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kivi',230,55,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kruške',230,55,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Lubenica',100,24,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Maline',170,40,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Mandarine',200,48,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Marelice',230,54,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Naranče',226,54,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Ribizl (crveni)',190,45,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Ribizl (crni)',260,63,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Šljive',245,58,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Trešnje',240,57,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Artičoke',90,23,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Brokula',140,33,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Cikla',150,37,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Cvjetača',117,28,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Celer',159,38,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Grah',480,110,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Grašak',389,93,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kelj',190,46,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Krastavci',42,10,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kupus (kiseli)',109,26,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Kupus (slatki)',218,52,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Luk',175,42,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Mrkva',146,35,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Paprika',117,28,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Patlidžan',110,26,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Poriluk',160,38,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Rajčica',80,19,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Šampinjoni',101,24,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Šparoga',80,20,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Špinat',96,23,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Zelena salata',59,14,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Zelje',100,25,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Maslac',3190,755,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Margarin',3040,720,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Majoneza',3200,761,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Majoneza light',1440,341,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Svinjska mast',3800,900,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Biljna mast',3150,753,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Tartar umak',1975,480,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Ulje maslinovo',3800,900,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Ulje repino',3800,900,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Ulje od suncokreta',3885,928,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Ulje od kukuruznih klica',3891,930,3,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Cijelo jaje',700,167,2,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Biskvit masni',1945,462,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Bomboni tvrdi obični',1630,390,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Bomboni voćni',1220,292,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Čokolada mliječna',2355,563,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Čokolada za kuhanje',2355,564,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Čokoladni bomboni',1985,490,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Čokoladni namaz - nutella',2220,534,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Guma za žvakanje',1170,280,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Gumeni bomboni',1450,345,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Keks sa čokoladnim preljevom',2200,530,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Marmelada',1090,261,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Med',1275,303,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Napolitanke',2305,550,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Piškoti',1635,393,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Plazma keks',1810,440,1,1,100);
insert into [Namirnica]([Naziv],[Energija_kJ],[Energija_kcal],[TipNamirniceID],[JedinicaID],[Kolicina]) values ('Puding u prahu',1600,380,1,1,100);
go

insert into Meni values('1/2/2010',3,1)
insert into Obrok values(1,1,4)
insert into Obrok values(1,3,3)
insert into Obrok values(1,5,10)
insert into Kombinacija values(3,'1/2/2005','6/5/2025')
go

insert into KombinacijaDetalji values(1,25,25,50,25,1)
insert into KombinacijaDetalji values(2,25,25,50,50,1)
insert into KombinacijaDetalji values(3,25,25,50,25,1)
go

insert into Kombinacija values(4,'1/2/2005','4/5/2025')
go

insert into KombinacijaDetalji values(1,25,25,50,25,2)
insert into KombinacijaDetalji values(2,25,25,50,25,2)
insert into KombinacijaDetalji values(3,25,25,50,25,2)
insert into KombinacijaDetalji values(3,25,25,50,25,2)
go
