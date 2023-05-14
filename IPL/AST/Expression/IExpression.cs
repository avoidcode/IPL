using IPL.Logic.Typization;

namespace IPL.AST.Expression
{
    public interface IExpression
    {
        IValue Evaluate();
    }
}
