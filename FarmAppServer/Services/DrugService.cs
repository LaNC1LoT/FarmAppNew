using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Helpers;
using FarmAppServer.Models.Drugs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FarmAppServer.Services
{
    public interface IDrugService
    {
        Task<bool> PostDrugAsync(string values);
        Task<bool> UpdateDrugAsync(int key, string values);
        Task<bool> DeleteDrugAsync(DrugDto model);
    }
    public class DrugService : IDrugService
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;

        public DrugService(FarmAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> PostDrugAsync(string values)
        {
            var drug = new Drug();
            JsonConvert.PopulateObject(values,drug);
            var existDrug = await _context.Drugs.Where(x => x.DrugName == drug.DrugName 
                                                            || x.CodeAthType.Code == drug.CodeAthType.Code).FirstOrDefaultAsync();

            if (existDrug != null) return false;
            _context.Drugs.Add(drug);
            var posted = await _context.SaveChangesAsync();

            return posted > 0;
        }

        public async Task<bool> UpdateDrugAsync(int key, string values)
        {
            var drug = await _context.Drugs.FirstOrDefaultAsync(x => x.Id == key);

            if (drug == null) return false;

            JsonConvert.PopulateObject(values, drug);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeleteDrugAsync(DrugDto model)
        {
            if (model.IsDeleted) return false;
            
            var drug = await _context.Drugs.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            drug.IsDeleted = true;
            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }
    }
}
