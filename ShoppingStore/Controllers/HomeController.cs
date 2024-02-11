using Microsoft.AspNetCore.Mvc;
using ShoppingStore.Data;
using ShoppingStore.Models;
using ShoppingStore.Repository.Interfaces;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol;
using ShoppingStore.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using ShoppingStore.Common;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemsRepository _itemsService;
        private readonly ShoppingStoreDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger,IItemsRepository itemsService,ShoppingStoreDBContext context,
            RoleManager<IdentityRole> roleManager
            )
        {
            _logger = logger;
            _itemsService = itemsService;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int pageSize = 5, [FromQuery] string filter = "")
        {
            var order = HttpContext.Session.GetObjectFromJson<Orders>("Order");

            if (order == null)
            {
                order = new Orders();
                HttpContext.Session.SetObjectAsJson("Order", order);
            }

            var pagedItems = await _itemsService.GetAllItems(page, pageSize, filter);

            ViewBag.counter = (order?.Items != null) ? order?.Items?.Count : 0;
            ViewBag.Basket = order;
            ViewBag.Alert = TempData["Message"] as string;
            ViewBag.AlertError = TempData["ErrorMessage"] as string;

            return View(pagedItems);
        }
        public IActionResult EditItems(Guid Id)
        {

            var order = HttpContext.Session.GetObjectFromJson<Orders>("Order");
            if(order != null && order.Items != null)
            {
                var finditem = order.Items.FirstOrDefault(i => i.Id == Id);
                if (finditem == null)
                    return View("Error");

                order.Items.Remove(finditem);
                ViewBag.total = order.Items.Sum(item => item.Price);
                HttpContext.Session.SetObjectAsJson("Order", order);
                if(order.Items.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("ItemDetails", "Home", new { order?.Items });

        }
        [Authorize(Roles = "Administrator,Customer")]
        public IActionResult ItemDetails()
        {
            var order = HttpContext.Session.GetObjectFromJson<Orders>("Order");

            if (order == null)
            {
                return View(new List<Items>());
            }

            ViewBag.total = order.Items?.Sum(item => item.Price);

            return View(order.Items);
        }

        public async Task<IActionResult> AddItem(Guid id)
        {

            var item = await _itemsService.GetItem(id);

            if (item == null)
            {
                return View("Index");
            }
            var order = HttpContext.Session.GetObjectFromJson<Orders>("Order");
            if (order?.Items == null)
            {
                order!.Items = new List<Items>
                {
                    item
                };
            }
            else
            {
                order.Items.Add(item);
            }

            HttpContext.Session.SetObjectAsJson("Order", order);


            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
