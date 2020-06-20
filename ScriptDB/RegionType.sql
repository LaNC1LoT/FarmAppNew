IF NOT EXISTS (SELECT TOP 1 1 FROM dist.RegionTypes)
BEGIN
	INSERT INTO dist.RegionTypes (RegionTypeName) VALUES
		('������'),
		('������'),
		('�����'),
		('����'),
		('����������')
END 

IF NOT EXISTS (SELECT TOP 1 1 FROM dist.Regions)
BEGIN

	SET IDENTITY_INSERT dist.Regions ON
		INSERT INTO dist.Regions (Id, RegionId, RegionTypeId, RegionName, Population) VALUES
			( 1, NULL, 1, '�������������', 465100 ),
			( 2, 1, 3, '���������', 144243 ),
			( 3, 1, 3, '�������',  91882 ),
			( 4, 1, 2, '���������������� �����', 11381 ),
			( 5, 1, 2, '������������ �����', 27060 ),
			( 6, 1, 2, '��������� �����', 9871 ),
			( 7, 1, 2, '��������� �����', 49949  ),
			( 8, 1, 2, '������������� �����', 15618 ),
			( 9, 7, 3, '�������', 47949 ),
			(10, 5, 3, '���������', 25060 ),
			(11, 8, 3, '���������', 14618 ),
			(12, 2, 3, '����������', 10436  ),
			(13, 4, 3, '������������', 9381 ),
			(14, 6, 3, '�������', 8871 ),

			(15, NULL, 1, '������', 144321123),
			(16, 15, 3, '������', 9542874),
			(17, 15, 2, '������� �������', 5159745),
			(18, 17, 3, '�������', 354124)
	SET IDENTITY_INSERT dist.Regions OFF
END 

SELECT * FROM dist.RegionTypes
SELECT * FROM dist.Regions

