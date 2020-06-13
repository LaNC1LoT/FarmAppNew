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
        public async Task<ActionResult<IEnumerable<RegionTypeDto>>> GetRegionTypes()
        {
            var regions = _context.RegionTypes.AsQueryable();
            var model = await _mapper.ProjectTo<RegionTypeDto>(regions).ToListAsync();

            return Ok(model);
        }

        [HttpGet("RegionTypeById")]
        public async Task<ActionResult<RegionTypeDto>> GetRegionType([FromQuery]int id)
        {
            var regionType = await _context.RegionTypes.FindAsync(id);

            if (regionType == null) 
                return NotFound(new ResponseBody()
                {
                    Header = "Error",
                    Result = "RegionType not found"
                });

            if (regionType.IsDeleted == true)
                return NotFound(new ResponseBody()
                {
                    Header = "Error",
                    Result = "RegionType not found"
                });

            var result = _mapper.Map<RegionTypeDto>(regionType);
            
            return result;
        }

        // PUT: api/RegionTypes/5
        [HttpPut]
        public async Task<ActionResult<RegionTypeDto>> PutRegionType([FromQuery]int id, [FromQuery]string regionTypeName)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseBody()
                {
                    Header = "Error",
                    Result = "Bad request"
                });

            var regionType = await _context.RegionTypes.Where(rt => rt.Id == id).FirstOrDefaultAsync();

            if (regionType == null) 
                return NotFound(new ResponseBody()
                {
                    Header = "Error",
                    Result = "RegionType not found"
                });

            if (regionType.IsDeleted == true) 
                return NotFound(new ResponseBody()
                {
                    Header = "Error",
                    Result = "RegionType not found"
                });

            regionType.RegionTypeName = regionTypeName;
            _context.Update(regionType);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<RegionTypeDto>(regionType));
        }

        // POST: api/RegionTypes
        [HttpPost]
        public async Task<ActionResult<RegionTypeDto>> PostRegionType([FromBody]PostRegionTypeDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseBody()
                {
                    Header = "Error",
                    Result = "Bad request"
                });

            var regionType = _mapper.Map<RegionType>(model);
            var request = await _regionTypeService.PostRegionTypeAsync(regionType);
            var result = _mapper.Map<RegionTypeDto>(request);

            return Created("PostRegionType", result);
        }

        // DELETE: api/RegionTypes/5
        [HttpDelete]
        public async Task<ActionResult<RegionTypeDto>> DeleteRegionType([FromQuery]int id)
        {
            var regionType = await _context.RegionTypes.FindAsync(id);
            
            if (regionType == null)
                return NotFound(new ResponseBody()
                {
                    Header = "Error",
                    Result = "RegionType not found"
                });

            regionType.IsDeleted = true;
            await _context.SaveChangesAsync();
            var result = _mapper.Map<RegionTypeDto>(regionType);

            return result;
        }

        private bool RegionTypeExists(int id)
        {
            return _context.RegionTypes.Any(e => e.Id == id && e.IsDeleted == false);
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
