using IPL.Logic.Exceptions;
using IPL.Logic.Typization;
using IPL.Logic.Typization.Function;

namespace IPL.Helpers
{
    public class Builtin
    {
        public static readonly List<InternalFunction> Functions = new List<InternalFunction>()
        {
            // Math
            new InternalFunction("sin", 1, (args) => new NumberValue(Math.Sin(args[0].AsNumber()))),
            new InternalFunction("cos", 1, (args) => new NumberValue(Math.Cos(args[0].AsNumber()))),
            new InternalFunction("sqrt", 1, (args) =>
            {
                if (args[0].AsNumber() < 0)
                    throw new IPLRuntimeException("SQRT of < 0");
                return new NumberValue(Math.Sqrt(args[0].AsNumber()));
            }),

            // Type cast
            new InternalFunction("int", 1, (args) => new NumberValue(args[0].AsNumber())),
            new InternalFunction("str", 1, (args) => new StringValue(args[0].AsString())),

            // Output
            new InternalFunction("print", 1, (args) =>
            {
                Console.Write(args[0].AsString());
                return new NumberValue(0);
            }),
            new InternalFunction("println", 1, (args) =>
            {
                Console.WriteLine(args[0].AsString());
                return new NumberValue(0);
            }),

            // Input
            new InternalFunction("input", 0, (args) =>
            {
                string? data = Console.ReadLine();
                return new StringValue(data is null ? "" : data);
            }),
            new InternalFunction("input", 1, (args) =>
            {
                Console.Write(args[0].AsString());
                string? data = Console.ReadLine();
                return new StringValue(data is null ? "" : data);
            }),

            // File IO
            new InternalFunction("read", 1, (args) =>
            {
                string filePath = args[0].AsString();
                return new StringValue(File.ReadAllText(filePath));
            }),
            new InternalFunction("write", 2, (args) =>
            {
                string data = args[0].AsString();
                string filePath = args[1].AsString();
                File.WriteAllText(filePath, data);
                return new NumberValue(0);
            }),

            // Misc
            new InternalFunction("size", 1, (args) =>
            {
                if (args[0] is StringValue)
                    return new NumberValue(args[0].AsString().Length);
                if (args[0] is ISubscriptable)
                    return new NumberValue((args[0] as ISubscriptable).GetSize());
                throw new IPLRuntimeException($"Could not get size of {args[0]}");
            }),

            // Web
            new InternalFunction("request", 2, (args) =>
            {
                string requestURL = args[0].AsString();
                string requestType = args[1].AsString();
                return WebModule.Request(requestURL, requestType, "");
            }),
            new InternalFunction("request", 3, (args) =>
            {
                string requestURL = args[0].AsString();
                string requestType = args[1].AsString();
                string requestData = args[2].AsString();
                return WebModule.Request(requestURL, requestType, requestData);
            }),
            
            // Json
            new InternalFunction("unjson", 1, (args) =>
            {
                string json = args[0].AsString();
                return WebModule.ParseJson(json);
            }),
            new InternalFunction("jsonify", 1, (args) =>
            {
                if (args[0] is not DictionaryValue)
                    throw new IPLRuntimeException($"Could not serialize non-dictionary value: {args[0]}");
                return WebModule.MakeJson(args[0] as DictionaryValue);
            })

        };
    }
}
