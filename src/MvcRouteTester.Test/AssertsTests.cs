//
using System;
//
using Xunit;


namespace MvcRouteTester.Test
{
	public class AssertsTests
	{
		[Fact]
		public void EqualStringsPass()
		{
			"foo".ShouldEqualWithDiff("foo", "fail");
		}

        [Fact]
		public void EqualStringsOnlyDifferentCaseStringsWithIgnoreCasePass()
		{
			"foo".ShouldEqualWithDiff("Foo", StringComparison.OrdinalIgnoreCase, "fail");
		}

        [Fact]
        public void DifferentCaseStringsCheckCaseFail()
        {
            "foo".ShouldNotEqualWithDiff("Foo", "fail");
        }
	
	}
}
