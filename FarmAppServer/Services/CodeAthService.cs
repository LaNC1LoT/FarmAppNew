using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Helpers;
using FarmAppServer.Models.CodeAthTypes;
using FarmAppServer.Models.Sales;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceStack;

namespace FarmAppServer.Services
{
    public interface ICodeAthService
    {
        Task<bool> UpdateCodeAthTypeAsync(int key, string values);
        Task<bool> PostCodeAthTypeAsync(string values);
        Task<CodeAthTypeDto> GetCodeAthTypeById(int key);
        IQueryable<CodeAthTypeDto> GetCodeAthTypes();
        Task<bool> DeleteCodeAthTypeAsync(int key);
    }
    public class CodeAthService : ICodeAthService
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;

        public CodeAthService(FarmAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> UpdateCodeAthTypeAsync(int key, string values)
        {
            if (key <= 0) return false;
            if (values.IsNullOrEmpty()) return false;

            var codeAthType = _context.CodeAthTypes.First(c => c.Id == key && c.IsDeleted == false);

            if (codeAthType == null) return false;

            JsonConvert.PopulateObject(values, codeAthType);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> PostCodeAthTypeAsync(string values)
        {
            var codeAthType = new CodeAthType();
            JsonConvert.PopulateObject(values, codeAthType);
            var existCodeAthType = await _context.CodeAthTypes
                .Where(x => x.Code == codeAthType.Code && x.IsDeleted == false)
                .FirstOrDefaultAsync();
            
            if (existCodeAthType != null) return false;
            
            var dto = new PostCodeAthType();
            JsonConvert.PopulateObject(values, dto);
            codeAthType.CodeAthId = dto.ParentCodeAthId;

            if (codeAthType.CodeAthId == 0) codeAthType.CodeAthId = null;

            _context.CodeAthTypes.Add(codeAthType);
            var posted = await _context.SaveChangesAsync();

            return posted > 0;
        }

        public async Task<CodeAthTypeDto> GetCodeAthTypeById(int key)
        {
            var codeAthType = _context.CodeAthTypes.Where(x => x.Id == key && x.IsDeleted == false);

            var result = await _mapper.ProjectTo<CodeAthTypeDto>(codeAthType).FirstOrDefaultAsync();
            
            return result;
        }

        public IQueryable<CodeAthTypeDto> GetCodeAthTypes()
        {
            var codeAthTypes = _context.CodeAthTypes;

            if (codeAthTypes == null)
                throw new AppException("CodeAthType not found!");

            var result = _mapper.ProjectTo<CodeAthTypeDto>(codeAthTypes);

            return result;
        }

        public async Task<bool> DeleteCodeAthTypeAsync(int key)
        {
            var region = await _context.CodeAthTypes.FindAsync(key);

            if (region == null || region.IsDeleted == true) return false;

            region.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
