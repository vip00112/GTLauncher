using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GTUtil
{
    public static class StringExtension
    {
        public static bool IsMatchByPattern(this string src, string pattern, bool ignoreCare = false)
        {
            if (string.IsNullOrWhiteSpace(pattern)) return false;
            if (pattern == "*" || pattern == "*.*") return true;

            pattern = pattern.Replace(@"*", "\xfc");
            pattern = pattern.Replace(@"?", "\xfd");
            pattern = pattern.Replace(@"#", "\xfe");

            pattern = Regex.Escape(pattern);

            pattern = pattern.Replace("\xfc", @".*[^.]");
            pattern = pattern.Replace("\xfd", @".");
            pattern = pattern.Replace("\xfe", @"[0-9]");

            var option = RegexOptions.Compiled;
            if (ignoreCare)
            {
                option |= RegexOptions.IgnoreCase;
            }

            return Regex.IsMatch(src, pattern, option);
        }
    }
}
