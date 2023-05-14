using CommandLine;

namespace IPL
{
    public class Options
    {
        [Value(0, MetaName = "Input file", Required = true, HelpText = "Path to file with IPL program code to execute")]
        public string ProgramFilePath { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Print additional info to standard output")]
        public bool Verbose { get; set; }
    }
}
