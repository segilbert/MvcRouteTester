using System;
using System.Collections.Specialized;
using System.Web;
using NSubstitute;

namespace MvcRouteTester
{
	public class Mockery
	{
		public static HttpContextBase ContextForUrl(string url)
		{
			var routeParts = url.Split('?');
			var relativeUrl = routeParts[0];
			var queryParams = UrlHelpers.MakeQueryParams(url);

			var httpContextMock = Substitute.For<HttpContextBase>();
            httpContextMock.Request.AppRelativeCurrentExecutionFilePath.Returns(relativeUrl);
            httpContextMock.Request.QueryString.Returns(queryParams);
			httpContextMock.Request.Params.Returns(queryParams);
			httpContextMock.Request.PathInfo.Returns(string.Empty);
			return httpContextMock;
		}
	}
}
