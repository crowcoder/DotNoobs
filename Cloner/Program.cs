using Newtonsoft.Json;
using System;

namespace Cloner
{
    class Program
    {
        static void Main(string[] args)
        {
            SloppyClone();

            DeepClone();

            EasyClone();

            Console.ReadKey();
        }

        static void SloppyClone()
        {
            Console.WriteLine("Sloppy:");

            Copyable copyable = new Copyable();
            copyable.RefTypeProperty = new AReferenceType { ANumber = 123, AString = "foo" };

            Console.WriteLine($"Original object: {copyable}");

            Copyable copyable2 = new Copyable();
            copyable2.RefTypeProperty = copyable.RefTypeProperty;

            copyable2.RefTypeProperty.ANumber = 456;
            copyable2.RefTypeProperty.AString = "bar";

            Console.WriteLine($"Cloned object: {copyable2}");
            Console.WriteLine($"Original object: {copyable}");
            Console.WriteLine();
        }

        static void DeepClone()
        {
            Console.WriteLine("Deep:");
            Copyable copyable = new Copyable();
            copyable.RefTypeProperty = new AReferenceType { ANumber = 123, AString = "foo" };
            Console.WriteLine($"Original object: {copyable}");

            Copyable copyable2 = new Copyable();
            copyable2.RefTypeProperty =
                new AReferenceType
                {
                    ANumber = copyable.RefTypeProperty.ANumber,
                    AString = copyable.RefTypeProperty.AString
                };

            Console.WriteLine(object.ReferenceEquals(copyable.RefTypeProperty.AString, copyable2.RefTypeProperty.AString));

            copyable2.RefTypeProperty.ANumber = 456;
            copyable2.RefTypeProperty.AString = "bar";

            Console.WriteLine(object.ReferenceEquals(copyable.RefTypeProperty.AString, copyable2.RefTypeProperty.AString));

            Console.WriteLine($"Cloned object: {copyable2}");
            Console.WriteLine($"Original object: {copyable}");
            Console.WriteLine();
        }

        static void EasyClone()
        {
            Console.WriteLine("Easy:");
            Copyable copyable = new Copyable();
            copyable.RefTypeProperty = new AReferenceType { ANumber = 123, AString = "foo" };
            Console.WriteLine($"Original object: {copyable}");

            string originalAsJson = JsonConvert.SerializeObject(copyable);

            Copyable copyable2 = JsonConvert.DeserializeObject<Copyable>(originalAsJson);
            
            copyable2.RefTypeProperty.ANumber = 456;
            copyable2.RefTypeProperty.AString = "bar";

            Console.WriteLine($"Cloned object: {copyable2}");
            Console.WriteLine($"Original object: {copyable}");
        }
    }

    class Copyable
    {
        public AReferenceType RefTypeProperty { get; set; }

        public override string ToString()
        {
            return $"ANumber = {RefTypeProperty.ANumber}, AString = {RefTypeProperty.AString}";
        }
    }

    class AReferenceType
    {
        public string AString { get; set; }
        public int ANumber { get; set; }
    }

    class Foo : ICloneable
    {
        public int ANumber { get; set; }
        public string AString { get; set; }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
