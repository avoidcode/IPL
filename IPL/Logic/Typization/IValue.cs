namespace IPL.Logic.Typization
{
    public interface IValue
    {
        double AsNumber();

        string AsString();

        bool AsBool();

        CompareResult CompareTo(IValue value);
    }
}
