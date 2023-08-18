create database Assignment6Db

use Assignment6Db

create table Products
(ProductId int primary key,
ProductName nvarchar(50) not null,
Price decimal(10,0) not null,
Quantity int,
MFDate date,
ExpDate date)

insert into Products values (104,'Perfume',1500.2,1,'2023-11-23','2025-12-12')
select * from Products

