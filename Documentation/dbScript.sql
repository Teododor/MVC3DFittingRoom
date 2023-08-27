use dbProiectATI;

CREATE TABLE Permissions
(
	Id int primary key identity,
	Name nvarchar(100) not null,
	Description nvarchar(500)
);

CREATE TABLE Role
(
	Id int primary key identity,
	Name nvarchar(100) not null,
	Description nvarchar(500),
	CreatedBy int not null,
	CreatedOn Datetime,
	ModifiedBy int not null,
	ModifiedOn Datetime,
	IsDeleted bit,

	constraint UC_Name Unique (Name)
);

CREATE TABLE RolePermissions
(
	RoleId int not null,
	PermissionId int not null,

	constraint FK_ROLEPERMISSIONS_ROLE FOREIGN KEY (RoleId) references Role (Id),
	constraint FK_ROLEPERMISSIONS_PERMISSIONS FOREIGN KEY (PermissionId) references Permissions(Id)
);

CREATE TABLE Dimensions
(
	Id int primary key identity,
	BustSize Decimal(10,2),
	WaistSize Decimal(10,2),
	HipSize Decimal(10,2)
);

CREATE TABLE UserDimensions
(
	Id int primary key identity,
	UserId Int not null,
	Dimensions Int not null,


	constraint FK_USERDIMENSIONS_USER FOREIGN KEY (UserId) references [User](Id),
	constraint FK_USERDIMENSIONS_DIMENSIONS FOREIGN KEY (Dimensions) references Dimensions(Id)
);

CREATE TABLE AddressLine
(
	Id int primary key identity,
	Street nvarchar(100) not null,
	Block nvarchar(100),
	Number int not null,
	Entrance nvarchar(100),
	PostalCode nvarchar(100) not null
)

CREATE TABLE Country
(
	Id int primary key identity,
	Name nvarchar(100),
	ISO nvarchar(50)
)

CREATE TABLE City
(
	Id int primary key identity,
	Name nvarchar(100) not null,
	CountryId Int not null
);

CREATE TABLE PaymentType
(
	Id int primary key identity,
	Card nvarchar(20),
	Repay Bit
);
/*
CREATE TABLE UserActions 
(
	Id int primary key identity,
	CreatedBy int not null,
	ModifiedBy int not null,
);

CREATE TABLE UserActions /* -> audit */
(
    Id INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    ActionType NVARCHAR(50) NOT NULL, -- e.g., 'created', 'updated', 'deleted', etc.
    Entity NVARCHAR(100) NOT NULL, -- e.g., 'User', 'Product', etc.
    EntityID INT NOT NULL, -- ID of the record in the entity table
    ActionTime DATETIME NOT NULL,
    CONSTRAINT FK_USERACTIONS_USER FOREIGN KEY (UserID) REFERENCES [User](Id)
);\
*/


CREATE TABLE [User]
(
	Id int primary key identity,
	FirstName nvarchar(500) not null,
	LastName nvarchar(500) not null,
	PasswordHash nvarchar(2500) not null,
	Email nvarchar(100) not null,
	Telephone nvarchar(100),
	CreatedAt datetime not null,
	ModifiedAt datetime not null,
	ChatColor nvarchar(100),
	IsDeleted bit,
	CreatedBy int not null,
	ModifiedBy int not null,

	CONSTRAINT UC_User_Email UNIQUE (Email)
);

CREATE TABLE UserAddress
(
	Id int primary key identity,
	UserId Int not null,
	AddressLineId Int not null,
	CountryId Int not null,
	CityId Int not null,
	MobileNo nvarchar(100),

	CONSTRAINT FK_USERADDRESS_USER FOREIGN KEY (UserId) references [User] (Id),
	CONSTRAINT FK_USERADDRESS_ADDRESSLINE FOREIGN KEY (AddressLineId) references AddressLine(Id),
	CONSTRAINT FK_USERADDRESS_COUNTRY FOREIGN KEY (CountryId) references Country (Id),
	CONSTRAINT FK_USERADDRESS_CITY FOREIGN KEY (CityId) references City (Id)
);

CREATE TABLE UserPayment
(
	Id int primary key identity,
	UserId Int not null,
	PaymentTypeId Int not null,
	Provider nvarchar(100),
	AccountNo nvarchar(100),
	Expiry Date,

	CONSTRAINT FK_USERPAYMENT_USER FOREIGN KEY (UserId) references [User] (Id),
	CONSTRAINT FK_USERPAYMENT_PAYMENTTYPE FOREIGN KEY (PaymentTypeId) references PaymentType(Id)
);

CREATE TABLE Currency
(
	Id Int primary key identity,
	FullName nvarchar(250),
	Code nvarchar(10),
	currency_symbol nvarchar(10),
	exchange_rate decimal (5,3),
);

CREATE TABLE ProductDiscount
(
	Id int primary key,
	Name nvarchar(100),
	[Description] nvarchar(500),
	DiscountPercent decimal(10,2),
	IsActive Bit,
	CreatedAt smallDatetime,
	ModifiedAt smallDatetime,
	DeletedAt smallDatetime,
);

CREATE TABLE Product
(
	Id int primary key identity,
	Name nvarchar(100) not null,
	Description nvarchar(100),
	Price decimal(10,2) not null,
	CurrencyId Int not null,
	StockNo Int,
	IsDeleted bit,
	CreatedAt datetime not null,
	ModifiedAt datetime not null,
	DeletedAt datetime,
	CreatedBy int not null,
	ModifiedBy int not null,
	ProductDiscount int not null,
	ProductInventory int not null,
	ProductDimensions int not null,

	CONSTRAINT FK_PRODUCT_CURRENCY FOREIGN KEY (CurrencyId) references Currency(Id),
	CONSTRAINT FK_PRODUCT_USER_CB FOREIGN KEY (CreatedBy) references [User](Id),
	CONSTRAINT FK_PRODUCT_USER_MB FOREIGN KEY (ModifiedBy) references [User](Id),
	CONSTRAINT FK_PRODUCT_DISCOUNT FOREIGN KEY (ProductDiscount) references ProductDiscount(Id),
);

CREATE TABLE Comment
(
	UserId Int references [User](Id),
	ProductId int references Product(Id),
	Review nvarchar(2500),
	Mark Int,
	LastModifiedBy int,
	DeletedBy int,
	LastModifiedAt datetime,
	DeletedAt  datetime,

	PRIMARY KEY (UserId, ProductId)
);

CREATE TABLE FavoriteProducts
(
	UserId Int references [User](Id),
	ProductId Int references [Product](Id),
	DateAddFavProduct date
);

CREATE TABLE OrderDetails
(
	Id Int primary key identity,
	UserId Int not null,
	Total decimal(10,2),
	OrderTime decimal(10,2),

	CONSTRAINT FK_ORDERDETAILS_USER FOREIGN KEY (UserId) references [User](Id),
);

CREATE TABLE OrderItems
(
	Id int primary key identity,
	ProductId Int not null,
	OrderId Int not null,
	UserId Int not null,
	NumberOfItemsBought Int not null

	CONSTRAINT FK_ORDERITEMS_PRODUCT FOREIGN KEY (ProductId) references Product(Id),
	CONSTRAINT FK_ORDERITEMS_USER FOREIGN KEY (UserId) references [User](Id),
	CONSTRAINT FK_ORDERITEMS_ORDERDETAILS FOREIGN KEY (OrderId) references [OrderDetails](Id)
)
;

CREATE TABLE UserRoles
(
	RoleId Int references [Role](Id),
	UserId Int references [User](Id),
	PRIMARY KEY (RoleId, UserId)
);






INSERT INTO dbo.[Comment] (UserId, ProductId, Review, Mark, LastModifiedBy, DeletedBy, LastModifiedAt, DeletedAt)
		VALUES (1,3, 'very pleased about the product', 5, 1,1,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP),
			   (2,4, 'quite pleased about the product', 4, 1,1,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP),
			   (3,5, 'I do not like the product', 1, 1,1,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP);

select * from dbo.[Product];

select * from dbo.[Comment];



select * from dbo.[User];
select * from dbo.[Product];

INSERT INTO dbo.[Product] (Name, Description, Price, CurrencyId, StockNo, IsDeleted, CreatedAt, ModifiedAt, DeletedAt, CreatedBy, ModifiedBy, ProductDiscount, ProductInventory, ProductDimensions)
VALUES ('T-Shirt1','tricoul alb',3.14,1,2,0,CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1, 1, 2,1,1),
('T-Shirt1','tricoul alb',3.14,1,2,0,CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1, 1, 2,1,1),
('T-Shirt1','tricoul alb',3.14,1,2,0,CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1, 1, 2,1,1),
('T-Shirt1','tricoul alb',3.14,1,2,0,CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1, 1, 2,1,1),
('T-Shirt1','tricoul alb',3.14,1,2,0,CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1, 1, 2,1,1),
('T-Shirt1','tricoul alb',3.14,1,2,0,CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 1, 1, 2,1,1);

INSERT INTO dbo.[Currency] (FullName, Code, currency_symbol, exchange_rate)
VALUES ('euro', 'eur', '€', 4.96),
('dollar', 'usd', '$', 4.52);

INSERT INTO dbo.[ProductDiscount] (Id, Name, Description, DiscountPercent, IsActive, CreatedAt, ModifiedAt, DeletedAt)
VALUES  (1,'discount 1', 'primul meu discount', 2.55, 1,  CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP ),
		(2,'discount 2', 'second discount', 3.5, 1,  CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP ),
		(3,'discount 3', 'third discount', 5.55, 0,  CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP );


ALTER TABLE dbo.[User]
ADD birthDate DATE DEFAULT CURRENT_TIMESTAMP; /*CAST(GETDATE() AS DATE)*/

ALTER TABLE dbo.[User]
DROP COLUMN birthDate;


select * from dbo.[Currency];
select * from dbo.[ProductDiscount];
select * from dbo.[User]; 
select * from dbo.[Product];


delete from dbo.[User];



