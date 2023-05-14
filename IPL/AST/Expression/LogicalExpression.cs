using IPL.Helpers;
using IPL.Logic.Tokenization;
using IPL.Logic.Typization;

namespace IPL.AST.Expression
{
    public class LogicalExpression : BinaryExpression
    {
        public LogicalExpression(Token operationToken, IExpression left_expression, IExpression right_expression) :
            base(operationToken, left_expression, right_expression)
        { }

        public override IValue Evaluate()
        {
            return new BoolValue(LogicalOperation.GetResolverFor(operation)
                .Invoke(left_expression.Evaluate().AsBool(), right_expression.Evaluate().AsBool()));
        }
    }
}
