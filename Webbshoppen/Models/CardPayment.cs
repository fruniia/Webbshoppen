using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Models
{
    public class CardPayment
    {
        public int Id { get; set; }
        public long CardNumber { get; set; }
        public int CardOwnerName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int CvvCode { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}