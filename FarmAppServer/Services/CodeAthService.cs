using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models.CodeAthTypes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Services
{
    public interface ICodeAthService
    {
        Task<CodeAthTypeDto> PostCodeAthTypeAsync(string values);
        Task<IEnumerable<CodeAthTypeDto>> GetCodeAthTypesAsync();
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

        public Task<IEnumerable<CodeAthTypeDto>> GetCodeAthTypesAsync()
        {
            return Task.Run(() =>
            {
                var codeAthTypes = _context.CodeAthTypes.Where(w => w.IsDeleted == false).AsNoTracking();
                return _mapper.Map<IEnumerable<CodeAthTypeDto>>(codeAthTypes);
            });
        }

        public Task<CodeAthTypeDto> PostCodeAthTypeAsync(string values)
        {
            return Task.Run(() =>
            {
                var codeAthType = new CodeAthType();
                JsonConvert.PopulateObject(values, codeAthType);

                _context.Add(codeAthType);
                _context.SaveChanges();

                var codeAthTypeDto = _mapper.Map<CodeAthTypeDto>(codeAthType);

                return codeAthTypeDto;
            });
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
