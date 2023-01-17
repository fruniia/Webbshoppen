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
        //Visa vald produkt med information, pris, storlek

        public void ShowSelectedProducts()
        {
            using(var db = new MyDbContext())
            {
                var products = from p in db.Products
                               join s in db.Suppliers on p.SupplierId equals s.Id
                               where p.Selected == true
                               select new
                               {
                                   SelectedName = p.Name,
                                   SupplierName = s.Name,
                                   UnitPrice = p.UnitPrice
                               };
                foreach(var selected in products)
                {
                    Console.Write($"{selected.SelectedName}\n {selected.SupplierName} \n {selected.UnitPrice}:-\n");
                    Console.WriteLine();
                }
            }
        }
    }
}
