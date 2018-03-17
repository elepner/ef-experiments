using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.Sql;

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

            var patternConstant = patternExpression as ConstantExpression;
            if(patternConstant == null) throw new NotSupportedException("Dynamic variable execution is not supported so far");
            var sqlExpression =
                new SqlFunctionExpression("CONTAINS", typeof(bool),
                    new[] { objectExpression, patternExpression });
            
            //var sqlExpression = new SqlFragmentExpression($"CONTAINS({objectExpression.Name}, N'{patternConstant.Value}')");

            return sqlExpression;
        }
    }
}