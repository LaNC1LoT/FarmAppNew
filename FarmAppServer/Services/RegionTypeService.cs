using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Helpers;

namespace FarmAppServer.Services
{
    public interface IRegionTypeService
    {
        IQueryable SearchRegionType(string param);
        Task<RegionType> PostRegionTypeAsync(RegionType regionType);
    }
    public class RegionTypeService : IRegionTypeService
    {
        private readonly FarmAppContext _context;

        public RegionTypeService(FarmAppContext context)
        {
            _context = context;
        }

        public IQueryable SearchRegionType(string param)
        {
            var searchString = param.ToUpper();
            var regionType = _context.RegionTypes
                .Where(x => x.RegionTypeName.ToUpper().Contains(searchString) && x.IsDeleted != true);//

            return regionType;
        }

        public async Task<RegionType> PostRegionTypeAsync(RegionType regionType)
        {
            if (regionType == null) throw new ArgumentNullException(nameof(regionType));

            if(_context.RegionTypes.Any(x => x.RegionTypeName == regionType.RegionTypeName & x.IsDeleted == false))
                throw new AppException("RegionTypeName \"" + regionType.RegionTypeName + "\" is already taken ");

            _context.RegionTypes.Add(regionType); // don't use AddAsync
            await _context.SaveChangesAsync();

            return regionType;
        }
    }
}
