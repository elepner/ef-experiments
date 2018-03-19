using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.Query.Sql.Internal;

namespace EF.Experiments.Data
{
    public class FreeTextTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo
            = typeof(StringExt).GetRuntimeMethod(nameof(StringExt.ContainsText), new[] {typeof(string), typeof(string)});

        public Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.Method != _methodInfo) return null;

            var patternExpression = methodCallExpression.Arguments[1];
            var objectExpression = (ColumnExpression) methodCallExpression.Arguments[0];
            
            var sqlExpression =
                new SqlFunctionExpression("CONTAINS", typeof(bool),
                    new[] { objectExpression, patternExpression });
            return sqlExpression;
        }
    }

    public class FreeTextSqlGenerator : DefaultQuerySqlGenerator
    {
        internal FreeTextSqlGenerator(QuerySqlGeneratorDependencies dependencies, SelectExpression selectExpression) : base(dependencies, selectExpression)
        {
        }

        protected override Expression VisitBinary(BinaryExpression binaryExpression)
        {
            if (binaryExpression.Left is SqlFunctionExpression sqlFunctionExpression
                && sqlFunctionExpression.FunctionName == "CONTAINS")
            {
                Visit(binaryExpression.Left);

                return binaryExpression;
            }

            return base.VisitBinary(binaryExpression);
        }
    }

    public class CustomSqlServerGeneratorFacotry : SqlServerQuerySqlGeneratorFactory
    {
        public CustomSqlServerGeneratorFacotry(QuerySqlGeneratorDependencies dependencies, ISqlServerOptions sqlServerOptions) : base(dependencies, sqlServerOptions)
        {
        }

        public override IQuerySqlGenerator CreateDefault(SelectExpression selectExpression) => new FreeTextSqlGenerator(
            Dependencies,
            selectExpression);
    }
}