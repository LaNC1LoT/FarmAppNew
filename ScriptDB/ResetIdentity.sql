DELETE FROM dist.Vendors
DBCC CHECKIDENT ('dist.Vendors', RESEED, 0);
GO