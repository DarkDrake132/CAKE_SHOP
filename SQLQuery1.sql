use master
go
if DB_ID('CakeShop') is not null
	drop database CakeShop
create database CakeShop
go
use CakeShop

create table CAKE_TYPE
(
	ID int IDENTITY(1,1) primary key,
	C_NAME nvarchar(max)
)

create table CAKE
(
	ID int IDENTITY(1,1) primary key,
	C_NAME nvarchar(max),
	TYPEID int,
	PRICE int,
	IMG nvarchar(max)
	foreign key (TYPEID) references CAKE_TYPE(ID)
)

create table RECEIPT
(
	ID int IDENTITY(1,1) primary key,
	INPUTDATE date
)

create table RECEIPT_DETAIL
(
	SERIAL int not null,
	RECEIPT_ID int not null,
	CAKE_ID int,
	TYPEID int,
	AMOUNT int,
	TOTAL int,
	primary key (SERIAL,RECEIPT_ID),
	foreign key (RECEIPT_ID) references RECEIPT(ID),
	foreign key (CAKE_ID) references CAKE(ID),
	foreign key (TYPEID) references CAKE_TYPE(ID),
)


