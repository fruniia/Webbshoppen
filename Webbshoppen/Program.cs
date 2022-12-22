using Webbshoppen.Data;

namespace Webbshoppen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SupplierManager sm = new();
            sm.AddSuppliers();
        }

    }
}