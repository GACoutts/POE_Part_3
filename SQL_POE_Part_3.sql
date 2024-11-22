create database POEPart3;
use POEPart3;

create table Claim(
Id int not null,
UserId int not null,
HoursWorked int not null,
HourlyRate double not null,
Notes varchar(250),
FilePath varchar(300),
TotalAmount double,
Status varchar(10)
);

create table User(
Id int not null,
Name varchar(50),
Surname varchar(50),
Username varchar(50),
Password varchar(50),
ContactInfo varchar(100),
Role varchar(50)
);