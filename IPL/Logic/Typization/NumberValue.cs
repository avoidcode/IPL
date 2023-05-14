using IPL.Logic.Exceptions;

namespace IPL.Logic.Typization
{
    public class NumberValue : IValue
    {
        private readonly double value;

        public NumberValue(double value)
        {
            this.value = value;
        }

        public double AsNumber()
        {
            return value;
        }

        public string AsString()
        {
            return value.ToString();
        }

        public bool AsBool()
        {
            return value > 0;
        }

        public CompareResult CompareTo(IValue value)
        {
            if (value is NumberValue)
            {
                double a = this.value;
                double b = value.AsNumber();
                if (a > b)
                    return CompareResult.Greater;
                else if (a < b)
                    return CompareResult.Less;
                else if (a == b)
                    return CompareResult.Equal;
            }
            throw new IPLRuntimeException($"Could not compare number to {value}");
        }

        public override string ToString()
        {
            return AsString();
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not NumberValue)
                return false;
            double val = (obj as NumberValue).value;
            return value.Equals(val);
        }
    }
}
