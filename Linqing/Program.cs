using System;
using System.Linq;

namespace Linqing
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            do
            {
                switch (input)
                {
                    case "a":
                        DeferredQry();
                        break;
                    default:
                        Console.WriteLine("No selection");
                        break;
                }

                input = Console.ReadLine();

            } while (input != "x");

            Console.ReadKey();
        }

        static void DeferredQry()
        {
            using (var fruitCtx = new FruitContext())
            {
                var AFruits = fruitCtx.Fruits.Where(f => f.FruitName.StartsWith("A"));

                Console.WriteLine(AFruits.ToList());

            }
        }
    }
}
