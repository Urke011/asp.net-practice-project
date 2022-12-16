using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Infrastructure
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            if(context.Products.Count() == 0 && context.Categories.Count() == 0)
            {
                Category fruits = new Category { Name = "fruits" };

                Category shirts = new Category { Name = "shirts" };

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Apple",
                        Price = 1.50M,
                        Category = fruits
                    },
                     new Product
                     {
                         Name = "Lemons",
                         Price = 2M,
                        Category = fruits
                     },
                      new Product
                      {
                          Name = "watermellone",
                          Price = 0.50M,
                        Category = fruits
                      },
                       new Product
                       {
                           Name = "Bananas",
                           Price = 2.50M,
                        Category = fruits
                       }
                       ,
                       new Product
                       {
                           Name = "blue shirts",
                           Price = 2.50M,
                           Category = shirts
                       }
                        ,
                       new Product
                       {
                           Name = "black shirts",
                           Price = 2.50M,
                           Category = shirts
                       }
                        ,
                       new Product
                       {
                           Name = "red shirts",
                           Price = 2.50M,
                           Category = shirts
                       }
                    );
                context.SaveChanges();
            }
        }
    }
}
