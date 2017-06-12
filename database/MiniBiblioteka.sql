create table Ksiazki(
ID_KSIAZKI int constraint PK_KSIAZKI primary key identity(1,1),
TYTUL varchar (250),
AUTOR varchar (200),
OPIS varchar (5000),
ILOSC_STRON int,
MIEJSCE_WYDANIA varchar (30),
WYDAWNICTWO varchar (30),
ROK_WYDANIA int,
ZDJECIE image);