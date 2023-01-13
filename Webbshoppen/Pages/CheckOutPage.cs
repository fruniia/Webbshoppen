using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Pages
{
    internal class CheckOutPage
    {
        //Fraktvy
        //Namn, adress, , postnummer, stad, land, telefon, homedelivery, shippingprice
        //Betalavy
        //Produkter visas med pris
        //Pris med frakt, samt moms, 
        //Val av betalning 
        public void Run()
        {
            string prompt = ("Dags att betala");
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
                    //Logga in som kund
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
