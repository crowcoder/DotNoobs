using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Linqing
{
    public class FruitContext : DbContext
    {
        public FruitContext()
            : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fruit;Integrated Security=True;Application Name=Linqing;")
        {

        }
        public DbSet<Fruit> Fruits { get; set; }
    }

    public class Fruit
    {
        [Key]
        public int FruitID { get; set; }
        public string FruitName { get; set; }
        public bool? FruitIsYummy { get; set; }

        public override string ToString()
        {
            return $"FruitID: {FruitID}, FruitName: {FruitName}, FruitIsYummy: {FruitIsYummy}";
        }
    }
}
