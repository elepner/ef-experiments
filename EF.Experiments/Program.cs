﻿using System;
using System.Collections.Generic;
using System.Linq;
using EF.Experiments.Data;
using EF.Experiments.Data.Data;
using EF.Experiments.Data.Specifications.Implementations;
using Microsoft.EntityFrameworkCore;

namespace EF.Experiments
{
    class Program
    {
        static BloggingContext dbContext = new BloggingContext();
        static void Main(string[] args)
        {
            dbContext.Database.Migrate();
            Seeder.Seed();

            var query = dbContext.Authors.Include(x => x.Posts).Select(x => new ViewModels.Author
            {
                Id = x.Id,
                Name = x.Name + " " + x.LastName,
                Posts = x.Posts.Select(y => y.Id).ToList()
            }).Where(authorView => authorView.Id != 5)
                .OrderBy(autoryView => autoryView.Name)
                .ToArray();

            //var query = dbContext.Posts.Where(x => x.Content.ContainsText("egg")).ToArray();
            
            
            var john = dbContext.Authors.First(x => x.LastName == "Doe");
            var post = dbContext.Posts.Where(x => x.Title == "John Second Post Title").Include(x => x.Author).First();
            Console.WriteLine(post.Author == john);

            var textContains = dbContext.Posts.Where(x => !x.Content.ContainsText("egg")).ToArray();

            var programmersBlog = dbContext.Blogs.First();
            var last15DaysJohnsPostsFromBlog = new LastPosts(15) && new ByAuthor(john) && new FromBlog(programmersBlog.Id);
            
            var posts = dbContext.Posts.Where(last15DaysJohnsPostsFromBlog).ToList();
            posts.ForEach(Console.WriteLine);

        }
    }
}

