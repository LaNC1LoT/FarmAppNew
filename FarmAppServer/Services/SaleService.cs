using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Helpers;
using FarmAppServer.Models.Sales;
using Microsoft.EntityFrameworkCore;

namespace FarmAppServer.Services
{
    public interface ISaleService
    {
        Task<Sale> PostSale(Sale sale);
        Task<SaleDto> GetSaleById(int id);
        IQueryable<SaleDto> GetSales();
        Task<bool> DeleteSaleAsync(int id);
    }
    public class SaleService : ISaleService
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;

        public SaleService(FarmAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Sale> PostSale(Sale sale)
        {
            if (sale == null) throw new ArgumentNullException(nameof(sale));

            if(_context.Sales.Any(x => x.Id == sale.Id & x.IsDeleted == false))
                throw new AppException("SaleId \"" + sale.Id + "\" is already taken");
            
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return sale;
        }

        public async Task<SaleDto> GetSaleById(int id)
        {
            var sale = _context.Sales.Where(x => x.Id == id && x.IsDeleted == false);

            if(sale == null)
                throw new AppException("Sale not found!" );

            var result = await _mapper.ProjectTo<SaleDto>(sale).FirstOrDefaultAsync();

            return result;
        }

        public IQueryable<SaleDto> GetSales()
        {
            var sales = _context.Sales;

            if (sales == null)
                throw new AppException("Sales not found!");

            var result = _mapper.ProjectTo<SaleDto>(sales);

            return result;
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null || sale.IsDeleted == true) return false;

            sale.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
