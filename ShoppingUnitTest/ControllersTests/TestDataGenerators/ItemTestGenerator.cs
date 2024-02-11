using ShoppingStore.Models;
using ShoppingStore.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingUnitTest.ControllersTests.TestDataGenerators
{
    public class ItemTestGenerator
    {
        public ItemTestGenerator() { }
        public static PagedResultDto<Items> GetTestItems()
        {
            var items = new List<Items>
            {
                new Items
                {
                    DateCreated = DateTime.Now,
                    Description = "description",
                    ImageURL = "",
                    Name = "name",
                    Price = 1,
                    Id = Guid.NewGuid()
                }
            };
            var orders = new PagedResultDto<Items>
            {
                CurrentPage = 1,
                PageSize = 10,
                TotalCount = 10,
                TotalPages = 10,
                Items = items
            };
            return orders;
        }
        public static Items GetTestItem()
        {
            return new Items
            {
                DateCreated = DateTime.Now,
                Description = "description",
                ImageURL = "",
                Name = "name",
                Price = 1,
                Id = Guid.NewGuid()
            };
        }
    }
}
