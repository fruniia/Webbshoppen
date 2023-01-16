using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public bool PaymentOption { get; set; } //Kort = 1, Faktura = 0
        public ICollection<Order>? Orders { get; set; }
    }
}