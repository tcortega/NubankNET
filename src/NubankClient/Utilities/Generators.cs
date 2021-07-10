using System;
using System.Linq;

namespace tcortega.NubankClient.Helpers
{
    class Generators
    {
        private static Random s_random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[s_random.Next(s.Length)]).ToArray());
        }
    }
}
