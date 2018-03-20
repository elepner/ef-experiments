using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;

namespace EF.Experiments.Data
{
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
}