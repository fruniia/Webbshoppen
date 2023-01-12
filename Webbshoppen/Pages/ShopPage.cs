using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Data;
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
                        //Visa_alla_kategorier,
                        category.ShowCategories();
                        break;
                    case 1:
                        //Sök,
                        break;
                    case 2:
                        //Varukorg,
                        break;
                    case 3:
                        //Betala,
                        break;
                    case 4:
                        //Mina_sidor,
                        break;
                    case 5:
                        running = false;
                        ConsoleUtils.QuitConsole();
                        break; 

                }
                Console.ReadKey();
            }
        }

    }
}
