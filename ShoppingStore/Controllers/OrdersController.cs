using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.Common.Extensions;
using ShoppingStore.Models;
using ShoppingStore.Repository;
using ShoppingStore.Repository.Interfaces;
using System.Security.Claims;

namespace ShoppingStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [Authorize(Roles = "Administrator,Customer")]
        public async Task<IActionResult> Index()
        {
            var userId = User.IsInRole("Administrator") ? string.Empty : User.FindFirstValue(ClaimTypes.NameIdentifier); ;
            return View(await _orderRepository.GetOrdersAsync(userId));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var orders = await _orderRepository.GetOrderAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var order = HttpContext.Session.GetObjectFromJson<Orders>("Order");
            order.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _orderRepository.AddOrder(order, order.UserID);
            TempData["Message"] = "Order Added Successfully";
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _orderRepository.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
