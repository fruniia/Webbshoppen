using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Models;
using Microsoft.EntityFrameworkCore;

namespace Webbshoppen.Data
{
    public class SupplierManager
    {  
        public void AddSuppliers()
        {
            using (var context = new MyDbContext())
            {
                context.AddRange(
                    new Supplier { Name = "Adidas" },
                    new Supplier { Name = "Nike" },
                    new Supplier { Name = "Puma" },
                    new Supplier { Name = "Sail racing" },
                    new Supplier { Name = "Holebrook" },
                    new Supplier { Name = "Helly Hansen" },
                    new Supplier { Name = "Pelle Petersson" }
                    );
               //context.SaveChanges();
            }
        }

        public void ShowSuppliers()
        {
            using (var db = new MyDbContext())
            {
              
                foreach(var supplier in db.Suppliers.Select(x=> x))
                {
                    Console.WriteLine($"{supplier.Id} {supplier.Name}");
                }  
            }
        }
    }
}
