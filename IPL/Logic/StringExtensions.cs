using System.Text.RegularExpressions;

namespace IPL.Logic
{
    public static class StringExtensions
    {
        public static string? GetPrefix(this string code, string regex)
        {
            Regex compiledRegex = new Regex(regex);
            Match match = compiledRegex.Match(code);
            if (match.Success && match.Index == 0)
                return code.Substring(0, match.Length);
            return null;
        }
    }
}
