using IPL.Logic.Typization;

namespace IPL.AST.Expression
{
    public class DictionaryExpression : IExpression
    {
        private readonly List<Tuple<IExpression, IExpression>> elements;

        public DictionaryExpression(List<Tuple<IExpression, IExpression>> elements)
        {
            this.elements = elements;
        }

        public IValue Evaluate()
        {
            DictionaryValue dictionary = new DictionaryValue();
            foreach (Tuple<IExpression, IExpression> element in elements)
                dictionary.Set(element.Item1.Evaluate(), element.Item2.Evaluate());
            return dictionary;
        }
    }
}
