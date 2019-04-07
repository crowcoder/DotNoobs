using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    case "b":
                        ImmediateQryAggregate();
                        break;
                    case "c":
                        ChainQry();
                        break;
                    case "d":
                        Deferred2();
                        break;
                    case "e":
                        ReEvaluation();
                        break;
                    default:
                        Console.WriteLine("No selection");
                        break;
                }

                input = Console.ReadLine();

            } while (input != "x");

            Console.ReadKey();
        }

        //a
        static void DeferredQry()
        {
            using (var fruitCtx = new FruitContext())
            {
                var AFruits = fruitCtx.Fruits.Where(f => f.FruitName.StartsWith("A"));

                Console.WriteLine(AFruits.ToList());
            }
        }

        //b
        static void ImmediateQryAggregate()
        {
            using (var ctx= new FruitContext())
            {
                var fruitCount = ctx.Fruits.Sum(f => f.FruitID);
            }
        }

        //c
        static void ChainQry()
        {
            using (var fruitCtx = new FruitContext())
            {
                Debugger.Break();

                var qry = fruitCtx.Fruits.Select(f => f);

                qry = qry.Where(f => f.FruitName.StartsWith("A"));

                qry = qry.Where(f => f.FruitIsYummy == true);

                foreach (Fruit fruit in qry)
                {
                    Console.WriteLine(fruit);
                }
            }
        }

        //d
        static void Deferred2()
        {
            var numbers = new List<int>();
            numbers.Add(1);

            IEnumerable<int> query = numbers.Select(n => n * 10);    // Build query
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("sneak in extra element");

            numbers.Add(2);  // Sneak in an extra element
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        //e
        static void ReEvaluation()
        {
            var numbers = new List<int>() { 1, 2 };

            IEnumerable<int> query = numbers.Select(n => n * 10);

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            Debugger.Break();

            numbers.Clear();

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }
    }
}
