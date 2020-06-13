using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Helpers;
using FarmAppServer.Models;
using FarmAppServer.Models.Regions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceStack;

namespace FarmAppServer.Services
{
    public interface IRegionService
    {
        IQueryable RegionNameSearch(string param);
        Task<bool> PostRegion(string values);
        Task<bool> UpdateRegionAsync(int id, string values);
        Task<bool> DeleteRegionAsync(int id);
    }
    public class RegionService : IRegionService
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;

        public RegionService(FarmAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        //Post region
        public async Task<bool> PostRegion(string values)
        {
            if (values.IsNullOrEmpty()) return false;
            
            var region = new Region();
            JsonConvert.PopulateObject(values, region);
            var existRegion = await _context.Regions.Where(x => x.RegionName == region.RegionName && x.IsDeleted == false).FirstOrDefaultAsync();

            if (existRegion != null) return false;

            if (region.RegionId == 0) region.RegionId = null;


            if (region.Population < 0 || region.RegionId < 0 || region.RegionTypeId < 0) return false;
            if (region.RegionTypeId == 0) region.RegionTypeId = 1;

            _context.Regions.Add(region);
            var posted = await _context.SaveChangesAsync();

            return posted > 0;
        }

        public async Task<bool> DeleteRegionAsync(int id)
        {
            var region = await _context.Regions.FindAsync(id);

            if (region == null || region.IsDeleted == true) return false;

            region.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateRegionAsync(int key, string values)
        {
            if (key <= 0) return false;
            if (values.IsNullOrEmpty()) return false;

            var region = _context.Regions.First(r => r.Id == key);

            if (region == null) return false;
            
            JsonConvert.PopulateObject(values, region);


            //_mapper.Map(model, region);

            //if (region.RegionId == 0) region.RegionId = null;

            //_context.Update(region);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        //Фильтры: TextBox -> RegionName
        public IQueryable RegionNameSearch(string param)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentException("Value cannot be null or empty.", nameof(param));

            var request = param.ToUpper();

            var regions = _context.Regions.Where(x => x.RegionName.ToUpper().Contains(request) ||
                                                      x.Population.ToString().Contains(request));// ||
                                                      //x.ParentRegion.RegionName.ToUpper().Contains(request));
            return regions;
        }
    }
}
