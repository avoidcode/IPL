using IPL.Logic.Exceptions;
using IPL.Logic.Typization;
using IPL.Logic.Typization.Function;

namespace IPL.Helpers
{
    public class VariableContext
    {
        private class Context
        {
            public Dictionary<string, IValue> Variables { get; set; }
            public List<IFunction> Functions { get; set; }

            public Context()
            {
                Variables = new Dictionary<string, IValue>();
                Functions = new List<IFunction>();
            }

            public Context(Context reference)
            {
                Variables = new Dictionary<string, IValue>();
                Functions = new List<IFunction>();
                foreach (KeyValuePair<string, IValue> pair in reference.Variables)
                    Variables.Add(pair.Key, pair.Value);
                foreach (IFunction function in reference.Functions)
                    Functions.Add(function);
            }
        }

        private readonly Stack<Context> contexts;

        private Context CurrentContext { get => contexts.Peek(); }

        public VariableContext()
        {
            contexts = new Stack<Context>();
            contexts.Push(new Context());
            foreach (InternalFunction function in Builtin.Functions)
                PutFunction(function);
        }

        public void PopContext()
        {
            contexts.Pop();
        }

        public void PushContext()
        {
            contexts.Push(new Context(CurrentContext));
        }

        public void PutFunction(IFunction function)
        {
            CurrentContext.Functions.Add(function);
        }

        public IFunction GetFunction(string name, int argsCount)
        {
            IFunction? function = CurrentContext.Functions.Single(func => func.Name == name && func.ArgsCount == argsCount);
            if (function is null)
                throw new Exception($"Undefined function is used: {name}");
            return function;
        }

        public void PutVariable(string name, IValue value)
        {
            CurrentContext.Variables[name] = value;
        }

        public IValue GetVariable(string name)
        {
            if (!CurrentContext.Variables.ContainsKey(name))
                throw new IPLRuntimeException($"Undefined variable is used: {name}");
            return CurrentContext.Variables[name];
        }
    }
}
