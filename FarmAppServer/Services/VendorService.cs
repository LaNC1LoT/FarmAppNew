using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Services
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorDto>> GetVendorsAsync();
        Task<bool> DeleteVendorAsync(int key);
        Task<bool> PostVendorAsync(string values);
        Task<bool> UpdateVendorAsync(int key, string values);
    }
    public class VendorService : IVendorService
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;

        public VendorService(FarmAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IEnumerable<VendorDto>> GetVendorsAsync()
        {
            return Task.Run(() =>
            {
                var vendors = _context.Vendors.Include(i => i.Region).AsNoTracking();
                return _mapper.Map<IEnumerable<VendorDto>>(vendors);
            });
        }

        public async Task<bool> DeleteVendorAsync(int key)
        {
            var vendor = await _context.Vendors.FindAsync(key);

            if (vendor == null || vendor.IsDeleted == true) return false;

            vendor.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PostVendorAsync(string values)
        {
            if (string.IsNullOrWhiteSpace(values)) return false;

            var vendor = new Vendor();
            JsonConvert.PopulateObject(values, vendor);

            var existVendor = await _context.Vendors.Where(x => x.VendorName == vendor.VendorName && x.IsDeleted == false).FirstOrDefaultAsync();

            if (existVendor != null) return false;

            _context.Vendors.Add(vendor);
            var posted = await _context.SaveChangesAsync();

            return posted > 0;
        }

        public async Task<bool> UpdateVendorAsync(int key, string values)
        {
            if (key <= 0) return false;
            if (string.IsNullOrWhiteSpace(values)) return false;

            var vendor = _context.Vendors.First(r => r.Id == key);

            if (vendor == null) return false;

            JsonConvert.PopulateObject(values, vendor);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }
    }
}
