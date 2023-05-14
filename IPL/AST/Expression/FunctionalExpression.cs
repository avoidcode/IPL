using IPL.Helpers;
using IPL.Logic.Exceptions.Internal;
using IPL.Logic.Typization;
using IPL.Logic.Typization.Function;

namespace IPL.AST.Expression
{
    public class FunctionalExpression : IExpression
    {
        private readonly string functionName;
        private readonly List<IExpression> arguments;
        private readonly VariableContext variableContext;

        public FunctionalExpression(VariableContext context, string functionName, List<IExpression> args)
        {
            this.functionName = functionName;
            this.arguments = args;
            this.variableContext =  context;
        }

        public IValue Evaluate()
        {
            variableContext.PushContext();
            IFunction function = variableContext.GetFunction(functionName, arguments.Count);
            try
            {
                IValue result = function.Invoke(arguments.Select((arg) => arg.Evaluate()).ToArray());
                variableContext.PopContext();
                return result;
            }
            catch (ReturnCall returnCall)
            {
                variableContext.PopContext();
                return returnCall.ReturnValue;
            }
        }
    }
}
