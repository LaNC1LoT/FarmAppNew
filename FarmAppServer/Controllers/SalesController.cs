using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Models.Sales;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;
        private readonly ISaleService _saleService;

        public SalesController(FarmAppContext context, IMapper mapper, ISaleService saleService)
        {
            _context = context;
            _mapper = mapper;
            _saleService = saleService;
        }

        // GET: api/Sales
        [HttpGet]
        public ActionResult<IEnumerable<SaleDto>> GetSales([FromQuery]int page = 1, [FromQuery]int pageSize = 25)
        {
            var sales = _saleService.GetSales();
            var result = sales.GetPaged(page, pageSize);

            return Ok(result);
        }

        // GET: api/Sales/5
        [HttpGet("SaleById")]
        public async Task<ActionResult<SaleDto>> GetSale([FromQuery]int id)
        {
            var sale = await _saleService.GetSaleById(id);

            return Ok(sale);
        }

        // PUT: api/Sales/5
        [HttpPut]
        public async Task<IActionResult> PutSale([FromQuery]int id, [FromBody]UpdateSaleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var sale = await _context.Sales.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (sale == null)
                return NotFound("Sale no found");

            _mapper.Map(model, sale);
            _context.Update(sale);
            await _context.SaveChangesAsync();

            //var result = _mapper.Map<SaleDto>(sale);

            return NoContent();
        }

        // POST: api/Sales
        [HttpPost]
        public async Task<ActionResult<SaleDto>> PostSale([FromBody]PostSaleDto model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var sale = _mapper.Map<Sale>(model);
            var request = await _saleService.PostSale(sale);
            var result = _mapper.Map<SaleDto>(request);

            return Created("PostSale", result);
        }

        // DELETE: api/Sales/5
        [HttpDelete]
        public async Task<ActionResult> DeleteSale([FromQuery]int id)
        {
            if (await _saleService.DeleteSaleAsync(id))
                return Ok();

            return NotFound("Sale not found");
        }

        // GET: api/Sales/SaleForPeriod
        [HttpGet("SaleForPeriod")]
        public IActionResult SaleForPeriod([FromQuery]DateTime start, [FromQuery]DateTime end, [FromQuery]int page = 1, [FromQuery]int pageSize = 25)
        {
            var sales= _context.Sales.Where(x => x.SaleDate >= start && x.SaleDate <= end);
            var result = sales.GetPaged(page, pageSize);

            if (result == null)
                return NotFound(new ResponseBody()
                {
                    Header = "Error",
                    Result = "Logs not found"
                });

            return Ok(result);
        }
    }
}
