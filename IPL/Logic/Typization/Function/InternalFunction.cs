using IPL.Logic.Exceptions;

namespace IPL.Logic.Typization.Function
{
    public class InternalFunction : IFunction
    {
        private readonly IPLFunction csFunction;

        public string Name { get; private set; }

        public int ArgsCount { get; private set; }

        public InternalFunction(string name, int argsCount, IPLFunction csFunction)
        {
            this.Name = name;
            this.ArgsCount = argsCount;
            this.csFunction = csFunction;
        }

        public IValue Invoke(params IValue[] args)
        {
            if (args.Length != ArgsCount)
                throw new IPLRuntimeException("Args count mismatch");
            return csFunction.Invoke(args);
        }
    }
}
