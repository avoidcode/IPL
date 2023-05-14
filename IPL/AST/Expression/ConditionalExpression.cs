using IPL.Helpers;
using IPL.Logic.Tokenization;
using IPL.Logic.Typization;

namespace IPL.AST.Expression
{
    public class ConditionalExpression : BinaryExpression
    {
        public ConditionalExpression(Token operationToken, IExpression left_expression, IExpression right_expression) :
            base(operationToken, left_expression, right_expression)
        { }

        public override IValue Evaluate()
        {
            return new BoolValue(CompareOperation.GetResolverFor(operation)
                .Invoke(left_expression.Evaluate(), right_expression.Evaluate()));
        }
    }
}
