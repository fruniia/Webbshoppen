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
        public enum ProductMenu
        {
            Handla_produkt = 1,
            Visa_mer_om_produkten
        }
        public ShopPage()
        {
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
                int userId = 1; //TODO: Eventuellt ändra - Hårdkodat!
                switch (selectedIndex)
                {
                    case 0:
                        //Shoppa
                        product.ShowProductsCategory();
                        SelectProduct(userId);
                        break;
                    case 1:
                        SearchProduct();
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
                        UserPage userPage = new();
                        if (userId != 0)
                        {
                            userPage.LogInUser(userId);
                            userPage.Run();
                        }
                        else
                        {
                            Run();
                        }
                        break;
                    case 5:
                        running = false;
                        ConsoleUtils.QuitConsole();
                        break;

                }
                Console.ReadKey();
            }
        }
        public void SelectProduct(int userId)
        {
            Console.WriteLine();
            int option = ConsoleUtils.GetIntFromUser("Välj 1 = Handla produkt, 2 = Se mer om produkt: ");
            if (option == (int)ProductMenu.Handla_produkt)
            {
                int productId = ConsoleUtils.GetIntFromUser($"Ange produktid för produkten du vill köpa: ");
                ShopProduct(productId, userId);

            }
            else if (option == (int)ProductMenu.Visa_mer_om_produkten)
            {
                int productId = ConsoleUtils.GetIntFromUser($"Ange produktid för att visa mer info: ");

                ShowOneProduct(productId);
            }
            else
            {
                Console.WriteLine("Inget giltigt alternativ");
                ConsoleUtils.WaitForKeyPress();
            }
        }
        public void ShowOneProduct(int productId)
        {
            using (var db = new MyDbContext())
            {
                foreach (var product in db.Products.Where(x => x.Id == productId))
                {
                    Console.WriteLine($"{product.Name.PadRight(15)} {product.UnitPrice} {product.Description.PadRight(15)}");
                }
            }
        }
        public void SearchProduct()
        {
            var description = ConsoleUtils.GetStringFromUser("Sök efter produkt: ");
            using (var db = new MyDbContext())
            {
                var result = from p in db.Products
                             where p.Description.Contains(description) || p.Name.Contains(description)
                             select p;

                foreach (var product in result)
                {

                    Console.WriteLine($"{product.Name}\t {product.UnitPrice}\t{product.Description} ");

                }
                ConsoleUtils.WaitForKeyPress();
                Console.Clear();
            }
        }
        public void ShopProduct(int productId, int userId)
        {
            int quantity = ConsoleUtils.GetIntFromUser($"Ange antal: ");
            var quantityAnswer = ConsoleUtils.GetStringFromUser($"Vill du lägga in {quantity} st? j/n: ");
            using (var db = new MyDbContext())
            {
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


