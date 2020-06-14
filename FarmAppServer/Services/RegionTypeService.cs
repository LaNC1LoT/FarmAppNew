using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FarmAppServer.Services
{
    public interface IRegionTypeService
    {
        Task<bool> PostRegionTypeAsync(string values);
        Task<bool> UpdateRegionTypeAsync(int key, string values);
        Task<bool> DeleteRegionTypeAsync(int key);
        IQueryable SearchRegionType(string param);
    }
    public class RegionTypeService : IRegionTypeService
    {
        private readonly FarmAppContext _context;

        public RegionTypeService(FarmAppContext context)
        {
            _context = context;
        }

        public async Task<bool> PostRegionTypeAsync(string values)
        {
            var regionType = new RegionType();
            JsonConvert.PopulateObject(values, regionType);
            var existRegionType = await _context.RegionTypes
                .Where(x => x.RegionTypeName == regionType.RegionTypeName && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (existRegionType != null) return false;

            _context.RegionTypes.Add(regionType);
            var posted = await _context.SaveChangesAsync();

            return posted > 0;
        }

        public async Task<bool> UpdateRegionTypeAsync(int key, string values)
        {
            var regionType = await _context.RegionTypes.FirstOrDefaultAsync(x => x.Id == key && x.IsDeleted == false);

            if (regionType == null) return false;

            JsonConvert.PopulateObject(values, regionType);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeleteRegionTypeAsync(int key)
        {
            var regionType = await _context.RegionTypes.Where(x => x.Id == key && x.IsDeleted == false).FirstOrDefaultAsync();

            if (regionType.IsDeleted == true) return false;

            regionType.IsDeleted = true;
            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }

        public IQueryable SearchRegionType(string param)
        {
            var searchString = param.ToUpper();
            var regionType = _context.RegionTypes
                .Where(x => x.RegionTypeName.ToUpper().Contains(searchString) && x.IsDeleted != true);//

            return regionType;
        }
    }
}
