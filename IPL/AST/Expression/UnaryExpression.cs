using IPL.Logic.Tokenization;
using IPL.Logic.Typization;

namespace IPL.AST.Expression
{
    public class UnaryExpression : IExpression
    {
        private readonly string operation;
        private readonly IExpression expression;

        public UnaryExpression(Token operationToken, IExpression expression)
        {
            operation = operationToken.TokenText;
            this.expression = expression;
        }

        public IValue Evaluate()
        {
            if (operation == "-")
                return new NumberValue(-expression.Evaluate().AsNumber());
            return expression.Evaluate();
        }

        public override string ToString()
        {
            return $"UnaryExpression: {operation}";
        }
    }
}
