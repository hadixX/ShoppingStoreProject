using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingStore.Data;
using ShoppingStore.Models;
using ShoppingStore.Repository.Interfaces;

namespace ShoppingStore.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoppingStoreDBContext _dbContext;
        private readonly IItemsRepository _itemsRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderRepository(ShoppingStoreDBContext dbContext, IItemsRepository itemsRepository, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _itemsRepository = itemsRepository;
            _userManager = userManager;
        }
        public async Task AddOrder(Orders order, string userId)
        {
            //think on adding Unit of work for the below
            order.Id = Guid.NewGuid();
            order.Timestamp = DateTime.Now;
            order.User = await _userManager.FindByIdAsync(order.UserID);
            order.TotalPrice = order.Items!.Sum(item => item.Price);
            var itemQuantities = order.Items!
            .GroupBy(item => item.Id)
            .Select(group => new { Id = group.Key, Quantity = group.Count() });
            order.Items = null;
            _dbContext.Orders.Add(order);  
            foreach (var groupedItem in itemQuantities)
            {
                var item = await _itemsRepository.GetItem(groupedItem.Id);
                var orderItem = new OrdersItems { OrderId = order.Id, order = order, ItemsId= groupedItem.Id, item = item!, Quantity = groupedItem.Quantity};
                _dbContext.OrdersItems.Add(orderItem);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrder(Guid? id)
        {
            if (_dbContext.Orders == null)
            {
                throw new InvalidOperationException();
            }
            var order = await _dbContext.Orders.FindAsync(id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Orders> GetOrderAsync(Guid? id)
        {
            if (_dbContext.Orders == null)
            {
                throw new InvalidOperationException();
            }
            return await _dbContext.Orders.Include(order => order.User)
                .Include(order => order.Items).SingleAsync(order => order.Id == id);
        }

        public async Task<IEnumerable<Orders>> GetOrdersAsync(string userId = "")
        {
            return await _dbContext.Orders.Include(order => order.User)
                .Where(order => string.IsNullOrEmpty(userId) || order.UserID == userId)
                .ToListAsync();
        }
    }
}
