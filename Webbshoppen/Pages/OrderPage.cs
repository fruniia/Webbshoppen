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
        int userid = 1;
        public void ShowOrders(int userid)
        {
            using (var db = new MyDbContext())
            {
                float totalPrice = 0;

                foreach (var o in db.OrderDetails.Include(p => p.Product).Include(o => o.Order).Where(p => p.Order.UserId == userid))
                {
                    Console.WriteLine("========================");
                    Console.WriteLine($"Orderid: {o.Order.Id} Orderdatum: {o.Order.OrderDate}\n{o.Product.Name} Antal:[{o.Quantity}] Pris: {o.UnitPrice} kr Moms: {o.VAT} kr " +
                        $"Totalpris: {(o.UnitPrice * o.Quantity)}");

                }
                Console.WriteLine("===============================");
                foreach (var o in db.OrderDetails.Select(x => x))
                {
                    float sum = ((float)o.UnitPrice * (float)o.Quantity);
                    totalPrice += sum;
                }
                Console.WriteLine($"Totalsumman: {totalPrice}");
            }
        }
        public void CreateOrder(int userid, int shippingId, int paymentId)
        {
            CheckOutPage checkOutPage = new();
            DateTime date = new DateTime();
            date = DateTime.Now;
            if (userid > 0 && shippingId > 0 && paymentId > 0)
            {
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
            else
            {
                Console.WriteLine("===============\nDu behöver välja betalnings- och fraktalternativ");
                ConsoleUtils.WaitForKeyPress();
                checkOutPage.Run();
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
