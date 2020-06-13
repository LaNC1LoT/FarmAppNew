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

namespace FarmAppServer.Services
{
    public interface IApiMethodService
    {
        Task<ApiMethod> PostApiMethodAsync(ApiMethod sale);
        Task<ApiMethodDto> GetApiMethodByIdAsync(int id);
        IQueryable<ApiMethodDto> GetApiMethodsDto();
        Task<bool> DeleteSaleAsync(int id);
        Task<bool> UpdateApiMethodAsync(int id, UpdateApiMethodDto model);
        Task<IEnumerable<ApiMethodDto>> SearchAsync(string param, bool? isNotNullParam, bool? isNeedAuthentication, bool? isDeleted);
        Task<IEnumerable<ApiMethodDto>> CheckForNullParam(bool isNotNullParam);
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
        public async Task<ApiMethod> PostApiMethodAsync(ApiMethod apiMethod)
        {
            if (apiMethod == null) throw new ArgumentNullException(nameof(apiMethod));

            if (_context.Sales.Any(x => x.Id == apiMethod.Id & x.IsDeleted == false))
                throw new AppException("ApiMethodId \"" + apiMethod.Id + "\" is already taken");

            _context.ApiMethods.Add(apiMethod);
            await _context.SaveChangesAsync();

            return apiMethod;
        }

        public async Task<ApiMethodDto> GetApiMethodByIdAsync(int id)
        {
            var apiMethod = _context.ApiMethods.Where(x => x.Id == id && x.IsDeleted == false);

            if (apiMethod == null)
                throw new AppException("Api method not found!");

            var result = await _mapper.ProjectTo<ApiMethodDto>(apiMethod).FirstOrDefaultAsync();

            return result;
        }

        public IQueryable<ApiMethodDto> GetApiMethodsDto()
        {
            var apiMethod = _context.ApiMethods;

            if (apiMethod == null)
                throw new AppException("Api method not found!");

            var result = _mapper.ProjectTo<ApiMethodDto>(apiMethod);

            return result;
        }

        public async Task<bool> UpdateApiMethodAsync(int id, UpdateApiMethodDto model)
        {
            var apiMethod = await _context.ApiMethods.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefaultAsync();

            if (apiMethod == null) return false;

            _mapper.Map(model, apiMethod);
            _context.Update(apiMethod);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<IEnumerable<ApiMethodDto>> SearchAsync(string param, bool? isNotNullParam, bool? isNeedAuthentication, bool? isDeleted)
        {
            var apiMethods = await _context.ApiMethods.Where(x => x.ApiMethodName.Contains(param) ||
                                                            x.Description.Contains(param) ||
                                                            x.HttpMethod.Contains(param) ||
                                                            x.PathUrl.Contains(param) ||
                                                            x.StoredProcedureName.Contains(param)).ToListAsync();

            var result = _mapper.Map<IEnumerable<ApiMethodDto>>(apiMethods);

            return result;
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var apiMethod = await _context.Users.FindAsync(id);

            if (apiMethod == null || apiMethod.IsDeleted == true) return false;

            apiMethod.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ApiMethodDto>> CheckForNullParam(bool isNotNullParam)
        {
            var apiMethods = await _context.ApiMethods.Where(x => x.IsNotNullParam == true).ToListAsync();
            var result = _mapper.Map<IEnumerable<ApiMethodDto>>(apiMethods);

            return result;
        }
    }
}
