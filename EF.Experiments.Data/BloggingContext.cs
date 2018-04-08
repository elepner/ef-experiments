using System;
using EF.Experiments.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace EF.Experiments.Data
{
    public class BloggingContext : DbContext
    {
        private static readonly LoggerFactory ConsoleLoggerFactory =
            new LoggerFactory(new[] {new ConsoleLoggerProvider((_, __) => true, true)});

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=localhost,14333;initial catalog=EFExperiments;persist security info=True;user id=SA;password=yourStrong0Password")
                .ConfigureWarnings(wartings => wartings.Throw());
            optionsBuilder.ReplaceService<ICompositeMethodCallTranslator, CustomSqlMethodCallTranslator>();
            optionsBuilder.ReplaceService<IQuerySqlGeneratorFactory, CustomSqlServerGeneratorFacotry>();
            optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory);
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>()
                .HasKey(t => new { t.PostId, t.TagId });

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);
        }

        public override int SaveChanges()
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                if(!(entityEntry.Entity is Post post)) continue;

                if (entityEntry.State == EntityState.Added)
                {
                    post.Created = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}