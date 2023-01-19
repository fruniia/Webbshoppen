using Webbshoppen.Data;
using Webbshoppen.Pages;

namespace Webbshoppen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "~M-A-M~";
            Console.CursorVisible = false;
            StartPage startPage = new StartPage();
            startPage.Run();
        }
       
    }
}