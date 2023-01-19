using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webbshoppen.Models;

namespace Webbshoppen.Pages
{
    internal class ProductPage
    {
        public string ShowSelectedProducts()
        {
            string showSelectedProducts = string.Empty;
            using(var db = new MyDbContext())
            {
                var products = (from p in db.Products
                               join s in db.Suppliers on p.SupplierId equals s.Id
                               where p.Selected == true
                               select new
                               {
                                   SelectedName = p.Name,
                                   SupplierName = s.Name,
                                   UnitPrice = p.UnitPrice
                               }).ToList().Take(3);
                foreach(var selected in products)
                {
                    showSelectedProducts += $"{selected.SelectedName}\t{selected.SupplierName.PadRight(15)}{(selected.UnitPrice)} kr\t\n";
                    
                }
                return showSelectedProducts;
            }
        }
        public List<Product> GetAllProducts()
        { 
            List<Product> productList = new List<Product>();

            using (var db = new MyDbContext())
            {
                var products = (from p in db.Products
                                select p).ToList();

                for (int i = 0; i < products.Count; i++)
                {
                    productList.AddRange(products);
                }
            }
            return productList;
        }
        
    }
}
