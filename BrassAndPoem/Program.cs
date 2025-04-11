public class Program
{
    // Initialize data
    public static List<ProductType> productTypes = new()
    {
        new ProductType { Id = 1, Title = "Poetry Book" },
        new ProductType { Id = 2, Title = "Brass Instrument" }
    };

    public static List<Product> products = new()
    {
        new Product { Name = "The Raven", Price = 9.99m, ProductTypeId = 1 },
        new Product { Name = "Leaves of Grass", Price = 12.50m, ProductTypeId = 1 },
        new Product { Name = "Trumpet", Price = 199.99m, ProductTypeId = 2 },
        new Product { Name = "Trombone", Price = 249.99m, ProductTypeId = 2 },
        new Product { Name = "The Odyssey", Price = 14.99m, ProductTypeId = 1 }
    };

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Brass & Poem!");

        // Main loop
        bool running = true;
        while (running)
        {
            DisplayMenu();
            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("No input. Exiting.");
                break;
            }
            switch (input)
            {
                case "1":
                    DisplayAllProducts(products, productTypes);
                    break;
                case "2":
                    DeleteProduct(products, productTypes);
                    break;
                case "3":
                    AddProduct(products, productTypes);
                    break;
                case "4":
                    UpdateProduct(products, productTypes);
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    // Method implementations
    public static void DisplayMenu()
    {
        Console.WriteLine("1. Display all products");
        Console.WriteLine("2. Delete a product");
        Console.WriteLine("3. Add a new product");
        Console.WriteLine("4. Update product properties");
        Console.WriteLine("5. Exit");
    }

    public static void DisplayAllProducts(List<Product> products, List<ProductType> productTypes)
    {
        for (int i = 0; i < products.Count; i++)
        {
            Product product = products[i];
            ProductType? type = productTypes.FirstOrDefault(pt => pt.Id == product.ProductTypeId);
            Console.WriteLine($"{i + 1}. {product.Name} - ${product.Price} - Type: {type?.Title}");
        }
    }
    public static void DeleteProduct(List<Product> products, List<ProductType> productTypes)
    {
        DisplayAllProducts(products, productTypes);
        Console.Write("Enter product number to delete: ");

        string? input = Console.In.Peek() == -1 ? null : Console.ReadLine();
        if (input == null)
        {
            Console.WriteLine("No input detected. Cancelling.");
            return;
        }

        if (int.TryParse(input, out int index) && index > 0 && index <= products.Count)
        {
            products.RemoveAt(index - 1);
            Console.WriteLine("Product deleted.");
        }
        else
        {
            Console.WriteLine("Invalid product number");
        }
    }


    public static void AddProduct(List<Product> products, List<ProductType> productTypes)
    {
        const int maxAttempts = 3;
        int attempts = 0;

        // Name input
        string name = "";
        while (attempts < maxAttempts)
        {
            Console.Write("Product name: ");
            name = Console.ReadLine()?.Trim() ?? "";
            if (!string.IsNullOrEmpty(name)) break;
            Console.WriteLine("Name cannot be empty");
            attempts++;
        }

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Product creation cancelled");
            return;
        }

        // Price input
        decimal price = 0m;
        attempts = 0;
        while (attempts < maxAttempts)
        {
            Console.Write("Price (e.g. 150.99): ");
            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("No input detected. Cancelling.");
                return;
            }

            // Try parsing raw
            if (decimal.TryParse(input, out price)) break;

            // Try parsing after replacing comma with dot
            string modified = input.Replace(",", ".");
            if (decimal.TryParse(modified, out price)) break;

            Console.WriteLine("Invalid price format. Please try again.");
            attempts++;
        }

        if (attempts >= maxAttempts)
        {
            Console.WriteLine("Price input failed");
            return;
        }

        // Product type selection
        int typeNumber = 0;
        attempts = 0;
        while (attempts < maxAttempts)
        {
            DisplayProductTypes(productTypes);
            Console.Write("Type number: ");
            if (int.TryParse(Console.ReadLine(), out typeNumber) && typeNumber > 0 && typeNumber <= productTypes.Count)
                break;
            Console.WriteLine("Invalid type number");
            attempts++;
        }

        if (attempts >= maxAttempts || typeNumber == 0)
        {
            Console.WriteLine("Type selection failed");
            return;
        }

        products.Add(new Product
        {
            Name = name,
            Price = price,
            ProductTypeId = productTypes[typeNumber - 1].Id
        });

        Console.WriteLine("Product added successfully!");
    }

    public static void UpdateProduct(List<Product> products, List<ProductType> productTypes)
    {
        DisplayAllProducts(products, productTypes);
        Console.Write("Enter product number: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= products.Count)
        {
            Product product = products[index - 1];

            Console.Write($"New name [{product.Name}]: ");
            string name = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Product name cannot be empty. Please try again: ");
                name = Console.ReadLine()!;
            }
            product.Name = name;

            decimal price;
            while (true)
            {
                Console.Write($"New price [{product.Price}]: ");
                string? priceInput = Console.ReadLine();
                if (decimal.TryParse(priceInput, out price))
                {
                    product.Price = price;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid price format. Please try again.");
                }
            }
            DisplayProductTypes(productTypes);
            Console.Write($"New type [Current: {product.ProductTypeId}]: ");

            const int maxAttempts = 3;
            int attempts = 0;
            int typeNumber = 0;

            while (attempts < maxAttempts)
            {
                string? input = Console.In.Peek() == -1 ? null : Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("No input detected. Cancelling update.");
                    return;
                }

                if (int.TryParse(input, out typeNumber) && typeNumber > 0 && typeNumber <= productTypes.Count)
                {
                    product.ProductTypeId = productTypes[typeNumber - 1].Id;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid product type. Please try again.");
                    attempts++;
                }
            }

            if (attempts >= maxAttempts)
            {
                Console.WriteLine("Too many failed attempts. Type not updated.");
            }

        }
    }

    public static void DisplayProductTypes(List<ProductType> productTypes)
    {
        Console.WriteLine("Available types:");
        for (int i = 0; i < productTypes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {productTypes[i].Title}");
        }
    }
}