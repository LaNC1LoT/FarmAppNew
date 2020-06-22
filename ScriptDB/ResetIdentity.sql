DELETE FROM dist.Vendors
DBCC CHECKIDENT (N'dist.Vendors', RESEED, 0);
GO