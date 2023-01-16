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
        public enum Payment
        { 
            Kort = 1,
            Faktura
        }
        public enum Checkout
        {
            Shoppa,
            Logga_in,
            Välj_fraktalternativ,
            Välj_betalningsalternativ,
            Betala,
            Avsluta
        }
        //Fraktvy
        //Namn, adress, , postnummer, stad, land, telefon, homedelivery, shippingprice
        //Betalavy
        //Produkter visas med pris
        //Pris med frakt, samt moms, 
        //Val av betalning 
        public void Run()
        {
            string prompt = ("Dags att betala");
            string[] startOptions = Enum.GetNames(typeof(Checkout));
            Menu startMenu = new Menu(prompt, startOptions);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    ShopPage sp = new();
                    sp.Run();
                    break;
                case 1:
                    //Logga in som kund ->
                    break;
                case 2:
                    // Välj frakt- och betalningsalternativ
                    //Få information om frakt = home/inte och betalning = faktura/kort
                    break;
                case 3:
                    int paymentId = PaymentOptions();
                    Console.WriteLine(paymentId);
                    //betala -> OrderPage
                    break;
                case 4:
                    ConsoleUtils.QuitConsole();
                    break;
            }
        }

        public void LogInCustomer()
        {

        }
        public void ShippingOptions()
        {
            using (var db = new MyDbContext())
            {
                var shipping = new Shipping
                {
                    CustomerName = "",
                    Address = "",
                    PostalCode = 1111,
                    CityId = 1,
                    PhoneNumber = "",
                    DeliveryOption = true, //Homedelivery
                    ShippingPrice = 10,

                };
            }
        }

        public int PaymentOptions()
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
                    PaymentOptions();
                }
                db.SaveChanges();

                var paymentId = db.Payments.Select(x => x.Id).Max().ToString();

                return Convert.ToInt32(paymentId);
            }
        }
    }
}
