use [master]
go

create database [PPPK_DATABASE]
go

use PPPK_DATABASE
go

-----------------------------------------------------------------
--TABLES
-----------------------------------------------------------------
create table [tip_vozila]
(
    [id]  int constraint PK_tip_vozila primary key identity,
    [tip] nvarchar(32) not null
)
create table [mjesto]
(
    [id]    int constraint PK_mjesto primary key identity,
    [naziv] nvarchar(128) not null
)
create table [status]
(
    [id]     int constraint PK_status primary key identity,
    [status] nvarchar(32) not null
)
go

create table [vozac]
(
    [id]            int constraint PK_vozac primary key identity,
    [ime]           nvarchar(128) not null,
    [prezime]       nvarchar(128) not null,
    [broj_mobitela] nvarchar(32) not null,
    [broj_vozacke]  nvarchar(16) not null
)
create table [vozilo]
(
    [id]                 int constraint PK_vozilo primary key identity,
    [tip_vozila_id]      int constraint FK__tip_vozila__vozilo foreign key references [dbo].[tip_vozila](id),
    [marka]              nvarchar(128) not null,
    [godina_proizvodnje] int not null,
    [pocetni_km]         decimal(10,1) not null,
    [trenutni_km]        decimal(10,1) not null
)
go

create table [putni_nalog]
(
    [id]           int constraint PK_putni_nalog primary key identity,
    [vozac_id]     int constraint FK__vozac__putni_nalog foreign key references [dbo].[vozac](id),
    [vozilo_id]    int constraint FK__vozilo__putni_nalog foreign key references [dbo].[vozilo](id),
    [status_id]    int constraint FK__status__putni_nalog foreign key references [dbo].[status](id),
    [datum_izrade] date not null
)
create table [zauzece_vozilo]
(
    id	        int constraint PK_zauzece_vozilo primary key identity,
    [vozilo_id] int constraint FK__vozilo__zauzece_vozilo foreign key references [dbo].[vozilo](id),
    [datum]     date not null
)
create table [zauzece_vozac]
(
    id	        int constraint PK_zauzece_vozac primary key identity,
    [vozilo_id] int constraint FK__vozilo__zauzece_vozac foreign key references [dbo].[vozilo](id),
    [datum]     date not null
)
create table [servis]
(
    id	            int constraint PK_servis primary key identity,
    [vozilo_id]	    int constraint FK__vozilo__servis foreign key references [dbo].[vozilo](id),
    [datum_servisa] date not null,
    [naziv_servisa] nvarchar(128),
    [cijena]        decimal(10,2) not null,
    [info]          nvarchar(512) not null
)
go

create table [kupnja_goriva]
(
    id	             int constraint PK_kupnja_goriva primary key identity,
    [putni_nalog_id] int constraint FK__putni_nalog__kupnja_goriva foreign key references [dbo].[putni_nalog](id),
    [mjesto_id]      int constraint FK__mjesto__kupnja_goriva foreign key references [dbo].[mjesto](id),
    [cijena]         decimal(10,2) not null,
    [kolicina]       decimal(10,2) not null,
    [datum]          date not null
)
create table [ruta]
(
    id                 int constraint PK_ruta primary key identity,
    [putni_nalog_id]   int constraint FK__putni_nalog__ruta foreign key references [dbo].[putni_nalog](id),
    [x_koordinata_a]   decimal(20,10) not null,
    [y_koordinata_a]   decimal(20,10) not null,
    [x_koordinata_b]   decimal(20,10) not null,
    [y_koordinata_b]   decimal(20,10) not null,
    [km_izmedu_a_b]    decimal(10,2) not null,
    [prosjecna_brzina] decimal(6,2) not null
)
go
-----------------------------------------------------------------
--DUMMY DATA
-----------------------------------------------------------------
insert into [dbo].[tip_vozila]
values ('Karavan'),('Kabriolet'),('Limuzina'),('Hecbek'), ('Kupe'), ('Monovolumen'), ('Dzip')
insert into [dbo].[mjesto]
values ('Zagreb'),('Bjelovar'),('Split'),('Karlovac'),('Dubrovnik'),('Rijeka'),('Zadar'),('Vukovar')
insert into [dbo].[status]
values ('Traje'),('Zavrsio')
go

insert into [dbo].[vozac]
values 
('Pero','Peric','+385912345678','12345678'),
('Ivo','Ivic','+385912345678', '87654321')

insert into [dbo].[vozilo]
values
(2,'Ferrari Enzo',2012,10000.25,10010.10),
(4,'Golf 7',2010,143215.1,153215.2)

insert into [dbo].[putni_nalog]
values
(1,1,2,'1/10/2020'),
(2,2,2,'12/10/2019')

insert into [dbo].[zauzece_vozac]
values
(1,'1/10/2020'),
(1,'1/11/2020'),
(2,'12/10/2019'),
(2,'12/11/2019'),
(2,'12/12/2019'),
(2,'12/13/2019')

insert into [dbo].[zauzece_vozilo]
values
(1,'1/10/2020'),
(1,'1/11/2020'),
(2,'12/10/2019'),
(2,'12/11/2019'),
(2,'12/12/2019'),
(2,'12/13/2019')

insert into [dbo].[servis]
values
(2,'12/14/2019','Tokic',500.00,'Zamjena diskova')
go

insert into [dbo].[kupnja_goriva]
values
(1,1,9.81,20.00,'1/10/2020'),
(2,2,9.7,15.00,'12/11/2019'),
(2,3,9.90,20.00,'12/12/2019')

insert into [dbo].[ruta]
values
(1,50.1241241,40.1241251,40.5215121,50.512411,20.1,110.2),
(1,60.1241241,10.1241251,40.5215121,90.512411,30.21,90.2),
(2,70.1241241,30.1241251,30.5215121,60.512411,40.31,70.2),
(2,80.1241241,40.1241251,20.5215121,70.512411,50.41,70.2)
go
