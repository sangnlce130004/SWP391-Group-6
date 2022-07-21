using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShop.Models
{
	[Table("OrderDetails")]
    public class OrderDetail
    {
		[Key]
        public int Id { get; set; }

		[ForeignKey("Order")]
        public int OrderId { get; set; }

		[ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public double UnitPrice { get; set; }
        public int quantity { get; set; }

    }
}
