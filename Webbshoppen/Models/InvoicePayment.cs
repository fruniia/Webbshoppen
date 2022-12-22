using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Models
{
    public class InvoicePayment
    {
        public int Id { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
