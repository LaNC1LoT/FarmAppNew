using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
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
    public class DosageFormsController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public DosageFormsController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DosageFormDto>>> GetAsync(CancellationToken cancellationToken = default)
        {
            var dosageFormTypes = await _farmAppContext.DosageFormTypes.Where(w => w.IsDeleted == false).AsNoTracking().ToListAsync(cancellationToken);
            if (!dosageFormTypes.Any())
                return BadRequest("DosageForms not found");

            return Ok(_mapper.Map<IEnumerable<DosageFormDto>>(dosageFormTypes));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var dosageFormType = await _farmAppContext.DosageFormTypes.FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
            if (dosageFormType == null)
                return BadRequest($"Cannot be found Ath with key {key}");

            JsonConvert.PopulateObject(values, dosageFormType);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<DosageFormDto>> PostAsync([FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var dosageFormType = new DosageFormType();
            JsonConvert.PopulateObject(values, dosageFormType);

            await _farmAppContext.AddAsync(dosageFormType, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok(_mapper.Map<DosageFormDto>(dosageFormType));
        }

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> DeleteAsync([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key cannot be <= 0");

            var dosageFormType = await _farmAppContext.DosageFormTypes.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (dosageFormType == null)
                return BadRequest($"Not found Code with key {key}");

            dosageFormType.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
