using IPL.AST.Expression;
using IPL.Logic.Exceptions.Internal;

namespace IPL.AST.Statement
{
    public class ReturnStatement : IStatement
    {
        private readonly IExpression returnExpression;

        public ReturnStatement(IExpression returnExpression)
        {
            this.returnExpression = returnExpression;
        }

        public void Execute()
        {
            throw new ReturnCall(returnExpression.Evaluate());
        }
    }
}
