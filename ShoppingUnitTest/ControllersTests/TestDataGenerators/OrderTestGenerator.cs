using ShoppingStore.Models;

namespace Shopping.UnitTest.UnitTests.ControllersTests.TestDataGenerators
{
    public static class OrderTestGenerator
    {
        public static List<Orders> GetTestOrders()
        {
            var orders = new List<Orders>
            {
                new Orders()
                {
                    Timestamp = new DateTime(2016, 7, 2),
                    Id = Guid.NewGuid(),
                    TotalPrice = 25
                },
                new Orders()
                {
                    Timestamp = new DateTime(2016, 7, 1),
                    Id = Guid.NewGuid(),
                    TotalPrice = 23.1
                }
            };
            return orders;
        }
        public static Orders GetTestOrder()
        {
            return new Orders()
            {
                Timestamp = new DateTime(2016, 7, 2),
                Id = Guid.NewGuid(),
                TotalPrice = 25
            };
        }
    }
}
