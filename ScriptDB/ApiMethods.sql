--SELECT * FROM api.ApiMethods

INSERT INTO api.ApiMethods (ApiMethodName, Description, PathUrl, HttpMethod, IsNeedAuthentication) VALUES
(N'GetApiMethodRoles', N'Получение списка', N'/api​/ApiMethodRoles', 'GET', 1),
(N'PutApiMethodRoles', N'Редактирование', N'/api​/ApiMethodRoles', 'PUT', 1),
(N'PostApiMethodRoles', N'Добавление', N'/api​/ApiMethodRoles', 'POST', 1),
(N'DeleteApiMethodRoles', N'Удаление', N'/api​/ApiMethodRoles', 'DELETE', 1),

(N'GetApiMethods', N'Получение списка', N'/api​/ApiMethods', 'GET', 1),
(N'PutApiMethods', N'Редактирование', N'/api​/ApiMethods', 'PUT', 1),
(N'PostApiMethods', N'Добавление', N'/api​/ApiMethods', 'POST', 1),
(N'DeleteApiMethods', N'Удаление', N'/api​/ApiMethods', 'DELETE', 1),

(N'GetCodeAthTypes', N'Получение списка', N'/api​/CodeAthTypes', 'GET', 1),
(N'PutCodeAthTypes', N'Редактирование', N'/api​/CodeAthTypes', 'PUT', 1),
(N'PostCodeAthTypes', N'Добавление', N'/api​/CodeAthTypes', 'POST', 1),
(N'DeleteCodeAthTypes', N'Удаление', N'/api​/CodeAthTypes', 'DELETE', 1),

(N'GetDosageForms', N'Получение списка', N'/api​/DosageForms', 'GET', 1),
(N'PutDosageForms', N'Редактирование', N'/api​/DosageForms', 'PUT', 1),
(N'PostDosageForms', N'Добавление', N'/api​/DosageForms', 'POST', 1),
(N'DeleteDosageForms', N'Удаление', N'/api​/DosageForms', 'DELETE', 1),

(N'GetDrugs', N'Получение списка', N'/api​/Drugs', 'GET', 1),
(N'PutDrugs', N'Редактирование', N'/api​/Drugs', 'PUT', 1),
(N'PostDrugs', N'Добавление', N'/api​/Drugs', 'POST', 1),
(N'DeleteDrugs', N'Удаление', N'/api​/Drugs', 'DELETE', 1),

(N'GetPharmacies', N'Получение списка', N'/api​/Pharmacies', 'GET', 1),
(N'PutPharmacies', N'Редактирование', N'/api​/Pharmacies', 'PUT', 1),
(N'PostPharmacies', N'Добавление', N'/api​/Pharmacies', 'POST', 1),
(N'DeletePharmacies', N'Удаление', N'/api​/Pharmacies', 'DELETE', 1),

(N'GetRegions', N'Получение списка', N'/api​/Regions', 'GET', 1),
(N'PutRegions', N'Редактирование', N'/api​/Regions', 'PUT', 1),
(N'PostRegions', N'Добавление', N'/api​/Regions', 'POST', 1),
(N'DeleteRegions', N'Удаление', N'/api​/Regions', 'DELETE', 1),

(N'GetRegionTypes', N'Получение списка', N'/api​/RegionTypes', 'GET', 1),
(N'PutRegionTypes', N'Редактирование', N'/api​/RegionTypes', 'PUT', 1),
(N'PostRegionTypes', N'Добавление', N'/api​/RegionTypes', 'POST', 1),
(N'DeleteRegionTypes', N'Удаление', N'/api​/RegionTypes', 'DELETE', 1),

(N'GetRoles', N'Получение списка', N'/api​/Roles', 'GET', 1),
(N'PutPutRoles', N'Редактирование', N'/api​/Roles', 'PUT', 1),
(N'PostRoles', N'Добавление', N'/api​/Roles', 'POST', 1),
(N'DeleteRoles', N'Удаление', N'/api​/Roles', 'DELETE', 1),

(N'GetSales', N'Получение списка', N'/api​/Sales', 'GET', 1),
(N'PutSales', N'Редактирование', N'/api​/Sales', 'PUT', 1),
(N'PostSales', N'Добавление', N'/api​/Sales', 'POST', 1),
(N'DeleteSales', N'Удаление', N'/api​/Sales', 'DELETE', 1),

(N'PostUsersAuthenticate', N'Авторизация', N'/api/Users/Authenticate', 'POST', 1),
(N'PostUsersRegister', N'Регистрация', N'/api/Users/Register', 'POST', 1),
(N'GetUsers', N'Получение списка', N'/api​/Users', 'GET', 1),
(N'PutUsers', N'Редактирование', N'/api​/Users', 'PUT', 1),
(N'PostUsers', N'Добавление', N'/api​/Users', 'POST', 1),
--(N'DeleteUsers', N'Удаление', N'/api​/Users', 'DELETE', 1),

(N'GetVendors', N'Получение списка', N'/api​/Vendors', 'GET', 1),
(N'PutVendors', N'Редактирование', N'/api​/Vendors', 'PUT', 1),
(N'PostVendors', N'Добавление', N'/api​/Vendors', 'POST', 1),
(N'DeleteVendors', N'Удаление', N'/api​/Vendors', 'DELETE', 1),

(N'GetLogs', N'Получение списка', N'/api/Logs', 'GET', 1)
