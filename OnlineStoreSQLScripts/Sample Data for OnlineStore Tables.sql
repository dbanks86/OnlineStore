use OnlineStore

--Merge script to add records to database if records specified do not exist

DBCC CHECKIDENT ('Departments', RESEED, 1);
GO

MERGE INTO Departments AS Target 
USING (VALUES 
        ('Electronics'),
		('Video Games'),
		('Personal Care'),
		('Movies')
) 
AS Source (Name) 
ON Target.Name = Source.Name 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Name) 
VALUES (Name);



DBCC CHECKIDENT ('Products', RESEED, 1);
GO

MERGE INTO Products AS Target 
USING (VALUES
		('Call of Duty: Black Ops III (PS4)', (select DepartmentID from Departments where Name = 'Video Games'), 59.99, 1),
		('Call of Duty: Black Ops III (Xbox One)', (select DepartmentID from Departments where Name = 'Video Games'), 59.99, 6),
		('Call of Duty: Black Ops III (PC)', (select DepartmentID from Departments where Name = 'Video Games'), 59.99, 0), 
        ('Fallout 3 (PS3)', (select DepartmentID from Departments where Name = 'Video Games'), 59.99, 70),
		('Fallout 3 (Xbox 360)', (select DepartmentID from Departments where Name = 'Video Games'), 59.99, 9),
		('Fallout 3 (PC)', (select DepartmentID from Departments where Name = 'Video Games'), 59.99, 50),
		('Fallout 4 (PS4)', (select DepartmentID from Departments where Name = 'Video Games'), 59.99, 4),
		('Fallout 4 (Xbox One)', (select DepartmentID from Departments where Name = 'Video Games'), 59.99, 12),
		('Fallout 4 (PC)', (select DepartmentID from Departments where Name = 'Video Games'), 59.99, 45),
		('Super Smash TV (Sega Genesis)', (select DepartmentID from Departments where Name = 'Video Games'), 5.99, 66),
		('Super Smash TV (SNES)', (select DepartmentID from Departments where Name = 'Video Games'), 5.99, 66),
		('Marvel Super Heroes: War of Gems', (select DepartmentID from Departments where Name = 'Video Games'), 11.99, 33),
		('X-Men', (select DepartmentID from Departments where Name = 'Video Games'), 5.99, 5),
		('Dark Knight', (select DepartmentID from Departments where Name = 'Movies'), 9.99, 1),
		('300', (select DepartmentID from Departments where Name = 'Movies'), 9.99, 4),
		('Man of Steel', (select DepartmentID from Departments where Name = 'Movies'), 12.99, 11),
		('X-Men', (select DepartmentID from Departments where Name = 'Movies'), 8.99, 12),
		('Old Spice Body Wash Swagger 32 Oz', (select DepartmentID from Departments where Name = 'Personal Care'), 4.99, 22),
		('Old Spice Body Wash Aqua Reef 32 Oz', (select DepartmentID from Departments where Name = 'Personal Care'), 4.99, 13),
		('Old Spice Body Wash Timber 32 Oz', (select DepartmentID from Departments where Name = 'Personal Care'), 4.99, 2),
		('Crest Complete Whitening Plus Scope Toothpaste - Minty Fresh 8 Oz', (select DepartmentID from Departments where Name = 'Personal Care'), 2.79, 23),
		('Reach Mint Waxed Floss 80M', (select DepartmentID from Departments where Name = 'Personal Care'), 4.99, 22),
		('Suave Lotion Advanced Therapy', (select DepartmentID from Departments where Name = 'Personal Care'), 2.49, 10),
		('Vizio 55" 4K HD TV', (select DepartmentID from Departments where Name = 'Electronics'), 656.99, 3),
		('Black and Decker Toaster', (select DepartmentID from Departments where Name = 'Electronics'), 12.99, 27),
		('Sony 80" 4K HD TV', (select DepartmentID from Departments where Name = 'Electronics'), 1005.99, 1),
		('Black and Decker Toaster Oven', (select DepartmentID from Departments where Name = 'Electronics'), 50.99, 80),
		('Samsung 40" 4K HD TV', (select DepartmentID from Departments where Name = 'Electronics'), 599.99, 17)
) 
AS Source (Name, DepartmentID, Price, StockCount) 
ON Target.Name = Source.Name 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Name, DepartmentID, Price, StockCount)  
VALUES (Name, DepartmentID, Price, StockCount);


DBCC CHECKIDENT ('ShippingOptions', RESEED, 1);
GO

MERGE INTO ShippingOptions AS Target 
USING (VALUES 
        ('Rush', 9.99, 1),
		('Expedited', 6.99, 2),
		('Standard', 4.99, 3),
		('Value', 1.99, 5),
		('No-Rush', null, 10)
) 
AS Source (Name, Price, ExpectedDeliveryDays) 
ON Target.Name = Source.Name 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Name, Price, ExpectedDeliveryDays)  
VALUES (Name, Price, ExpectedDeliveryDays);