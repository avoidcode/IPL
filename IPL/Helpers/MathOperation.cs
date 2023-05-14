namespace IPL.Helpers
{
    public class MathOperation
    {
        public static readonly string[] Addition       = { "+", "plus" };
        public static readonly string[] Subtraction    = { "-", "minus" };
        public static readonly string[] Multiplication = { "*", "times" };
        public static readonly string[] Division       = { "/", "divided by" };
        public static readonly string[] Power          = { "**", "in power of" };

        public static Func<double, double, double> GetResolverFor(string operation)
        {
            if (Addition.Contains(operation))
                return ((a, b) => a + b);
            if (Subtraction.Contains(operation))
                return ((a, b) => a - b);
            if (Multiplication.Contains(operation))
                return ((a, b) => a * b);
            if (Division.Contains(operation))
                return ((a, b) => a / b);
            if (Power.Contains(operation))
                return ((a, b) => Math.Pow(a, b));
            throw new Exception("Invalid math operation");
        }
    }
}
