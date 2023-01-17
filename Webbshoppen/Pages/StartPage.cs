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
        //Välkomsttext
        //Tre utvald produkter
        //Inloggning

        public StartPage()
        {

        }

        public void Run()
        {
            string prompt = ("Välkommen till Webshoppen");
            string[] startOptions = Enum.GetNames(typeof(StartMenu));
            Menu startMenu = new Menu(prompt, startOptions);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    AdminPage a = new AdminPage();
                    a.Run();
                    break;
                case 1:
                    UserPage userPage= new ();
                    userPage.Run();
                    break;
                case 2:
                   // Shoppa som gäst
                    break;
                case 3:
                    ConsoleUtils.QuitConsole();
                    break;
            }
        }

    }
}
