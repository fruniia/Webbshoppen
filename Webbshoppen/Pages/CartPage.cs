using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Webbshoppen.Pages.AdminPage;
using Webbshoppen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;

namespace Webbshoppen.Pages
{
    internal class CartPage
    {
        ProductPage p = new();
        ShopPage MyShopPage = new();
        List<Product> products = new List<Product>();
        List<Cart> carts = new List<Cart>();
        public enum CartOptions
        {
            Visa_varukorg,
            Ändra_antal,
            Ta_bort_produkt,
            Töm_varukorg,
            Fortsätt_shoppa,
            Gå_till_betalning
        }
        public CartPage()
        {

        }
        public void Run()
        {
            bool running = true;
            while (running)
            {
                int userid = 1; //TODO: Userid hårdkodat
                string prompt = $"Varukorg\n";
                Console.WriteLine("==================");
                string[] options = Enum.GetNames(typeof(CartOptions));

                Menu cartMenu = new Menu(prompt, options);
                int selectedIndex = cartMenu.Run();

                products = p.GetAllProducts();
                carts = GetProductsInCart(userid);
                switch (selectedIndex)
                {
                    case 0:
                        PrintCart(carts, products);
                        ConsoleUtils.WaitForKeyPress();
                        break;
                    case 1:
                        PrintCart(carts, products);
                        int productId = ConsoleUtils.GetIntFromUser("Ange produktid: ");
                        int quantity = ConsoleUtils.GetIntFromUser("Ange antal: ");
                        ChangeQuantityOfProduct(productId, userid, quantity);
                        break;
                    case 2:
                        PrintCart(carts, products);
                        productId = ConsoleUtils.GetIntFromUser("Ange produktid: ");
                        RemoveProductFromCart(productId, userid);
                        break;
                    case 3:
                        EmptyCartFromProducts(userid);
                        break;
                    case 4:
                        MyShopPage.Run();
                        break;
                    case 5:
                        CheckOutPage checkOut = new();
                        checkOut.Run();
                        break;
                }
                Console.ReadKey();
            }
        }
        public void PrintCart(List<Cart> carts, List<Product> products)
        {
            string[] beskrivning = { "Produktnamn", "Antal", "Pris", "Totalpris"};
            Console.WriteLine();
            Console.WriteLine($"{beskrivning[0].PadRight(25)}{beskrivning[1].PadRight(7)}{beskrivning[2].PadRight(6)}{beskrivning[3]}");

            foreach (Cart c in carts)
            {
                var product = products.FirstOrDefault(p => p.Id == c.ProductId);
                if (product != null)
                {
                    Console.WriteLine($"[{c.ProductId}]{product.Name.PadRight(24)}{c.Quantity}\t{c.UnitPrice}\t{c.TotalPrice}");
                }
            }
            using (var db = new MyDbContext())
            {
                float totalPrice = 0;
                foreach (var o in db.Carts.Select(x => x))
                {
                    float sum = ((float)o.UnitPrice * (float)o.Quantity);
                    totalPrice += sum;
                }
                Console.WriteLine($"===================\nTotalsumman: {totalPrice} kr");
            }
            Console.WriteLine();
            ConsoleUtils.WaitForKeyPress();
            Run();
        }
        public List<Cart> GetProductsInCart(int userid)
        {
            List<Cart> carts = new List<Cart>();
            using (var db = new MyDbContext())
            {
                var query = (from c in db.Carts
                             join p in db.Products on c.ProductId equals p.Id
                             where c.UserId == userid
                             select c).ToList();
                for (int i = 0; i < 1; i++)
                {
                    carts.AddRange(query);
                }
            }
            return carts;
        }
        public void ChangeQuantityOfProduct(int productId, int userId, int quantity)
        {

            using (var db = new MyDbContext())
            {
                var productInCart = (from c in db.Carts
                                     join p in db.Products on c.ProductId equals p.Id
                                     where c.ProductId == productId && c.UserId == userId
                                     && quantity <= p.UnitsInStock
                                     select c).SingleOrDefault();
                if (productInCart != null)
                {
                    productInCart.Quantity = quantity;
                }
                else
                {
                    Console.WriteLine("Överskrider lagersaldo. Inga ändringar gjorda");
                }
                db.SaveChanges();
            };
        }
        public void RemoveProductFromCart(int productId, int userId)
        {
            using (var db = new MyDbContext())
            {
                var productInCart = from c in db.Carts
                                    join p in db.Products on c.ProductId equals p.Id
                                    where c.ProductId == productId && c.UserId == userId
                                    select c;
                if (productInCart != null)
                {
                    db.Carts.RemoveRange(productInCart);
                }
                else
                {
                    Console.WriteLine("Plockade inte bort produkten.");
                }
                db.SaveChanges();
            };
        }
        public bool EmptyCartFromProducts(int userId)
        {
            GetProductsInCart(userId);
            using (var db = new MyDbContext())
            {
                
                var productInCart = from c in db.Carts
                                    join p in db.Products on c.ProductId equals p.Id
                                    where c.UserId == userId
                                    select c;
                if (productInCart != null)
                {
                    string answer = ConsoleUtils.GetStringFromUser("Är du säker? j/n");
                    if (answer.Trim().ToLower().StartsWith("j"))
                    {
                        db.Carts.RemoveRange(productInCart);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Varukorgen tömdes inte");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Varukorgen tömdes inte.");
                    return false;
                }
                db.SaveChanges();
            };
        }
    }
}

