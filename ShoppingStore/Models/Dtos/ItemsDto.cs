namespace ShoppingStore.Models.Dtos
{
    public class ItemsDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? ImageURL { get; set; }    
        public DateTime DateCreated { get; set; }
    }
}
