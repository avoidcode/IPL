using IPL.AST.Statement;
using IPL.Logic.Exceptions;

namespace IPL.Logic.Main
{
    public class Interpreter
    {
        private readonly List<IStatement> statements;

        public Interpreter(List<IStatement> statements)
        {
            this.statements = statements;
        }

        public int Interpret()
        {
            try
            {
                foreach (var statement in statements)
                    statement.Execute();
            }
            catch (Exception e)
            {
                throw new InterpreterException(e.ToString());
            }
            return 0;
        }
    }
}
