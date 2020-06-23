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
    public class VendorsController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public VendorsController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorDto>>> GetVendorsAsync(CancellationToken cancellationToken = default)
        {
            var vendors = await _farmAppContext.Vendors.Where(x => x.IsDeleted == false).Include(i => i.Region).AsNoTracking().ToListAsync(cancellationToken);
            if (!vendors.Any())
                return BadRequest("Vendors not found");

            return Ok(_mapper.Map<IEnumerable<VendorDto>>(vendors));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutVendorAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var vendor = await _farmAppContext.Vendors.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (vendor == null)
                return BadRequest($"Cannot be found Ath with key {key}");

            JsonConvert.PopulateObject(values, vendor);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<VendorDto>> PostVendorAsync([FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(values)) 
                return BadRequest("Values cannot be null or empty");

            var vendor = new Vendor();
            JsonConvert.PopulateObject(values, vendor);

            await _farmAppContext.AddAsync(vendor, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok(_mapper.Map<VendorDto>(vendor));
        }

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<VendorDto>> DeleteVendorAsync([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key cannot be <= 0");

            var vendor = await _farmAppContext.Vendors.FirstOrDefaultAsync(x => x.Id == key, cancellationToken);
            if (vendor == null)
                return BadRequest($"Not found Code with key {key}");

            vendor.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
