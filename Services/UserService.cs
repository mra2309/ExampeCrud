using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ExampeCrud.DTOs.Users;
using ExampeCrud.Models;
using ExampeCrud.Services.Interfaces;
using ExampeCrud.Utils;
using ExampleCrud.Data;
using Microsoft.EntityFrameworkCore;

namespace ExampeCrud.Services
{
    public class UserService : IUserServices
    {
        private readonly AppDbContext _contex;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(AppDbContext contex,IHttpContextAccessor httpContextAccessor,IJwtService jwtService)
        {
            _contex = contex;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object> GetProfileAsync()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(string.IsNullOrEmpty(userIdClaim))
            {
                return ApiResponseHelper.Failed<List<Users>>("Unautorized");
            }

            int userId = int.Parse(userIdClaim);

            var user = await _contex.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null)
            {
                return ApiResponseHelper.Failed<List<Users>>("User Not Found!");    
            }

            return ApiResponseHelper.Success(user);
        }

        public async Task<object> LoginAsync(LoginDto request)
        {
            var user = await _contex.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if(user == null || user.Password != HashPasswordHelper.HashPassword(request.Password))
            {
                return ApiResponseHelper.Failed<List<Users>>("Invalid Email or password");
            } 

            var token = _jwtService.GenerateToken(user);
            var response = new
            {
                user.Id,
                user.Fullname,
                user.Email,
                Token = token
            };

            return ApiResponseHelper.Success(response);
        }

        public async Task<object> RegisterAsync(RegisterDto request)
        {
            /*chek uniq email*/ 
            var exitingUser  = await _contex.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if(exitingUser != null )
            {
                return ApiResponseHelper.Failed<List<Users>>("email alredy exist");
            }

            var newUser = new Users
            {
                Fullname = request.Fullname,
                Email = request.Email,
                Password = HashPasswordHelper.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _contex.Users.AddAsync(newUser);
            await _contex.SaveChangesAsync();

            return ApiResponseHelper.Success(request);
        }
    }
}