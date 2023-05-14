using IPL.Logic.Exceptions;

namespace IPL.Logic.Typization
{
    public class BoolValue : IValue
    {
        private readonly bool value;

        public BoolValue(bool value)
        {
            this.value = value;
        }

        public bool AsBool()
        {
            return value;
        }

        public double AsNumber()
        {
            return value ? 1 : 0;
        }

        public string AsString()
        {
            return value ? "True" : "False";
        }

        public CompareResult CompareTo(IValue value)
        {
            if (value is NumberValue)
            {
                double a = AsNumber();
                double b = value.AsNumber();
                if (a > b)
                    return CompareResult.Greater;
                else if (a < b)
                    return CompareResult.Less;
                else if (a == b)
                    return CompareResult.Equal;
            }
            throw new IPLRuntimeException($"Could not compare bool to {value}");
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not BoolValue)
                return false;
            bool val = (obj as BoolValue).value;
            return value.Equals(val);
        }

        public override string ToString()
        {
            return AsString();
        }
    }
}
