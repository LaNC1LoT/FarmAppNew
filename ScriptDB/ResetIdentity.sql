DELETE FROM api.ApiMethods
DBCC CHECKIDENT (N'api.ApiMethods', RESEED, 0);
GO