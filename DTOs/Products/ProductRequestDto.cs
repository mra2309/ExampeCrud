using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExampeCrud.DTOs.Products
{
    public class ProductRequestDto
    {
        // public int Id { get; set;}
        [Required]
        [MinLength(3)]
        public string Name { get; set;} = string.Empty;

        [Required]
        public decimal Price {get;set;}

        public ExampleCrud.Model.Product ToEntity()
        {
            return new ExampleCrud.Model.Product
            {
                Name = this.Name,
                Price = this.Price
            };
        }
    }
}