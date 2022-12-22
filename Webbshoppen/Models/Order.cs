using Microsoft.VisualBasic;
using System;
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

        public int Quantity { get; set; }
        public int OrderDate { get; set; }
        public float TotalPrice { get; set; }
        public float VAT { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        
        public ICollection<Shipping> Shippings { get; set; }
        public ICollection<CardPayment> CardPayments { get; set; }

        public ICollection<InvoicePayment> InvoicePayments { get; set; }

    }
}
