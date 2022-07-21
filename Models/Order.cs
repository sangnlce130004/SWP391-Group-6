using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShop.Models
{
	[Table("Orders")]
    public class Order
    {
		[Key]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string FullName { get; set; }

        public string Address { get; set; }

        public double TotalCost { get; set; }
        
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public bool IsComplete { get; set; } = false;

        public IList<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
