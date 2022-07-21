using BikeShop.Data;
using BikeShop.Extensions;
using BikeShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BikeShop.Controllers
{
    public class CartController : Controller
    {

        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            Cart cart = new State(HttpContext).LoadCart();
            Dictionary<Product, int> CartItems = new Dictionary<Product, int>();

            foreach(var item in cart)
            {
                int proId = item.Key;
                Product product = _context.Products.Find(proId);
                if(product != null)
                {
                    CartItems[product] = item.Value;
                }
            }

            return View(CartItems);
        }

        public IActionResult AddToCart(int proId)
        {
            Product product = _context.Products.Find(proId);
            if(product == null)
            {
                return NotFound();
            }

            State httpState = new State(HttpContext);
            Cart cart = httpState.LoadCart();
            cart.PushItem(proId);
            httpState.SaveCart(cart);

            return Redirect("/Home");

        }

		[HttpPost]
        public IActionResult UpdateProductToCart(int proId, int quantity)
		{
            State httpState = new State(HttpContext);
            Cart cart = httpState.LoadCart();
            if(quantity == 0)
			{
                return RemoveProductFromCart(proId);
            }

            cart[proId] = quantity;
            httpState.SaveCart(cart);

            return RedirectToAction("Index");
		}

		[HttpPost]
        public IActionResult RemoveProductFromCart(int proId)
		{
            State httpState = new State(HttpContext);
            Cart cart = httpState.LoadCart();
            cart.Remove(proId);
            httpState.SaveCart(cart);

            return RedirectToAction("Index");
        }

    }
}
