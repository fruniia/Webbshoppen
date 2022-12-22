using Webbshoppen.Data;

namespace Webbshoppen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductManager pm = new();
            pm.AddProducts();
        }

    }
}