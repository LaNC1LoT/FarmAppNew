using FarmApp.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Infrastructure.Data.DataBaseHelper
{
    public class InitData : IDisposable
    {
        public IEnumerable<Role> InitRoles { get; private set; } = new List<Role>(2)
        {
            new Role { Id = 1, RoleName = "admin" },
            new Role { Id = 2, RoleName = "user" },
        };

        // public IEnumerable<User> InitUsers { get; private set; } = new List<User>(2)
        // {
        //     new User { Id = 1, UserName = "Админ", UserName = "admin", Password = "123456", RoleId = 1 },
        //     new User { Id = 2, UserName = "Пользователь", Login = "user", Password = "123456", RoleId = 2 },
        // };

        public IEnumerable<RegionType> InitRegionTypes { get; private set; } = new List<RegionType>(5)
        {
            new RegionType{ Id = 1, RegionTypeName = "Государство" },
            new RegionType{ Id = 2, RegionTypeName = "Субъект(регион)" },
            new RegionType{ Id = 3, RegionTypeName = "Город" },
            new RegionType{ Id = 4, RegionTypeName = "Сёла, деревни и др." },
            new RegionType{ Id = 5, RegionTypeName = "Микрорайон" }
        };

        public IEnumerable<ApiMethod> InitApiMethods { get; private set; } = new List<ApiMethod>(2)
        {
            //UsersController
            new ApiMethod{ Id = 1, ApiMethodName = "Authenticate", Description = "", StoredProcedureName = null, PathUrl = "/api/Users/authenticate", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = false, IsDeleted = false },
            new ApiMethod{ Id = 2, ApiMethodName = "Register", Description = "", StoredProcedureName = null, PathUrl = "/api/Users/register", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = false, IsDeleted = false },
            new ApiMethod{ Id = 3, ApiMethodName = "GetAll", Description = "", StoredProcedureName = null, PathUrl = "/api/Users/getAll", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 4, ApiMethodName = "GetById", Description = "", StoredProcedureName = null, PathUrl = "/api/Users/getById", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 5, ApiMethodName = "Update", Description = "", StoredProcedureName = null, PathUrl = "/api/Users/update", HttpMethod = "PUT", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 6, ApiMethodName = "Delete", Description = "", StoredProcedureName = null, PathUrl = "/api/Users/delete", HttpMethod = "DELETE", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 7, ApiMethodName = "GetUsersByRoleAsync", Description = "", StoredProcedureName = null, PathUrl = "/api/Users/getUsersByRoleAsync", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 8, ApiMethodName = "SearchUser", Description = "", StoredProcedureName = null, PathUrl = "/api/Users/searchUser", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            
            //VendorsController
            new ApiMethod{ Id = 9, ApiMethodName = "GetVendors", Description = "", StoredProcedureName = null, PathUrl = "/api/Vendors/GetVendors", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 10, ApiMethodName = "GetVendor", Description = "", StoredProcedureName = null, PathUrl = "/api/Vendors/GetVendor", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 11, ApiMethodName = "PutVendor", Description = "", StoredProcedureName = null, PathUrl = "/api/Vendors/PutVendor", HttpMethod = "PUT", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 12, ApiMethodName = "PostVendor", Description = "", StoredProcedureName = null, PathUrl = "/api/Vendors/PostVendor", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 13, ApiMethodName = "DeleteVendor", Description = "", StoredProcedureName = null, PathUrl = "/api/Vendors/DeleteVendor", HttpMethod = "DELETE", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            
            //SalesController
            new ApiMethod{ Id = 14, ApiMethodName = "GetSales", Description = "", StoredProcedureName = null, PathUrl = "/api/Sales/GetSales", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 15, ApiMethodName = "GetSale", Description = "", StoredProcedureName = null, PathUrl = "/api/Sales/GetSale", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 16, ApiMethodName = "PutSale", Description = "", StoredProcedureName = null, PathUrl = "/api/Sales/PutSale", HttpMethod = "PUT", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 17, ApiMethodName = "PostSale", Description = "", StoredProcedureName = null, PathUrl = "/api/Sales/PostSale", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 18, ApiMethodName = "DeleteSale", Description = "", StoredProcedureName = null, PathUrl = "/api/Sales/DeleteSale", HttpMethod = "DELETE", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            
            //RolesController
            new ApiMethod{ Id = 54, ApiMethodName = "GetRoles", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetRoles", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 55, ApiMethodName = "GetRole", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetRole", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 56, ApiMethodName = "PutRole", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PutRole", HttpMethod = "PUT", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 57, ApiMethodName = "PostRole", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PostRole", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 58, ApiMethodName = "DeleteRole", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetDeleteRoleUser", HttpMethod = "DELETE", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            
            //RegionTypes
            new ApiMethod{ Id = 19, ApiMethodName = "GetRegionTypes", Description = "", StoredProcedureName = null, PathUrl = "/api/RegionTypes/GetRoles", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 20, ApiMethodName = "GetRegionType", Description = "", StoredProcedureName = null, PathUrl = "/api/RegionTypes/GetRole", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 21, ApiMethodName = "PutRegionType", Description = "", StoredProcedureName = null, PathUrl = "/api/RegionTypes/PutRole", HttpMethod = "PUT", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 22, ApiMethodName = "PostRegionType", Description = "", StoredProcedureName = null, PathUrl = "/api/RegionTypes/PostRole", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 23, ApiMethodName = "DeleteRegionType", Description = "", StoredProcedureName = null, PathUrl = "/api/RegionTypes/GetDeleteRoleUser", HttpMethod = "DELETE", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },

            //RegionsController
            new ApiMethod{ Id = 24, ApiMethodName = "GetRegions", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetRegions", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 25, ApiMethodName = "GetRegion", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetRegion", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 26, ApiMethodName = "PutRegion", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PutRegion", HttpMethod = "PUT", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 27, ApiMethodName = "PostRegion", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PostRegion", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 28, ApiMethodName = "DeleteRegion", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/DeleteRegion", HttpMethod = "DELETE", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },

            //PharmaciesController
            new ApiMethod{ Id = 29, ApiMethodName = "GetPharmacies", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetPharmacies", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 30, ApiMethodName = "GetPharmacy", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetPharmacy", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 31, ApiMethodName = "PutPharmacy", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PutPharmacy", HttpMethod = "PUT", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 32, ApiMethodName = "PostPharmacy", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PostPharmacy", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 33, ApiMethodName = "DeletePharmacy", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/DeletePharmacy", HttpMethod = "DELETE", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },

            //DrugsController
            new ApiMethod{ Id = 34, ApiMethodName = "GetDrugs", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetDrugs", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 35, ApiMethodName = "GetDrug", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetDrug", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 36, ApiMethodName = "PutDrug", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PutDrug", HttpMethod = "PUT", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 37, ApiMethodName = "PostDrug", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PostDrug", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 38, ApiMethodName = "DeleteDrug", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/DeleteDrug", HttpMethod = "DELETE", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },

            //CodeAthTypesController
            new ApiMethod{ Id = 39, ApiMethodName = "GetCodeAthTypes", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetCodeAthTypes", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 40, ApiMethodName = "GetCodeAthType", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetCodeAthType", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 41, ApiMethodName = "PutCodeAthType", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PutCodeAthType", HttpMethod = "PUT", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 42, ApiMethodName = "PostCodeAthType", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PostCodeAthType", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 43, ApiMethodName = "DeleteCodeAthType", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/DeleteCodeAthType", HttpMethod = "DELETE", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },

            //ApiMethodsController
            new ApiMethod{ Id = 44, ApiMethodName = "GetApiMethods", Description = "", StoredProcedureName = null, PathUrl = "api/Roles/GetApiMethods", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 45, ApiMethodName = "GetApiMethod", Description = "", StoredProcedureName = null, PathUrl = "api/Roles/GetApiMethod", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 46, ApiMethodName = "PutApiMethod", Description = "", StoredProcedureName = null, PathUrl = "api/Roles/PutApiMethod", HttpMethod = "PUT", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 47, ApiMethodName = "PostApiMethod", Description = "", StoredProcedureName = null, PathUrl = "api/Roles/PostApiMethod", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 48, ApiMethodName = "DeleteApiMethod", Description = "", StoredProcedureName = null, PathUrl = "api/Roles/DeleteApiMethod", HttpMethod = "DELETE", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },

            //ApiMethodRolesController
            new ApiMethod{ Id = 49, ApiMethodName = "GetApiMethodRoles", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetApiMethodRoles", HttpMethod = "GET", IsNotNullParam = false, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 50, ApiMethodName = "GetApiMethodRole", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/GetApiMethodRole", HttpMethod = "GET", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 51, ApiMethodName = "PutApiMethodRole", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PutApiMethodRole", HttpMethod = "PUT", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 52, ApiMethodName = "PostApiMethodRole", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/PostApiMethodRole", HttpMethod = "POST", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false },
            new ApiMethod{ Id = 53, ApiMethodName = "DeleteApiMethodRole", Description = "", StoredProcedureName = null, PathUrl = "/api/Roles/DeleteApiMethodRole", HttpMethod = "DELETE", IsNotNullParam = true, IsNeedAuthentication = true, IsDeleted = false }
        };

        public IEnumerable<ApiMethodRole> InitApitMethodRoles { get; private set; } = new List<ApiMethodRole>(3)
        {
            new ApiMethodRole{ Id = 1, ApiMethodId = 1, RoleId = 1, IsDeleted = false },
            new ApiMethodRole{ Id = 2, ApiMethodId = 1, RoleId = 2, IsDeleted = false },
            new ApiMethodRole{ Id = 3, ApiMethodId = 2, RoleId = 1, IsDeleted = false }
        };

        public static IEnumerable<Region> InitRegions
        {
            get => new List<Region>()
            {
                new Region{ Id = 1, RegionId = 1, RegionTypeId = 1, RegionName = "Приднестровье", Population = 469000 },
                new Region{ Id = 2, RegionId = 1, RegionTypeId = 2, RegionName = "Григориопольский район", Population = 38694 },
                new Region{ Id = 3, RegionId = 1, RegionTypeId = 2, RegionName = "Дубоссарский район", Population = 30491 },
                new Region{ Id = 4, RegionId = 1, RegionTypeId = 2, RegionName = "Каменский район", Population = 19681 },
                new Region{ Id = 5, RegionId = 1, RegionTypeId = 2, RegionName = "Рыбницкий район", Population = 67659 },
                new Region{ Id = 6, RegionId = 1, RegionTypeId = 2, RegionName = "Слободзейский район", Population = 82303 },
                new Region{ Id = 7, RegionId = 1, RegionTypeId = 2, RegionName = "Тирасполь", Population = 133807 },
                new Region{ Id = 8, RegionId = 1, RegionTypeId = 2, RegionName = "Бендеры", Population = 91882 },
                new Region{ Id = 9, RegionId = 7, RegionTypeId = 3, RegionName = "Тирасполь", Population = 133807 },
                new Region{ Id = 10, RegionId = 8, RegionTypeId = 3, RegionName = "Бендеры", Population = 91882 },

            };
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }

                InitRoles = null;
                //InitUsers = null;
                InitRegionTypes = null;
                InitApiMethods = null;
                InitApitMethodRoles = null;

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~InitData()
        // {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        void IDisposable.Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
