using IPL.AST.Expression;
using IPL.Helpers;
using IPL.Logic.Exceptions;
using IPL.Logic.Typization;

namespace IPL.AST.Statement
{
    public class ElementAssignStatement : IStatement
    {
        private readonly VariableContext variableContext;
        private readonly string identifier;
        private readonly IExpression keyExpression, valueExpression;

        public ElementAssignStatement(VariableContext context, string identifier, IExpression key, IExpression value)
        {
            this.variableContext = context;
            this.identifier = identifier;
            this.keyExpression = key;
            this.valueExpression = value;
        }

        public void Execute()
        {
            IValue subscriptable = variableContext.GetVariable(identifier);
            if (subscriptable is not ISubscriptable)
                throw new IPLRuntimeException($"{subscriptable} is not subscriptable");
            (subscriptable as ISubscriptable).Set(keyExpression.Evaluate(), valueExpression.Evaluate());
        }
    }
}
