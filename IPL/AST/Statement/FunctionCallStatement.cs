using IPL.AST.Expression;

namespace IPL.AST.Statement
{
    public class FunctionCallStatement : IStatement
    {
        public FunctionalExpression expression;

        public FunctionCallStatement(FunctionalExpression expression)
        {
            this.expression = expression;
        }

        public void Execute()
        {
            expression.Evaluate();
        }
    }
}
