using IPL.AST.Statement;
using IPL.Helpers;

namespace IPL.Logic.Typization.Function
{
    public class UserDefinedFunction : IFunction
    {
        private readonly BlockStatement functionBody;
        private readonly List<string> argNames;
        private readonly VariableContext variableContext;

        public string Name { get; private set; }

        public int ArgsCount { get; private set; }


        public UserDefinedFunction(VariableContext context, string name, List<string> argNames, BlockStatement body)
        {
            variableContext = context;
            this.argNames = argNames;
            Name = name;
            ArgsCount = argNames.Count;
            functionBody = body;
        }

        public IValue Invoke(params IValue[] args)
        {
            for (int i = 0; i < argNames.Count; i++)
                variableContext.PutVariable(argNames[i], args[i]);
            functionBody.Execute();
            return new NumberValue(0);
        }
    }
}
