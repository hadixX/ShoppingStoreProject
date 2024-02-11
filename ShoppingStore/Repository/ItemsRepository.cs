using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ShoppingStore.Data;
using ShoppingStore.Models;
using ShoppingStore.Models.Dtos;
using ShoppingStore.Repository.Interfaces;

namespace ShoppingStore.Repository
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly ShoppingStoreDBContext _context;

        public ItemsRepository(ShoppingStoreDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResultDto<Items>> GetAllItems(int page, int pageSize, string filter)
        {
            var query = _context.Items.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(item => item.Name!.Contains(filter) || item.Description!.Contains(filter));
            }
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            var pagedResultDto = new PagedResultDto<Items>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = await query.ToListAsync()
            };
            return pagedResultDto;
        }

        public async Task<Items?> GetItem(Guid id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(item => item.Id == id);
            return item;
        }

        public async Task<bool> UpdateItem(Guid id, Items item)
        {
            var existingItem = await _context.Items.FindAsync(id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Description = item.Description;
                existingItem.Price = item.Price;
                existingItem.ImageURL = item.ImageURL;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteItem(Guid id)
        {
            var finditem = await _context.Items.FindAsync(id);
            if (finditem != null)
            {
                _context.Items.Remove(finditem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Items> AddItem(Items items)
        {
            await _context.Items.AddAsync(items);
            await _context.SaveChangesAsync();
            return items;
        }

        public async Task<ICollection<Items>> GetAllItems()
        {
            return await _context.Items.ToListAsync();
        }
    }
}
