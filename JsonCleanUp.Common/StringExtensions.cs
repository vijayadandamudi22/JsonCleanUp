namespace JsonCleanUp.Common
{
    public static class StringExtensions
    {
        public static bool IsStringValid(String node)
            => String.IsNullOrEmpty(node) || String.Equals(node, Constants.NA, StringComparison.OrdinalIgnoreCase) || String.Equals(node, Constants.Hypen, StringComparison.OrdinalIgnoreCase);
    }
}