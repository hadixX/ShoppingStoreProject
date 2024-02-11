using Microsoft.AspNetCore.Identity;

namespace ShoppingStore.Models
{
    public class Orders
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public DateTime Timestamp { get; set; }
        public double TotalPrice { get; set; }

        public IdentityUser? User { get; set; }
        public ICollection<Items>? Items { get; set; }
    }
}
