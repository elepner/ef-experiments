using System.Linq.Expressions;
using System.Reflection;
using EF.Experiments.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
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
            optionsBuilder.UseSqlServer("data source=localhost,1433;initial catalog=EFExperiments;persist security info=True;user id=SA;password=yourStrong0Password");
            optionsBuilder.ReplaceService<ICompositeMethodCallTranslator, CustomSqlMethodCallTranslator>();
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


    }

    public class CustomSqlMethodCallTranslator : SqlServerCompositeMethodCallTranslator
    {
        public CustomSqlMethodCallTranslator(RelationalCompositeMethodCallTranslatorDependencies dependencies) : base(dependencies)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            AddTranslators(new [] {new FreeTextTranslator() });
        }

        public override Expression Translate(MethodCallExpression methodCallExpression, IModel model)
        {
            return base.Translate(methodCallExpression, model);
        }
    }

    public class FreeTextTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo
            = typeof(StringExt).GetRuntimeMethod(nameof(StringExt.ContainsText), new[] {typeof(string)});

        public Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.Method != _methodInfo) return null;

            var patternExpression = methodCallExpression.Arguments[0];
            var patternConstantExpression = patternExpression as ConstantExpression;
            return null;
        }
    }
    public static class StringExt
    {
        public static bool ContainsText(this string text, string sub)
        {
            return text.Contains(sub);
        }
    }
}