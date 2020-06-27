DECLARE @Start INT = 1, @End INT = 1000, @DateFrom DATETIME = '2020-01-01'
DECLARE @Run BIT = 1

IF @Run = 1
BEGIN
	;WITH cte (Rn, DrugId, PharmacyId, CreateDate, Quantity) AS
	(
		SELECT 
			@Start,
			ABS(CHECKSUM(NEWID()) % 1437) + 1,
			--ABS(CHECKSUM(NEWID()) % 12) + 2,
			--ABS(CHECKSUM(NEWID()) % 2) + 25,
			ABS(Checksum(NewID()) % (25 - 15)) + 15,
			DATEADD(SECOND, ABS(CHECKSUM(NEWID()) % 31449600), @DateFrom),
			ABS(CHECKSUM(NEWID()) % 100) + 1
				UNION ALL
		SELECT
			Rn + 1,
			ABS(CHECKSUM(NEWID()) % 1437) + 1,
			--ABS(CHECKSUM(NEWID()) % 12) + 2, 
			--ABS(CHECKSUM(NEWID()) % 2) + 25,
			ABS(Checksum(NewID()) % (25 - 15)) + 15,
			DATEADD(SECOND, ABS(CHECKSUM(NEWID()) % 31449600), @DateFrom),
			ABS(CHECKSUM(NEWID()) % 100) + 1
		FROM cte WHERE Rn < @End
	)

	INSERT INTO tab.Stocks (PharmacyId, DrugId, CreateDate, Quantity)
		SELECT PharmacyId, DrugId, CreateDate, Quantity FROM cte OPTION (MaxRecursion 1000)

	--SELECT * FROM tab.Stocks
END