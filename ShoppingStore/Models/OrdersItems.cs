using Microsoft.Extensions.Hosting;

namespace ShoppingStore.Models
{
    public class OrdersItems
    {
        public Guid OrderId { get; set; }
        public Guid ItemsId { get; set; }
        public int Quantity { get; set; }
        public Orders order { get; set; } = null!;
        public Items item { get; set; } = null!;
    }
}
