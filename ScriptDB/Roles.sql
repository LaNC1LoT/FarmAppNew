IF NOT EXISTS (SELECT TOP 1 1 FROM dist.Roles)
BEGIN
	INSERT INTO dist.Roles (RoleName) VALUES
		(N'Администратор'), (N'Пользователь')
END

SELECT * FROM dist.Roles
