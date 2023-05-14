using CommandLine;
using IPL.AST.Statement;
using IPL.Logic.Main;
using IPL.Logic.Tokenization;
using System.Diagnostics;
using System.Reflection;
using CommandLineParser = CommandLine.Parser;
using IPLParser = IPL.Logic.Main.Parser;

namespace IPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CommandLineParser.Default.ParseArguments<Options>(args)
                .WithParsed(Execute);
        }

        private static void Execute(Options options)
        {
            if (options.Verbose)
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                string version = versionInfo.ProductVersion is null ? "1.0.0" : versionInfo.ProductVersion;
                Console.WriteLine("IPL - Interpretable Programming Language");
                Console.WriteLine($"Interpreter v{version} >>>>>");
            }

            string programPath = options.ProgramFilePath;
            string code = File.ReadAllText(programPath);

            if (options.Verbose)
                Console.WriteLine($"Executing program file: {Path.GetFullPath(programPath)}");

            try
            {
                if (options.Verbose)
                    Console.WriteLine("-=========================================[TOKENIZATION]=-");

                Lexer lexer = new(code);
                List<Token> tokens = lexer.Tokenize();

                if (options.Verbose)
                {
                    foreach (Token token in tokens)
                        Console.WriteLine(string.Format("TOKEN:\n    TYPE: {0}\n    TEXT: {1}\n    POSITION: {2}",
                            token.TokenType, token.TokenText, token.Position));
                    Console.WriteLine("-==============================================[PARSING]=-");
                }

                IPLParser parser = new(tokens);
                List<IStatement> statements = parser.Parse();
                if (options.Verbose)
                {
                    foreach (IStatement statement in statements)
                        Console.WriteLine($"STATEMENT: {statement}");
                    Console.WriteLine("-============================================[EXECUTION]=-");
                }
                (new Interpreter(statements)).Interpret();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}