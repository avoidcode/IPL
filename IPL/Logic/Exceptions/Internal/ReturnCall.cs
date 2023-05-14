using IPL.Logic.Typization;

namespace IPL.Logic.Exceptions.Internal
{
    public class ReturnCall : Exception
    {
        public IValue ReturnValue { get; private set; }

        public ReturnCall(IValue returnValue)
        {
            ReturnValue = returnValue;
        }
    }
}
