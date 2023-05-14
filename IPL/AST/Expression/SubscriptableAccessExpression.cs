using IPL.Helpers;
using IPL.Logic.Exceptions;
using IPL.Logic.Typization;

namespace IPL.AST.Expression
{
    public class SubscriptableAccessExpression : IExpression
    {
        private readonly VariableContext context;
        private readonly string identifier;
        private readonly IExpression keyExpression;

        public SubscriptableAccessExpression(VariableContext context, string identifier, IExpression keyExpression)
        {
            this.context = context;
            this.identifier = identifier;
            this.keyExpression = keyExpression;
        }

        public IValue Evaluate()
        {
            IValue subscriptable = context.GetVariable(identifier);
            if (subscriptable is not ISubscriptable)
                throw new IPLRuntimeException($"{identifier} is not subscriptable");
            return (subscriptable as ISubscriptable).Get(keyExpression.Evaluate());
        }
    }
}
