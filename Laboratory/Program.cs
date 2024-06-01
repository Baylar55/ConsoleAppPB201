using Laboratory.Entities;
using Laboratory.Helpers;

namespace Laboratory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Shop shop = new() { Name = "Code Academy", TotalIncome = 0 };

            while (true)
            {
                ShowMenu();
                int menu = GetMenuSelection();

                if (menu >= 1 && menu <= 5)
                {
                    switch (menu)
                    {
                        case 1:
                            shop.CreateProduct();
                            break;

                        case 2:
                            shop.SellProduct();
                            break;

                        case 3:
                            shop.ShowProductDetails();
                            break;

                        case 4:
                            shop.ShowShopDetails();
                            break;

                        case 5:
                            ConsoleHelper.WriteLine(ConsoleColor.Cyan, "Goodbye!");
                            return;
                    }
                }
                else
                {
                    ConsoleHelper.WriteLine(ConsoleColor.DarkRed, "Invalid menu number.Try again");
                }
            }


        }

        private static void ShowMenu()
        {
            Console.WriteLine("""
                              1. Add product to the shop.
                              2. Sell product from the shop.
                              3. Show all product details.
                              4  Show all shop details.
                              5. Exit from application.
                              """);
            Console.WriteLine();
            ConsoleHelper.WriteDactylo(text: "Enter menu number: ", color: ConsoleColor.Blue);
        }

        private static int GetMenuSelection()
        {
            string? menuStr = Console.ReadLine();

            if (int.TryParse(menuStr, out int menu))
                return menu;
            else
                return -1;

            //Və ya yuxarıdakı if-else ifadəsini qısaltmaq üçün aşağıdakı kodu istifadə edə bilərik. Burada ternary operator istifadə olunub.
            //return int.TryParse(menuStr, out int menu) ? menu : -1;
        }
    }
}
