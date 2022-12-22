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
                new Product { Name = "", UnitPrice = 99, Selected = true, };
            }
        }
    }
}
