using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;
using Microsoft.EntityFrameworkCore;

namespace FarmAppServer.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMethodsController : ControllerBase
    {
        private readonly FarmAppContext _context;
        private readonly IApiMethodService _apiMethodService;
        private readonly IMapper _mapper;

        public ApiMethodsController(FarmAppContext context, IApiMethodService apiMethodService, IMapper mapper)
        {
            _context = context;
            _apiMethodService = apiMethodService;
            _mapper = mapper;
        }

        // GET: api/ApiMethods
        [HttpGet]
        public ActionResult<IEnumerable<ApiMethodDto>> GetApiMethods([FromQuery]int page = 1, [FromQuery]int pageSize = 20)
        {
            var apiMethods = _context.ApiMethods;
            var model = _mapper.ProjectTo<ApiMethodDto>(apiMethods);

            if (model == null) return NotFound("ApiMethods not found");

            var query = model.GetPaged(page, pageSize);

            return Ok(query);
        }

        // GET: api/ApiMethods/5
        [HttpGet("ApiMethodById")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<ApiMethodDto>> GetApiMethod([FromForm]int key)
        {
            var apiMethod = _context.ApiMethods.Where(x => x.Id == key && x.IsDeleted == false);
            var data = await _mapper.ProjectTo<ApiMethodDto>(apiMethod).FirstOrDefaultAsync();

            if (data == null || data.IsDeleted)
                return NotFound("ApiMethod not found");

            return Ok(apiMethod);
        }

        // PUT: api/ApiMethods/5
        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> UpdateApiMethod([FromForm]int key, [FromForm]string values)
        {
            if (key <= 0) return BadRequest("key must be > 0");
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");

            var updated = await _apiMethodService.UpdateApiMethodAsync(key, values);

            if (updated) return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "ApiMethod not found or nothing to update"
            });
        }

        // POST: api/ApiMethods
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<ApiMethod>> PostApiMethod([FromForm]string values)
        {
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");

            var request = await _apiMethodService.PostApiMethodAsync(values);

            if (request)
                return Ok();

            return BadRequest("ApiMethod is already taken");
        }

        // DELETE: api/ApiMethods/5
        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<ApiMethod>> DeleteApiMethod([FromForm]int key)
        {
            if (key <= 0) return BadRequest("key must be > 0");

            var deleted = await _apiMethodService.DeleteApiMethodAsync(key);

            if (deleted) return Ok();

            return NotFound("ApiMethod not found");
        }
    }
}
