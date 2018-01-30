use OnlineStore

--Merge script to add records to database if records specified do not exist

DBCC CHECKIDENT ('Departments', RESEED, 1);
GO

MERGE INTO Departments AS Target 
USING (VALUES 
        ('Electronics'),
		('Video Games'),
		('Personal Care'),
		('Movies'),
		('House Care')

) 
AS Source (Name) 
ON Target.Name = Source.Name 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Name) 
VALUES (Name);



DBCC CHECKIDENT ('Products', RESEED, 0);
GO

MERGE INTO Products AS Target 
USING (VALUES
		('Body Wash, Original Scent, 32 Oz (Pack of 4)', (select DepartmentID from Departments where Name = 'Personal Care'), 19.99, 22),
		('Body Wash, Fresh Springs Scent, 64 Oz (Pack of 2)', (select DepartmentID from Departments where Name = 'Personal Care'), 21.99, 13),
		('Whitening Toothpaste, Fresh Mint, 7.5 Oz (Pack of 3)', (select DepartmentID from Departments where Name = 'Personal Care'), 9.99, 2),
		('Whitening Toothpaste, Icy Mint, 10.5 Oz (Pack of 4)', (select DepartmentID from Departments where Name = 'Personal Care'), 12.99, 25),
		('USB C Cable, Black (6 Feet, USB 3.0, Gold-Plated)', (select DepartmentID from Departments where Name = 'Electronics'), 7.49, 23),
		('USB C Cable, Blue (6 Feet, USB 3.0, Gold-Plated)', (select DepartmentID from Departments where Name = 'Electronics'), 7.49, 4),
		('70-Inch 4K Ultra HD Smart LED TV (2018 Model)', (select DepartmentID from Departments where Name = 'Electronics'), 1799.99, 1),
		('60-Inch 4K Ultra HD Smart LED TV (2017 Model)', (select DepartmentID from Departments where Name = 'Electronics'), 1399.99, 5),
		('Platinum Dishwasher Detergent Pods, Original Scent, 100 Count', (select DepartmentID from Departments where Name = 'House Care'), 15.99, 50),
		('Elite Dishwasher Detergent, Fresh Breeze Scent, 125 Oz', (select DepartmentID from Departments where Name = 'House Care'), 10.99, 50),
		('PC Desktop (Intel Quad-Core i7 3.6GHz Processor, 16GB DDR4, 2TB HDD', (select DepartmentID from Departments where Name = 'Electronics'), 999.99, 5),
		('2-in-1 Laptop (15.6 inch, Intel Core i7 2.7GHz, 16GB DDR4, 256GB SSD', (select DepartmentID from Departments where Name = 'Electronics'), 879.99, 3)

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

DBCC CHECKIDENT ('Orders', RESEED, 1138597656);