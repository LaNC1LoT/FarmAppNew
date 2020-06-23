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
    public class RegionTypesController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public RegionTypesController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionTypeDto>>> GetRegionTypesAsync(CancellationToken cancellationToken = default)
        {
            var regionTypes = await _farmAppContext.RegionTypes.Where(w => w.IsDeleted == false).AsNoTracking().ToListAsync(cancellationToken);
            if (!regionTypes.Any())
                return BadRequest("CodeAthType not found");

            return Ok(_mapper.Map<IEnumerable<RegionTypeDto>>(regionTypes));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutRegionTypeAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var regionType = await _farmAppContext.RegionTypes.FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
            if (regionType == null)
                return BadRequest($"Cannot be found Ath with key {key}");

            JsonConvert.PopulateObject(values, regionType);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<RegionTypeDto>> PostRegionTypeAsync([FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var regionType = new RegionType();
            JsonConvert.PopulateObject(values, regionType);

            await _farmAppContext.AddAsync(regionType, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok(_mapper.Map<RegionTypeDto>(regionType));
        }

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<RegionTypeDto>> DeleteRegionType([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key cannot be <= 0");

            var regionType = await _farmAppContext.RegionTypes.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (regionType == null)
                return BadRequest($"Not found Code with key {key}");

            regionType.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
