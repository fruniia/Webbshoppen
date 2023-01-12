using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Net.WebSockets;

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
                        UnitsInStock = 4,
                        SupplierId = 2
                    },
                    new Product
                    {
                        Name = "T-shirt",
                        UnitPrice = 399,
                        Selected = false,
                        Description = $"Dam, M, Rosa",
                        CategoryId = 1,
                        UnitsInStock = 5,
                        SupplierId = 3
                    },
                    new Product
                    {
                        Name = "Sweatshirt",
                        UnitPrice = 599,
                        Selected = false,
                        Description = $"Herr, XL, Grön",
                        CategoryId = 1,
                        UnitsInStock = 9,
                        SupplierId = 1
                    },
                    new Product
                    {
                        Name = "Jultröja",
                        UnitPrice = 499,
                        Selected = true,
                        Description = $"Dam, L, Röd",
                        CategoryId = 1,
                        UnitsInStock = 12,
                        SupplierId = 1
                    },
                    new Product
                    {
                        Name = "Linne",
                        UnitPrice = 199,
                        Selected = false,
                        Description = $"Dam, S, Vit",
                        CategoryId = 1,
                        UnitsInStock = 4,
                        SupplierId = 2
                    },
                    // 5 byxor
                    new Product
                    {
                        Name = "Mjukisbyxa",
                        UnitPrice = 799,
                        Selected = false,
                        Description = $"Herr, L, Gul",
                        CategoryId = 2,
                        UnitsInStock = 4,
                        SupplierId = 5
                    },
                    new Product
                    {
                        Name = "Kostymbyxa",
                        UnitPrice = 1399,
                        Selected = false,
                        Description = $"Dam, M, Svart",
                        CategoryId = 2,
                        UnitsInStock = 4,
                        SupplierId = 7
                    },
                    new Product
                    {
                        Name = "Jeans",
                        UnitPrice = 1199,
                        Selected = false,
                        Description = $"Herr, XL, Blå",
                        CategoryId = 2,
                        UnitsInStock = 4,
                        SupplierId = 6
                    },
                    new Product
                    {
                        Name = "Manchesterbyxa",
                        UnitPrice = 899,
                        Selected = true,
                        Description = $"Dam, L, Lila",
                        CategoryId = 2,
                        UnitsInStock = 4,
                        SupplierId = 3
                    },
                    new Product
                    {
                        Name = "Outdoor-byxa",
                        UnitPrice = 1599,
                        Selected = false,
                        Description = $"Herr, S, Grön",
                        CategoryId = 2,
                        UnitsInStock = 4,
                        SupplierId = 4
                    },
                    // 5 jackor
                    new Product
                    {
                        Name = "Dunjacka",
                        UnitPrice = 4999,
                        Selected = true,
                        Description = $"Dam, M, Grå",
                        CategoryId = 3,
                        UnitsInStock = 4,
                        SupplierId = 4
                    },
                    new Product
                    {
                        Name = "Jeansjacka",
                        UnitPrice = 999,
                        Selected = false,
                        Description = $"Herr, M, Blå",
                        CategoryId = 3,
                        UnitsInStock = 4,
                        SupplierId = 6
                    },
                    new Product
                    {
                        Name = "Softshelljacka",
                        UnitPrice = 1199,
                        Selected = false,
                        Description = $"Dam, L, Svart",
                        CategoryId = 3,
                        UnitsInStock = 4,
                        SupplierId = 6
                    },
                    new Product
                    {
                        Name = "Fleecejacka",
                        UnitPrice = 899,
                        Selected = false,
                        Description = $"Herr, M, Grå",
                        CategoryId = 3,
                        UnitsInStock = 4,
                        SupplierId = 3
                    },
                    new Product
                    {
                        Name = "Dunväst",
                        UnitPrice = 1199,
                        Selected = false,
                        Description = $"Herr, S, Grön",
                        CategoryId = 3,
                        UnitsInStock = 4,
                        SupplierId = 4
                    }

                    );
               //context.SaveChanges();
            }
        }

        public void ShowProducts()
        {
            using (var db = new MyDbContext())
            {
                var product = from p in db.Products
                              join c in db.Categories on p.CategoryId equals c.Id
                              join s in db.Suppliers on p.SupplierId equals s.Id
                              select new
                              {
                                  Id = p.Id,
                                  Selected = p.Selected,
                                  Name = p.Name,
                                  UnitPrice = p.UnitPrice,
                                  Description = p.Description,
                                  UnitsInStock = p.UnitsInStock,
                                  CategoryName = c.Name,
                                  SupplierName = s.Name
                              };

                foreach (var l in product)
                {
                    Console.WriteLine($"{(l.Selected == true ? "Utvald" : " ")}\t {l.Id} {l.Name} {l.UnitPrice} {l.Description} {l.UnitsInStock} {l.CategoryName} {l.SupplierName}");
                }
            }
        }

        public void ShowProductsCategory()
        {
            using (var db = new MyDbContext())
            {

                foreach (var category in db.Categories.Include(p => p.Products).ThenInclude(s => s.Supplier))
                {
                    Console.WriteLine($"{category.Name}");
                    foreach(var product in category.Products)
                    {
                        Console.WriteLine($"\t{product.Id} {product.Name} {product.UnitPrice}kr {product.Supplier.Name}");
                    }
                    
                }
            }
        }
    }
}
