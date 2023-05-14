namespace IPL.Logic.Typization.Function
{
    public delegate IValue IPLFunction(params IValue[] args);

    public interface IFunction
    {
        string Name { get; }

        int ArgsCount { get; }

        IValue Invoke(params IValue[] args);
    }
}
