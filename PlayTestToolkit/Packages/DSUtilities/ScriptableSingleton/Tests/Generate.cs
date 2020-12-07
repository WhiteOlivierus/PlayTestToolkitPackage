using System;
using System.Linq;

namespace Tests
{
    public class Generate
    {
        private const string CHARACTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random RANDOM = new Random();
        public static string String(int length)
        {
            return new string(Enumerable.Repeat(CHARACTERS, length)
                                        .Select(s => s[RANDOM.Next(s.Length)])
                                        .ToArray());
        }
    }
}
