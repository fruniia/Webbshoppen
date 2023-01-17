using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float VAT { get; set; }
     
        public virtual Product? Product { get; set; }
        public virtual Order? Order { get; set; }
    }
}
