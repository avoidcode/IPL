using IPL.Logic.Exceptions;

namespace IPL.Logic.Typization
{
    public class ArrayValue : IValue, ISubscriptable
    {
        private readonly List<IValue> elements;

        public ArrayValue(List<IValue> elements)
        {
            this.elements = elements;
        }

        public ArrayValue(int size)
        {
            this.elements = new List<IValue>(size);
        }

        public IValue Get(int index)
        {
            return elements[index];
        }

        public void Set(int index, IValue value)
        {
            elements.Insert(index, value);
        }

        public IValue Get(IValue key)
        {
            int index = (int)key.AsNumber();
            return Get(index);
        }

        public void Set(IValue key, IValue value)
        {
            int index = (int)key.AsNumber();
            Set(index, value);
        }

        public int GetSize()
        {
            return elements.Count;
        }

        public bool AsBool()
        {
            throw new IPLRuntimeException("Could not cast array to a bool value");
        }

        public double AsNumber()
        {
            throw new IPLRuntimeException("Could not cast array to a number");
        }

        public string AsString()
        {
            return "[" + string.Join(", ", elements) + "]";
        }

        public CompareResult CompareTo(IValue value)
        {
            throw new IPLRuntimeException("Arrays could not be compared directly");
        }

        public override string ToString()
        {
            return AsString();
        }
    }
}
