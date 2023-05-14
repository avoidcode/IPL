using IPL.AST.Expression;
using IPL.Helpers;

namespace IPL.AST.Statement
{
    public class AssignmentStatement : IStatement
    {
        private readonly VariableContext variableContext;
        private readonly string variableName;
        private readonly IExpression expression;

        public AssignmentStatement(VariableContext context, string variableName, IExpression expression)
        {
            variableContext = context;
            this.variableName = variableName;
            this.expression = expression;
        }

        public void Execute()
        {
            variableContext.PutVariable(variableName, expression.Evaluate());
        }
    }
}
