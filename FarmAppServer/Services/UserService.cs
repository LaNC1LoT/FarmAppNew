using System;
using System.Collections.Generic;
using System.Linq;
using FarmApp.Domain.Core.Entity;
using FarmAppServer.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings;
using System.Threading.Tasks;
using AutoMapper;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace FarmAppServer.Services
{
    public interface IUserService
    {
        Task<IQueryable<User>> AuthenticateUserAsync(string username, string password);
        IQueryable GetAllUsers();
        IQueryable GetUserById(int id);
        Task<User> CreateUserAsync(User user, string password);
        Task<bool> UpdateUserAsync(int id, UpdateModelDto model);
        Task<bool> DeleteUserAsync(int id);
        IQueryable UsersByRoleAsync(string role);
        IQueryable UserSearchAsync(string param);
        IQueryable IsEnabled(bool isChecked);
    }
    
    public class UserService : IUserService
    {
        private readonly FarmAppContext _context;
        private readonly IMapper _mapper;

        public UserService(FarmAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IQueryable<User>> AuthenticateUserAsync(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var users = _context.Users.Where(x => x.Login == login && x.IsDeleted == false);
            var user = await users.FirstOrDefaultAsync();
            
            if (user==null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return users;
        }

        public IQueryable GetAllUsers()
        {
            return _context.Users;//.Where(x => x.IsDeleted == false);
        }

        public IQueryable GetUserById(int id)
        {
            //return await _context.Users.FindAsync(id);
            return _context.Users.Where(x => x.Id == id && x.IsDeleted == false);
            
        }

        public async Task<User> CreateUserAsync(User user, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new AppException("Password is required");
            
            if (_context.Users.Any(x => x.Login == user.Login & x.IsDeleted == false))
                throw new AppException("Username \"" + user.Login + "\" is already taken");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateModelDto model)
        {
            var user = await _context.Users.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefaultAsync();

            if (user == null) return false;

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                CreatePasswordHash(model.Password, out var passwordHash, out var passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _mapper.Map(model, user);
            _context.Update(user);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;


            //var user = _context.Users.Find(userParam.Id);
            //var user = _context.Users.FirstOrDefault(x => x.Id == userParam.Id && x.IsDeleted == false);

            //if (user == null)
            //    throw new AppException("User not found");

            //if (!string.IsNullOrWhiteSpace(userParam.UserName) && userParam.UserName != user.UserName)
            //{
            //    if (_context.Users.Any(x => x.UserName == userParam.UserName))
            //        throw new AppException("UserNAme " + userParam.UserName + " is already taken");

            //    user.UserName = userParam.UserName;
            //}

            ////update
            //if (!string.IsNullOrWhiteSpace(userParam.FirstName)) user.FirstName = userParam.FirstName;
            //if (!string.IsNullOrWhiteSpace(userParam.LastName)) user.LastName = userParam.LastName;
            //if (!string.IsNullOrWhiteSpace(userParam.Login)) user.Login = userParam.Login;

            //// update password if provided
            //if (!string.IsNullOrWhiteSpace(password))
            //{
            //    CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            //    user.PasswordHash = passwordHash;
            //    user.PasswordSalt = passwordSalt;
            //}

            //_context.Entry(user).State = EntityState.Modified;
            //_context.SaveChanges();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null || user.IsDeleted == true) return false;
            
            user.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }


        //user Filter By Role  //⦁	Combobox -> По ролям (Админ, пользователь …)
        public IQueryable UsersByRoleAsync(string role)
        {
            try
            {
                var users = _context.Users.Where(r => r.Role.RoleName == role);
                return users;

            }
            catch (Exception e)
            {
                throw new AppException(e.Message);
            }
        }


        //user search by //⦁	TextBox  -> По Login, UserName
        public IQueryable UserSearchAsync(string param)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentException("Value cannot be null or empty.", nameof(param));

            var query = _context.Users
                .Where(x => x.FirstName.Contains(param) || x.LastName.Contains(param) || x.Role.RoleName.Contains(param)
                || x.UserName.Contains(param) || x.Login.Contains(param)) ;

            return query;
        }

        //check is enabled
        public IQueryable IsEnabled(bool isChecked)
        {
            if (isChecked)
            {
                var users = _context.Users.Where(x => x.IsDeleted == true);
                return users;
            }
            else
            {
                var users = _context.Users.Where(x => x.IsDeleted == false);
                return users;
            }
        }

        //private helper method
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
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
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}