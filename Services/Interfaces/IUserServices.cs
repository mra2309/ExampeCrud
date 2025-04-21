using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampeCrud.DTOs.Users;
using ExampeCrud.Models;

namespace ExampeCrud.Services.Interfaces
{
    public interface IUserServices
    {
        Task<object> LoginAsync(LoginDto request);
        Task<object> RegisterAsync(RegisterDto request);
        Task<object> GetProfileAsync();
    }
}