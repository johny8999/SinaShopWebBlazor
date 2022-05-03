﻿using NETCore.Encrypt;
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
        public static Guid ToGuid(this string input)
        {
            return Guid.Parse(input);
        }

        /// <summary>
        /// رمز نگاری متن
        /// </summary>
        /// <param name="input">متن اصلی</param>
        /// <param name="key">کلید رمز نگاری</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string AesEncpypt(this string input,string key)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"'{nameof(input)}' cannot be null or whitespace.", nameof(input));

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));

            return EncryptProvider.AESEncrypt(input, key);
        }

        /// <summary>
        /// رمز گشایی متن
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string AesDecrypt(this string Input, string key)
        {
            if (string.IsNullOrWhiteSpace(Input))
                throw new ArgumentException($"'{nameof(Input)}' cannot be null or whitespace.", nameof(Input));

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));

            string Decrypt = EncryptProvider.AESDecrypt(Input, key);
            return Decrypt;
        }
    }
}
