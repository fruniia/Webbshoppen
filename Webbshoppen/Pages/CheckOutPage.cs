using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Models;

namespace Webbshoppen.Pages
{
    internal class CheckOutPage
    {
        public enum Payment
        { 
            Kort,
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
                    break;
                case 3:
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
                    HomeDelivery = true, //Homedelivery
                    ShippingPrice = 10,

                };
            }
        }

        public void PaymentOptions(int option)
        {
            using (var db = new MyDbContext())
            {
                if (option == (int)Payment.Kort)
                {
                    var payment = new CardPayment
                    {
                        CardNumber= 1,
                        CardOwnerName = 1, //TODO: Ändra till string
                        Month = 1,
                        Year = 2023,
                        CvvCode = 111,
                    };
                }
                if (option == (int)Payment.Faktura)
                {
                    //var invoice = new InvoicePayment
                    //{
                    //    Id = 1,
                    //    Orders = { }
                    //};
                }
            }
        }
    }
}
