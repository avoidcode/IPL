using IPL.Logic.Exceptions.Internal;

namespace IPL.AST.Statement
{
    public class ContinueStatement : IStatement
    {
        public void Execute()
        {
            throw new ContinueCall();
        }
    }
}
