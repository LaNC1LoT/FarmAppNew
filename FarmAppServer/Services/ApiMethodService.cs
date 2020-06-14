using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Helpers;
using FarmAppServer.Models;
using FarmAppServer.Models.ApiMethods;
using FarmAppServer.Models.Sales;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FarmAppServer.Services
{
    public interface IApiMethodService
    {
        Task<bool> PostApiMethodAsync(string values);
        Task<bool> UpdateApiMethodAsync(int key, string values);
        Task<bool> DeleteApiMethodAsync(int key);
        Task<IEnumerable<ApiMethodDto>> SearchAsync(string param, bool? isNotNullParam, bool? isNeedAuthentication, bool? isDeleted);
    }
    public class ApiMethodService : IApiMethodService
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;

        public ApiMethodService(FarmAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> PostApiMethodAsync(string values)
        {
            var apiMethod = new ApiMethod();
            JsonConvert.PopulateObject(values, apiMethod);

            var existApiMethod = await _context.ApiMethods
                .Where(x => x.IsDeleted == false
                            && x.ApiMethodName == apiMethod.ApiMethodName
                            && x.PathUrl == apiMethod.PathUrl)
                .FirstOrDefaultAsync();

            if (existApiMethod != null) return false;

            _context.ApiMethods.Add(apiMethod);
            var posted = await _context.SaveChangesAsync();

            return posted > 0;
        }

        public async Task<bool> UpdateApiMethodAsync(int key, string values)
        {
            var apiMethod = await _context.ApiMethods.FirstOrDefaultAsync(x => x.Id == key && x.IsDeleted == false);

            if (apiMethod == null) return false;

            JsonConvert.PopulateObject(values, apiMethod);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeleteApiMethodAsync(int key)
        {
            var apiMethod = await _context.ApiMethods.Where(x => x.Id == key && x.IsDeleted == false).FirstOrDefaultAsync();

            if (apiMethod.IsDeleted == true) return false;

            apiMethod.IsDeleted = true;
            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<IEnumerable<ApiMethodDto>> SearchAsync(string param, bool? isNotNullParam, bool? isNeedAuthentication, bool? isDeleted)
        {
            var apiMethods = await _context.ApiMethods.Where(x => x.ApiMethodName.Contains(param) ||
                                                            x.Description.Contains(param) ||
                                                            x.HttpMethod.Contains(param) ||
                                                            x.PathUrl.Contains(param) ||
                                                            x.StoredProcedureName.Contains(param) &&
                                                            x.IsDeleted == false).ToListAsync();

            var result = _mapper.Map<IEnumerable<ApiMethodDto>>(apiMethods);

            return result;
        }
    }
}
