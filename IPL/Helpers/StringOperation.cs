using IPL.Logic.Exceptions;
using IPL.Logic.Typization;
using System.Text;

namespace IPL.Helpers
{
    public class StringOperation
    {
        public static readonly string[] Concatination = { "+", "plus" };
        public static readonly string[] Repeat = { "*", "times" };

        public static Func<string, IValue, string> GetResolverFor(string operation)
        {
            if (Concatination.Contains(operation))
                return ((a, b) =>
                {
                    if (b is not StringValue)
                        throw new IPLRuntimeException($"Can not concatenate string with {b.GetType().Name}");
                    return a + b.AsString();
                });
            if (Repeat.Contains(operation))
                return ((a, b) =>
                {
                    int times = (int)Math.Round(b.AsNumber());
                    return new StringBuilder(a.Length * times).Insert(0, a, times).ToString();
                });
            throw new Exception("Invalid string operation");
        }
    }
}
