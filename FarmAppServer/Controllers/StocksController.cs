using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public StocksController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetAsync(CancellationToken cancellationToken = default)
        {
            var stocks = await _farmAppContext.Stocks.Where(w => w.IsDeleted == false).
                Include(p => p.Pharmacy).Include(d => d.Drug).AsNoTracking().ToListAsync(cancellationToken);
            if (!stocks.Any())
                return BadRequest("Stocks not found");

            return Ok(_mapper.Map<IEnumerable<StockDto>>(stocks));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var role = await _farmAppContext.Stocks.FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
            if (role == null)
                return BadRequest($"Cannot be found stock with key {key}");

            JsonConvert.PopulateObject(values, role);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<StockDto>> PostAsync([FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var stock = new Stock();
            JsonConvert.PopulateObject(values, stock);

            await _farmAppContext.AddAsync(stock, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok(_mapper.Map<StockDto>(stock));
        }

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> DeleteAsync([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key cannot be <= 0");

            var stock = await _farmAppContext.Stocks.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (stock == null)
                return BadRequest($"Not found stock with key {key}");

            stock.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
