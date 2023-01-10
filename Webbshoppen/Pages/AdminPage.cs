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
            Ändra_produkt,
            Utvalda_produkt,
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

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

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
            //TODO: Ta bort produkter
        }

        public void AlterProduct()
        {
            //TODO: Ändra produkter

        }
        public void SelectedProduct()
        {
            //TODO: Utvalda produkter på första startsidan
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
