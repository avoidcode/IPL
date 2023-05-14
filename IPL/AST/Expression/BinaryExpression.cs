using IPL.Helpers;
using IPL.Logic.Exceptions;
using IPL.Logic.Tokenization;
using IPL.Logic.Typization;

namespace IPL.AST.Expression
{
    public class BinaryExpression : IExpression
    {
        protected readonly string operation;
        protected readonly IExpression left_expression;
        protected readonly IExpression right_expression;

        public BinaryExpression(Token operationToken, IExpression left_expression, IExpression right_expression)
        {
            operation = operationToken.TokenText;
            this.left_expression = left_expression;
            this.right_expression = right_expression;
        }

        public virtual IValue Evaluate()
        {
            IValue left = left_expression.Evaluate();
            IValue right = right_expression.Evaluate();
            if (left is NumberValue && right is NumberValue)
                return new NumberValue(MathOperation.GetResolverFor(operation).Invoke(left.AsNumber(), right.AsNumber()));
            if (left is StringValue)
                return new StringValue(StringOperation.GetResolverFor(operation).Invoke(left.AsString(), right));
            if (left is BoolValue)
                return new BoolValue(LogicalOperation.GetResolverFor(operation).Invoke(left.AsBool(), right.AsBool()));
            throw new IPLRuntimeException("Bad binary operation");
        }
    }
}
