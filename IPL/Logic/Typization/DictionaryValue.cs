using IPL.Logic.Exceptions;

namespace IPL.Logic.Typization
{
    public class DictionaryValue : IValue, ISubscriptable
    {
        private readonly Dictionary<IValue, IValue> elements;

        public Dictionary<IValue, IValue> BaseDictionary { get => elements; }

        public DictionaryValue()
        {
            elements = new Dictionary<IValue, IValue>();
        }
        public IValue Get(IValue key)
        {
            return elements[key];
        }

        public void Set(IValue key, IValue value)
        {
            elements.Add(key, value);
        }

        public int GetSize()
        {
            return elements.Count;
        }

        public bool AsBool()
        {
            throw new IPLRuntimeException("Could not cast dictionary to a bool value");
        }

        public double AsNumber()
        {
            throw new IPLRuntimeException("Could not cast dictionary to a number value");
        }

        public string AsString()
        {
            List<string> elementsList = new List<string>();
            foreach(KeyValuePair<IValue, IValue> element in elements)
            {
                string value = element.Value.AsString();
                if (element.Value is StringValue)
                    value = "\"" + value + "\"";
                elementsList.Add($"\"{element.Key}\": {value}");
            }
            return "{" + string.Join(", ", elementsList) + "}";
        }

        public CompareResult CompareTo(IValue value)
        {
            throw new IPLRuntimeException("Dictionaries could not be compared directly");
        }

        public override string ToString()
        {
            return AsString();
        }
    }
}
