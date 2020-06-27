﻿using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Models.Pharmacies;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Services
{
    public interface IPharmacyService
    {
        Task<IEnumerable<PharmacyDto>> GetPharmacies();
        Task<bool> PostPharmacyAsync(string values);
        Task<bool> UpdatePharmacyAsync(int key, string values);
        Task<bool> DeletePharmacyAsync(int key);
        IQueryable SearchPharmacy(string pharmacyName);
    }
    public class PharmacyService : IPharmacyService
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;
        public PharmacyService(FarmAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IEnumerable<PharmacyDto>> GetPharmacies()
        {
            return Task.Run(() =>
            {
                var pharmacies = _context.Pharmacies.Where(ph => ph.IsDeleted == false).Include(x => x.Region).AsNoTracking();
                return _mapper.Map<IEnumerable<PharmacyDto>>(pharmacies);
            });
        }

        public async Task<bool> PostPharmacyAsync(string values)
        {
            if (string.IsNullOrWhiteSpace(values)) return false;

            var pharmacy = new Pharmacy();
            JsonConvert.PopulateObject(values, pharmacy);
            var existPharmacy = await _context.Pharmacies
                .Where(x => x.PharmacyName == pharmacy.PharmacyName && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (existPharmacy != null) return false;

            var dto = new PostPharmacyDto();
            JsonConvert.PopulateObject(values, dto);
            pharmacy.PharmacyId = dto.ParentPharmacyId;

            if (pharmacy.PharmacyId == 0) pharmacy.PharmacyId = null;
            if (pharmacy.RegionId == 0) pharmacy.RegionId = 1;

            _context.Pharmacies.Add(pharmacy);
            var posted = await _context.SaveChangesAsync();

            return posted > 0;
        }

        public async Task<bool> UpdatePharmacyAsync(int key, string values)
        {
            if (key <= 0) return false;
            if (string.IsNullOrWhiteSpace(values)) return false;

            var pharmacy = await _context.Pharmacies.FirstOrDefaultAsync(p => p.Id == key && p.IsDeleted == false);

            if (pharmacy == null) return false;

            JsonConvert.PopulateObject(values, pharmacy);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeletePharmacyAsync(int key)
        {
            var pharmacy = await _context.Pharmacies.FindAsync(key);

            if (pharmacy == null || pharmacy.IsDeleted == true) return false;

            pharmacy.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

        //TextBox -> PharmacyName
        public IQueryable SearchPharmacy(string pharmacyName)
        {
            if (string.IsNullOrEmpty(pharmacyName))
                throw new ArgumentException("Value cannot be null or empty.", nameof(pharmacyName));

            var pharmacy = _context.Pharmacies.Where(x => x.PharmacyName.ToLower()
                .Contains(pharmacyName.ToLower()));

            return pharmacy;
        }

        public bool CheckForeignKey(Pharmacy pharmacy)
        {
            return _context.Regions.Any(x => x.Id == pharmacy.RegionId && x.IsDeleted == false);
        }

    }
}