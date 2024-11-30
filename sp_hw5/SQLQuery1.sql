use master
go
create database UserDb
go
use UserDb
go
create table Users(
	Id int primary key identity,
	Name nvarchar(50) not null,
	Age int not null
)
select * from Users