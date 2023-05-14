using IPL.Logic.Exceptions.Internal;

namespace IPL.AST.Statement
{
    public class BreakStatement : IStatement
    {
        public void Execute()
        {
            throw new BreakCall();
        }
    }
}
