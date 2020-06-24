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
    public class SalesController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public SalesController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetAsync(CancellationToken cancellationToken = default)
        {
            var sales = await _farmAppContext.Sales.Where(w => w.IsDeleted == false).Include(d => d.Drug)
                                    .Include(p => p.Pharmacy).AsNoTracking().ToListAsync(cancellationToken);
            if (!sales.Any())
                return BadRequest("CodeAthType not found");

            return Ok(_mapper.Map<IEnumerable<SaleDto>>(sales));
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var sale = await _farmAppContext.Sales.FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
            if (sale == null)
                return BadRequest($"Cannot be found Sale with key {key}");

            JsonConvert.PopulateObject(values, sale);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<SaleDto>> PostAsync([FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var sale = new Sale();
            JsonConvert.PopulateObject(values, sale);

            await _farmAppContext.AddAsync(sale, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok(_mapper.Map<SaleDto>(sale));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key cannot be <= 0");

            var sale = await _farmAppContext.Sales.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (sale == null)
                return BadRequest($"Not found Sale with key {key}");

            sale.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
