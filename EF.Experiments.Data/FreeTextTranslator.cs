using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

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
}