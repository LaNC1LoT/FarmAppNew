using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models.Roles;
using Microsoft.EntityFrameworkCore;

namespace FarmAppServer.Services
{
    public interface IRoleService
    {
        public IQueryable RoleSearch(string param);
        Task<bool> UpdateRoleAsync(int id, UpdateRoleDto model);
    }
    public class RoleService : IRoleService
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;

        public RoleService(FarmAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //⦁	TextBox по всем полям
        public IQueryable RoleSearch(string param)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentException("Value cannot be null or empty.", nameof(param));

            var query = _context.Roles.Where(x => x.RoleName.Contains(param));

            return query;
        }

        public async Task<bool> UpdateRoleAsync(int id, UpdateRoleDto model)
        {
            var role = await _context.Roles.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefaultAsync();

            if (role == null) return false;

            _mapper.Map(model, role);
            _context.Update(role);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }
    }
}