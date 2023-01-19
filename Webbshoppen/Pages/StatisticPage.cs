using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Webbshoppen.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Webbshoppen.Pages.AdminPage;

namespace Webbshoppen.Pages
{
    public enum StatisticMenu
    {
        Populäraste_kategorin,
        Populäraste_produkten_herr_eller_dam,
        Populäraste_leverantörerna,
        Lagersaldo_per_vara,
        Tillbaka_till_Adminsidan,
        Exit
    }
    internal class StatisticPage
    {
        public void Run()
        {
            AdminPage adminPage = new();
            bool running = true;
            while (running)
            {
                string prompt = "Statistik";
                string[] options = Enum.GetNames(typeof(StatisticMenu));

                Menu statisticMenu = new Menu(prompt, options);
                int selectedIndex = statisticMenu.Run();
                switch (selectedIndex)
                {
                    case 0:
                        List<Product> categoryList = GetMostPopularCategory();
                        SetMostPopularCategory(categoryList);
                        break;
                    case 1:
                        List<Product> productList = GetPopularGenderProduct();
                        SetPopularGenderProduct(productList);
                        break;
                    case 2:
                        List<Product> supplierList = GetMostPopularSupplier();
                        SetMostPopularSupplier(supplierList);                        
                        break;
                    case 3:
                        List<Product> unitsInStock = GetUnitsInStock();
                        SetUnitsInStock(unitsInStock);
                        break;
                    case 4:
                        adminPage.Run();
                        break;
                    case 5:
                        running = false;
                        ConsoleUtils.QuitConsole();
                        break;
                }
                Console.ReadKey();
            }
        }

        public List<Product> GetUnitsInStock()
        {
            string connString = "Server=tcp:mohammad2.database.windows.net,1433;Initial Catalog=Webbshopp;Persist Security Info=False;User ID=mohammad;Password=alzuabi1#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var sql = @"SELECT p.UnitsInStock, P.Name FROM Products p order by p.UnitsInStock DESC";

            var unitsInStock = new List<Product>();

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                unitsInStock = connection.Query<Product>(sql).ToList();
                connection.Close();
            }

            return unitsInStock;
        }
        public List<Product> SetUnitsInStock(List<Product> unitsInStock)
        {
            Console.WriteLine();
            Console.WriteLine("Lagersaldo per vara: ");
            foreach (Product units in unitsInStock)
            {
                Console.WriteLine($"{units.Name.PadRight(20)} {units.UnitsInStock} st");
            }
            return unitsInStock;
        }
        public List<Product> GetMostPopularSupplier()
        {
            string connString = "Server=tcp:mohammad2.database.windows.net,1433;Initial Catalog=Webbshopp;Persist Security Info=False;User ID=mohammad;Password=alzuabi1#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var sql = @"SELECT s.Name ,
                        COUNT(o.ProductId) ANTAL
                        FROM OrderDetails o
                        JOIN Products p on o.ProductId = p.Id
                        JOIN Suppliers s on s.Id = p.SupplierId
                        WHERE s.Id = p.SupplierId
                        GROUP BY s.Name
                        ORDER BY ANTAL DESC";

            var supplierList = new List<Product>();

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                supplierList = connection.Query<Product>(sql).ToList();
                connection.Close();
            }

            return supplierList;
        }
        public List<Product> SetMostPopularSupplier(List<Product> supplierId)
        {
            Console.WriteLine();
            Console.WriteLine("Populäraste leverantörerna: ");
            foreach (Product supplier in supplierId)
            {
                Console.WriteLine($"{supplier.Name.PadRight(15)}");
            }
            return supplierId;
        }
        public List<Product> GetPopularGenderProduct()
        {
            string connString = "Server=tcp:mohammad2.database.windows.net,1433;Initial Catalog=Webbshopp;Persist Security Info=False;User ID=mohammad;Password=alzuabi1#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var sql = $@"SELECT TOP 1 Count(p.Name) AS Max, p.Name, p.Description FROM Products p
                      JOIN OrderDetails od ON p.Id = od.ProductId
                      JOIN Orders o ON od.OrderId = o.id
                      WHERE p.Description LIKE '%Herr%' OR p.Description LIKE '%Dam%'
                      GROUP BY p.Name, p.Description";

            var productList = new List<Product>();

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                productList = connection.Query<Product>(sql).ToList();
                connection.Close();
            }

            return productList;
        }
        public List<Product> SetPopularGenderProduct(List<Product> productList)
        {
            Console.WriteLine();
            foreach (Product product in productList)
            {
                Console.WriteLine($"{product.Name.PadRight(15)}{product.Description}");
            }
            return productList;
        }

        public List<Product> GetMostPopularCategory()
        {
            string connString = "Server=tcp:mohammad2.database.windows.net,1433;Initial Catalog=Webbshopp;Persist Security Info=False;User ID=mohammad;Password=alzuabi1#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var sql = $@"SELECT TOP 1  c.Name,
                        COUNT(o.ProductId) ANTALPRODUCT
                        FROM OrderDetails o
                        JOIN Products p on p.Id = o.ProductId
                        JOIN Categories c on c.Id = p.CategoryId
                        WHERE p.CategoryId = c.Id
                        GROUP BY c.Name
                        ORDER BY c.Name DESC";

            var categoryList = new List<Product>();

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                categoryList = connection.Query<Product>(sql).ToList();
                connection.Close();
            }

            return categoryList;
        }
        public List<Product> SetMostPopularCategory(List<Product> categoryList)
        {
            Console.WriteLine();
            foreach (Product category in categoryList)
            {
                Console.WriteLine($"Populäraste kategorin: {category.Name}");
            }
            return categoryList;
        }
    }
}
