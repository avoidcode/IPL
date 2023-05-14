namespace IPL.Helpers
{
    public class LogicalOperation
    {
        public static readonly string[] And = { "and" };
        public static readonly string[] Or= { "or" };
        public static readonly string[] Xor = { "xor" };

        public static Func<bool, bool, bool> GetResolverFor(string operation)
        {
            if (And.Contains(operation))
                return ((a, b) => a && b);
            if (Or.Contains(operation))
                return ((a, b) => a || b);
            if (Xor.Contains(operation))
                return ((a, b) => a ^ b);
            throw new Exception("Invalid math operation");
        }
    }
}
