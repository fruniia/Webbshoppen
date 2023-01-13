using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Webbshoppen.Pages.AdminPage;
using Webbshoppen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Webbshoppen.Pages
{
    internal class CartPage
    {
        ShopPage MyShopPage = new();
        enum CartOptions
        {
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
                string prompt = $"Varukorg\n{ShowProductsInCart(userid)}";
                //ShowProductsInCart(userid);
                string[] options = Enum.GetNames(typeof(CartOptions));

                Menu cartMenu = new Menu(prompt, options);
                int selectedIndex = cartMenu.Run();
                switch (selectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        MyShopPage.Run();
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
                Console.ReadKey();
            }
        }
        public string ShowProductsInCart(int userid)
        {
            using (var db = new MyDbContext())
            {
                //var query = db.Carts.Where(c => c.UserId == userid).Select(c => c.TotalPrice);
                foreach (var value in db.Carts.Include(p => p.Product).Where(c => c.UserId == userid))
                {
                    return $"{value.Id}\t{value.Product.Name}\t{value.Quantity}\t{value.UnitPrice}\t{value.TotalPrice}";
                    //$"\t{query.Count()}";
                }
                return "";
            }
            //Priset visas och summan av produkterna visas längst ner
        }
        public void ChangeQuantityOfProduct(int productId, int userId, int quantity)
        {
            //Möjlighet att ändra antal
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
        public void RemoveProductFromCart()
        {
            //Möjlighet att ta bort produkt
        }
        public void EmptyCartFromProducts()
        {
            //Ska tömmas när betalning är gjord
        }

        public void GoToCheckOut()
        {
            //Skicka med varukorgen till CheckOut
        }
    }
}

