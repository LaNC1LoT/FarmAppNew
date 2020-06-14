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
using FarmAppServer.Models.Regions;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;
using ServiceStack;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionTypesController : ControllerBase
    {
        private readonly FarmAppContext _context;
        private readonly IRegionTypeService _regionTypeService;
        private readonly IMapper _mapper;

        public RegionTypesController(FarmAppContext context, IRegionTypeService regionTypeService, IMapper mapper)
        {
            _context = context;
            _regionTypeService = regionTypeService;
            _mapper = mapper;
        }

        // GET: api/RegionTypes
        [HttpGet]
        public ActionResult<IEnumerable<RegionTypeDto>> GetRegionTypes([FromQuery]int page = 1, [FromQuery]int pageSize = 25)
        {
            var regionTypes = _context.RegionTypes;
            var model = _mapper.ProjectTo<RegionTypeDto>(regionTypes);

            if (model == null) return NotFound("RegionTypes not found");

            var query = model.GetPaged(page, pageSize);

            return Ok(query);
        }

        [HttpGet("RegionTypeById")]
        public async Task<ActionResult<RegionTypeDto>> GetRegionType([FromQuery]int key)
        {
            if (key <= 0) return BadRequest("Key must be > 0");

            var regionTypes = _context.RegionTypes.Where(x => x.Id == key);
            var data = await _mapper.ProjectTo<RegionTypeDto>(regionTypes).FirstOrDefaultAsync();

            if (data == null || data.IsDeleted)
                return NotFound("RegionType not found");

            return Ok(regionTypes);
        }

        // PUT: api/RegionTypes/5
        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<RegionTypeDto>> PutRegionType([FromForm]int key, [FromForm]string values)
        {
            if (key <= 0) return BadRequest("key must be > 0");
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");

            var updated = await _regionTypeService.UpdateRegionTypeAsync(key, values);

            if (updated) return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "RegionType not found or nothing to update"
            });
        }

        // POST: api/RegionTypes
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<RegionTypeDto>> PostRegionType([FromForm]string values)
        {
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");

            var request = await _regionTypeService.PostRegionTypeAsync(values);

            if (request)
                return Ok();

            return BadRequest("RegionType is already taken");
        }

        // DELETE: api/RegionTypes/5
        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<RegionTypeDto>> DeleteRegionType([FromForm]int key)
        {
            if (key <= 0) return BadRequest("key must be > 0");

            var deleted = await _regionTypeService.DeleteRegionTypeAsync(key);

            if (deleted) return Ok();

            return NotFound("Drug not found");
        }

        //Фильтр: Текстбокс по RegionTypes и IsEnabled
        [HttpGet("RegionTypeSearch")]
        public async Task<ActionResult<RegionTypeDto>> RegionTypeSearch([FromQuery]string param)
        {
            if (string.IsNullOrWhiteSpace(param))
                return BadRequest(new ResponseBody()
                {
                    Header = "Error",
                    Result = $"Value cannot be null or whitespace. {nameof(param)}"
                });
            
            var regionsType = _regionTypeService.SearchRegionType(param);
            var model = await _mapper.ProjectTo<RegionTypeDto>(regionsType).ToListAsync();

            return Ok(model);
        }
    }
}
