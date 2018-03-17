namespace EF.Experiments.Data
{
    public static class StringExt
    {
        public static bool ContainsText(this string text, string sub)
        {
            return text.Contains(sub);
        }
    }
}