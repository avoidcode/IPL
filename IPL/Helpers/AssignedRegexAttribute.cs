namespace IPL.Helpers
{
    public class AssignedRegexAttribute : Attribute
    {
        public string Regex { get; private set; }
        public AssignedRegexAttribute(string regex)
        {
            Regex = regex;
        }
    }
}
