create database AuditManagementSystem

use AuditManagementSystem

create table Userdetails(userid int primary key identity(1,1),email varchar(50),password varchar(50))


create table Audit(auditid int primary key identity(101,1),ProjectName varchar(50),ProjectManagerName varchar(50),ApplicationOwnerName varchar(50),AuditType varchar(10),AuditDate datetime,ProjectExecutionStatus varchar(50),RemedialActionDuration varchar(max),userid int foreign key references userdetails(userid))


