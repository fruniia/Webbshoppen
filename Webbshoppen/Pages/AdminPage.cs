using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Data;
using Webbshoppen.Models;

namespace Webbshoppen.Pages
{
    internal class AdminPage
    {
        ProductManager product = new();
        SupplierManager supplier = new();
        StartPage startPage = new();
        CategoryManager category = new();
        public enum AdminMenu
        {
            Visa_alla_produkter,
            Lägg_till_produkt,
            Ta_bort_produkt,
            Ändra_namn_på_produkt,
            Ändra_pris_på_produkt,
            Ändra_lagersaldo_på_produkt,
            Ändra_utvalda_produkter,
            Ändra_beskrivning_på_produkt,
            Visa_alla_kategorier,
            Lägg_till_kategori,
            Ta_bort_kategori,
            Statistik,
            Tillbaka_till_startsida,
            Exit
        }

        public AdminPage()
        {

        }

        public void Run()
        {
            
            bool running = true;
            while (running)
            {
                string prompt = "Vad vill du administrera?";
                string[] options = Enum.GetNames(typeof(AdminMenu));

                Menu adminMenu = new Menu(prompt, options);
                int selectedIndex = adminMenu.Run();
                switch (selectedIndex)
                {
                    case 0:
                        product.ShowProducts();
                        break;
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        RemoveProduct();
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        AlterProduct(selectedIndex);
                        break;
                    case 8:
                        category.ShowCategories();
                        break;
                    case 9:
                        AddCategory();
                        break;
                    case 10:
                        RemoveCategory();
                        break;
                    case 11:
                        //TODO: Statistics
                        break;
                    case 12:
                        startPage.Run();
                        break;
                    case 13:
                        running = false;
                        ConsoleUtils.QuitConsole();
                        break;
                }
                Console.ReadKey();
            }
        }
        public void AddProduct()
        {

            Console.WriteLine("Kategorier");
            category.ShowCategories();
            Console.WriteLine("==================");
            Console.WriteLine("Leverantörer");
            supplier.ShowSuppliers();
            Console.WriteLine("===================");

            var productname = ConsoleUtils.GetStringFromUser("Ange produktnamn: ");
            var price = ConsoleUtils.GetFloatFromUser("Ange pris: ");
            var description = ConsoleUtils.GetStringFromUser("Ange information om produkten: ");
            var unitsInStock = ConsoleUtils.GetIntFromUser("Ange lagersaldo: ");
            var categoryId = ConsoleUtils.GetIntFromUser("Ange Kategori-id: ");
            var supplierId = ConsoleUtils.GetIntFromUser("Ange leverantör-id: ");
            var selectedAsInt = ConsoleUtils.GetIntFromUser("Ange 1 om utvald annars 0: ");
            var selected = (selectedAsInt == 1 ? true : false);

            using (var db = new MyDbContext())
            {
                var product = new Product
                {
                    Selected = selected,
                    Name = productname,
                    UnitPrice = price,
                    Description = description,
                    UnitsInStock = unitsInStock,
                    CategoryId = categoryId,
                    SupplierId = supplierId
                };
                var confirm = ConsoleUtils.GetStringFromUser($"Vill du lägga till produkten: {productname} \n Bekräfta med j");
                if (confirm.Trim().ToLower().StartsWith("j"))
                {
                    try
                    {
                        db.Add(product);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Felaktig val av leverantörs- eller kategori-id");
                        ConsoleUtils.WaitForKeyPress();
                        Console.Clear();
                        AddProduct();
                    }
                }
                else
                {
                    Console.WriteLine("Produkten lades inte till.");
                };
                ConsoleUtils.WaitForKeyPress();
                Run();
            }
        }
      
        public void RemoveProduct()
        {
            product.ShowProducts();
            using (var db = new MyDbContext())
            {
                var productId = ConsoleUtils.GetIntFromUser("Ange id på produkten du vill ta bort: ");
                var deleteProduct = (from p in db.Products
                                     where p.Id == (productId)
                                     select p).SingleOrDefault();
                if (deleteProduct != null)
                {
                    var productAnswer = ConsoleUtils.GetStringFromUser($"Är du säker på att du vill ta bort? j/n ");
                    if (productAnswer.Trim().ToLower().StartsWith("j"))
                    {
                        db.Products.Remove(deleteProduct);
                        db.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Produkten plockades inte bort");
                    }
                }
                else
                {
                    Console.WriteLine("Id't du angav finns inte.");
                }
                ConsoleUtils.WaitForKeyPress();
                Run();
            }
        }
        public void AlterProduct(int selectedIndex)
        {
            product.ShowProducts();
            using (var db = new MyDbContext())
            {
                var productId = ConsoleUtils.GetIntFromUser("Ange Id på produkten du vill ändra");
                var alterProduct = (from p in db.Products
                                    where p.Id == (productId)
                                    select p).SingleOrDefault();
                switch (selectedIndex)
                {
                    case (int)AdminMenu.Ändra_namn_på_produkt:
                        var productName = ConsoleUtils.GetStringFromUser($"Ange det nya namnet på produkten: ");
                        alterProduct.Name = productName;
                        break;
                    case (int)AdminMenu.Ändra_pris_på_produkt:
                        var productPrice = ConsoleUtils.GetFloatFromUser($"Ange det nya priset på produkten: ");
                        alterProduct.UnitPrice = productPrice;
                        break;
                    case (int)AdminMenu.Ändra_lagersaldo_på_produkt:
                        var unitsInStock = ConsoleUtils.GetIntFromUser($"Ange det nya lagersaldot för produkten: ");
                        alterProduct.UnitsInStock = unitsInStock;
                        break;
                    case (int)AdminMenu.Ändra_utvalda_produkter:
                        //TODO: Kolla så att det endast finns 3 utvalda produkter. Vilken utvald produkt skall tas bort
                        var selectedAsInt = ConsoleUtils.GetIntFromUser("Ange 1 om utvald annars 0");
                        var selected = (selectedAsInt == 1 ? true : false);
                        alterProduct.Selected = selected;
                        break;
                    case (int)AdminMenu.Ändra_beskrivning_på_produkt:
                        var description = ConsoleUtils.GetStringFromUser($"Ange en ny beskrivning för produkten: ");
                        alterProduct.Description = description;
                        break;
                }
                db.SaveChanges();
            }
        }
        public void AddCategory()
        {
            Console.WriteLine("Kategorier");
            category.ShowCategories();
            Console.WriteLine("==================");

            var categoryName = ConsoleUtils.GetStringFromUser("Ange Kategori: ");

            using (var db = new MyDbContext())
            {
                var category = new Category
                {
                    Name = categoryName
                };
                var confirm = ConsoleUtils.GetStringFromUser($"Vill du lägga till kategorin: {categoryName} \n Bekräfta med j: ");
                if (confirm.Trim().ToLower().StartsWith("j"))
                {
                    db.Add(category);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Kategorin lades inte till.");
                };
                ConsoleUtils.WaitForKeyPress();
                Run();
            }
        }
        public void RemoveCategory()
        {
            category.ShowCategories();
            using (var db = new MyDbContext())
            {
                var categoryId = ConsoleUtils.GetIntFromUser("Ange id på kategorin du vill ta bort: ");
                var deleteCategory = (from c in db.Categories
                                      where c.Id == (categoryId)
                                      select c).SingleOrDefault();
                if (deleteCategory != null)
                {
                    var categoryAnswer = ConsoleUtils.GetStringFromUser($"Är du säker på att du vill ta bort? j/n ");
                    if (categoryAnswer.Trim().ToLower().StartsWith("j"))
                    {
                        try
                        {
                            db.Categories.Remove(deleteCategory);
                            db.SaveChanges();
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine($"{categoryId} innehåller produkter");
                        }    
                    }
                    else
                    {
                        Console.WriteLine("Kategorin plockades inte bort.");
                    }
                }
                else
                {
                    Console.WriteLine("Id't du angav finns inte.");
                }
                ConsoleUtils.WaitForKeyPress();
                Run();
            }

        }
    }
}
