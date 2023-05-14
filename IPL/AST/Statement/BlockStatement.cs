namespace IPL.AST.Statement
{
    public class BlockStatement : IStatement
    {
        private readonly List<IStatement> statements;

        public BlockStatement()
        {
            statements = new List<IStatement>();
        }

        public void AddStatement(IStatement statement)
        {
            statements.Add(statement);
        }

        public void Execute()
        {
            foreach (IStatement statement in statements)
                statement.Execute();
        }
    }
}
