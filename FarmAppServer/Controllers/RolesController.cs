using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Helpers;
using FarmAppServer.Models;
using FarmAppServer.Models.Roles;
using FarmAppServer.Services;
using Microsoft.AspNetCore.Authorization;

namespace FarmAppServer.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public RolesController(FarmAppContext context,IMapper mapper, IRoleService roleService)
        {
            _context = context;
            _mapper = mapper;
            _roleService = roleService;
        }

        // GET: api/Roles
        [HttpGet]
        public ActionResult<IEnumerable<RoleDto>> GetRoles()
        {
            //return await _context.Roles.Where(x => x.IsDeleted == false).ToListAsync();
            var roles = _context.Roles.AsQueryable();
            var model = _mapper.ProjectTo<RoleDto>(roles);

            return Ok(model);
        }

        // GET: api/Roles/5
        [HttpGet("RoleById")]
        public async Task<ActionResult<RoleDto>> GetRole([FromQuery]int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return BadRequest(new ResponseBody()
                {
                    Header = "Error",
                    Result = "Role no found"
                });
            }

            if (role.IsDeleted == true)
            {
                return BadRequest(new ResponseBody()
                {
                    Header = "Error",
                    Result = "Role no found"
                });
            }

            var model = _mapper.Map<RoleDto>(role);
            return model;
        }

        // PUT: api/Roles/5
        [HttpPut]
        public async Task<IActionResult> PutRole([FromQuery]int id, [FromBody]UpdateRoleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updated = await _roleService.UpdateRoleAsync(id, model);

            if (updated)
                return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "user not found"
            });
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody]PostRoleDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            if (RoleExists(model.RoleName))
                return BadRequest("Username \"" + model.RoleName + "\" is already taken");

            var role = _mapper.Map<Role>(model);
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return Created("PostRole", _mapper.Map<RoleDto>(role));
        }

        // DELETE: api/Roles/5
        [HttpDelete]
        public async Task<ActionResult<Role>> DeleteRole([FromQuery]int id)
        {
            var role = await _context.Roles.FindAsync(id);
           
            if (role == null)
                return NotFound();

            role.IsDeleted = true;
            //_context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return role;
        }

        private bool RoleExists(string roleName)
        {
            return !string.IsNullOrEmpty(roleName) && _context.Roles.Any(e => e.RoleName == roleName && e.IsDeleted == false);
        }

        [HttpGet("SearchRole")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> Search([FromQuery]string param)
        {
            try
            {
                var query = _roleService.RoleSearch(param);
                var result = await _mapper.ProjectTo<RoleDto>(query).ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseBody()
                {
                    Header = "Error",
                    Result = $"{e.Message}"
                });
            }
        }
    }
}
