using BikeShop.Data;
using BikeShop.Extensions;
using BikeShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BikeShop.Controllers
{
	public class OrderController : Controller
	{

		private AppDbContext _context;

		public OrderController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Create()
		{
			State httpState = new State(HttpContext);
			if (!httpState.IsAuthenticated())
			{
				return Redirect("/Home");
			}

			Customer customer = _context.Customers.Find(httpState.LoginUserName());
			if(customer == null)
			{
				return Unauthorized();
			}

			Cart cart = httpState.LoadCart();
			if(cart.GetSize() == 0)
			{
				return Content("No have any item in cart!");
			}

			Dictionary<Product, int> CartItems = new Dictionary<Product, int>();

			double totalCost = 0;
			foreach (var item in cart)
			{
				int proId = item.Key;
				Product product = _context.Products.Find(proId);
				if (product != null)
				{
					CartItems[product] = item.Value;
					totalCost += product.UnitPrice * item.Value;
				}
			}

			Order order = new Order()
			{
				UserName = customer.UserName,
				FullName = customer.FullName,

				Address = customer.Address,
				TotalCost = totalCost,
			};

			foreach(var item in CartItems)
			{
				Product product = item.Key;

				OrderDetail orderdetail = new OrderDetail()
				{
					ProductId = product.Id,
					quantity = item.Value,
					UnitPrice = product.UnitPrice,
				};

				order.OrderDetails.Add(orderdetail);

			}

			_context.Orders.Add(order);
			_context.SaveChanges();

			httpState.RemoveSession("cart");

			return RedirectToAction("Details", new { id=order.Id });
		}


		public IActionResult Details(int id)
		{
			Order order = _context.Orders.Find(id);
			if (order == null) return NotFound();

			State httpState = new State(HttpContext);
			if(order.UserName != httpState.LoginUserName())
			{
				return Redirect("/Home");
			}

			order.OrderDetails = _context.OrderDetails.Include(od => od.Product).Where(od => od.OrderId == order.Id).ToList();

			return View(order);

		}


		public IActionResult OrderHistory()
		{
			State httpState = new State(HttpContext);
			List<Order> orders = _context.Orders.Where(od => od.UserName == httpState.LoginUserName()).ToList();


			return View(orders);
		}

	}
}
