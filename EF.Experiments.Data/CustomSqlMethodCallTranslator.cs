using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;

namespace EF.Experiments.Data
{
    public class CustomSqlMethodCallTranslator : SqlServerCompositeMethodCallTranslator
    {
        public CustomSqlMethodCallTranslator(RelationalCompositeMethodCallTranslatorDependencies dependencies) : base(dependencies)
        {
            
            // ReSharper disable once VirtualMemberCallInConstructor
            AddTranslators(new [] {new FreeTextTranslator() });
        }
        
    }
}