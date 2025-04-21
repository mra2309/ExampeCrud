using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExampeCrud.DTOs.Users
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(100)]
        public string Fullname {set; get;} = String.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "invalid format email")]
        public string Email {set; get; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Password {set; get;} = String.Empty;
    }
}