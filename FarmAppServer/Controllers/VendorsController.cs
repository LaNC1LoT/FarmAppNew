using AutoMapper;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;
        private readonly IVendorService _vendorService;

        public VendorsController(FarmAppContext context, IMapper mapper, IVendorService vendorService)
        {
            _context = context;
            _mapper = mapper;
            _vendorService = vendorService;
        }

        // GET: api/Vendors
        [HttpGet]
        public async Task<IActionResult> GetVendors()
        {
            var vendors = await _vendorService.GetVendorsAsync();

            if (!vendors.Any()) 
                return NotFound("Vendors not found");

            return Ok(vendors);
        }

        // GET: api/Vendors/5
        [HttpGet("VendorById")]
        public async Task<ActionResult<VendorDto>> GetVendor([FromQuery]int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null || vendor.IsDeleted == true)
                return NotFound("Vendor not found");

            var data = _mapper.Map<VendorDto>(vendor);

            return Ok(data);
        }

        // PUT: api/Vendors/5
        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<VendorDto>> PutVendor([FromForm]int key, [FromForm]string values)
        {
            if (key <= 0) return NotFound("Vendor not found");

            var updated = await _vendorService.UpdateVendorAsync(key, values);

            if (updated) return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "Vendor not found or nothing to update"
            });
        }

        // POST: api/Vendors
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<VendorDto>> PostVendor([FromForm]string values)
        {
            if (string.IsNullOrWhiteSpace(values)) return BadRequest("values cannot be null or empty");

            var request = await _vendorService.PostVendorAsync(values);

            if (request) return Ok();

            return BadRequest();
        }

        // DELETE: api/Vendors/5
        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<VendorDto>> DeleteVendor([FromForm]int key)
        {
            if (await _vendorService.DeleteVendorAsync(key))
                return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "User not found"
            });
        }
    }
}
