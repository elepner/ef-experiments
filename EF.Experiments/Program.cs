using EF.Experiments.Data;

namespace EF.Experiments
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new BloggingContext();
            Seeder.Seed();
        }
    }
}

