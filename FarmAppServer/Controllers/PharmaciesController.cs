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
using FarmAppServer.Helpers;
using FarmAppServer.Models;
using FarmAppServer.Models.Pharmacies;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmaciesController : ControllerBase
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;
        private readonly IPharmacyService _pharmacyService;

        public PharmaciesController(FarmAppContext context, IMapper mapper, IPharmacyService pharmacyService)
        {
            _context = context;
            _mapper = mapper;
            _pharmacyService = pharmacyService;
        }

        // GET: api/Pharmacies
        [HttpGet]
        public ActionResult<IEnumerable<PharmacyFilterDto>> GetPharmacies([FromQuery]int page = 1, [FromQuery]int pageSize = 25)
        {
            var pharmacies = _context.Pharmacies;

            if (pharmacies == null) return NotFound("Pharmacies not found");
            
            var model = _mapper.ProjectTo<PharmacyFilterDto>(pharmacies);
            var query = model.GetPaged(page, pageSize);

            return Ok(query);
        }

        [HttpGet("PharmacyById")]
        public async Task<ActionResult<PharmacyFilterDto>> GetPharmacy([FromQuery]int id)
        {
            var pharmacy = await _context.Pharmacies.FindAsync(id);

            if (pharmacy == null || pharmacy.IsDeleted == true)
                return NotFound("Pharmacy not found");

            var data = _mapper.Map<PharmacyFilterDto>(pharmacy);

            return Ok(data);
        }

        // PUT: api/Pharmacies/5
        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutPharmacy([FromForm]int key, [FromForm]string values)
        {
            var updated = await _pharmacyService.UpdatePharmacyAsync(key, values);

            if (updated) return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "Pharmacy not found or nothing to update"
            });
        }

        // POST: api/Pharmacies
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<Pharmacy>> PostPharmacy([FromForm]string values)
        {
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");
                
            var request = await _pharmacyService.PostPharmacyAsync(values);

            if (request)
                return Ok();

            return BadRequest();
        }

        // DELETE: api/Pharmacies/5
        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<Pharmacy>> DeletePharmacy([FromForm]int key)
        {
            if (await _pharmacyService.DeletePharmacyAsync(key))
                return Ok();

            return NotFound("Pharmacy not found");
        }


        //Фильтры: TextBox -> PharmacyName
        [HttpGet("Search")]
        public async Task<ActionResult<PharmacyFilterDto>> SearchAsync([FromQuery] string pharmacyName)
        {
            try
            {
                var pharmacies = _pharmacyService.SearchPharmacy(pharmacyName);
                var model = await _mapper.ProjectTo<PharmacyFilterDto>(pharmacies).ToListAsync();

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseBody
                {
                    Header = "Error",
                    Result = $"{e.Message}"
                });
            }
        }
    }
}
