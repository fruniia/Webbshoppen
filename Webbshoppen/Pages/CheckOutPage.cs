using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Models;

namespace Webbshoppen.Pages
{
    internal class CheckOutPage
    {
        int shippingId = 0;
        int paymentId = 0;
        public enum Payment
        {
            Kort = 1,
            Faktura
        }
        public enum ShippingOption
        {
            Hemleverans = 1,
            Ombud
        }
        public enum Checkout
        {
            Shoppa,
            Logga_in,
            Välj_frakt_och_betalningsalternativ,
            Betala,
            Avsluta
        }
        public void Run()
        {
            string prompt = ("Dags att betala");
            string[] startOptions = Enum.GetNames(typeof(Checkout));
            Menu startMenu = new Menu(prompt, startOptions);
            int selectedIndex = startMenu.Run();
            int userid = 1;
            switch (selectedIndex)
            {
                case 0:
                    ShopPage sp = new();
                    sp.Run();
                    break;
                case 1:
                    UserPage up = new();
                    userid = up.CheckUserDetails();
                    up.LogInUser(userid);
                    break;
                case 2:
                    shippingId = SetShippingOptions();
                    paymentId = GoToPayment(userid, shippingId);
                    ConsoleUtils.WaitForKeyPress();
                    Run();
                    break;
                case 3:
                    OrderPage o = new();
                    CartPage c = new();
                    List<Cart> carts = c.GetProductsInCart(userid);
                    o.CreateOrder(userid, shippingId, paymentId);
                    int orderId = o.GetCurrentOrder();
                    o.CreateOrderDetails(orderId, carts);
                    bool empty = c.EmptyCartFromProducts(userid);
                    Console.WriteLine((empty == true ? "Tack för din order.\nOrderhistorik finns på dina sidor!" : "Varukorgen tömdes inte")); 
                    ConsoleUtils.WaitForKeyPress();
                    Run();
                    break;
                case 5:
                    ConsoleUtils.QuitConsole();
                    break;
            }
        }
        public int GoToPayment(int userid, int shippingId)
        {
            int paymentId = 0;
            float totalPriceInclShipping = 0;
            using (var db = new MyDbContext())
            {
                foreach (var item in db.Products.Include(x => x.Carts.Where(c => c.UserId == userid)))
                {
                    foreach (var c in item.Carts)
                    {
                        Console.WriteLine($"Produktnamn: {item.Name}");
                        Console.WriteLine($"Antal: {c.Quantity}");
                        Console.WriteLine($"Pris: {c.UnitPrice}");
                        Console.WriteLine($"Totalpris: {c.TotalPrice}");
                        Console.WriteLine($"Inkl moms: {c.TotalPrice * 0.20}");
                        Console.WriteLine();
                        totalPriceInclShipping += (float)c.TotalPrice;
                    }
                }
                var shipping = db.Shippings.Where(s => s.Id == shippingId);
                foreach (var s in shipping)
                {
                    Console.WriteLine($"Fraktssätt: {(s.DeliveryOption == true ? "Hemleverans" : "Ombud")}");
                    Console.WriteLine($"Pris för frakt: {s.ShippingPrice}");
                    totalPriceInclShipping += (float)s.ShippingPrice;
                }
                Console.WriteLine();
                Console.WriteLine($"Totalpris inkl frakt: {totalPriceInclShipping}");
            }
            paymentId = SetPaymentOptions();

            return paymentId;
        }
        public int SetShippingOptions()
        {
            Console.WriteLine("Mata in följande uppifter");
            var firstName = ConsoleUtils.GetStringFromUser("Förnamn: ");
            var lastName = ConsoleUtils.GetStringFromUser("Efternamn: ");
            var address = ConsoleUtils.GetStringFromUser("Adress: ");
            var postalCode = ConsoleUtils.GetIntFromUser("Postnummer: ");
            var city = ConsoleUtils.GetStringFromUser("Stad: ");
            var country = ConsoleUtils.GetStringFromUser("Land: ");
            var phoneNumber = ConsoleUtils.GetStringFromUser("Telefonnummer: ");
            int option = ConsoleUtils.GetIntFromUser("Välj fraktalternativ 1 = Hemleverans, 2 = Ombud: ");

            using (var db = new MyDbContext())
            {
                var shipping = new Shipping
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    PostalCode = postalCode,
                    City = city,
                    Country = country,
                    PhoneNumber = phoneNumber,
                    DeliveryOption = (option == (int)ShippingOption.Hemleverans ? true : false), //Homedelivery
                    ShippingPrice = (option == (int)ShippingOption.Hemleverans ? 79 : 49),

                };
                if (option == 1 || option == 2)
                {
                    db.Add(shipping);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Du valde ett felaktigt alternativ");
                    ConsoleUtils.WaitForKeyPress();
                    SetShippingOptions();
                }
                Console.WriteLine();
                var shippingId = db.Shippings.Select(x => x.Id).Max().ToString();

                return Convert.ToInt32(shippingId);
            }
        }
        public int SetPaymentOptions()
        {
            int option = ConsoleUtils.GetIntFromUser("Välj betalningsalternativ 1 = Kort, 2 = Faktura: ");

            using (var db = new MyDbContext())
            {
                if (option == (int)Payment.Kort)
                {
                    var payment = new Models.Payment
                    {
                        PaymentOption = true

                    };
                    db.Add(payment);
                }
                else if (option == (int)Payment.Faktura)
                {
                    var payment = new Models.Payment
                    {
                        PaymentOption = false
                    };
                    db.Add(payment);
                }
                else
                {
                    Console.WriteLine("Inget giltigt alternativ");
                    ConsoleUtils.WaitForKeyPress();
                    SetPaymentOptions();
                }
                db.SaveChanges();

                var paymentId = db.Payments.Select(x => x.Id).Max().ToString();
                Console.WriteLine();
                return Convert.ToInt32(paymentId);
            }
        }
    }
}
