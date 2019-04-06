using System;
using System.Data.SqlClient;

namespace GettingConnected
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
            bldr.DataSource = "(localdb)\\MSSQLLocalDB";
            bldr.ApplicationName = "GettingConnected";
            bldr.IntegratedSecurity = false;
            bldr.UserID = "SomeUser";
            bldr.Password = "password";
            //bldr.Pooling = false;
            bldr.InitialCatalog = "Fruit";

            string input = Console.ReadLine();

            do
            {
                switch (input)
                {
                    case "p":
                        ClearThePool();
                        break;
                    default:
                        DoQuery(bldr.ConnectionString);
                        input = Console.ReadLine();
                        break;
                }

            } while (input != "x");
        }
        
        static void DoQuery(string cnxnString)
        {
            SqlConnection cnxn = new SqlConnection(cnxnString);
            SqlCommand cmd = cnxn.CreateCommand();

            cnxn.Open();
            cmd.CommandText = "SELECT * FROM Fruit";
            cmd.ExecuteReader();
            cmd.Dispose();
            cnxn.Dispose();
            Console.WriteLine("Ran DoQuery!");
        }

        static void ClearThePool()
        {
            //Demonstrates how ADO.Net itself maintains the pool
            SqlConnection.ClearAllPools();
        }
    }
}
