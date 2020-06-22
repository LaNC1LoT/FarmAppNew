using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Models.CodeAthTypes;
using FarmAppServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeAthTypesController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public CodeAthTypesController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeAthTypeDto>>> GetCodeAthTypes()
        {
            var codeAthTypes = await _farmAppContext.CodeAthTypes.Where(w => w.IsDeleted == false).AsNoTracking().ToListAsync();
            if (!codeAthTypes.Any())
                return NotFound("CodeAthType not found");

            return Ok(_mapper.Map<IEnumerable<CodeAthTypeDto>>(codeAthTypes));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutCodeAthType([FromForm]int key, [FromForm]string values)
        {
            if (key <= 0) 
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values)) 
                return BadRequest("Value cannot be null or empty");

            var codeAthType = await _farmAppContext.CodeAthTypes.FirstOrDefaultAsync(c => c.Id == key);
            if (codeAthType == null)
                return NotFound($"Cannot be found Ath with key {key}");

            JsonConvert.PopulateObject(values, codeAthType);
            await _farmAppContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<CodeAthTypeDto>> PostCodeAthType([FromForm]string values)
        {
            if (string.IsNullOrEmpty(values)) 
                return BadRequest("Value cannot be null or empty");

            var codeAthType = new CodeAthType();
            JsonConvert.PopulateObject(values, codeAthType);

            await _farmAppContext.AddAsync(codeAthType);
            await _farmAppContext.SaveChangesAsync();

            return Ok(_mapper.Map<CodeAthTypeDto>(codeAthType));
        }

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> DeleteCodeAthType([FromForm]int key)
        {
            if (key <= 0) 
                return BadRequest("Key cannot be <= 0");

            var codeAthType = await _farmAppContext.CodeAthTypes.FindAsync(key);

            if (codeAthType == null)
                return NotFound($"Not found Code with key {key}");

            codeAthType.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync();

            return Ok();
        }
    }
}
