DECLARE @Start INT = 1, @End INT = 1000, @DateFrom DATETIME = '2019-01-01'
DECLARE @Run BIT = 0

IF @Run = 1
BEGIN
	;WITH cte (Rn, DrugId, PharmacyId, SaleDate, Price, Quantity, IsDiscount) AS
	(
		SELECT 
			@Start,
			ABS(CHECKSUM(NEWID()) % 1437) + 1,
			ABS(CHECKSUM(NEWID()) % 12) + 2,
			--ABS(CHECKSUM(NEWID()) % 2) + 25,
			--ABS(Checksum(NewID()) % (25 - 15)) + 15,
			DATEADD(SECOND, ABS(CHECKSUM(NEWID()) % 31449600), @DateFrom),
			CAST(ROUND(RAND(CHECKSUM(NEWID())) * (1000), 2) AS MONEY), 
			ABS(CHECKSUM(NEWID()) % 10) + 1,
			ABS(CHECKSUM(NEWID()) % 2)
				UNION ALL
		SELECT
			Rn + 1,
			ABS(CHECKSUM(NEWID()) % 1437) + 1,
			ABS(CHECKSUM(NEWID()) % 12) + 2, 
			--ABS(CHECKSUM(NEWID()) % 2) + 25,
			--ABS(Checksum(NewID()) % (25 - 15)) + 15,
			DATEADD(SECOND, ABS(CHECKSUM(NEWID()) % 31449600), @DateFrom),
			CAST(ROUND(RAND(CHECKSUM(NEWID())) * (1000), 2) AS MONEY), 
			ABS(CHECKSUM(NEWID()) % 10) + 1,
			ABS(CHECKSUM(NEWID()) % 2)
		FROM cte WHERE Rn < @End
	)

	INSERT INTO tab.Sales (DrugId, PharmacyId, SaleDate, Price, Quantity, Amount, IsDiscount)
		SELECT DrugId, PharmacyId, SaleDate, Price, Quantity, Price * Quantity AS Amount, IsDiscount FROM cte OPTION (MaxRecursion 1000)

	SELECT * FROM tab.Sales
END

