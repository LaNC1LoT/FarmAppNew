IF NOT EXISTS (SELECT TOP 1 1 FROM dist.RegionTypes)
BEGIN
	INSERT INTO dist.RegionTypes (RegionTypeName) VALUES
		('Страна'),
		('Регион'),
		('Город'),
		('Село'),
		('Микрорайон')
END 

IF NOT EXISTS (SELECT TOP 1 1 FROM dist.Regions)
BEGIN

	SET IDENTITY_INSERT dist.Regions ON
		INSERT INTO dist.Regions (Id, RegionId, RegionTypeId, RegionName, Population) VALUES
			( 1, NULL, 1, 'Приднестровье', 465100 ),
			( 2, 1, 3, 'Тирасполь', 144243 ),
			( 3, 1, 3, 'Бендеры',  91882 ),
			( 4, 1, 2, 'Григориопольский район', 11381 ),
			( 5, 1, 2, 'Дубоссарский район', 27060 ),
			( 6, 1, 2, 'Каменский район', 9871 ),
			( 7, 1, 2, 'Рыбницкий район', 49949  ),
			( 8, 1, 2, 'Слободзейский район', 15618 ),
			( 9, 7, 3, 'Рыбница', 47949 ),
			(10, 5, 3, 'Дубоссары', 25060 ),
			(11, 8, 3, 'Слободзея', 14618 ),
			(12, 2, 3, 'Днестровск', 10436  ),
			(13, 4, 3, 'Григориополь', 9381 ),
			(14, 6, 3, 'Каменка', 8871 ),

			(15, NULL, 1, 'Россия', 144321123),
			(16, 15, 3, 'Москва', 9542874),
			(17, 15, 2, 'Москвая область', 5159745),
			(18, 17, 3, 'Пушкино', 354124)
	SET IDENTITY_INSERT dist.Regions OFF
END 

SELECT * FROM dist.RegionTypes
SELECT * FROM dist.Regions

