using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models.Regions;
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
    public class RegionsController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public RegionsController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionDto>>> GetRegionsAsync(CancellationToken cancellationToken = default)
        {
            var regions = await _farmAppContext.Regions.Include(r => r.RegionType).Where(w => w.IsDeleted == false).AsNoTracking().ToListAsync(cancellationToken);
            if (!regions.Any())
                return BadRequest("Regions not found");

            return Ok(_mapper.Map<IEnumerable<RegionDto>>(regions));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutRegionAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var region = await _farmAppContext.Regions.FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
            if (region == null)
                return BadRequest($"Cannot be found Ath with key {key}");

            JsonConvert.PopulateObject(values, region);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<RegionDto>> PostRegionAsync([FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var region = new Region();
            JsonConvert.PopulateObject(values, region);

            await _farmAppContext.AddAsync(region, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok(_mapper.Map<RegionDto>(region));
        }

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> DeleteRegionAsync([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key cannot be <= 0");

            var region = await _farmAppContext.Regions.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (region == null)
                return BadRequest($"Not found Code with key {key}");

            region.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
