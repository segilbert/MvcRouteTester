//

using FluentAssertions;
using Xunit;

namespace MvcRouteTester.Test
{
	public class UrlHelpersTest
	{
		[Fact]
		public void AbsoluteUrlIsUnchanged()
		{
			var outputUrl = UrlHelpers.MakeAbsolute("http://foo.com");

			outputUrl.ShouldBeEquivalentTo("http://foo.com");
		}

		[Fact]
		public void AbsoluteHttpsUrlIsUnchanged()
		{
			var outputUrl = UrlHelpers.MakeAbsolute("https://bar.com");

			outputUrl.ShouldBeEquivalentTo("https://bar.com");
		}

		[Fact]
		public void FtpHttpsUrlIsUnchanged()
		{
			var outputUrl = UrlHelpers.MakeAbsolute("ftp://bar.com/filez.zip");

			outputUrl.ShouldBeEquivalentTo("ftp://bar.com/filez.zip");
		}

		[Fact]
		public void EmptyRelativeTildeUrlIsPrefixed()
		{
			var outputUrl = UrlHelpers.MakeAbsolute("~/");

			outputUrl.ShouldBeEquivalentTo("http://site.com/");
		}

		[Fact]
		public void EmptyRelativeSlashUrlIsPrefixed()
		{
			var outputUrl = UrlHelpers.MakeAbsolute("/");

			outputUrl.ShouldBeEquivalentTo("http://site.com/");
		}

		[Fact]
		public void NonEmptyTildeRelativeUrlIsPrefixed()
		{
			var outputUrl = UrlHelpers.MakeAbsolute("~/customers/1");

			outputUrl.ShouldBeEquivalentTo("http://site.com/customers/1");
		}

		[Fact]
		public void NonEmptySlashRelativeUrlIsPrefixed()
		{
			var outputUrl = UrlHelpers.MakeAbsolute("/customers/1");

			outputUrl.ShouldBeEquivalentTo("http://site.com/customers/1");
		}

		[Fact]
		public void SlashPathHasTildePrended()
		{
			var outputUrl = UrlHelpers.PrependTilde("/");

			outputUrl.ShouldBeEquivalentTo("~/");
		}

		[Fact]
		public void TildeSlashPathIsUnchanged()
		{
			var outputUrl = UrlHelpers.PrependTilde("~/");

			outputUrl.ShouldBeEquivalentTo("~/");
		}

		[Fact]
		public void PathWithTildeIsUnchanged()
		{
			var outputUrl = UrlHelpers.PrependTilde("~/customers/1");

			outputUrl.ShouldBeEquivalentTo("~/customers/1");
		}

		[Fact]
		public void PathHasTildePrepended()
		{
			var outputUrl = UrlHelpers.PrependTilde("/customers/1") ;

			outputUrl.ShouldBeEquivalentTo("~/customers/1");
		}

		[Fact]
		public void UrlWithNoQueryParamsIsParsed()
		{
			const string Url = "/foo/bar";

			var parsedParams = UrlHelpers.MakeQueryParams(Url);

			parsedParams.Count.ShouldBeEquivalentTo(0);
		}

		[Fact]
		public void UrlWithOneQueryParamsIsParsed()
		{
			const string Url = "/foo/bar?id=3";

			var parsedParams = UrlHelpers.MakeQueryParams(Url);

			parsedParams.Count.ShouldBeEquivalentTo(1);
			parsedParams["id"].ShouldBeEquivalentTo("3");
		}

		[Fact]
		public void UrlWithTwoQueryParamsIsParsed()
		{
			const string Url = "/foo/bar?name=fish&fish=trout";

			var parsedParams = UrlHelpers.MakeQueryParams(Url);

			parsedParams.Count.ShouldBeEquivalentTo(2);
			parsedParams["name"].ShouldBeEquivalentTo("fish");
            parsedParams["fish"].ShouldBeEquivalentTo("trout");
		}
	}
}
