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
    public class ApiMethodRolesController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public ApiMethodRolesController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiMethodRoleDto>>> GetAsync(CancellationToken cancellationToken = default)
        {
            var apiMethodRoles = await _farmAppContext.ApiMethodRoles.Include(a => a.ApiMethod).Include(r => r.Role).AsNoTracking().ToListAsync(cancellationToken);

            if (!apiMethodRoles.Any())
                return BadRequest("ApiMethodRoles not found");

            return Ok(_mapper.Map<IEnumerable<ApiMethodRoleDto>>(apiMethodRoles));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var apiMethodRole = await _farmAppContext.ApiMethodRoles.FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
            if (apiMethodRole == null)
                return BadRequest($"Cannot be found ApiMethodRole with key {key}");

            JsonConvert.PopulateObject(values, apiMethodRole);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<ApiMethodRoleDto>> PostAsync([FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var apiMethodRole = new ApiMethodRole();
            JsonConvert.PopulateObject(values, apiMethodRole);

            await _farmAppContext.AddAsync(apiMethodRole, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok(_mapper.Map<ApiMethodRoleDto>(apiMethodRole));
        }

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> DeleteAsync([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key cannot be <= 0");

            var apiMethodRole = await _farmAppContext.ApiMethodRoles.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (apiMethodRole == null)
                return BadRequest($"Not found ApiMethodRole with key {key}");

            apiMethodRole.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
