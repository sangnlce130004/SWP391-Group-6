using BikeShop.Data;
using BikeShop.Extensions;
using BikeShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BikeShop.Controllers
{
    public class LoginController : Controller
    {

        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            State userState = new State(HttpContext);

            if(userState.IsAuthenticated())
            {
                return Redirect("Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            Customer customer = _context.Customers.Where(cus => cus.UserName == username && cus.Password == password).FirstOrDefault();
            if(customer != null)
            {
                State userState = new State(HttpContext);
                userState.AddSession("loginKey", customer.UserName);
                userState.AddSession("IsAdmin", customer.IsAdmin.ToString());
                if(customer.IsAdmin)
				{
                    return Redirect("/Admin");
				}

                return Redirect("/Home");
            }

            ViewBag.ErrorMessage = "UserName or Password incorrect!";
            return View();

        }
    }
}
