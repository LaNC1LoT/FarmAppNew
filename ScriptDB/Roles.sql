IF NOT EXISTS (SELECT TOP 1 1 FROM dist.Roles)
BEGIN
	INSERT INTO dist.Roles (RoleName) VALUES
		('�������������'), ('������������')
END

SELECT * FROM dist.Roles
