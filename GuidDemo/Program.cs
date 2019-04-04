using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace GuidDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
            bldr.DataSource = "(localdb)\\MSSQLLocalDB";
            bldr.InitialCatalog = "Fruit";
            bldr.IntegratedSecurity = true;

            Stopwatch sw = new Stopwatch();

            SqlConnection cnxn = new SqlConnection(bldr.ConnectionString);
            cnxn.Open();

            SqlCommand InsCmd = cnxn.CreateCommand();
            InsCmd.CommandText = "INSERT INTO [DotNetGuidTable](Id, Description) VALUES(@Id, N'Blah, blah, blah');";
            SqlParameter p_InsCmddotNetId = new SqlParameter("@Id", System.Data.SqlDbType.UniqueIdentifier);
            InsCmd.Parameters.Add(p_InsCmddotNetId);

            SqlCommand UpdCmd = cnxn.CreateCommand();
            UpdCmd.CommandText = "UPDATE [DotNetGuidTable] SET [Description] = 'Changed...' WHERE Id = @Id;";
            SqlParameter p_UpdCmddotNet = new SqlParameter("@Id", System.Data.SqlDbType.UniqueIdentifier);
            UpdCmd.Parameters.Add(p_UpdCmddotNet);

            SqlCommand DelCmd = cnxn.CreateCommand();
            DelCmd.CommandText = "DELETE FROM [DotNetGuidTable] WHERE [Id] = @Id;";
            SqlParameter p_DelCmddotNet = new SqlParameter("@Id", System.Data.SqlDbType.UniqueIdentifier);
            DelCmd.Parameters.Add(p_DelCmddotNet);

            for (int j = 0; j < 5; j++)
            {
                sw.Start();
                for (int i = 0; i < 10000; i++)
                {
                    Guid g = Guid.NewGuid();

                    p_InsCmddotNetId.Value = g;
                    InsCmd.ExecuteNonQuery();

                    p_UpdCmddotNet.Value = g;
                    UpdCmd.ExecuteNonQuery();

                    p_DelCmddotNet.Value = g;
                    DelCmd.ExecuteNonQuery();
                }
                sw.Stop();

                Console.WriteLine($"The DotNet Guid operations took {sw.ElapsedMilliseconds / 1000} seconds.");

                InsCmd.Parameters.Clear();
                InsCmd.CommandText = "INSERT INTO [GuidKeyTable](Id, Description) OUTPUT inserted.Id VALUES(NEWID(), N'Blah, blah, blah');";
                UpdCmd.CommandText = "UPDATE [GuidKeyTable] SET [Description] = 'Changed...' WHERE Id = @Id;";
                DelCmd.CommandText = "DELETE FROM [GuidKeyTable] WHERE [Id] = @Id;";

                sw.Reset();
                sw.Start();
                for (int i = 0; i < 10000; i++)
                {
                    Guid sqlGuid = (Guid)InsCmd.ExecuteScalar();

                    p_UpdCmddotNet.Value = sqlGuid;
                    UpdCmd.ExecuteNonQuery();

                    p_DelCmddotNet.Value = sqlGuid;
                    DelCmd.ExecuteNonQuery();
                }
                sw.Stop();

                Console.WriteLine($"The SQL Guid operations took {sw.ElapsedMilliseconds / 1000} seconds.");
            }

            Console.ReadKey();
        }
    }
}
