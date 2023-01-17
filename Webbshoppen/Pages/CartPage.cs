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

namespace Webbshoppen.Pages
{
    internal class CartPage
    {
        ShopPage MyShopPage = new();
        public enum CartOptions
        {
            Ändra_antal,
            Ta_bort_produkt,
            Töm_varukorg,
            Fortsätt_shoppa,
            Gå_till_betalning,
            Visa_varukorg
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
                string[] options = Enum.GetNames(typeof(CartOptions));

                Menu cartMenu = new Menu(prompt, options);
                int selectedIndex = cartMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        ShowProductsInCart(userid);
                        int productId = ConsoleUtils.GetIntFromUser("Ange produktid: ");
                        int quantity = ConsoleUtils.GetIntFromUser("Ange antal: ");
                        ChangeQuantityOfProduct(productId, userid, quantity);
                        break;
                    case 1:
                        ShowProductsInCart(userid);
                        productId = ConsoleUtils.GetIntFromUser("Ange produktid: ");
                        RemoveProductFromCart(productId, userid);
                        break;
                    case 2:
                        EmptyCartFromProducts(userid);
                        break;
                    case 3:
                        MyShopPage.Run();
                        break;
                    case 4:
                        CheckOutPage checkOut = new();
                        checkOut.Run();
                        break;
                    case 5:
                        ShowProductsInCart(userid);
                        ConsoleUtils.WaitForKeyPress();
                        break;
                }
                Console.ReadKey();
            }
        }
        public void ShowProductsInCart(int userid)
        {
            using (var db = new MyDbContext())
            {
                var query = from c in db.Carts
                            join p in db.Products on c.ProductId equals p.Id
                            where c.UserId == userid
                            select new
                            {
                                Id = p.Id,
                                ProductName = p.Name,
                                Quantity = c.Quantity,
                                UnitPrice = c.UnitPrice,
                                TotalPrice = c.TotalPrice,
                            };

                foreach (var value in query)
                {
                    Console.WriteLine($"{value.Id}\t{value.ProductName}\t{value.Quantity}\t{value.UnitPrice}\t{value.TotalPrice}");
                }
            }
            //Priset visas och summan av produkterna visas längst ner
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
        public void EmptyCartFromProducts(int userId)
        {
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
                    }
                    else
                    {
                        Console.WriteLine("Varukorgen tömdes inte");
                    }
                }
                else
                {
                    Console.WriteLine("Varukorgen tömdes inte.");
                }
                db.SaveChanges();
            };
        }

        public void GoToCheckOut()
        {
            //Skicka med varukorgen till CheckOut
        }
    }
}

