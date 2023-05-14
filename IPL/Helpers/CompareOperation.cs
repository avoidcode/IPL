using IPL.Logic.Exceptions;
using IPL.Logic.Typization;

namespace IPL.Helpers
{
    public class CompareOperation
    {
        public static readonly string[] GreaterThan        = { ">",  "greater than" };
        public static readonly string[] GreaterOrEqualThan = { ">=", "greatereq than" };
        public static readonly string[] LessThan           = { "<",  "less than" };
        public static readonly string[] LessOrEqualThan    = { "<=", "lesseq than" };
        public static readonly string[] Equal              = { "==", "equal" };
        public static readonly string[] NotEqual           = { "!=", "not equal" };

        private static Func<IValue, IValue, bool> AwaitResultResolver(params CompareResult[] awaitedResults)
        {
            return ((a, b) =>
            {
                CompareResult result = a.CompareTo(b);
                if (awaitedResults.Contains(result))
                    return true;
                return false;
            });
        }

        public static Func<IValue, IValue, bool> GetResolverFor(string operation)
        {
            if (GreaterThan.Contains(operation))
                return AwaitResultResolver(CompareResult.Greater);
            if (GreaterOrEqualThan.Contains(operation))
                return AwaitResultResolver(CompareResult.Greater, CompareResult.Equal);
            if (LessThan.Contains(operation))
                return AwaitResultResolver(CompareResult.Less);
            if (LessOrEqualThan.Contains(operation))
                return AwaitResultResolver(CompareResult.Less, CompareResult.Equal);
            if (Equal.Contains(operation))
                return AwaitResultResolver(CompareResult.Equal);
            if (NotEqual.Contains(operation))
                return AwaitResultResolver(CompareResult.Greater, CompareResult.Less);
            throw new IPLRuntimeException("Invalid comparison operation");
        }
    }
}
