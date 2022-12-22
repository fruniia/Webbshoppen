using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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
                        SupplierId = 1,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Name = "T-shirt",
                        UnitPrice = 399,
                        Selected = false,
                        Description = $"Dam, M, Rosa",
                        SupplierId = 7,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Name = "Sweatshirt",
                        UnitPrice = 599,
                        Selected = false,
                        Description = $"Herr, XL, Grön",
                        SupplierId = 2,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Name = "Jultröja",
                        UnitPrice = 499,
                        Selected = true,
                        Description = $"Dam, L, Röd",
                        SupplierId = 3,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Name = "Linne",
                        UnitPrice = 199,
                        Selected = false,
                        Description = $"Dam, S, Vit",
                        SupplierId = 1,
                        CategoryId = 1
                    },
                    // 5 byxor
                    new Product
                    {
                        Name = "Mjukisbyxa",
                        UnitPrice = 799,
                        Selected = true,
                        Description = $"Herr, L, Gul",
                        SupplierId = 5,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Name = "Kostymbyxa",
                        UnitPrice = 1399,
                        Selected = false,
                        Description = $"Dam, M, Svart",
                        SupplierId = 6,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Name = "Jeans",
                        UnitPrice = 1199,
                        Selected = false,
                        Description = $"Herr, XL, Blå",
                        SupplierId = 4,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Name = "Manchesterbyxa",
                        UnitPrice = 899,
                        Selected = true,
                        Description = $"Dam, L, Lila",
                        SupplierId = 2,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Name = "Outdoor-byxa",
                        UnitPrice = 1599,
                        Selected = false,
                        Description = $"Herr, S, Grön",
                        SupplierId = 3,
                        CategoryId = 2
                    },
                    // 5 jackor
                    new Product
                    {
                        Name = "Dunjacka",
                        UnitPrice = 4999,
                        Selected = true,
                        Description = $"Dam, M, Grå",
                        SupplierId = 4,
                        CategoryId = 3
                    },
                    new Product
                    {
                        Name = "Jeansjacka",
                        UnitPrice = 999,
                        Selected = false,
                        Description = $"Herr, M, Blå",
                        SupplierId = 3,
                        CategoryId = 3
                    },
                    new Product
                    {
                        Name = "Softshelljacka",
                        UnitPrice = 1199,
                        Selected = false,
                        Description = $"Dam, L, Svart",
                        SupplierId = 5,
                        CategoryId = 3
                    },
                    new Product
                    {
                        Name = "Fleecejacka",
                        UnitPrice = 899,
                        Selected = false,
                        Description = $"Herr, M, Grå",
                        SupplierId = 2,
                        CategoryId = 3
                    },
                    new Product
                    {
                        Name = "Dunväst",
                        UnitPrice = 1199,
                        Selected = false,
                        Description = $"Herr, S, Grön",
                        SupplierId = 1,
                        CategoryId = 3
                    }

                    );
                //context.SaveChanges();
            }

        }
    }
}

