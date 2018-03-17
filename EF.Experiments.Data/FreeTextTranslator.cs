using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;
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
            var objectExpression = methodCallExpression.Arguments[0];
            
            var containsExpression =
                new SqlFunctionExpression("CONTAINS", typeof(bool),
                    new[] {objectExpression, patternExpression});
            
            return containsExpression;
        }
    }
}