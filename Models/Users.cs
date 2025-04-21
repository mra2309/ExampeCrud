using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ExampeCrud.Models
{
    public class Users
    {
        public int Id {set; get;}

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

        [Required]
        public DateTime CreatedAt {set; get;} = DateTime.UtcNow;
        public DateTime? UpdatedAt {set; get;}
        public DateTime? DeletedAt {set; get;}
    }
}