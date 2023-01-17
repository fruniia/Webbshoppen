using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Models;
using Microsoft.EntityFrameworkCore;

namespace Webbshoppen.Pages
{
    internal class OrderPage
    {
        //Quantity
        //Orderdate
        //Totalprice
        //VAT
        //UserId
        int userid = 2;
        public void ShowOrders(int userid)
        {
            using (var db = new MyDbContext())
            {

                foreach (var item in db.Orders.Include(x => x.Products).Include(y => y.Shipping)
                    .Include(z => z.Payment)
                    .Where(x => x.UserId == userid).GroupBy(x => x.Id))
                {
                    foreach (var i in item)
                    {
                        Console.WriteLine($"Orderid: {i.Id} {i.OrderDate}");
                        Console.WriteLine($"Betalningssätt: {(i.Payment.PaymentOption == true ? "Kort" : "Faktura")}");
                        Console.WriteLine($"Fraktssätt: {(i.Shipping.DeliveryOption == true ? "Hemleverans" : "Ombud")}");
                        Console.WriteLine($"Pris för frakt: {i.Shipping.ShippingPrice}");
                        foreach (var p in i.Products)
                        {
                            Console.WriteLine($"Produkt: {p.Name} Antal: {i.Quantity} Pris: {p.UnitPrice} Moms: {(p.UnitPrice * 0.20)} kr" +
                                $"-Totalpris: {i.TotalPrice}");

                        }
                    }
                }
            }
        }
    }
}
