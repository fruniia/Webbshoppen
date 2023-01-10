using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Pages
{
    public enum UserCategory
    {
        Logga_in_som_administratör,
        Logga_in_som_kund,
        Avsluta
    }
    internal class StartPage
    {
        //Välkomsttext
        //Tre utvald produkter
        //Inloggning

        public StartPage()
        {
            Run();
        }

        public void Run()
        {
            string prompt = ("Välkommen till Webshoppen");
            string[] startOptions = Enum.GetNames(typeof(UserCategory));
            Menu startMenu = new Menu(prompt, startOptions);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    AdminPage a = new AdminPage();
                    a.Run();
                    break;
                case 1:
                    Console.WriteLine("hopp");
                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;
            }
        }

    }
}
