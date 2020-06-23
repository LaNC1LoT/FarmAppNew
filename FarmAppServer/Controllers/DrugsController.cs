using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Models.Drugs;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [Route("api/Drugs")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public DrugsController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrugDto>>> GetDrugAsync(CancellationToken cancellationToken = default)
        {
            var drugs = await _farmAppContext.Drugs.Where(w => w.IsDeleted == false).Include(c => c.CodeAthType)
                .Include(v => v.Vendor)
                .Include(d => d.DosageFormType)
                .AsNoTracking().ToListAsync(cancellationToken);
            if (!drugs.Any())
                return BadRequest("Drugs not found");

            return Ok(_mapper.Map<IEnumerable<DrugDto>>(drugs));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutDrugAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var drug = await _farmAppContext.Drugs.FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
            if (drug == null)
                return BadRequest($"Cannot be found Ath with key {key}");

            JsonConvert.PopulateObject(values, drug);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<DrugDto>> PostDrugAsync([FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var drug = new Drug();
            JsonConvert.PopulateObject(values, drug);

            await _farmAppContext.AddAsync(drug, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok(_mapper.Map<DrugDto>(drug));
        }

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> DeleteDrugAsync([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key cannot be <= 0");

            var drug = await _farmAppContext.Drugs.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (drug == null)
                return BadRequest($"Not found drug with key {key}");

            drug.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
