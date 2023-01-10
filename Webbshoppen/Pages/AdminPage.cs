using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen.Pages
{
    internal class AdminPage
    {
        public enum AdminMenu
        {
            Visa_alla_produkter,
            Lägg_till_produkt,
            Ta_bort_produkt,
            Ändra_produkt,
            Utvalda_produkt,
            Visa_alla_kategorier,
            Lägg_till_kategori,
            Ta_bort_kategori,
            Tillbaka_till_startsida
        }

        public AdminPage()
        {

        }

        public void Run()
        {
            string prompt = "Vad vill du administrera?";
            string[] options = Enum.GetNames(typeof(AdminMenu));

            Menu adminMenu = new Menu(prompt, options);
            int selectedIndex = adminMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    break;
                case 1:

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
        public void AddProduct()
        {
            //TODO: Lägga till nya produkter
        }
        public void ShowProducts()
        {
            //Produktnamn
            //InfoText
            //Pris
            //Leverantör
            //Lagersaldo
            //Produktkategori

        }
        public void RemoveProduct()
        {
            //TODO: Ta bort produkter
        }

        public void AlterProduct()
        {
            //TODO: Ändra produkter

        }
        public void SelectedProduct()
        {
            //TODO: Utvalda produkter på första startsidan
        }

        public void ShowCategories()
        {

        }
        public void AddCategory()
        {
            //TODO: Lägga till nya Produktkategori
        }

        public void RemoveCategory()
        {
            //TODO: Ta bort Produktkategori
        }
    }
}
