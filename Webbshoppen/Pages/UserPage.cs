using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Models;

namespace Webbshoppen.Pages
{
    internal class UserPage
    {
        //TODO: Ändra uppgift om kunden
        //TODO: Beställningshistorik

        //Fortsätt utan inloggning
        //Handla
        //Logga in
        //Ny user
        //Skapa ny användare

        public int CheckUserDetails()
        {
            string emailaddress = ConsoleUtils.GetStringFromUser("Ange e-postadress: ");
            string password = ConsoleUtils.GetStringFromUser("Ange lösenord: ");
            int userid = 0;
            using (var db = new MyDbContext())
            {
                var getUserId = db.Users.Where(x => x.Email == emailaddress && x.Password == password)
                    .Select(x => x.Id).SingleOrDefault();
                if (getUserId != null)
                {
                    userid = Convert.ToInt32(getUserId);
                    if (userid == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Dina inloggningsuppgifter stämmer inte. Försök igen.");
                        ConsoleUtils.WaitForKeyPress();
                        CheckUserDetails();
                    }
                }
                return userid;
            }
        }

        public void LogInUser(int userid)
        {
            using(var db = new MyDbContext()) 
            { 
                var userDetails = db.Users.Select(x => x).Where(x => x.Id == userid);
                foreach (var value in userDetails)
                {
                    Console.WriteLine($"Välkommen {value.FirstName}");
                }
            }
        }
    }
}
