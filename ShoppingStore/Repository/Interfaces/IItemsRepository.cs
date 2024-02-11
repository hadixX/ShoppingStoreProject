using ShoppingStore.Models;
using ShoppingStore.Models.Dtos;

namespace ShoppingStore.Repository.Interfaces
{
    public interface IItemsRepository
    {
        Task<Items> AddItem(Items items);
        Task<PagedResultDto<Items>> GetAllItems(int page, int pageSize, string filter);
        Task<ICollection<Items>> GetAllItems();
        Task<Items?> GetItem(Guid id);
        Task DeleteItem(Guid id);
        Task<bool> UpdateItem(Guid id, Items item);
    }
}