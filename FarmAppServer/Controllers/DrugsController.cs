using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Models.Drugs;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;

namespace FarmAppServer.Controllers
{
    [Route("api/Drugs")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;
        private readonly IDrugService _drugService;

        public DrugsController(FarmAppContext context, IMapper mapper, IDrugService drugService)
        {
            _context = context;
            _mapper = mapper;
            _drugService = drugService;
        }

        // GET: api/Drugs
        [HttpGet]
        public ActionResult<IEnumerable<DrugDto>> GetDrugs([FromQuery]int page = 1 ,[FromQuery]int pageSize = 25)
        {
            var drugs = _context.Drugs;
            var model = _mapper.ProjectTo<DrugDto>(drugs);

            if (model == null) return NotFound("Drugs not found");

            var query = model.GetPaged(page, pageSize);

            return Ok(query);
        }

        // GET: api/Drugs/5
        [HttpGet("DrugById")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<DrugDto>> GetDrug([FromForm]int key)
        {
            if (key <= 0) return BadRequest("Key must be > 0");

            var drug = _context.Drugs.Where(x => x.Id == key && x.IsDeleted == false);
            var data = await _mapper.ProjectTo<DrugDto>(drug).FirstOrDefaultAsync();
            
            if (data == null || data.IsDeleted)
                return NotFound("Drug not found");
            
            return Ok(drug);
        }

        // PUT: api/Drugs/5
        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutDrug([FromForm]int key, [FromForm]string values)
        {
            if (key <= 0) return BadRequest("key must be > 0");
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");

            var updated = await _drugService.UpdateDrugAsync(key, values);

            if (updated) return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "Drug not found or nothing to update"
            });
        }

        // POST: api/Drugs
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PostDrug([FromForm]string values)
        {
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");

            var request = await _drugService.PostDrugAsync(values);

            if (request)
                return Ok();

            return BadRequest("Drug is already taken");
        }

        // DELETE: api/Drugs/5
        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> DeleteDrug([FromForm]int key)
        {
            if (key <= 0) return BadRequest("key must be > 0");

            var deleted = await _drugService.DeleteDrugAsync(key);

            if (deleted) return Ok();

            return NotFound("Drug not found");
        }
    }
}
