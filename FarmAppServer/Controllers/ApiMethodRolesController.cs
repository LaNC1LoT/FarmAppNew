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
using FarmAppServer.Models.Drugs;
using FarmAppServer.Services;
using FarmAppServer.Services.Paging;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMethodRolesController : ControllerBase
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;
        private readonly IApiMethodRoleService _apiMethodRoleService;

        public ApiMethodRolesController(FarmAppContext context, IMapper mapper, IApiMethodRoleService apiMethodRoleService)
        {
            _context = context;
            _mapper = mapper;
            _apiMethodRoleService = apiMethodRoleService;
        }

        // GET: api/ApiMethodRoles
        [HttpGet]
        public ActionResult<IEnumerable<ApiMethodRoleDto>> GetApiMethodRoles([FromQuery]int page = 1, [FromQuery]int pageSize = 25)
        {
            var apiMethodRoles = _context.ApiMethodRoles;
            var model = _mapper.ProjectTo<ApiMethodRoleDto>(apiMethodRoles);

            if (model == null) return NotFound("ApiMethodRoles not found");

            var query = model.GetPaged(page, pageSize);

            return Ok(query);
        }

        // GET: api/ApiMethodRoles/5
        [HttpGet("ApiMethodRoleById")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<ApiMethodRoleDto>> GetApiMethodRole([FromForm]int key)
        {
            if (key <= 0) return BadRequest("Key must be > 0");

            var apiMethodRole = _context.Drugs.Where(x => x.Id == key && x.IsDeleted == false);
            var data = await _mapper.ProjectTo<ApiMethodRoleDto>(apiMethodRole).FirstOrDefaultAsync();

            if (data == null || data.IsDeleted)
                return NotFound("ApiMethodRole not found");

            return Ok(apiMethodRole);
        }

        // PUT: api/ApiMethodRoles/5
        [HttpPut]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PutApiMethodRole([FromForm]int key, [FromForm]string values)
        {
            if (key <= 0) return BadRequest("key must be > 0");
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");

            if (key <= 0) return BadRequest("key must be > 0");
            
            var updated = await _apiMethodRoleService.UpdateApiMethodASync(key, values);

            if (updated) return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "ApiMethod/Role not found or nothing to update"
            });
        }

        // POST: api/ApiMethodRoles
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PostApiMethodRole([FromForm]string values)
        {
            if (string.IsNullOrEmpty(values)) return BadRequest("Value cannot be null or empty");

            var request = await _apiMethodRoleService.PostApiMethodRoleAsync(values);

            if (request)
                return Ok();

            return BadRequest("ApiMethod/Role not found or already have that role");
        }

        // DELETE: api/ApiMethodRoles/5
        [HttpDelete]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> DeleteApiMethodRole([FromForm]int key)
        {
            if (key <= 0) return BadRequest("key must be > 0");

            var deleted = await _apiMethodRoleService.DeleteApiMethodRoleAsync(key);

            if (deleted) return Ok();

            return NotFound("ApiMethodRole not found");
        }
    }
}
