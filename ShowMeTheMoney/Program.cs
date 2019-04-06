using System;

namespace ShowMeTheMoney
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
                        TenEqualsTen();
                        break;
                    case "b":
                        DecimalEquals();
                        break;
                    case "c":
                        NegativeFloat();
                        break;
                    case "d":
                        DecimalParts();
                        break;
                    default:
                        Console.WriteLine("No selection");
                        break;
                }

                input = Console.ReadLine();

            } while (input != "x");
        }

        //a
        static void TenEqualsTen()
        {
            double d1 = .1;
            double d2 = 1.1;

            double sum = d1 + d2;

            Console.WriteLine(sum);

            bool tenEqtwn = 1.2d == sum;

            Console.WriteLine(tenEqtwn);
        }

        //c
        static void NegativeFloat()
        {
            double d1 = -1.03;
            
            var d1AsBinary = BitConverter.DoubleToInt64Bits(d1);
            
            string bin = Convert.ToString(d1AsBinary, 2);
            Console.WriteLine(bin);
        }

        //d
        static void DecimalParts()
        {
            Decimal d = -1.03m; //not how becomes an int

            var parts = Decimal.GetBits(d);

            foreach (var part in parts)
            {
                Console.WriteLine(part);
            }

            //https://docs.microsoft.com/en-us/dotnet/api/system.decimal.getbits?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev15.query%3FappId%3DDev15IDEF1%26l%3DEN-US%26k%3Dk(System.Decimal.GetBits);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.7.1);k(DevLang-csharp)%26rd%3Dtrue&view=netframework-4.7.2
        }

        //b
        static void DecimalEquals()
        {
            decimal d1 = .1m;
            decimal d2 = 1.1m;

            decimal sum = d1 + d2;

            Console.WriteLine(sum);

            bool tenEqtwn = 1.2m == sum;

            Console.WriteLine(tenEqtwn);
        }
    }
}
