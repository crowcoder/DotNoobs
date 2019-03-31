using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linqing
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var fruitCtx = new FruitContext())
                {
                    var firstFruit = fruitCtx.Fruits.First();
                    Console.WriteLine(firstFruit.FruitName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();
        }
    }
}
