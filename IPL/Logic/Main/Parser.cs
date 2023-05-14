using IPL.Helpers;
using IPL.AST.Statement;
using IPL.AST.Expression;
using IPL.Logic.Exceptions;
using System.Globalization;
using IPL.Logic.Tokenization;

namespace IPL.Logic.Main
{
    public class Parser
    {
        private readonly List<Token> tokens;

        private readonly VariableContext variableContext;

        private int position;

        public Parser(List<Token> tokens)
        {
            variableContext = new VariableContext();
            this.tokens = tokens;
            position = 0;
        }

        public List<IStatement> Parse()
        {
            List<IStatement> result = new List<IStatement>();
            while (!Match(TokenType.EOF))
                result.Add(ParseStatement());
            return result;
        }

        private IStatement ParseStatement()
        {
            Token token = Get(0);
            if (CheckChain(TokenType.Identifier, TokenType.SquareBracketOpen))
            {
                IExpression keyExpression = ParseExpression();
                if (!CheckChain(TokenType.SquareBracketClose, TokenType.Assignment))
                    throw new ParserException("Bad array assignment");
                IExpression valueExpression = ParseExpression();
                return new ElementAssignStatement(variableContext, token.TokenText, keyExpression, valueExpression);
            }
            if (Match(TokenType.Return))
                return new ReturnStatement(ParseExpression());
            if (Match(TokenType.Continue))
                return new ContinueStatement();
            if (Match(TokenType.Break))
                return new BreakStatement();
            if (CheckChain(TokenType.Function, TokenType.Identifier, TokenType.ParenthesisOpen))
            {
                Token identifier = Get(-2);
                List<string> argNames = new List<string>();
                while (!Match(TokenType.ParenthesisClose))
                {
                    Token arg = Get(0);
                    if (Match(TokenType.Identifier))
                        argNames.Add(arg.TokenText);
                    else
                    {
                        if (!Match(TokenType.Comma))
                            throw new ParserException("Bad function definition syntax");
                    }
                }
                BlockStatement body;
                if (!TryParseBlock(out body))
                    throw new ParserException("Bad function definition");
                return new DefineFunctionStatement(variableContext, identifier.TokenText, argNames, body);
            }
            FunctionalExpression function;
            if (TryParseFunctionCall(out function))
                return new FunctionCallStatement(function);
            if (Match(TokenType.For))
            {
                IStatement init = ParseStatement();
                if (!Match(TokenType.Semicolon))
                    throw new ParserException("Bad loop syntax");
                IExpression term = ParseExpression();
                if (!Match(TokenType.Semicolon))
                    throw new ParserException("Bad loop syntax");
                IStatement inc = ParseStatement();
                IStatement body = ParseStatement();
                return new ForStatement(init, term, inc, body);
            }
            BlockStatement block;
            if (TryParseBlock(out block))
                return block;
            if (Match(TokenType.Identifier) && Match(TokenType.Assignment))
                return new AssignmentStatement(variableContext, token.TokenText, ParseExpression());
            if (Match(TokenType.If))
            {
                IExpression condition = ParseExpression();
                IStatement passStatement = ParseStatement();
                IStatement? failStatement = null;
                if (Match(TokenType.Else))
                    failStatement = ParseStatement();
                return new IfStatement(condition, passStatement, failStatement);
            }
            throw new ParserException($"Unknown statement at {token.Position}");
        }

        private IExpression ParseExpression()
        {
            return ParseLogical();
        }

        private IExpression ParseLogical()
        {
            IExpression result = ParseConditional();
            Token token = Get(0);
            while (Match(TokenType.And, TokenType.Or, TokenType.Xor))
                result = new LogicalExpression(token, result, ParseConditional());
            return result;
        }

        private IExpression ParseConditional()
        {
            IExpression result = ParseAdditive();
            Token token = Get(0);
            while (Match(TokenType.Greater, TokenType.GreaterOrEqual, TokenType.Less, TokenType.LessOrEqual, TokenType.Equal, TokenType.NotEqual))
                result = new ConditionalExpression(token, result, ParseAdditive());
            return result;
        }

        private IExpression ParseAdditive()
        {
            IExpression result = ParseMultiplicative();
            while (Match(TokenType.Plus, TokenType.Minus))
                result = new BinaryExpression(Get(-1), result, ParseMultiplicative());
            return result;
        }

        private IExpression ParseMultiplicative()
        {
            IExpression result = ParseUnary();
            while (Match(TokenType.Multiply, TokenType.Divide, TokenType.Power))
                result = new BinaryExpression(Get(-1), result, ParseUnary());
            return result;
        }

        private IExpression ParseUnary()
        {
            Token token = Get(0);
            if (Match(TokenType.Minus))
                return new UnaryExpression(token, ParsePrimary());
            return ParsePrimary();
        }

        private IExpression ParsePrimary()
        {
            Token token = Get(0);
            FunctionalExpression? function;
            if (CheckChain(TokenType.Identifier, TokenType.SquareBracketOpen))
            {
                IExpression indexExpression = ParseExpression();
                if (!Match(TokenType.SquareBracketClose))
                    throw new ParserException("Bad array/dictionary access");
                return new SubscriptableAccessExpression(variableContext, token.TokenText, indexExpression);
            }
            if (Match(TokenType.SquareBracketOpen))
            {
                List<IExpression> elements = new List<IExpression>();
                while (!Match(TokenType.SquareBracketClose))
                {
                    elements.Add(ParseExpression());
                    if (Match(TokenType.SquareBracketClose))
                        break;
                    if (!Match(TokenType.Comma))
                        throw new ParserException("Bad array definition");
                }
                return new ArrayExpression(elements);
            }
            if (Match(TokenType.OpenCurlyBrace))
            {
                List<Tuple<IExpression, IExpression>> elements = new List<Tuple<IExpression, IExpression>>();
                while (!Match(TokenType.CloseCurlyBrace))
                {
                    IExpression key = ParseExpression();
                    if (!Match(TokenType.Colon))
                        throw new ParserException("Bad dictionary definition");
                    IExpression value = ParseExpression();
                    elements.Add(new Tuple<IExpression, IExpression>(key, value));
                    if (Match(TokenType.CloseCurlyBrace))
                        break;
                    if (!Match(TokenType.Comma))
                        throw new ParserException("Bad dictionary definition");
                }
                return new DictionaryExpression(elements);
            }
            if (TryParseFunctionCall(out function))
                return function;
            if (Match(TokenType.Number))
                return new ValueExpression(double.Parse(token.TokenText, CultureInfo.InvariantCulture));
            if (Match(TokenType.String))
                return new ValueExpression(token.TokenText);
            if (Match(TokenType.Identifier))
                return new VariableExpression(variableContext, token.TokenText);
            if (Match(TokenType.ParenthesisOpen))
            {
                IExpression result = ParseExpression();
                if (!Match(TokenType.ParenthesisClose))
                    throw new ParserException("No closing parenthesis");
                return result;
            }
            throw new ParserException($"Unknown expression at {token.Position}");
        }

        private bool TryParseFunctionCall(out FunctionalExpression? function)
        {
            Token token = Get(0);
            if (CheckChain(TokenType.Identifier, TokenType.ParenthesisOpen))
            {
                List<IExpression> arguments = new();
                while (!Match(TokenType.ParenthesisClose))
                {
                    arguments.Add(ParseExpression());
                    Match(TokenType.Comma);
                }
                function = new FunctionalExpression(variableContext, token.TokenText, arguments);
                return true;
            }
            function = null;
            return false;
        }

        private bool TryParseBlock(out BlockStatement? block)
        {
            if (Match(TokenType.OpenCurlyBrace))
            {
                block = new();
                while (!Match(TokenType.CloseCurlyBrace))
                    block.AddStatement(ParseStatement());
                return true;
            }
            block = null;
            return false;
        }

        private bool Match(params TokenType[] types)
        {
            Token token = Get(0);
            if (!types.Contains(token.TokenType))
                return false;
            position++;
            return true;
        }

        private bool CheckChain(params TokenType[] types)
        {
            for (int i = 0; i < types.Length; i++)
            {
                if (Get(i).TokenType != types[i])
                    return false;
            }
            position += types.Length;
            return true;
        }

        private Token Get(int relative)
        {
            return tokens[position + relative];
        }
    }
}
