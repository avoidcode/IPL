using IPL.Helpers;
using System.Reflection;

namespace IPL.Logic.Tokenization
{
    public class Token
    {
        public string TokenText { get; private set; }

        public TokenType TokenType { get; private set; }

        public int Line { get; private set; }

        public int Column { get; private set; }

        public string Position
        {
            get => $"{Line}:{Column}";
        }

        public Token(string tokenText, TokenType tokenType, int line, int column)
        {
            TokenText = tokenText;
            TokenType = tokenType;
            Line = line;
            Column = column;
        }

        public static string GetRegexFor(TokenType tokenType)
        {
            AssignedRegexAttribute? attribute = tokenType.GetType().GetMember(tokenType.ToString())[0]
                .GetCustomAttribute(typeof(AssignedRegexAttribute), false) as AssignedRegexAttribute;
            if (attribute is null)
                throw new Exception("Attribute fetching failed :(");
            return attribute.Regex;
        }

        public static readonly Token EOF = new Token("<EOF>", TokenType.EOF, -1, -1);
    }
}
