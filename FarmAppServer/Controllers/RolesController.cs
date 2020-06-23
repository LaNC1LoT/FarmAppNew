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
    public class RolesController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public RolesController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> RolesAsync(CancellationToken cancellationToken = default)
        {
            var roles = await _farmAppContext.Roles.Where(w => w.IsDeleted == false).AsNoTracking().ToListAsync(cancellationToken);
            if (!roles.Any())
                return BadRequest("CodeAthType not found");

            return Ok(_mapper.Map<IEnumerable<RoleDto>>(roles));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> RoleAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var role = await _farmAppContext.Roles.FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
            if (role == null)
                return BadRequest($"Cannot be found role with key {key}");

            JsonConvert.PopulateObject(values, role);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<RoleDto>> RoleAsync([FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var role = new Role();
            JsonConvert.PopulateObject(values, role);

            await _farmAppContext.AddAsync(role, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok(_mapper.Map<RoleDto>(role));
        }

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> RoleAsync([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key cannot be <= 0");

            var role = await _farmAppContext.Roles.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (role == null)
                return BadRequest($"Not found Code with key {key}");

            role.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
