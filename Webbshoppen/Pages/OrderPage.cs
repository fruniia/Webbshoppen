using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Webbshoppen.Pages
{
    internal class OrderPage
    {
        //Quantity
        //Orderdate
        //Totalprice
        //VAT
        //UserId
        int userid = 1;
        public void ShowOrders(int userid)
        {
            using (var db = new MyDbContext())
            {

                foreach (var item in db.Orders.Include(o => o.OrderDetails).Include(x => x.Products)
                    .Where(x => x.UserId == userid).GroupBy(x => x.Id))
                {
                    foreach (var i in item)
                    {
                        Console.WriteLine($"Orderid: {i.Id} {i.OrderDate}");
                        Console.WriteLine($"Betalningssätt: {(i.Payment.PaymentOption == true ? "Kort" : "Faktura")}");
                        Console.WriteLine($"Fraktssätt: {(i.Shipping.DeliveryOption == true ? "Hemleverans" : "Ombud")}");
                        Console.WriteLine($"Pris för frakt: {i.Shipping.ShippingPrice}");
                       
                        foreach(var p in i.Products)
                        {
                            Console.WriteLine($"{p.Name}");
                        }
                        foreach (var o in i.OrderDetails)
                        {
                            Console.WriteLine($"Antal: {o.Quantity} Pris: {o.UnitPrice} Moms: {o.VAT} kr" +
                                $"-Totalpris: {(o.UnitPrice * o.Quantity)}");

                        }
                    }
                }
            }
        }


        public void CreateOrder(int userId, int shippingId, int paymentId)
        {
            // orderDetjels = Id, userId , datum, shipping, payment
            // Order = OrderId (orderdetjalsId) , produkt , moms, quantity, UnitPrice, 

            using (var db = new MyDbContext())
            {
                //var cart = from c in db.Carts
                //           join p in db.Products on c.ProductId equals p.Id
                //           where c.UserId == userId
                //           select new
                //           {
                //               QuantityX = c.Quantity,
                //               UnitPrice = c.UnitPrice,
                //               TotalPrice = c.TotalPrice,

                //           };
                //if (cart != null)
                //{
                //    var order = new Order
                //    {
                //       ShippingId = shippingId
                        
                        
                //    };

                //}
            }
        }
    }
}
