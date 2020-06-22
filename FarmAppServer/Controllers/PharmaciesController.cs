﻿using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Models.Pharmacies;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetPharmacies()
        {
            var pharmacies = await _pharmacyService.GetPharmaciesAsync();

            if (!pharmacies.Any())
                return NotFound("Pharmacies not found");
            return Ok(pharmacies);
        }

        [HttpGet("PharmacyById")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<PharmacyFilterDto>> GetPharmacy([FromForm]int key)
        {
            if (key <= 0) return BadRequest("Key must be > 0");

            var pharmacy = await _context.Pharmacies.FindAsync(key);

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
            if (key <= 0) return BadRequest("key must be > 0");
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");

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
            if (key <= 0) return BadRequest("key must be > 0");
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
