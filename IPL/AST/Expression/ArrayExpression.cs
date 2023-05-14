using IPL.Logic.Typization;

namespace IPL.AST.Expression
{
    public class ArrayExpression : IExpression
    {
        private readonly List<IExpression> elements;

        public ArrayExpression(List<IExpression> elements)
        {
            this.elements = elements;
        }

        public IValue Evaluate()
        {
            ArrayValue array = new ArrayValue(elements.Count);
            for (int i = 0; i < elements.Count; i++)
                array.Set(i, elements[i].Evaluate());
            return array;
        }
    }
}
