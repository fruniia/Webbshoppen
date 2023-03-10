using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Models;
using Microsoft.EntityFrameworkCore;

namespace Webbshoppen.Data
{
    public class CategoryManager
    {

        public void AddCategories()
        {
            using (var context = new MyDbContext())
            {
                context.AddRange(
                    new Category { Name = "Tröjor" },
                    new Category { Name = "Byxor" },
                    new Category { Name = "Jackor" }
                    );
                //context.SaveChanges();
            }
        }

        public void ShowCategories()
        {
            using (var db = new MyDbContext())
            {
                foreach (var category in db.Categories.Select(x => x))
                {
                    Console.WriteLine($"{category.Id} {category.Name}");
                }
            }
        }

    }
}
