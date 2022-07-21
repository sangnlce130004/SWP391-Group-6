using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShop.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string UserName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string FullName { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 8)]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        
        [Required]
        public bool IsAdmin { get; set; } = false;

    }
}
