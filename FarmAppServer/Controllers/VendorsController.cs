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
using FarmAppServer.Models.Vendors;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.OpenApi.Validations;
using ServiceStack;

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
        public ActionResult<IEnumerable<VendorDto>> GetVendors([FromQuery]int page = 1, [FromQuery]int pageSize = 25)
        {
            var vendors = _context.Vendors;

            if (vendors == null) return NotFound("Vendors not found");

            var model = _mapper.ProjectTo<VendorDto>(vendors);
            var query = model.GetPaged(page, pageSize);
            
            return Ok(query);
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
            if (values.IsNullOrEmpty()) return BadRequest("values cannot be null or empty");

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

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id && e.IsDeleted == false);
        }
    }
}
