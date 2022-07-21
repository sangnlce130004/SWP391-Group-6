using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeShop.Data;
using BikeShop.Models;
using BikeShop.Extensions;

namespace BikeShop.Controllers
{
    public class OrderManagementController : Controller
    {
        private readonly AppDbContext _context;

        public OrderManagementController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderManagement
        public async Task<IActionResult> Index()
        {
            State httpState = new State(HttpContext);
            Customer customer = _context.Customers.Find(httpState.LoginUserName());
            if (!(customer != null && customer.IsAdmin))
            {
                return Redirect("/Home");
            }

            return View(await _context.Orders.ToListAsync());
        }

        // GET: OrderManagement/Details/5
        public async Task<IActionResult> Details(int id)
        {
            State httpState = new State(HttpContext);
            Customer customer = _context.Customers.Find(httpState.LoginUserName());
            if (!(customer != null && customer.IsAdmin))
            {
                return Redirect("/Home");
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderDetails = _context.OrderDetails.Include(od => od.Product).Where(od => od.OrderId == id).ToList();

            return View(order);
        }

        // GET: OrderManagement/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            State httpState = new State(HttpContext);
            Customer customer = _context.Customers.Find(httpState.LoginUserName());
            if (!(customer != null && customer.IsAdmin))
            {
                return Redirect("/Home");
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.IsComplete = !order.IsComplete;
            _context.Orders.Update(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: OrderManagement/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            State httpState = new State(HttpContext);
            Customer customer = _context.Customers.Find(httpState.LoginUserName());
            if (!(customer != null && customer.IsAdmin))
            {
                return Redirect("/Home");
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderDetails = _context.OrderDetails.Include(od => od.Product).Where(od => od.OrderId == id).ToList();

            return View(order);
        }

        // POST: OrderManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
