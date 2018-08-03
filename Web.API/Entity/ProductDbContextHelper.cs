using System.Collections.Generic;

namespace Web.API.Entity
{
    // TODO: remove this entire class when moving to persistence database
    public static class ProductDbContextHelper
    {
        public static void EnsureDbSeeded(this ProductDbContext context)
        {
            // ensure data is clean 
            context.Products.RemoveRange(context.Products);
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product
                {
                    Id = "S00001-PS4",
                    Brand = "Sony",
                    Description = "Console gaming",
                    Model = "PS4"
                },
                new Product
                {
                    Id = "M00001-XBox",
                    Brand = "Microsoft",
                    Description = "Console gaming",
                    Model = "XBox"
                },
                new Product
                {
                    Id ="B00001-Game",
                    Brand = "Bathesda",
                    Description = "PS4 XBox game",
                    Model = "Evil Within"
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }

    }
}
