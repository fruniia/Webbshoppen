using Webbshoppen.Data;
using Webbshoppen.Pages;

namespace Webbshoppen
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Console.Title = "Klädesshoppen";
            //Console.CursorVisible = false;
            //StartPage sp = new StartPage();
            ////p.Run();
            ShopPage shopPage = new();
            shopPage.Run();
            //CartPage cartPage = new();
            //cartPage.Run();
            //CheckOutPage cop = new();          
            //cop.Run();
            //ProductPage pp = new();
            //pp.ShowSelectedProducts();




        }

    }
}