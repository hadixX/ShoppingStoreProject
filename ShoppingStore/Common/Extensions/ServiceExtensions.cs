using ShoppingStore.Repository.Interfaces;
using ShoppingStore.Repository;
using ShoppingStore.Common.Seed;

namespace ShoppingStore.Common.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddShopingServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IItemsRepository, ItemsRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddAuthorization(option =>
            {
                option.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
                option.AddPolicy("Customer", policy =>policy.RequireRole("Customer"));
            });
            //this to initialize a deafult admin user, note: all regesterd user will be added as 'Customer'
            services.AddTransient<DbInitializer>();
            services.BuildServiceProvider().GetService<DbInitializer>().Initialize().Wait();

        }
    }
}
