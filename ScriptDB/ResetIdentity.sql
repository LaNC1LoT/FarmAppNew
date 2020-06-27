DELETE FROM api.ApiMethodRoles
DBCC CHECKIDENT (N'api.ApiMethodRoles', RESEED, 0);
GO