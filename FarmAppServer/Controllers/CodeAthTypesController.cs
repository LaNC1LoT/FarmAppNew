using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models.CodeAthTypes;
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
        public async Task<ActionResult<IEnumerable<CodeAthTypeDto>>> GetAsync(CancellationToken cancellationToken = default)
        {
            var codeAthTypes = await _farmAppContext.CodeAthTypes.Where(w => w.IsDeleted == false).AsNoTracking().ToListAsync(cancellationToken);
            if (!codeAthTypes.Any())
                return BadRequest("CodeAthTypes not found");

            return Ok(_mapper.Map<IEnumerable<CodeAthTypeDto>>(codeAthTypes));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0) 
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values)) 
                return BadRequest("Value cannot be null or empty");

            var codeAthType = await _farmAppContext.CodeAthTypes.FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
            if (codeAthType == null)
                return BadRequest($"Cannot be found CodeAthType with key {key}");

            JsonConvert.PopulateObject(values, codeAthType);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<CodeAthTypeDto>> PostAsync([FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var codeAthType = new CodeAthType();
            JsonConvert.PopulateObject(values, codeAthType);

            if (codeAthType.CodeAthId == 0)
                codeAthType.CodeAthId = null;

            await _farmAppContext.AddAsync(codeAthType, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok(_mapper.Map<CodeAthTypeDto>(codeAthType));
        }

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> DeleteAsync([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0) 
                return BadRequest("Key cannot be <= 0");

            var codeAthType = await _farmAppContext.CodeAthTypes.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (codeAthType == null)
                return BadRequest($"Not found CodeAthType with key {key}");

            codeAthType.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
