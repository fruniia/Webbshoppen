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

                        foreach (var p in i.Products)
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
        public void CreateOrder(int userid, int shippingId, int paymentId)
        {
            DateTime date = new DateTime();
            date = DateTime.Now;

            using (var db = new MyDbContext())
            {
                var order = new Order
                {
                    OrderDate = date.Year + "" + date.Month + "" + date.Day,
                    ShippingId = shippingId,
                    PaymentId = paymentId,
                    UserId = userid
                };
                db.Add(order);
                db.SaveChanges();
            }

        }
        public int GetCurrentOrder()
        {
            using (var db = new MyDbContext()) 
            {
                var orderId = db.Orders.Select(x => x.Id).Max().ToString();

                return Convert.ToInt32(orderId);
            }
        }
        public void CreateOrderDetails(int orderId, List<Cart> carts)
        {
            using (var db = new MyDbContext())
            {

                foreach (Cart c in carts)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductId = c.ProductId,
                        Quantity = c.Quantity,
                        UnitPrice = c.UnitPrice,
                        VAT = (c.UnitPrice * (float)0.2),
                        OrderId = orderId,
                    };
                    db.Add(orderDetail);
                    db.SaveChanges();
                }
            }
        }
    }
}
