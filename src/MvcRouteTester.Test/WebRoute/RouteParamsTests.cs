//
using System.Web.Mvc;
using System.Web.Routing;
//
using Xunit;

namespace MvcRouteTester.Test.WebRoute
{
	public class RouteParamsTests
	{
		private RouteCollection routes;

        public RouteParamsTests()
		{
			routes = new RouteCollection();
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
		}

		[Fact]
		public void HasRouteWithParams()
		{
			RouteAssert.HasRoute(routes, "/test/index?foo=1&bar=2");
		}

		[Fact]
		public void HasRouteWithParamsCapturesValues()
		{
			var expectedRoute = new { controller = "Test", action = "Index", foo = "1", bar = "2" };
			RouteAssert.HasRoute(routes, "/test/index?foo=1&bar=2", expectedRoute);
		}
	}
}
