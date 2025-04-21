using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampeCrud.Models;

namespace ExampeCrud.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Users user);
    }
}