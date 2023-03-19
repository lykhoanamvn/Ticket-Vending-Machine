create database TicketVending;

create table destination(
	deid varchar(10),
	dename nvarchar(30),
	demoney int,

)

create table credit_card(
	creid varchar(10),
	crename nvarchar(30),
	crenumber varchar(6),
	amount int
)

create table QRcode(
	qrid varchar(10),
	qrname nvarchar(30),
	accnumber varchar(10),
	amount int,
)

insert into destination values('de01',N'Hà Nội',100000)
insert into destination values('de02',N'Thành Phố Hồ Chí Minh',123000)
insert into destination values('de03',N'Đà Nẵng',500000)
insert into destination values('de04',N'Ninh Bình',100)
insert into destination values('de05',N'Lào Cai',2400)

insert into credit_card values('Cre01',N'Lý Khoa Nam','123456',0)
insert into credit_card values('Cre01',N'Đặng Ngọc Giàu','111111',0)
insert into credit_card values('Cre01',N'Cristiano Ronaldo','000000',0)

insert into QRcode values('QR01',N'Đặng Thị Thanh Nhàn','0892030001',0);
delete from QRcode

select * from destination
select * from credit_card
delete  from destination
