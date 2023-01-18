using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public int PaymentId { get; set; }
        public int ShippingId { get; set; }
        public int UserId { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual Shipping? Shipping { get; set; }
        public virtual User? User { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<Product>? Products { get; set; }

    }
}
