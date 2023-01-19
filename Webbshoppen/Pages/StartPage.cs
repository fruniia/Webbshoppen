using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Pages
{
    public enum StartMenu
    {
        Logga_in_som_administratör,
        Logga_in_som_kund,
        Shoppa_utan_inloggning,
        Avsluta
    }
    internal class StartPage
    {
        public StartPage()
        {

        }
        public void Run()
        {
            UserPage userPage = new();
            ShopPage shopPage = new();
            string prompt = ("Webshop ~M-A-M~\n\n");
            prompt += PrintWelcomeMessage();
            Console.WriteLine();
            string[] startOptions = Enum.GetNames(typeof(StartMenu));
            Menu startMenu = new Menu(prompt, startOptions);
            int selectedIndex = startMenu.Run();
            PrintWelcomeMessage();
            switch (selectedIndex)
            {
                case 0:
                    AdminPage a = new AdminPage();
                    a.Run();
                    break;
                case 1:
                    userPage.Run();
                    break;
                case 2:
                    shopPage.Run();
                    break;
                case 3:
                    ConsoleUtils.QuitConsole();
                    break;
            }
        }
        public static string PrintWelcomeMessage()
        {
            ProductPage product = new();
            string startMessage = product.ShowSelectedProducts();
            return startMessage;
        }
    }
}
