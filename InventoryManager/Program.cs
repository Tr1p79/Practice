using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

class InventoryManager
{
    private List<Product> inventory = new List<Product>();

    public void AddProduct(string name, string category, decimal price, int quantity)
    {
        var existingProduct = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (existingProduct != null)
        {
            Console.WriteLine("Produkt s tímto názvem již existuje. Aktualizuji množství.");
            existingProduct.Quantity += quantity;
        }
        else
        {
            inventory.Add(new Product
            {
                Name = name,
                Category = category,
                Price = price,
                Quantity = quantity
            });
        }
        Console.WriteLine("Produkt byl úspěšně přidán/aktualizován.");
    }

    public void UpdateQuantity(string name, int change)
    {
        var product = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (product != null)
        {
            if (product.Quantity + change < 0)
            {
                Console.WriteLine("Chyba: Nelze odebrat více produktů, než je na skladě.");
                return;
            }
            product.Quantity += change;
            Console.WriteLine($"Množství produktu '{name}' bylo aktualizováno. Nové množství: {product.Quantity}");
        }
        else
        {
            Console.WriteLine("Produkt nebyl nalezen.");
        }
    }

    public void SearchProducts(string keyword)
    {
        var matchingProducts = inventory.Where(p =>
            p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            p.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matchingProducts.Any())
        {
            Console.WriteLine($"Nalezeno {matchingProducts.Count} produktů:");
            foreach (var product in matchingProducts)
            {
                Console.WriteLine($"- {product.Name} (Kategorie: {product.Category}, Cena: {product.Price:C}, Množství: {product.Quantity})");
            }
        }
        else
        {
            Console.WriteLine("Žádné produkty nebyly nalezeny.");
        }
    }

    public void DisplayLowStockProducts(int threshold)
    {
        var lowStockProducts = inventory.Where(p => p.Quantity < threshold).ToList();
        if (lowStockProducts.Any())
        {
            Console.WriteLine($"Produkty s nízkým stavem zásob (méně než {threshold}):");
            foreach (var product in lowStockProducts)
            {
                Console.WriteLine($"- {product.Name} (Množství: {product.Quantity})");
            }
        }
        else
        {
            Console.WriteLine("Žádné produkty nemají nízký stav zásob.");
        }
    }

    public void GenerateInventoryReport()
    {
        decimal totalValue = inventory.Sum(p => p.Price * p.Quantity);
        var categoryCounts = inventory.GroupBy(p => p.Category)
                                      .Select(g => new { Category = g.Key, Count = g.Count() })
                                      .OrderByDescending(x => x.Count);

        Console.WriteLine("Přehled inventáře:");
        Console.WriteLine($"Celková hodnota zásob: {totalValue:C}");
        Console.WriteLine("Počet položek v každé kategorii:");
        foreach (var category in categoryCounts)
        {
            Console.WriteLine($"- {category.Category}: {category.Count}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        InventoryManager manager = new InventoryManager();

        while (true)
        {
            Console.WriteLine("\nSprávce inventáře - Menu:");
            Console.WriteLine("1. Přidat nový produkt");
            Console.WriteLine("2. Aktualizovat množství produktu");
            Console.WriteLine("3. Vyhledat produkty");
            Console.WriteLine("4. Zobrazit produkty s nízkým stavem zásob");
            Console.WriteLine("5. Vygenerovat přehled inventáře");
            Console.WriteLine("6. Ukončit program");
            Console.Write("Vyberte možnost (1-6): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddNewProduct(manager);
                    break;
                case "2":
                    UpdateProductQuantity(manager);
                    break;
                case "3":
                    SearchProducts(manager);
                    break;
                case "4":
                    DisplayLowStockProducts(manager);
                    break;
                case "5":
                    manager.GenerateInventoryReport();
                    break;
                case "6":
                    Console.WriteLine("Děkujeme za použití Správce inventáře. Na shledanou!");
                    return;
                default:
                    Console.WriteLine("Neplatná volba. Zkuste to prosím znovu.");
                    break;
            }
        }
    }

    static void AddNewProduct(InventoryManager manager)
    {
        Console.Write("Název produktu: ");
        string name = Console.ReadLine();

        Console.Write("Kategorie: ");
        string category = Console.ReadLine();

        Console.Write("Cena: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Neplatná cena. Operace zrušena.");
            return;
        }

        Console.Write("Množství: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Neplatné množství. Operace zrušena.");
            return;
        }

        manager.AddProduct(name, category, price, quantity);
    }

    static void UpdateProductQuantity(InventoryManager manager)
    {
        Console.Write("Název produktu: ");
        string name = Console.ReadLine();

        Console.Write("Změna množství (použijte záporné číslo pro odebrání): ");
        if (!int.TryParse(Console.ReadLine(), out int change))
        {
            Console.WriteLine("Neplatná hodnota. Operace zrušena.");
            return;
        }

        manager.UpdateQuantity(name, change);
    }

    static void SearchProducts(InventoryManager manager)
    {
        Console.Write("Zadejte klíčové slovo pro vyhledávání: ");
        string keyword = Console.ReadLine();
        manager.SearchProducts(keyword);
    }

    static void DisplayLowStockProducts(InventoryManager manager)
    {
        Console.Write("Zadejte hranici pro nízký stav zásob: ");
        if (!int.TryParse(Console.ReadLine(), out int threshold))
        {
            Console.WriteLine("Neplatná hodnota. Použita výchozí hodnota 5.");
            threshold = 5;
        }

        manager.DisplayLowStockProducts(threshold);
    }
}