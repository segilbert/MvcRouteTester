//

using FluentAssertions;
using Xunit;

namespace MvcRouteTester.Test
{
	public class MockeryTest
	{
		[Fact]
		public void ShouldReturnPathForUrl()
		{
			var context = Mockery.ContextForUrl("/foo/bar");

            context.Request.AppRelativeCurrentExecutionFilePath.ShouldBeEquivalentTo("/foo/bar");
		}

		[Fact]
		public void ShouldReturnPathForUrlWithoutParams()
		{
			var context = Mockery.ContextForUrl("/foo/bar?a=b");

			context.Request.AppRelativeCurrentExecutionFilePath.ShouldBeEquivalentTo("/foo/bar");
		}

		[Fact]
		public void ShouldReturnQueryParam()
		{
			var context = Mockery.ContextForUrl("/foo/bar?a=b");

			var queryParams = context.Request.Params;
			queryParams.Should().NotBeNull();
			queryParams.Count.ShouldBeEquivalentTo(1);
			queryParams["a"].ShouldBeEquivalentTo("b");
		}

		[Fact]
		public void ShouldReturnMultipleQueryParams()
		{
			var context = Mockery.ContextForUrl("/foo/bar?a=b&cee=123");

			var queryParams = context.Request.Params;
            queryParams.Should().NotBeNull();
            queryParams.Count.ShouldBeEquivalentTo(2);
            queryParams["a"].ShouldBeEquivalentTo("b");
			queryParams["cee"].ShouldBeEquivalentTo("123");
		}

		[Fact]
		public void ShouldReturnQueryString()
		{
			var context = Mockery.ContextForUrl("/foo/bar?a=b");

			var queryString = context.Request.QueryString;
            queryString.Should().NotBeNull();
            queryString.Count.ShouldBeEquivalentTo(1);
            queryString["a"].ShouldBeEquivalentTo("b");
		}

		[Fact]
		public void ShouldHandleMissingParamValue()
		{
			var context = Mockery.ContextForUrl("/foo/bar?a=b&c=");

			var queryString = context.Request.QueryString;
            queryString.Should().NotBeNull();
            queryString.Count.ShouldBeEquivalentTo(2);
            queryString["a"].ShouldBeEquivalentTo("b");
			queryString["c"].ShouldBeEquivalentTo(string.Empty);
		}

		[Fact]
		public void ShouldHandleMissingParamAssign()
		{
			var context = Mockery.ContextForUrl("/foo/bar?a=b&c");

			var queryString = context.Request.QueryString;
            queryString.Should().NotBeNull();
            queryString.Count.ShouldBeEquivalentTo(2);
            queryString["a"].ShouldBeEquivalentTo("b");
            queryString["c"].ShouldBeEquivalentTo(string.Empty);
		}

		[Fact]
		public void ShouldHaveEmptyPathInfo()
		{
			var context = Mockery.ContextForUrl("/foo/bar?a=b");

			context.Request.PathInfo.ShouldBeEquivalentTo(string.Empty);
		}
	}
}
