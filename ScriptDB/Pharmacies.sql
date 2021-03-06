IF NOT EXISTS (SELECT TOP 1 1 FROM dist.Pharmacies)
BEGIN

	SET IDENTITY_INSERT dist.Pharmacies ON
		INSERT INTO dist.Pharmacies (Id, PharmacyId, PharmacyName, RegionId, IsMode, IsType, IsNetwork) VALUES
			( 1, NULL, N'ВиваФарм', 1, 0, 0, 1 ),
			( 2, 1, N'ВиваФарм №1', 2, 1, 0, 0 ),
			( 3, 1, N'ВиваФарм №2', 3, 0, 0, 0 ),
			( 4, 1, N'ВиваФарм №3', 8, 1, 0, 0 ),
			( 5, 1, N'ВиваФарм №4', 10, 0, 0, 0 ),
			( 6, 1, N'ВиваФарм №5', 11, 1, 0, 0 ),
			( 7, 1, N'ВиваФарм №6', 12, 0, 0, 0 ),
			( 8, 1, N'ВиваФарм №7', 13, 1, 0, 0 ),
			( 9, 1, N'ВиваФарм №8', 14, 0, 0, 0 ),
			( 10, 1, N'ВиваФарм №9', 2, 1, 0, 0 ),
			( 11, 1, N'ВиваФарм №10', 3, 0, 0, 0 ),
			( 12, 1, N'ВиваФарм №11', 2, 1, 0, 0 ),
			( 13, 1, N'ВиваФарм №12', 9, 0, 0, 0 ),

			( 14, NULL, N'Столетник', 1, 0, 0, 1 ),
			( 15, 14, N'Столетник №1', 2, 0, 0, 0 ),
			( 16, 14, N'Столетник №2', 3, 1, 0, 0 ),
			( 17, 14, N'Столетник №3', 8, 0, 0, 0 ),
			( 18, 14, N'Столетник №4', 10, 0, 0, 0 ),
			( 19, 14, N'Столетник №5', 11, 1, 0, 0 ),
			( 20, 14, N'Столетник №6', 12, 0, 0, 0 ),
			( 21, 14, N'Столетник №7', 13, 1, 0, 0 ),
			( 22, 14, N'Столетник №8', 14, 0, 0, 0 ),
			( 23, 14, N'Столетник №9', 2, 1, 0, 0 ),
			( 24, 14, N'Столетник №10', 3, 0, 0, 0 ),

			( 25, NULL, N'Аптека №1', 3, 1, 1, 0 ),
			( 26, NULL, N'Аптека №2', 3, 0, 1, 0 )
	SET IDENTITY_INSERT dist.Pharmacies OFF
END 

SELECT * FROM dist.Pharmacies
