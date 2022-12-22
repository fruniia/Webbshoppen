using Webbshoppen.Data;

namespace Webbshoppen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CategoryManager cm = new();
            cm.AddCategories();
            SupplierManager sm = new();
            sm.AddSuppliers();
            ProductManager pm = new();
            pm.AddProducts();
        }

    }
}