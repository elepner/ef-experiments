using System;
using System.Collections.Generic;
using System.Linq;
using EF.Experiments.Data;
using EF.Experiments.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace EF.Experiments
{
    class Program
    {
        static BloggingContext dbContext = new BloggingContext();
        static void Main(string[] args)
        {
            //Seeder.Seed();
            
            //var query = dbContext.Authors.Include(x => x.Posts).Select(x => new ViewModels.Author
            //{
            //    Id = x.Id,
            //    Name = x.Name + " " + x.LastName,
            //    Posts = x.Posts.Select(y => y.Id).ToList()
            //});

            var query = dbContext.Posts.Where(x => x.Content.ContainsText("egg")).ToArray();
            var text = "Some";
            var query2 = dbContext.Posts.Where(x => x.Content.ContainsText(text)).ToArray();
            //query = query.Where(x => x.Name.StartsWith("John"));
            //var result = query.ToArray();
            //var result = GetPostsWithRating(5);
            //var result2 = GetPostsWithRating(null);
            //var test = dbContext.Tags.Where(x => x.Description == null).ToArray();

            //var john = dbContext.Authors.First(x => x.LastName == "Doe");
            //var post = dbContext.Posts.First();
            //var post2 = dbContext.Posts.Where(x => x.Title == "John Second Post Title").Include(x => x.Author).First();
            //Console.WriteLine(post2.Author == john);
        }

        static ICollection<Post> GetPostsWithRating(int? rating)
        {
            return dbContext.Posts.Where(x => x.Rating == rating).ToArray();
        }
    }
}

