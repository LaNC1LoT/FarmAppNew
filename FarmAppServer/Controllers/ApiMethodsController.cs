using AutoMapper;
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
    public class ApiMethodsController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;

        public ApiMethodsController(FarmAppContext farmAppContext, IMapper mapper)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiMethodDto>>> GetAsync(CancellationToken cancellationToken = default)
        {
            var apiMethods = await _farmAppContext.ApiMethods.AsNoTracking().ToListAsync(cancellationToken);
            if (!apiMethods.Any())
                return BadRequest("ApiMethods not found");

            return Ok(_mapper.Map<IEnumerable<ApiMethodDto>>(apiMethods));
        }

        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var apiMethod = await _farmAppContext.ApiMethods.FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
            if (apiMethod == null)
                return BadRequest($"Cannot be found ApiMethod with key {key}");

            JsonConvert.PopulateObject(values, apiMethod);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        //[HttpPost]
        //[Consumes("application/x-www-form-urlencoded")]
        //public async Task<ActionResult<RoleDto>> PostAsync([FromForm]string values, CancellationToken cancellationToken = default)
        //{
        //    if (string.IsNullOrEmpty(values))
        //        return BadRequest("Value cannot be null or empty");

        //    var apiMethod = new ApiMethod();
        //    JsonConvert.PopulateObject(values, apiMethod);

        //    await _farmAppContext.AddAsync(apiMethod, cancellationToken);
        //    await _farmAppContext.SaveChangesAsync(cancellationToken);

        //    return Ok(_mapper.Map<RoleDto>(apiMethod));
        //}

        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> DeleteAsync([FromForm]int key, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key cannot be <= 0");

            var apiMethod = await _farmAppContext.ApiMethods.FirstOrDefaultAsync(f => f.Id == key, cancellationToken);
            if (apiMethod == null)
                return BadRequest($"Not found ApiMethod with key {key}");

            apiMethod.IsDeleted = true;
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
