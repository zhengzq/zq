using System;

namespace Zq.Common
{
    public class Ensure
    {
        public static void IsNotNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentException($"{name}不能为空");
            }
        }

        public static void IsNotNullOrEmpty(string input, string name)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException($"{name}不能为空");
            }
        }

        public static void IsNotNullOrWhiteSpace(string input, string name)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"{name}不能为空");
            }
        }

        public static void AreEqual(string id1, string id2, string errorMessageFormat)
        {
            if (id1 != id2)
            {
                throw new ArgumentException(string.Format(errorMessageFormat, id1, id2));
            }
        }
    }
}
