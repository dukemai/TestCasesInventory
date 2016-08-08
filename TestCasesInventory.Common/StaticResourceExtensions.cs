using System;

namespace TestCasesInventory.Common
{
    public static class StaticResourceExtensions
    {
        public static string AppendVersioningQueryString(this string input, string versioning = "")
        {
            var template = input.IndexOf("?") == -1 ? "{0}?v={1}" : "{0}&v={1}";

            if (string.IsNullOrEmpty(versioning))
            {
                versioning = DateTime.Now.Ticks.ToString();
            }

            return string.Format(template, input, versioning);
        }
    }
}
