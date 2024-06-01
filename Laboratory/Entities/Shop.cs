using Laboratory.Enums;
using Laboratory.Helpers;

namespace Laboratory.Entities
{
    public class Shop
    {
        public string Name { get; set; }
        public double TotalIncome { get; set; }

        public Product[] Products;

        public Shop()
        {
            Products = [];
        }

        public void ShowShopDetails()
        {
            ConsoleHelper.WriteLine(ConsoleColor.Cyan, $"\nShop name: {Name}, Total income: {TotalIncome}$\n");
        }

        public void ShowProductDetails()
        {
            if (CheckProductsArray())
            {
                foreach (var item in Products)
                {
                    ConsoleHelper.WriteLine(ConsoleColor.Cyan, $"\nProduct name: {item.Name}, Price: {item.Price}$, Count: {item.Count}, Product Type: {item.Type}");
                }
                Console.WriteLine();
            }
        }

        public string GetInput(string inputMessage)
        {
            Console.Write(inputMessage);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                ConsoleHelper.WriteLine(ConsoleColor.Red, "Input can not be empty.");
                return GetInput(inputMessage);        // burada əgər input boşdursa, istifadəçidən yenidən input almaq üçün metodun özünü çağırırıq. Buna "recursion" deyilir. 
            }
            return input;
        }

        public int GetValidInput(string inputMessage, string errorMessage)
        {
            while (true)
            {
                Console.Write(inputMessage);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.WriteLine(ConsoleColor.Red, "Input can not be empty.");
                    continue;
                }

                bool isNumber = int.TryParse(input, out int result);

                if (isNumber)
                {
                    if (result > 0)
                    {
                        return result;
                    }
                }
                ConsoleHelper.WriteLine(ConsoleColor.Red, errorMessage);
            }
        }

        private ProductType GetValidProductType()
        {
            while (true)
            {
                Console.Write("Enter product type (c for coffee, t for tea): ");
                string input = Console.ReadLine().Trim().ToLower();

                switch (input)
                {
                    case "c":
                        return ProductType.Coffee;
                    case "t":
                        return ProductType.Tea;
                    default:
                        ConsoleHelper.WriteLine(ConsoleColor.Red, "Invalid product type. Try again.");
                        break;
                }
            }
        }

        public bool IsProductExists(string name)
        {
            foreach (var item in Products)
            {
                if (item.Name.Trim().ToLower() == name.Trim().ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public void CreateProduct()
        {
            Console.Clear();

            string productName = GetInput("Enter product name: ");

            if (IsProductExists(productName))
            {
                ShowMessageAndWait("Product already exists. Returning to main menu ...");
                return;
            }

            int price = GetValidInput("Enter product price: ", "Invalid price. Try again.");
            int count = GetValidInput("Enter product count: ", "Invalid count. Try again.");

            ProductType productType = GetValidProductType();

            switch (productType)
            {
                case ProductType.Coffee:
                    Coffee coffee = new() { Name = productName, Price = price, Count = count, Type = productType };
                    AddProduct(coffee);
                    ConsoleHelper.WriteLine(ConsoleColor.Green, $"{coffee.Name} added successfully.");
                    break;
                case ProductType.Tea:
                    Tea tea = new() { Name = productName, Price = price, Count = count, Type = productType };
                    AddProduct(tea);
                    ConsoleHelper.WriteLine(ConsoleColor.Green, $"{tea.Name} added successfully.");
                    break;

                default:
                    ConsoleHelper.WriteLine(ConsoleColor.Red, "Invalid product type. Try again.");
                    return;
            }

            ShowMessageAndWait("Returning to main menu ........");
        }

        private void AddProduct(Product product)
        {
            Array.Resize(ref Products, Products.Length + 1);
            Products[^1] = product;
        }

        public void SellProduct()
        {
            if (!CheckProductsArray())
                return;

            string productName = GetInput("Enter product name you want to sell: ");
            int count = GetValidInput("Enter product count you want to sell: ", "Invalid count. Try again.");

            Product? product = null;          

            foreach (var item in Products)
            {
                if (item.Name.Trim().ToLower() == productName.Trim().ToLower())
                {
                    product = item;
                    break;
                }
            }

            if (product == null)
            {
                ConsoleHelper.WriteLine(ConsoleColor.Red, "Product not found.");
                return;
            }

            if (product.Count == 0)
            {
                ConsoleHelper.WriteLine(ConsoleColor.Red, "Product is out of stock.");  
                return;
            }

            if (product.Count < count)
            {
                ConsoleHelper.WriteLine(ConsoleColor.Red, "Not enough product in stock.");
                return;
            }

            product.Count -= count;
            TotalIncome += product.Price * count;

            ShowMessageAndWait($"{count} {product.Name} sold successfully. Total income: {TotalIncome}$\nReturning main menu .........");

            return;

        }

        private void ShowMessageAndWait(string message, ConsoleColor color = ConsoleColor.Cyan, int waitingTime = 2000)
        {
            ConsoleHelper.WriteLine(color, message);
            Thread.Sleep(waitingTime);
            Console.Clear();
        }

        public bool CheckProductsArray()
        {
            if (Products.Length == 0)
            {
                ShowMessageAndWait("Shop is empty. Add product first. Returning to main menu ...");
                return false;
            }
            return true;
        }
    }
}
