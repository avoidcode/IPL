using IPL.Helpers;

namespace IPL.Logic.Tokenization
{
    /* language = regex */
    #region RegexZone

    public enum TokenType
    {
        [AssignedRegex(@"//")] Comment,
        [AssignedRegex(@"\*\*|in power of")] Power,
        [AssignedRegex(@"\+|plus")] Plus,
        [AssignedRegex(@"\-|minus")] Minus,
        [AssignedRegex(@"\*|times")] Multiply,
        [AssignedRegex(@"\/|divided by")] Divide,
        [AssignedRegex(@">=|greatereq than")] GreaterOrEqual,
        [AssignedRegex(@">|greater than")] Greater,
        [AssignedRegex(@"<=|lesseq than")] LessOrEqual,
        [AssignedRegex(@"<|less than")] Less,
        [AssignedRegex(@"==|equal")] Equal,
        [AssignedRegex(@"!=|not equal")] NotEqual,
        [AssignedRegex(@"\-?([0-9]*\.[0-9]+|[0-9]+)")] Number,
        [AssignedRegex(@"\(")] ParenthesisOpen,
        [AssignedRegex(@"\)")] ParenthesisClose,
        [AssignedRegex(@"\[")] SquareBracketOpen,
        [AssignedRegex(@"\]")] SquareBracketClose,
        [AssignedRegex(@"\{")] OpenCurlyBrace,
        [AssignedRegex(@"\}")] CloseCurlyBrace,
        [AssignedRegex(@"&&|and")] And,
        [AssignedRegex(@"\|\||or")] Or,
        [AssignedRegex(@"\^|xor")] Xor,
        [AssignedRegex("'[^']*'|\"[^\"]*\"")] String,
        [AssignedRegex(@"fun ")] Function,
        [AssignedRegex(@"ret ")] Return,
        [AssignedRegex(@"break")] Break,
        [AssignedRegex(@"continue")] Continue,
        [AssignedRegex(@"for ")] For,
        [AssignedRegex(@";")] Semicolon,
        [AssignedRegex(@":")] Colon,
        [AssignedRegex(@",")] Comma,
        [AssignedRegex(@"if")] If,
        [AssignedRegex(@"else")] Else,
        [AssignedRegex(@"[a-zA-Z_$][a-zA-Z0-9_$]*")] Identifier,
        [AssignedRegex(@"=")] Assignment,
        [AssignedRegex(@"^\s*$")] EOF
    }

    #endregion
}
