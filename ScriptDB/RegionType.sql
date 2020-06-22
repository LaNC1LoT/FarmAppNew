IF NOT EXISTS (SELECT TOP 1 1 FROM dist.RegionTypes)
BEGIN
	INSERT INTO dist.RegionTypes (RegionTypeName) VALUES
		(N'Страна'),
		(N'Регион'),
		(N'Город'),
		(N'Село'),
		(N'Микрорайон')
END 

IF NOT EXISTS (SELECT TOP 1 1 FROM dist.Regions)
BEGIN

	SET IDENTITY_INSERT dist.Regions ON
		INSERT INTO dist.Regions (Id, RegionId, RegionTypeId, RegionName, Population) VALUES
			( 1, NULL, 1, N'Приднестровье', 465100 ),
			( 2, 1, 3, N'Тирасполь', 144243 ),
			( 3, 1, 3, N'Бендеры',  91882 ),
			( 4, 1, 2, N'Григориопольский район', 11381 ),
			( 5, 1, 2, N'Дубоссарский район', 27060 ),
			( 6, 1, 2, N'Каменский район', 9871 ),
			( 7, 1, 2, N'Рыбницкий район', 49949  ),
			( 8, 1, 2, N'Слободзейский район', 15618 ),
			( 9, 7, 3, N'Рыбница', 47949 ),
			(10, 5, 3, N'Дубоссары', 25060 ),
			(11, 8, 3, N'Слободзея', 14618 ),
			(12, 2, 3, N'Днестровск', 10436  ),
			(13, 4, 3, N'Григориополь', 9381 ),
			(14, 6, 3, N'Каменка', 8871 ),

			(15, NULL, 1, N'Россия', 144321123),
			(16, 15, 3, N'Москва', 9542874),
			(17, 15, 2, N'Москвая область', 5159745),
			(18, 17, 3, N'Пушкино', 354124)
	SET IDENTITY_INSERT dist.Regions OFF
END 

SELECT * FROM dist.RegionTypes
SELECT * FROM dist.Regions

