namespace IPL.Logic.Typization
{
    public interface ISubscriptable
    {
        IValue Get(IValue key);

        void Set(IValue key, IValue value);

        int GetSize();
    }
}
