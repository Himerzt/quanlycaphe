--create database FinalProject
Use FinalProject


-- Drink
-- Table Drink
-- DrinkCategory
-- Account
-- Bill
-- Bill info

Create Table StatusTableDrink (
IDstatus int primary key ,
Status nvarchar (100) not null default N'Trống'

)

Create Table TableDrink
(
IDTableDrink int primary key,
Name nvarchar (100) not null Default N'Chưa đặt tên', 
IDstatus int not null default 0, --0 là bàn trống, 1 có ngườii
Foreign key (IDstatus) references StatusTableDrink(IDstatus),
)
go


Create table DrinkCategory
(
IDDrinkCategory int primary key,
Name nvarchar(100) not null Default N'Chưa đặt tên', 
)
go


Create table Drink
(
IDDrink int primary key,
Name nvarchar(100) not null Default N'Chưa đặt tên',
IDDrinkCategory int not null, 
Price float not null, 
Foreign key (IDDrinkCategory) references DrinkCategory(IDDrinkCategory), 
)
go

Create table Staff
(
IDStaff int primary key, 
Name nvarchar(100),
Phone varchar(11),
BirthDate date,
StartDate date,
)
go

Create table Bill
(
IDBill int primary key,
DateCheckIn date not null default getdate(),
DateCheckOut date,
IDTableDrink int not null, 
Total float not null,
Discount float,
Status int not null default 0, -- 1 la da thanh toan // 0 la chua thanh toan
foreign key (IDTableDrink) References TableDrink(IDTableDrink),
IDStaff int not null, 
foreign key (IDStaff) References Staff(IDStaff)
)
go

Create table Billinfo
(
IDBill int not null,
IDDrink int not null,
Count int not null default 0, 
primary key (IDBill,IDDrink),
foreign key (IDBill) References Bill(IDBill),
foreign key (IDDrink) References Drink(IDDrink),
)
go 



Create table Payroll
(
IDPayroll int primary key,
Pay money,
Incentive money, 
PayrollDate date,
IDStaff int not null, 
foreign key (IDStaff) References Staff(IDStaff),
)

go

Create table Account_Type
(
NameType nvarchar(50),
Type int primary key,
)
go

Create table Account
(
UserName nvarchar (100) primary key,
DisplayName nvarchar (100) not null Default N'UEHer' ,
PassWord nvarchar (1000) not null default 999,
Type int not null default 2,   -- 1 la admin, 2 là phục vụ, 3 là thu ngân
IDStaff int not null, 
foreign key (IDStaff) References Staff(IDStaff),
foreign key (Type) References Account_Type(Type),
)
go

set dateformat dmy

--Account_Type--
Insert into Account_Type(NameType, Type) values (N'Admin','1');
Insert into Account_Type(NameType, Type) values (N'Phục vụ','2');
Insert into Account_Type(NameType, Type) values (N'Thu ngân','3');
select * from Account_Type

--Staff--


Insert into Staff(IDStaff, Name, Phone, BirthDate, StartDate) values ('123456',N'Nguyễn Thị Giang','0111111111', '1998-09-16', '2021-09-20');
Insert into Staff(IDStaff, Name, Phone, BirthDate, StartDate) values ('234560',N'Nguyễn Vân Anh' ,'0222222222', '1998-09-17','2021-09-21');
Insert into Staff(IDStaff, Name, Phone, BirthDate, StartDate) values ('234561',N'Trần Văn Bảo' ,'0333333333', '1998-09-18', '2021-09-22');
Insert into Staff(IDStaff, Name, Phone, BirthDate, StartDate) values ('234562',N'Lê Ngọc Thư' ,'0444444444', '1998-09-19', '2021-09-23');
Insert into Staff(IDStaff, Name, Phone, BirthDate, StartDate) values ('345671',N'Phạm Thị Thu Thảo' ,'0555555555', '1998-09-20', '2021-09-24');
select * from Staff

--Payroll--
Insert into Payroll(IDPayroll, Pay, Incentive, PayrollDate, IDStaff) values ('111','12000000','3000000', '2023-11-05', '123456');
Insert into Payroll(IDPayroll, Pay, Incentive, PayrollDate, IDStaff) values ('112','12000000','2000000', '2023-12-05', '123456');
Insert into Payroll(IDPayroll, Pay, Incentive, PayrollDate, IDStaff) values ('113','8000000','1000000', '2023-11-05', '234560');
Insert into Payroll(IDPayroll, Pay, Incentive, PayrollDate, IDStaff) values ('114','8000000','500000', '2023-12-05', '234560');
Insert into Payroll(IDPayroll, Pay, Incentive, PayrollDate, IDStaff) values ('115','8000000','500000', '2023-11-05', '234561');
Insert into Payroll(IDPayroll, Pay, Incentive, PayrollDate, IDStaff) values ('116','8000000','1000000', '2023-12-05', '234561');
Insert into Payroll(IDPayroll, Pay, Incentive, PayrollDate, IDStaff) values ('117','8000000','1000000', '2023-11-05', '234562');
Insert into Payroll(IDPayroll, Pay, Incentive, PayrollDate, IDStaff) values ('118','8000000','1000000', '2023-12-05', '234562');
Insert into Payroll(IDPayroll, Pay, Incentive, PayrollDate, IDStaff) values ('119','9000000','500000', '2023-11-05', '345671');
Insert into Payroll(IDPayroll, Pay, Incentive, PayrollDate, IDStaff) values ('120','9000000','1000000', '2023-12-05', '345671');

--Account--

Insert into Account(UserName, DisplayName, PassWord, Type, IDStaff) values ('Admin',N'AdminNguyen' ,'123', '1', '123456');
Insert into Account(UserName, DisplayName, PassWord, Type, IDStaff) values ('NguyenVanAnh',N'PVNguyenAnh' ,'234', '2', '234560');
Insert into Account(UserName, DisplayName, PassWord, Type, IDStaff) values ('TranVanBao',N'PVTranBao' ,'234', '2', '234561');
Insert into Account(UserName, DisplayName, PassWord, Type, IDStaff) values ('LeNgocThu',N'PVLeThu' ,'234', '2', '234562');
Insert into Account(UserName, DisplayName, PassWord, Type, IDStaff) values ('PhamThiThuThao',N'TNPhamThao' ,'345', '3', '345671');

--StatusTableDrink--
Insert into StatusTableDrink(IDstatus,Status) values (0,N'Trống');
Insert into StatusTableDrink(IDstatus,Status) values (1,N'Có người');


--TableDrink--

Insert into TableDrink(IDTableDrink,Name,IDstatus) values (100,N'Bàn 1',0);
Insert into TableDrink(IDTableDrink,Name,IDstatus) values (200,N'Bàn 2',0);
Insert into TableDrink(IDTableDrink,Name,IDstatus) values (300,N'Bàn 3',0);
Insert into TableDrink(IDTableDrink,Name,IDstatus) values (400,N'Bàn 4',0);
Insert into TableDrink(IDTableDrink,Name,IDstatus) values (500,N'Bàn 5',0);
Insert into TableDrink(IDTableDrink,Name,IDstatus) values (600,N'Bàn 6',0);
Insert into TableDrink(IDTableDrink,Name,IDstatus) values (700,N'Bàn 7',0);
Insert into TableDrink(IDTableDrink,Name,IDstatus) values (800,N'Bàn 8',0);
Insert into TableDrink(IDTableDrink,Name,IDstatus) values (900,N'Bàn 9',0);
Insert into TableDrink(IDTableDrink,Name,IDstatus) values (999,N'Bàn 10',0);
select * from TableDrink



select * from Bill

--DrinkCategory--

Insert into DrinkCategory(IDDrinkCategory, Name) values (333,N'Trà sữa');
Insert into DrinkCategory(IDDrinkCategory, Name) values (444,N'Nước trái cây');
Insert into DrinkCategory(IDDrinkCategory, Name) values (555,N'Sinh tố');
select * from DrinkCategory


--Drink--

Insert into Drink(IDDrink,Name,IDDrinkCategory,Price) values (3331,N'Trà sữa ô long',333,50000);
Insert into Drink(IDDrink,Name,IDDrinkCategory,Price) values (3332,N'Trà sữa lài',333,45000);
Insert into Drink(IDDrink,Name,IDDrinkCategory,Price) values (3333,N'Trà sữa latte',333,60000);
Insert into Drink(IDDrink,Name,IDDrinkCategory,Price) values (3334,N'Trà sữa trứng nướng',333,75000);
Insert into Drink(IDDrink,Name,IDDrinkCategory,Price) values (4441,N'Nước chanh',444,40000);
Insert into Drink(IDDrink,Name,IDDrinkCategory,Price) values (4442,N'Nước cam ép',444,45000);
Insert into Drink(IDDrink,Name,IDDrinkCategory,Price) values (4443,N'Nước ép dưa hấu',444,35000);
Insert into Drink(IDDrink,Name,IDDrinkCategory,Price) values (5551,N'Sinh tố bơ',555,40000);
Insert into Drink(IDDrink,Name,IDDrinkCategory,Price) values (5552,N'Sinh tố dâu',555,45000);
Insert into Drink(IDDrink,Name,IDDrinkCategory,Price) values (5553,N'Sinh tố dưa gang',555,35000);
select * from Drink


go

Create  PROC USP_InsertBill
@idBill INT , @idTable INT , @idStaff INT
AS
BEGIN
	INSERT dbo.Bill 
	        ( IDBill,
			  DateCheckIn ,
	          DateCheckOut ,
	          IDTableDrink ,
			  Total,
	          status,
			  IDStaff,
			  Discount
	        )
	VALUES  ( @idBill,
			  GETDATE() , -- DateCheckIn - date
	          null , -- DateCheckOut - date
	          @idTable , -- idTable - int
			  0,
	          0,  -- status - int
			  @idStaff,
			  0
	        )
END
GO

Create PROC USP_InsertBillInfo
@idBill INT , @idDrink INT , @count INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT
	DECLARE @drinkCount INT = 1
	
	SELECT @isExitsBillInfo = b.IDBill, @drinkCount = b.count 
	FROM dbo.BillInfo AS b 
	WHERE idBill = @idBill AND IDDrink = @idDrink

	IF (@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @drinkCount + @count
		IF (@newCount > 0 )
			UPDATE dbo.BillInfo	SET count = @drinkCount + @count WHERE idBill = @idBill AND idDrink = @idDrink 
		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idDrink = @idDrink
	END
	ELSE
	BEGIN
		INSERT	dbo.BillInfo
        ( idBill, idDrink, count )
		VALUES  ( @idBill, -- idBill - int
          @idDrink, -- idDrink - int
          @count  -- count - int
          )
	END
END
GO

CREATE TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = idBill FROM Inserted
	
	DECLARE @idTable INT
	
	SELECT @idTable = IDTableDrink FROM dbo.Bill WHERE IDBill = @idBill AND status = 0
	
	UPDATE TableDrink SET IDstatus = 1 WHERE IDTableDrink = @idTable
END
GO

CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	
	SELECT @idBill = IDBill FROM Inserted	
	
	DECLARE @idTable INT
	
	SELECT @idTable = IDTableDrink FROM dbo.Bill WHERE IDBill = @idBill
	
	DECLARE @count int = 0
	
	SELECT @count = COUNT(*) FROM dbo.Bill WHERE IDTableDrink = @idTable AND status = 0
	
	IF (@count = 0)
		UPDATE TableDrink SET IDstatus = 0 WHERE IDTableDrink = @idTable
END
GO

select * from Drink
select * from TableDrink 

select * from StatusTableDrink
select * from Bill
select * from Billinfo



	