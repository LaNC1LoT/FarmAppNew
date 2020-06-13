using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmAppServer.Helpers;
using FarmAppServer.Models;
using FarmAppServer.Models.Users;
using FarmAppServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FarmAppServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthResponseDto>> Authenticate([FromBody]AuthenticateModelDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var model = JsonConvert.DeserializeObject<AuthenticateModelDto>(requestBody.Param);

            if (model == null) throw new ArgumentNullException(nameof(model));

            var users = await _userService.AuthenticateUserAsync(model.Login, model.Password);

            if (users == null)
                return BadRequest(new ResponseBody { Result = "Login or password is incorrect", Header = "Authenticate" });

            var user = await users.SingleOrDefaultAsync();

            if (user == null)
                return BadRequest(new ResponseBody { Result = "Login or password is incorrect", Header = "Authenticate" });

            if (user.IsDeleted ?? true)
                return BadRequest(new ResponseBody { Result = "Пользователь заблокирован!", Header = "Authenticate" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("RoleId", user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token

            var response = await _mapper.ProjectTo<AuthResponseDto>(users).FirstOrDefaultAsync();
            response.Token = tokenString;

            return Ok(response);
        }

        [AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]RegisterModelDto model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                await _userService.CreateUserAsync(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModelDto>>> GetAll([FromQuery]int page = 1, [FromQuery]int pageSize = 25)
        {
            try
            {
                var users = _userService.GetAllUsers();
                var model = await _mapper.ProjectTo<UserModelDto>(users).ToListAsync();
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(new {e.Message, e.StackTrace});
            }
        }

        [HttpGet("UserById")]
        public async Task<ActionResult<UserModelDto>> GetById([FromQuery]int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                var model = await _mapper.ProjectTo<UserModelDto>(user).FirstOrDefaultAsync();
                
                if (model == null) return NotFound("User not found");
                
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(new {e.Message, e.StackTrace});
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery]int id, [FromBody]UpdateModelDto userToUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updated = await _userService.UpdateUserAsync(id, userToUpdate);

            if (updated)
                return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "user not found"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            if (await _userService.DeleteUserAsync(id))
                return Ok();

            return NotFound(new ResponseBody()
            {
                Header = "Error",
                Result = "User not found"
            });
        }



        //use User service UserFilterByRole
        //⦁	Combobox -> По ролям (Админ, пользователь …)
        [HttpGet]
        [NonAction]
        [Route("RoleName")]
        public async Task<ActionResult<IEnumerable<UserFilterByRoleDto>>> GetUsersByRoleAsync([FromQuery]string role)
        {
            if (string.IsNullOrEmpty(role))
                return BadRequest(new
                {
                    Message = $"Value cannot be null or empty.  {nameof(role)}"
                });

            var users = _userService.UsersByRoleAsync(role);
            var model = await _mapper.ProjectTo<UserFilterByRoleDto>(users).ToListAsync();
            return Ok(model);
        }

        //search by Login or UserName
        //⦁	TextBox  -> По Login, UserName
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult<UserFilterByRoleDto>> SearchUser([FromQuery] string param)
        {
            if (string.IsNullOrEmpty(param))
                return BadRequest(new ResponseBody { Result = "param cannot be null or empty", Header = "Search" });

            var users = _userService.UserSearchAsync(param);
            var model = await _mapper.ProjectTo<UserFilterByRoleDto>(users).ToListAsync();

            if (model.Count == 0)
                return NotFound(new ResponseBody()
                {
                    Header = "Error",
                    Result = "User not found"
                });

            return Ok(model);
        }

        //checkbox switch
        [HttpGet]
        [NonAction]
        [Route("CheckBox")]
        public async Task<ActionResult<IEnumerable<UserFilterByRoleDto>>> CheckBoxFilter([FromQuery] bool isChecked)
        {
            var users = _userService.IsEnabled(isChecked);
            var model = await _mapper.ProjectTo<UserFilterByRoleDto>(users).ToListAsync();

            return model;
        }
    }
}
