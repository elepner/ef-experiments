using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.Query.Sql.Internal;

namespace EF.Experiments.Data
{
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