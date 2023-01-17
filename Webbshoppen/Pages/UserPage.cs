﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Models;
using static Webbshoppen.Pages.AdminPage;

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

        public enum UserMenu
        { 
            Beställningshistorik,
            Ändra_förnamn,
            Ändra_efternamn,
            Ändra_emailadress,
            Ändra_lösenord,
            Visa_varukorg,
            Shoppa,
            Avsluta
        }
        public void Run()
        {
            bool running = true;
            while (running)
            {
                string prompt = "Mina sidor";
                string[] options = Enum.GetNames(typeof(UserMenu));

                Menu userMenu = new Menu(prompt, options);
                int selectedIndex = userMenu.Run();
                int userid = 2;
                switch (selectedIndex)
                {
                    case 0:
                        //OrderPage orderPage = new();
                        // orderPage.Run();
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        ChangeUserDetails(userid, selectedIndex);
                        break;
                    case 5:
                        CartPage cartPage = new ();
                        cartPage.Run();
                        break;
                    case 6:
                        ShopPage shopPage = new ();
                        shopPage.Run();
                        break;
                    case 7:
                        running = false;
                        ConsoleUtils.QuitConsole();
                        break;
                }
                Console.ReadKey();
            }
        }
        public void ChangeUserDetails(int userid, int selectedIndex)
        { 
            using(var db = new MyDbContext())
            {   
                var alterUser = (from u in db.Users
                                    where u.Id == (userid)
                                    select u).SingleOrDefault();
                switch (selectedIndex)
                {
                    case (int)UserMenu.Ändra_förnamn:
                        Console.WriteLine($"Nuvarande förnamn: {alterUser.FirstName}");
                        var userFirstName = ConsoleUtils.GetStringFromUser($"Ange ditt nya förnamn: ");
                        alterUser.FirstName = userFirstName;
                        break;
                    case (int)UserMenu.Ändra_efternamn:
                        Console.WriteLine($"Nuvarande efternamn: {alterUser.LastName}");
                        var userLastName = ConsoleUtils.GetStringFromUser($"Ange ditt nya efternamn: ");
                        alterUser.LastName = userLastName;
                        break;
                    case (int)UserMenu.Ändra_emailadress:
                        Console.WriteLine($"Nuvarande email: {alterUser.Email}");
                        var email = ConsoleUtils.GetStringFromUser($"Ange ny emailadress: ");
                        alterUser.Email = email;
                        break;
                    case (int)UserMenu.Ändra_lösenord:
                        //Om tid finns be användaren mata in nuvarande lösenord innan nytt lösenord anges
                        var password = ConsoleUtils.GetStringFromUser("Ange nytt lösenord: ");
                        alterUser.Password = password;
                        break;
                }
                var confirm = ConsoleUtils.GetStringFromUser($"Vill du ändra?\n Bekräfta med j: ");
                if (confirm.Trim().ToLower().StartsWith("j"))
                {
                    db.SaveChanges();
                }
                else
                {
                    Run();
                }
            }
        }

        public void ShowOrders(int userid)
        {
            using (var db = new MyDbContext())
            {
                var productOrder = db.Products.Include(p => p.Orders);
                var orderProduct = db.Orders.Where(x => x.UserId == userid);

                foreach (var order in orderProduct)
                {
                    Console.WriteLine($"OrderId: {order.Id}");
                    foreach (var product in productOrder)
                    { 
                        //product.Orders; 
                    
                    }
                }
                //foreach (var item in db.Orders.Include(x => x.Products)
                //    .Where(x => x.UserId == userid);
                //{
                //    //Console.WriteLine($"Order{item.Key};");

                //    //foreach (var group in item)
                //    //{
                //    //    Console.WriteLine($"{item.OrderDate}\t");
                //    //    Console.WriteLine($"{item.Products.}");
                //    //    Datum
                //        //Produkt
                //        //Antal
                //        //Pris
                //        //Fraktpris
                //        //Moms
                //        //Totalpris
                //        //Betalningssätt
                //        //Fraktsätt
                //        //}
                //}
            }
        }
    }
}
