using System;

namespace CSharpOverload
{
    class Program
    {
        static void Main(string[] args)
        {
            Foo f = new Foo();
            int p1 = 223;

            f.DoSomething(p1, "test");

            f.DoSomething(p1, "test", "test2");


            Console.ReadKey();
        }

        class Foo
        {
            //A
            //internal void DoSomething(double nbr, string str)
            //{
            //    Console.WriteLine("Called A");
            //}

            //B
            public void DoSomething(float nbr, string str, string str2 = "optional")
            {
                Console.WriteLine("Called B");
            }

            //C
            //public void DoSomething(int nbr, string str)
            //{
            //    Console.WriteLine("Called C");
            //}

            //D
            //internal void DoSomething(decimal nbr, string str)
            //{
            //    Console.WriteLine("Called D");
            //}

            //E
            internal void DoSomething(decimal nbr, string str, byte b = 3)
            {
                Console.WriteLine("Called E");
            }
        }
    }
}
