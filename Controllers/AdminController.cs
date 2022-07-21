using BikeShop.Data;
using BikeShop.Extensions;
using BikeShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.Controllers
{
	public class AdminController : Controller
	{

		private AppDbContext _context;

		public AdminController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			State httpState = new State(HttpContext);
			Customer customer = _context.Customers.Find(httpState.LoginUserName());
			if(!(customer != null && customer.IsAdmin))
			{
				return Redirect("/Home");
			}
			
			return View();

		}
	}
}
