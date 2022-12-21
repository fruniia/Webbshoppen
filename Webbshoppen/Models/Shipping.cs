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
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public bool HomeDelivery { get; set; } //1 = Home, 0 = ShippingAgent
        public float ShippingPrice { get; set; }
        public int CityId { get; set; }
        public virtual City? City { get; set; }
        public int CountryId { get; set; }
        public virtual Country? Country { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
