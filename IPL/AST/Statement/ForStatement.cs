using IPL.AST.Expression;
using IPL.Logic.Exceptions.Internal;

namespace IPL.AST.Statement
{
    public class ForStatement : IStatement
    {
        private readonly IStatement initStatement;
        private readonly IExpression termExpression;
        private readonly IStatement incStatement;
        private readonly IStatement loopBody;

        public ForStatement(IStatement initStatement, IExpression termExpression, IStatement incStatement, IStatement loopBody)
        {
            this.initStatement = initStatement;
            this.termExpression = termExpression;
            this.incStatement = incStatement;
            this.loopBody = loopBody;
        }

        public void Execute()
        {
            for (initStatement.Execute(); termExpression.Evaluate().AsBool(); incStatement.Execute())
            {
                try { loopBody.Execute(); }
                catch (ContinueCall) { continue; }
                catch (BreakCall) { break; }
            }
        }
    }
}
