using IPL.Logic.Exceptions;

namespace IPL.Logic.Typization
{
    public class StringValue : IValue
    {
        private readonly string value;

        public StringValue(string value)
        {
            this.value = value;
        }

        public double AsNumber()
        {
            try
            {
                return double.Parse(value);
            }
            catch
            {
                throw new IPLRuntimeException($"String {value} cannot be converted to number");
            }
        }

        public string AsString()
        {
            return value;
        }

        public bool AsBool()
        {
            throw new IPLRuntimeException("Could not convert string to bool");
        }

        public CompareResult CompareTo(IValue value)
        {
            if (value is StringValue)
            {
                switch (this.value.CompareTo(value.AsString()))
                {
                    case < 0:
                        return CompareResult.Greater;
                    case > 0:
                        return CompareResult.Less;
                    case 0:
                        return CompareResult.Equal;
                }
            }
            throw new IPLRuntimeException($"Could not compare string to {value}");
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
            if (obj == null || obj is not StringValue)
                return false;
            string val = (obj as StringValue).value;
            return value.Equals(val);
        }
    }
}
