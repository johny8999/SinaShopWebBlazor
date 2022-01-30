using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrameWork.ExMethods
{
    public static class StringEx
    {
        public static bool IsMatch(this string input, string pattern, RegexOptions regexOptions = default)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException($"'{nameof(input)}' cannot be null or empty.", nameof(input));
            }

            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentException($"'{nameof(pattern)}' cannot be null or empty.", nameof(pattern));
            }
            return Regex.IsMatch(input, pattern, regexOptions);
        }
    }
}
