using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShop.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "UnitPrice must be > 0!")]
        public double UnitPrice { get; set; }


        public string ImageURL { get; set; } = "";

        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
