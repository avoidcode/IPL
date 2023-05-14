using IPL.Helpers;
using IPL.Logic.Typization.Function;

namespace IPL.AST.Statement
{
    public class DefineFunctionStatement : IStatement
    {
        private readonly VariableContext variableContext;
        private readonly string functionName;
        private readonly List<string> argNames;
        private BlockStatement functionBody;

        public DefineFunctionStatement(VariableContext context, string funcName, List<string> argNames, BlockStatement funcBody)
        {
            this.variableContext = context;
            this.functionName = funcName;
            this.argNames = argNames;
            this.functionBody = funcBody;
        }

        public void Execute()
        {
            variableContext.PutFunction(new UserDefinedFunction(variableContext, functionName, argNames, functionBody));
        }
    }
}
