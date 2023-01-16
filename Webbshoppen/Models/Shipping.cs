using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Models
{
    public class Shipping
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool DeliveryOption { get; set; } //1 = Home, 0 = ShippingAgent
        public float ShippingPrice { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
