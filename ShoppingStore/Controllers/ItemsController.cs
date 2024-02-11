using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.Models;
using ShoppingStore.Models.Dtos;
using ShoppingStore.Repository.Interfaces;
using System.Data;

namespace ShoppingStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ItemsController : Controller
    {
        private readonly IItemsRepository _itemsService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public ItemsController(IItemsRepository itemsService, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _itemsService = itemsService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int pageSize = 3, [FromQuery] string filter = "")
        {
              return View(await _itemsService.GetAllItems(page,pageSize,filter));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _itemsService.GetItem((Guid)id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ImageURL,DateCreated")] ItemsDto itemDto)
        {
            if (ModelState.IsValid)
            {
              var item = _mapper.Map<Items>(itemDto);
              item.Id = Guid.NewGuid();
              var result = await _itemsService.AddItem(item);
              if(item == null)
                    return NotFound();
               return RedirectToAction(nameof(Index));
            }
            return View(itemDto);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _itemsService.GetItem((Guid)id);
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,ImageURL,DateCreated")] Items items)
        {
            if (id != items.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                  var result =  await _itemsService.UpdateItem(id,items);
                  if(!result)
                        return NotFound();
               
                return RedirectToAction(nameof(Index));
            }
            return View(items);
        }

        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _itemsService.GetItem((Guid)id);  
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
           
            var items = await _itemsService.GetItem(id);
            if (items != null)
            {
              await _itemsService.DeleteItem(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
