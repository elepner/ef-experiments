using System.Linq;
using EF.Experiments.Data;
using Microsoft.EntityFrameworkCore;

namespace EF.Experiments
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new BloggingContext();
            
            var query = dbContext.Authors.Include(x => x.Posts).Select(x => new ViewModels.Author
            {
                Id = x.Id,
                Name = x.Name + " " + x.LastName,
                Posts = x.Posts.Select(y => y.Id).ToList()
            });

            query = query.Where(x => x.Name.StartsWith("John"));
            var result = query.ToArray();
        }
    }
}

