using System.Linq.Expressions;

namespace EF.Experiments.Data.Specifications
{
    internal class ParameterReplacer : ExpressionVisitor
    {
        readonly ParameterExpression _parameter;
        readonly ParameterExpression _replacement;

        public ParameterReplacer(ParameterExpression parameter, ParameterExpression replacement)
        {
            _parameter = parameter;
            _replacement = replacement;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(_parameter == node ? _replacement : node);
        }
    }
}
