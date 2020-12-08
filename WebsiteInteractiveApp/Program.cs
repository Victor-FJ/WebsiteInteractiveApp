using System;
using System.Threading.Channels;

namespace WebsiteInteractiveApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Selenium driver = new Selenium();

            Console.WriteLine("Press to check if we are in school today: Esc to close");
            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                    break;
                driver.IsClassInSchool();
                Console.WriteLine("\n");
            }
        }
    }
}
