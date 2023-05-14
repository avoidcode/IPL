using IPL.AST.Expression;

namespace IPL.AST.Statement
{
    public class IfStatement : IStatement
    {
        private readonly IExpression condition;
        private readonly IStatement passStatement;
        private readonly IStatement? failStatement;

        public IfStatement(IExpression condition, IStatement passStatement, IStatement? failStatement)
        {
            this.condition = condition;
            this.passStatement = passStatement;
            this.failStatement = failStatement;
        }

        public void Execute()
        {
            if (condition.Evaluate().AsBool())
                passStatement.Execute();
            else
                failStatement?.Execute();
        }
    }
}
