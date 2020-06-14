using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FarmAppServer.Services
{
    public interface IApiMethodRoleService
    {
        Task<bool> PostApiMethodRoleAsync(string values);
        Task<bool> UpdateApiMethodASync(int key, string values);
        Task<bool> DeleteApiMethodRoleAsync(int key);
    }
    public class ApiMethodRoleService : IApiMethodRoleService
    {
        private readonly FarmAppContext _context;

        public ApiMethodRoleService(FarmAppContext context)
        {
            _context = context;
        }

        public async Task<bool> PostApiMethodRoleAsync(string values)
        {
            var apiMethodRole = new ApiMethodRole();
            JsonConvert.PopulateObject(values, apiMethodRole);
            
            var existApiMethod = await _context.ApiMethods
                .Where(x => x.Id == apiMethodRole.ApiMethodId && x.IsDeleted == false).FirstOrDefaultAsync();
            
            if (existApiMethod == null) return false;

            var existRole = await _context.Roles.Where(x => x.Id == apiMethodRole.RoleId && x.IsDeleted == false).FirstOrDefaultAsync();

            if (existRole == null) return false;

            var existApiMethodRole = await _context.ApiMethodRoles
                .Where(x => x.ApiMethodId == apiMethodRole.ApiMethodId && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (existApiMethodRole != null && existApiMethodRole.RoleId == existRole.Id) return false;

            _context.ApiMethodRoles.Add(apiMethodRole);
            var posted = await _context.SaveChangesAsync();

            return posted > 0;
        }

        public async Task<bool> UpdateApiMethodASync(int key, string values)
        {
            var apiMethodRole = await _context.ApiMethodRoles.FirstOrDefaultAsync(x => x.Id == key && x.IsDeleted == false);
            
            if (apiMethodRole == null) return false;

            JsonConvert.PopulateObject(values, apiMethodRole);
            var existApiMethod = await _context.ApiMethods
                .Where(x => x.Id == apiMethodRole.ApiMethodId && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (existApiMethod == null) return false;

            var existRole = await _context.Roles
                .Where(x => x.Id == apiMethodRole.RoleId && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (existRole == null) return false;

            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeleteApiMethodRoleAsync(int key)
        {
            var apiMethodRole = await _context.ApiMethodRoles.Where(x => x.Id == key && x.IsDeleted == false).FirstOrDefaultAsync();

            if (apiMethodRole.IsDeleted == true) return false;

            apiMethodRole.IsDeleted = true;
            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }
    }
}
