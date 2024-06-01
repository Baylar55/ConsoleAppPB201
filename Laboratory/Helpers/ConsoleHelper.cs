namespace Laboratory.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteLine(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void Write(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void WriteDactylo(string text)
        {
            foreach (var item in text)
            {
                Console.Write(item);
                Thread.Sleep(30);
            }
        }

        public static void WriteDactylo(ConsoleColor color, string text)
        {
            foreach (var item in text)
            {
                Write(color, item.ToString());
                Thread.Sleep(30);
            }
        }
    }
}
