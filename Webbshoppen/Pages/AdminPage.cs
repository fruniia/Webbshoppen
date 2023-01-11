using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshoppen.Data;
using Webbshoppen.Models;

namespace Webbshoppen.Pages
{
    internal class AdminPage
    {
        SupplierManager supplier = new();
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
                    ShowProducts();
                    break;
                case 1:
                    AddProduct();
                    break;
                case 2:
                    RemoveProduct();
                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:
                    ShowCategories();
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
            }
        }
            
        public void AddProduct()
        {
            
            Console.WriteLine("kategorier");
            ShowCategories();
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
                db.Add(product);  
                db.SaveChanges();
            }
        }

        
        public void ShowProducts()
        {

            using(var db = new MyDbContext())
            {
                var product = from p in db.Products
                              join c in db.Categories on p.CategoryId equals c.Id
                              join s in db.Suppliers on p.SupplierId equals s.Id
                              select new 
                              {
                                  Selected = p.Selected,
                                  Name = p.Name,
                                  UnitPrice = p.UnitPrice,
                                  Description = p.Description,
                                  UnitsInStock = p.UnitsInStock,
                                  CategoryName = c.Name,
                                  SupplierName = s.Name

                              };

                foreach(var l in product)
                {
                    Console.WriteLine($"{(l.Selected == true ? "Utvald" : " ")}\t {l.Name} {l.UnitPrice} {l.Description} {l.UnitsInStock} {l.CategoryName} {l.SupplierName}");
                }

            }

        }
        public void RemoveProduct()
        {
            ShowProducts();
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
            }
        }

        public void AlterProductName()
        {
            //TODO: Ändra produkter
            //Ta reda på produktens Id
            //Vilken kolumn vill admin ändra
            //Name,Unitprice, UnitsinStock, Selected, Description
            ShowProducts();
            using (var db = new MyDbContext())
            {
                var productId = ConsoleUtils.GetIntFromUser("Ange Id på produkten du vill ändra");
                var alterProductName = (from p in db.Products
                                        where p.Id == (productId)
                                        select p).SingleOrDefault();
                if (alterProductName != null)
                {
                    var productName = ConsoleUtils.GetStringFromUser($"Ange det nya namnet på produkten: ");
                    alterProductName.Name = productName;
                    db.SaveChanges();

                }
            }
        }
        public void AlterProductPrice() 
        { 
        }
        public void AlterUnitsInStock() 
        { 
        }
        public void AlterSelectedProduct()
        {
            //TODO: Utvalda produkter på första startsidan
        }
        public void AlterProductDescription() 
        {
        }

        public void ShowCategories()
        {
            using(var db = new MyDbContext())
            {               
                foreach(var category in db.Categories.Select(x=>x))
                {
                    Console.WriteLine($"{category.Id} {category.Name}");
                }
            }
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
