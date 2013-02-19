using System;
using System.Globalization;
using System.IO;
using Xunit;

namespace MvcRouteTester
{
	public static class Asserts
	{
		public static void Fail(string message)
		{
		    Assert.True(false, message);
		}
	
        public static void ShouldEqualWithDiff(this string actualValue, string expectedValue)
        {
            ShouldEqualWithDiff(actualValue, expectedValue, StringComparison.Ordinal, DiffStyle.Full, Console.Out, string.Empty);
        }

        public static void ShouldEqualWithDiff(this string actualValue, string expectedValue, string message)
        {
            ShouldEqualWithDiff(actualValue, expectedValue, StringComparison.Ordinal, DiffStyle.Full, Console.Out, message);
        }

        public static void ShouldEqualWithDiff(this string actualValue, string expectedValue, StringComparison stringComparison)
        {
            ShouldEqualWithDiff(actualValue, expectedValue, stringComparison, DiffStyle.Full, Console.Out, string.Empty);
        }

        public static void ShouldEqualWithDiff(this string actualValue, string expectedValue, StringComparison stringComparison, string message)
        {
            ShouldEqualWithDiff(actualValue, expectedValue, stringComparison, DiffStyle.Full, Console.Out, message);
        }

        public static void ShouldEqualWithDiff(this string actualValue, string expectedValue, DiffStyle diffStyle)
        {
            ShouldEqualWithDiff(actualValue, expectedValue, StringComparison.Ordinal, diffStyle, Console.Out, string.Empty);
        }

        public static void ShouldEqualWithDiff(this string actualValue, string expectedValue, StringComparison stringComparison, DiffStyle diffStyle, TextWriter output, string message)
        {
            if (actualValue == null || expectedValue == null)
            {
                Assert.Equal(expectedValue, actualValue);
                return;
            }

            if (actualValue.Equals(expectedValue, stringComparison)) return;

            output.WriteLine( message );
            output.WriteLine("  Idx Expected  Actual");
            output.WriteLine("-------------------------");
            int maxLen = Math.Max(actualValue.Length, expectedValue.Length);
            int minLen = Math.Min(actualValue.Length, expectedValue.Length);
            for (int i = 0; i < maxLen; i++)
            {
                if (diffStyle != DiffStyle.Minimal || i >= minLen || actualValue[i] != expectedValue[i])
                {
                    output.WriteLine(String.Format("{0} {1,-3} {2,-4} {3,-3}  {4,-4} {5,-3}",
                        i < minLen && actualValue[i] == expectedValue[i] ? " " : "*", // put a mark beside a differing row
                        i, // the index
                        i < expectedValue.Length ? ((int)expectedValue[i]).ToString() : "", // character decimal value
                        i < expectedValue.Length ? expectedValue[i].ToSafeString() : "", // character safe string
                        i < actualValue.Length ? ((int)actualValue[i]).ToString() : "", // character decimal value
                        i < actualValue.Length ? actualValue[i].ToSafeString() : "" // character safe string
                    ));
                }
            }
            output.WriteLine();

            Assert.Equal(expectedValue, actualValue);
        }

        public static void ShouldNotEqualWithDiff(this string actualValue, string expectedValue)
        {
            ShouldNotEqualWithDiff(actualValue, expectedValue, StringComparison.Ordinal, DiffStyle.Full, Console.Out, string.Empty);
        }

        public static void ShouldNotEqualWithDiff(this string actualValue, string expectedValue, string message)
        {
            ShouldNotEqualWithDiff(actualValue, expectedValue, StringComparison.Ordinal, DiffStyle.Full, Console.Out, message);
        }

        public static void ShouldNotEqualWithDiff(this string actualValue, string expectedValue, StringComparison stringComparison, DiffStyle diffStyle, TextWriter output, string message)
        {
            if (actualValue == null || expectedValue == null)
            {
                Assert.NotEqual(expectedValue, actualValue);
                return;
            }

            if (!actualValue.Equals(expectedValue, stringComparison)) return;

            output.WriteLine(message);
            output.WriteLine("  Idx Expected  Actual");
            output.WriteLine("-------------------------");
            int maxLen = Math.Max(actualValue.Length, expectedValue.Length);
            int minLen = Math.Min(actualValue.Length, expectedValue.Length);
            for (int i = 0; i < maxLen; i++)
            {
                if (diffStyle != DiffStyle.Minimal || i >= minLen || actualValue[i] != expectedValue[i])
                {
                    output.WriteLine(String.Format("{0} {1,-3} {2,-4} {3,-3}  {4,-4} {5,-3}",
                        i < minLen && actualValue[i] == expectedValue[i] ? " " : "*", // put a mark beside a differing row
                        i, // the index
                        i < expectedValue.Length ? ((int)expectedValue[i]).ToString() : "", // character decimal value
                        i < expectedValue.Length ? expectedValue[i].ToSafeString() : "", // character safe string
                        i < actualValue.Length ? ((int)actualValue[i]).ToString() : "", // character decimal value
                        i < actualValue.Length ? actualValue[i].ToSafeString() : "" // character safe string
                    ));
                }
            }
            output.WriteLine();

            Assert.NotEqual(expectedValue, actualValue);
        }

        private static string ToSafeString(this char c)
        {
            if (Char.IsControl(c) || Char.IsWhiteSpace(c))
            {
                switch (c)
                {
                    case '\r':
                        return @"\r";
                    case '\n':
                        return @"\n";
                    case '\t':
                        return @"\t";
                    case '\a':
                        return @"\a";
                    case '\v':
                        return @"\v";
                    case '\f':
                        return @"\f";
                    case ' ':
                        return @"\s";
                    default:
                        return String.Format("\\u{0:X};", (int)c);
                }
            }
            return c.ToString(CultureInfo.InvariantCulture);
        }
    }

    public enum DiffStyle
    {
        Full,
        Minimal
    }
}
