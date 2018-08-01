using System.Linq;

namespace ShoppingStatistics.Core.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveCharacters(this string src, char[] characters)
        {
            return characters.Aggregate(src, (current, c) => current.Replace(c.ToString(), ""));
        }
    }
}
