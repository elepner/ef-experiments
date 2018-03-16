using System;
using System.Collections.Generic;
using System.Linq;
using EF.Experiments.Data;
using EF.Experiments.Data.Data;

namespace EF.Experiments
{
    static class Seeder
    {
        public static void Seed()
        {
            var dbContext = new BloggingContext();
            if(dbContext.Authors.Any()) return;

            var tags = new[]
            {
                new Tag
                {
                    Id = "csharp",
                    Description = "C#"
                },
                new Tag
                {
                    Id = "javascript",
                    Description = "JavaScript"
                },
                new Tag
                {
                    Id = "cooking",
                    Description = "Cooking"
                },
                new Tag
                {
                    Id = "egg",
                    Description = "Egg"
                },
                new Tag
                {
                    Id = "forbeginners",
                    Description = "For Beginners"
                },
                new Tag
                {
                    Id = "db",
                    Description = "Databases"
                },
                new Tag
                {
                    Id = "ef",
                    Description = "Entity Framework"
                },
                new Tag
                {
                    Id = "advanced",
                    Description = "Advanced"
                },
                new Tag
                {
                    Id = "oriental",
                    Description = "Oriental Cooking"
                }
            };

            var blogs = new[]
            {
                new Blog
                {
                    Created = DateTime.Now.AddDays(-100),
                    Title = "Programmer's Spot"
                },
                new Blog
                {
                    Created = DateTime.Now.AddDays(-1000),
                    Title = "Cooking at home"
                }
            };

            var authors = new[]
            {
                new Author
                {
                    Name = "Eduard",
                    LastName = "Lepner",
                    Posts = new List<Post>
                    {
                        new Post
                        {
                            Title = "How to start with EF and don't shoot your head",
                            Content = "Some interesting post content",
                            Blog = blogs[0],
                            Rating = 5,
                            Created = DateTime.Now.AddDays(-1)
                        },
                        new Post
                        {
                            Title = "Functional Programming For Body Builders",
                            Content = "Some content",
                            Rating = 4,
                            Blog = blogs[0],
                            Created = DateTime.Today
                        },
                        new Post
                        {
                            Title = "How to cook an Egg",
                            Content = "Put an egg into tea pot and turn it on.",
                            Blog = blogs[1],
                            Created = DateTime.Now
                        }
                    }
                },
                new Author
                {
                    Name = "John",
                    LastName = "Doe",
                    Posts = new List<Post>
                    {
                        new Post
                        {
                            Title = "John First Post Title",
                            Content = "Some content for John's first post.",
                            Blog = blogs[0]
                        },
                        new Post
                        {
                            Title = "John Second Post Title",
                            Content = "Some content for John's second post.",
                            Blog = blogs[1]
                        }
                    }
                },
                new Author
                {
                    Name = "Jan",
                    LastName = "Kovalski",
                    Posts = new List<Post>
                    {
                        new Post
                        {
                            Title = "Jan's first post",
                            Content = "Some content for Jan's first post.",
                            Blog = blogs[1],
                            Rating = 3
                        }
                    }
                }
            };

            var posts = authors.SelectMany(x => x.Posts).ToArray();
            posts[0].AssignTags("csharp", "db", "ef");
            posts[1].AssignTags("csharp", "forbeginners");
            posts[2].AssignTags("forbeginners");
            posts[3].AssignTags("javascript", "db", "advanced", "csharp");
            posts[4].AssignTags("oriental", "db", "advanced", "csharp");

            dbContext.Blogs.AddRange(blogs);
            dbContext.Authors.AddRange(authors);
            dbContext.Tags.AddRange(tags);
            dbContext.SaveChanges();
        }

        static void AssignTags(this Post post, params string[] tags)
        {
            if(post.PostTags == null)
                post.PostTags = new List<PostTag>();

            post.PostTags.AddRange(tags.Select(tag => new PostTag {Post = post, TagId = tag}));
        }
    }
}