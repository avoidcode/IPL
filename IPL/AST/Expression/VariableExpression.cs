using IPL.Helpers;
using IPL.Logic.Typization;

namespace IPL.AST.Expression
{
    public class VariableExpression : IExpression
    {
        private readonly string variableName;

        private readonly VariableContext variableContext;

        public VariableExpression(VariableContext context, string variableName)
        {
            this.variableName = variableName;
            variableContext = context;
        }

        public IValue Evaluate()
        {
            return variableContext.GetVariable(variableName);
        }
    }
}
