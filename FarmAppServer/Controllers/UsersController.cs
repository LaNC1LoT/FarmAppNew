using AutoMapper;
using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Helpers;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FarmAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FarmAppContext _farmAppContext;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public UsersController(FarmAppContext farmAppContext, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _farmAppContext = farmAppContext;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult<UserResponseDto>> AuthenticateAsync(UserAuntificationDto model, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Не заполнен логин или пароль!");

            var user = await _farmAppContext.Users.Include(i => i.Role).FirstOrDefaultAsync(f => f.Login == model.Login, cancellationToken);
            if (user == null)
                return BadRequest("Пользователь не найден!");

            if (user.IsDeleted == true)
                return BadRequest("Пользователь заблокирован!");

            if (user.Role.IsDeleted == true)
                return BadRequest("Роль заблокирована!");

            if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Неверный логин или пароль!");

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


            var response = _mapper.Map<UserResponseDto>(user);
            response.Token = tokenString;

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterDto model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest("Все поля обязательны к заполнению!");

            var user = await _farmAppContext.Users.FirstOrDefaultAsync(f => f.Login == model.Login);

            if (user != null)
                return BadRequest("Пользователь с таким Login занят!");

            CreatePasswordHash(model.Password, out var passwordHash, out var passwordSalt);

            var newUser = new User
            {
                Login = model.Login,
                UserName = model.FirstName + " " + model.LastName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _farmAppContext.Users.AddAsync(user, cancellationToken);
            await _farmAppContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var users = await _farmAppContext.Users.Include(i => i.Role).AsNoTracking().ToListAsync(cancellationToken);

            if (!users.Any())
                return BadRequest("Users not found!");

            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm]int key, [FromForm]string values, CancellationToken cancellationToken = default)
        {
            if (key <= 0)
                return BadRequest("Key must be > 0");
            if (string.IsNullOrEmpty(values))
                return BadRequest("Value cannot be null or empty");

            var user = await _farmAppContext.Users.FirstOrDefaultAsync(x => x.Id == key);
            if (user == null)
                return BadRequest($"Cannot be found user with key {key}");

            UserDto userDto = new UserDto();
            JsonConvert.PopulateObject(values, userDto);

            CreatePasswordHash(userDto.Password, out var passwordHash, out var passwordSalt);

            user = _mapper.Map<User>(userDto);
            user.UserName = user.FirstName + " " + user.LastName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _farmAppContext.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}
