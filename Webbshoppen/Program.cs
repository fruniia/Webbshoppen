using Webbshoppen.Data;
using Webbshoppen.Pages;

namespace Webbshoppen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CategoryManager cm = new();
            //cm.AddCategories();
            //SupplierManager sm = new();
            //sm.AddSuppliers();
            //ProductManager pm = new();
            //pm.AddProducts();
            //Console.Title = "Klädesshoppen";
            //Console.CursorVisible = false;
            AdminPage ap = new();
            StartPage sp = new StartPage();


        }

    }
}