using BikeShop.Data;
using BikeShop.Extensions;
using BikeShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search)
        {

            var products = _context.Products.ToList();

            if(search != null)
            {
                products = products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
                ViewBag.SearchKey = search;
            }

            Cart cart = new State(HttpContext).LoadCart();
            ViewBag.CartSize = cart.GetSize();

            return View(products);
        }

        public IActionResult Logout()
        {
            State userState = new State(HttpContext);
            userState.Logout();

            return Redirect("/Home");
        }

    }
}
