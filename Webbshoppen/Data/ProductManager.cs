using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Models;
using Microsoft.EntityFrameworkCore;

namespace Webbshoppen.Data
{
    public class ProductManager
    {
        public void AddProducts()
        {

            using (var context = new MyDbContext())
            {
                context.AddRange(
                    //5 Tröjor
                    new Product
                    {
                        Name = "Collegetröja",
                        UnitPrice = 299,
                        Selected = false,
                        Description = $"Herr, L, Svart",
                        CategoryId = 1,
                        UnitsInStock = 4
                    },
                    new Product
                    {
                        Name = "T-shirt",
                        UnitPrice = 399,
                        Selected = false,
                        Description = $"Dam, M, Rosa",
                        CategoryId = 1,
                        UnitsInStock = 5
                    },
                    new Product
                    {
                        Name = "Sweatshirt",
                        UnitPrice = 599,
                        Selected = false,
                        Description = $"Herr, XL, Grön",
                        CategoryId = 1,
                        UnitsInStock = 9
                    },
                    new Product
                    {
                        Name = "Jultröja",
                        UnitPrice = 499,
                        Selected = true,
                        Description = $"Dam, L, Röd",
                        CategoryId = 1,
                        UnitsInStock = 12
                    },
                    new Product
                    {
                        Name = "Linne",
                        UnitPrice = 199,
                        Selected = false,
                        Description = $"Dam, S, Vit",
                        CategoryId = 1,
                        UnitsInStock = 4
                    },
                    // 5 byxor
                    new Product
                    {
                        Name = "Mjukisbyxa",
                        UnitPrice = 799,
                        Selected = false,
                        Description = $"Herr, L, Gul",
                        CategoryId = 2,
                        UnitsInStock = 4
                    },
                    new Product
                    {
                        Name = "Kostymbyxa",
                        UnitPrice = 1399,
                        Selected = false,
                        Description = $"Dam, M, Svart",
                        CategoryId = 2,
                        UnitsInStock = 4
                    },
                    new Product
                    {
                        Name = "Jeans",
                        UnitPrice = 1199,
                        Selected = false,
                        Description = $"Herr, XL, Blå",
                        CategoryId = 2,
                        UnitsInStock = 4
                    },
                    new Product
                    {
                        Name = "Manchesterbyxa",
                        UnitPrice = 899,
                        Selected = true,
                        Description = $"Dam, L, Lila",
                        CategoryId = 2,
                        UnitsInStock = 4
                    },
                    new Product
                    {
                        Name = "Outdoor-byxa",
                        UnitPrice = 1599,
                        Selected = false,
                        Description = $"Herr, S, Grön",
                        CategoryId = 2,
                        UnitsInStock = 4
                    },
                    // 5 jackor
                    new Product
                    {
                        Name = "Dunjacka",
                        UnitPrice = 4999,
                        Selected = true,
                        Description = $"Dam, M, Grå",
                        CategoryId = 3,
                        UnitsInStock = 4
                    },
                    new Product
                    {
                        Name = "Jeansjacka",
                        UnitPrice = 999,
                        Selected = false,
                        Description = $"Herr, M, Blå",
                        CategoryId = 3,
                        UnitsInStock = 4
                    },
                    new Product
                    {
                        Name = "Softshelljacka",
                        UnitPrice = 1199,
                        Selected = false,
                        Description = $"Dam, L, Svart",
                        CategoryId = 3,
                        UnitsInStock = 4
                    },
                    new Product
                    {
                        Name = "Fleecejacka",
                        UnitPrice = 899,
                        Selected = false,
                        Description = $"Herr, M, Grå",
                        CategoryId = 3,
                        UnitsInStock = 4
                    },
                    new Product
                    {
                        Name = "Dunväst",
                        UnitPrice = 1199,
                        Selected = false,
                        Description = $"Herr, S, Grön",
                        CategoryId = 3,
                        UnitsInStock = 4
                    }

                    );
                context.SaveChanges();
            }
        }
    }
}
