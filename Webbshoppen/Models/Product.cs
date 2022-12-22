using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float? UnitPrice { get; set; }
        public bool? Selected { get; set; }
        public string? Description { get; set; }
        public int UnitsInStock { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }
    }
}
