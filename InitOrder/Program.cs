using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitOrder
{
    class Program
    {
        static void Main(string[] args)
        {

            //Example example = new Example();

            Mistake mistake = new Mistake();
            Console.WriteLine(mistake.AProperty);

            Console.ReadKey();
        }
    }

    class Example
    {
        private string StringField = ReflectString();

        private static string StaticStringField = ReflectString();

        private string StringProperty { get; set; } = ReflectString();

        private static string StaticStringProperty { get; set; } = ReflectString();
        
        public Example()
        {
            Console.WriteLine("Constructor");
        }

        static Example()
        {
            Console.WriteLine("Static Constructor");
        }

        private static string ReflectString([CallerMemberName]string value = "not provided")
        {
            Console.WriteLine(value);
            return "default value";
        }
    }

    class Mistake
    {
        public static MyClass MyClassProperty { get; set; }

        //Boom! MyClassProperty is not instantiated until the constructor
        //runs so the initialization of AProperty blows up.
        public string AProperty { get; set; } = MyClassProperty.ToString();
        
        public Mistake()
        {
            MyClass myClass = new MyClass(); 
        }

        public static string GetAString()
        {
            return MyClassProperty.ToString();
        }
    }

    class MyClass
    {
        public override string ToString()
        {
            return "MyClass";
        }

    }
}
