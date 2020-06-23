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
    public class PharmaciesController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public PharmaciesController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PharmacyDto>>> PharmaciesAsync(CancellationToken cancellationToken = default)
        {
            var pharmacies = await _farmAppContext.Pharmacies.Where(w => w.IsDeleted == false).Include(x => x.Region).AsNoTracking().ToListAsync(cancellationToken);
            if (!pharmacies.Any())
                return BadRequest("Pharmacies not found");

            return Ok(_mapper.Map<IEnumerable<PharmacyDto>>(pharmacies));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutCodeAthTypeAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var pharmacy = await _farmAppContext.Pharmacies.FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
            if (pharmacy == null)
                return BadRequest($"Cannot be found Ath with key {key}");

            JsonConvert.PopulateObject(values, pharmacy);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<PharmacyDto>> PostCodeAthTypeAsync([FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var pharmacy = new Pharmacy();
            JsonConvert.PopulateObject(values, pharmacy);

            await _farmAppContext.AddAsync(pharmacy, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok(_mapper.Map<PharmacyDto>(pharmacy));
        }

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> DeleteCodeAthTypeAsync([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key cannot be <= 0");

            var pharmacy = await _farmAppContext.Pharmacies.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (pharmacy == null)
                return BadRequest($"Not found Pharmacies with key {key}");

            pharmacy.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
