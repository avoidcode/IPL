using IPL.Logic.Exceptions;
using IPL.Logic.Tokenization;

namespace IPL.Logic.Main
{
    public class Lexer
    {
        private List<Token> tokens;

        public string Code { get; private set; }

        private int currentLine, currentColumn;

        public Lexer(string code)
        {
            Code = code;
            tokens = new List<Token>();
            currentLine = 0;
            currentColumn = 0;
        }

        private Token? processNextToken()
        {
            if (string.IsNullOrEmpty(Code))
                return null;
            foreach (TokenType type in Enum.GetValues(typeof(TokenType)))
            {
                string? tokenText = Code.GetPrefix(Token.GetRegexFor(type));
                if (!string.IsNullOrEmpty(tokenText))
                {
                    if (type == TokenType.Comment)
                    {
                        Code = Code.Substring(Code.IndexOf('\n'));
                        TrimStart();
                        continue;
                    }
                    int length = tokenText.Length;
                    if (type == TokenType.String)
                        tokenText = tokenText[1..^1];
                    Token token = new Token(tokenText, type, currentLine, currentColumn);
                    currentColumn += length;
                    Code = Code.Substring(length);
                    TrimStart();
                    return token;
                }
            }
            throw new LexerException($"{currentLine}:{currentColumn} -> Unknown token");
        }

        public List<Token> Tokenize()
        {
            tokens.Clear();
            TrimStart();
            Token? currentToken;
            while ((currentToken = processNextToken()) != null)
                tokens.Add(currentToken);
            tokens.Add(Token.EOF);
            return tokens;
        }

        private void TrimStart()
        {
            while (true)
            {
                if (Code.Length < 1)
                    break;
                if (Code.Length >= 2 && Code[0..2] == "\r\n")
                {
                    currentLine++;
                    currentColumn = 0;
                    Code = Code.Substring(2);
                    continue;
                }
                else if ("\r\n\f\v".Contains(Code[0]))
                {
                    currentLine++;
                    currentColumn = 0;
                    Code = Code.Substring(1);
                    continue;
                }
                else if (" \t".Contains(Code[0]))
                {
                    currentColumn++;
                    Code = Code.Substring(1);
                    continue;
                }
                else
                    break;
            }
        }
    }
}
