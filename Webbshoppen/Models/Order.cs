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
        public int CardId { get; set; }
        public virtual CardPayment? Card { get; set; }
        public int InvoiceId { get; set; }
        public virtual InvoicePayment? Invoice { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        

    }
}
