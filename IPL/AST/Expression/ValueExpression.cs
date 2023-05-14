using IPL.Logic.Typization;

namespace IPL.AST.Expression
{
    public class ValueExpression : IExpression
    {
        private readonly IValue value;

        public ValueExpression(string value)
        {
            this.value = new StringValue(value);
        }

        public ValueExpression(double value)
        {
            this.value = new NumberValue(value);
        }

        public ValueExpression(bool value)
        {
            this.value = new BoolValue(value);
        }

        public IValue Evaluate()
        {
            return value;
        }
    }
}
