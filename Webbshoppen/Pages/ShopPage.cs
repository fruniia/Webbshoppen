using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Data;
using Webbshoppen.Models;
using static Webbshoppen.Pages.AdminPage;

namespace Webbshoppen.Pages
{
    internal class ShopPage
    {
        //Visa minst tre kategorier
        //Visa minst fem produkter av varje
        //Möjlighet att frisöka
        //Visa kort text om produkten
        //Kunna välja mer information om produkten
        //Lägg till produkten i ShopPage
        //Pris
        CategoryManager category = new();
        ProductManager product = new();
        public enum ShopMenu
        {
            Shoppa,
            Sök,
            Varukorg,
            Betala,
            Mina_sidor,
            Avsluta
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                string prompt = "Vad vill du göra idag?";
                string[] options = Enum.GetNames(typeof(ShopMenu));

                Menu shopMenu = new Menu(prompt, options);
                int selectedIndex = shopMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        //Shoppa
                        product.ShowProductsCategory();
                        int productId = ConsoleUtils.GetIntFromUser($"Ange nummer för produkten du vill köpa: ");
                        int userId = 1; //TODO: Eventuellt ändra - Hårdkodat!
                        ShopProduct(productId, userId);
                        break;
                    case 1:
                        //TODO: Sök,
                        break;
                    case 2:
                        CartPage cart = new();
                        cart.Run();
                        break;
                    case 3:
                        CheckOutPage checkOut = new();
                        checkOut.Run();
                        break;
                    case 4:
                        UserPage userPage= new ();
                        userPage.Run();
                        break;
                    case 5:
                        running = false;
                        ConsoleUtils.QuitConsole();
                        break;

                }
                Console.ReadKey();
            }
        }

        public void ShopProduct(int productId, int userId)
        {
            int quantity = ConsoleUtils.GetIntFromUser($"Ange antal: ");
            var quantityAnswer = ConsoleUtils.GetStringFromUser($"Vill du lägga in {quantity} st? j/n: ");
            using (var db = new MyDbContext())
            {
                //var cart = db.Carts;
                var product = (from p in db.Products
                               where p.Id == (productId)
                               select p).SingleOrDefault();
                if (quantityAnswer.Trim().ToLower().StartsWith("j"))
                {
                    var cart = new Cart
                    {
                        UserId = userId,
                        ProductId = product.Id,
                        Quantity = quantity,
                        UnitPrice = product.UnitPrice,
                        TotalPrice = (product.UnitPrice * quantity)

                    };
                    db.Add(cart);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Produkten lades inte till");
                    ConsoleUtils.WaitForKeyPress();
                    //Run();
                }
            }
        }
    }
}
