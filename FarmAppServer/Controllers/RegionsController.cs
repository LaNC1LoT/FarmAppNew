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
    public class RegionsController : ControllerBase
    {
        private readonly FarmAppContext _context;
        private readonly IRegionService _regionService;
        private readonly IMapper _mapper;

        public RegionsController(FarmAppContext context, IRegionService regionService, IMapper mapper)
        {
            _context = context;
            _regionService = regionService;
            _mapper = mapper;
        }

        // GET: api/Regions
        [HttpGet]
        public ActionResult<IEnumerable<RegionDto>> GetRegions([FromQuery]int page = 1, [FromQuery]int pageSize = 25)
        {
            var regions = _context.Regions.Include(x => x.Regions);
            
            var model = _mapper.ProjectTo<RegionDto>(regions);

            if (model == null)
                return NotFound("Region not found");

            var query = model.GetPaged(page, pageSize);

            return Ok(query);
        }

        // GET: api/Regions/5
        [HttpGet("RegionById")]
        public async Task<ActionResult<RegionDto>> GetRegion([FromQuery]int id)
        {
            var region = _context.Regions.Where(x => x.Id == id);
            var data = await _mapper.ProjectTo<RegionDto>(region).FirstOrDefaultAsync();

            if (data == null || data.IsDeleted)
                return NotFound("Region not found");

            return Ok(data);
        }

        // PUT: api/Regions/5
        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutRegion([FromForm]int key, [FromForm]string values)
        {
            if (key <= 0) return NotFound("Region not found");

            var updated = await _regionService.UpdateRegionAsync(key, values);

            if (updated) return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "Region not found or nothing to update"
            });
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<RegionDto>> PostRegion([FromForm]string values)
        {
            if (values.IsNullOrEmpty()) return BadRequest("values cannot be null or empty");

            var request = await _regionService.PostRegion(values);
            
            //var result = _mapper.Map<RegionDto>(request);
            if (request)
                return Ok();

            return BadRequest("Population, regionId, regionTypeId >= 0");
        }

        // DELETE: api/Regions/5
        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<RegionDto>> DeleteRegion([FromForm]int key)
        {
            if (await _regionService.DeleteRegionAsync(key))
                return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "Region not found"
            });
        }

        private bool RegionExists(int id)
        {
            return _context.Regions.Any(e => e.Id == id && e.IsDeleted == false);
        }

        //Фильтры: TextBox -> RegionName
        [HttpGet("SearchRegion")]
        public async Task<ActionResult<RegionDto>> SearchRegion([FromQuery]string regionName)
        {
            var regions = _regionService.RegionNameSearch(regionName);
            var model = await _mapper.ProjectTo<RegionDto>(regions).ToListAsync();

            if (model.Count == 0)
                return NotFound("Region not found");
            //???
            foreach (var item in model.Where(item => item.IsDeleted))
                model.Remove(item);

            return Ok(model);
        }
    }
}
