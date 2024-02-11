using ShoppingStore.Models;

namespace ShoppingStore.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<Orders> GetOrderAsync(Guid? id);
        Task<IEnumerable<Orders>> GetOrdersAsync(string userId = ""); 
        Task AddOrder(Orders order, string userId); 
        Task DeleteOrder(Guid? id); 
    }
}
