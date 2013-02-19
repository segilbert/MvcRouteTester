//
using System.Web.Mvc;
using System.Web.Routing;
//
using Xunit;

namespace MvcRouteTester.Test.WebRoute
{
	public class DefaultsTests
	{
		private RouteCollection routes;

        public DefaultsTests()
		{
			routes = new RouteCollection();
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = 32 });
		}

		[Fact]
		public void HasRouteWithAllValuesSpecified()
		{
			var expectedRoute = new { controller = "Home", action = "Index", id = "42" };
			RouteAssert.HasRoute(routes, "/home/index/42", expectedRoute);
		}

		[Fact]
		public void HasRouteWithDefaultId()
		{
			var expectedRoute = new { controller = "Home", action = "Index", id = "32" };
			RouteAssert.HasRoute(routes, "/home/index", expectedRoute);
		}

		[Fact]
		public void HasRouteWithDefaultActionAndId()
		{
			var expectedRoute = new { controller = "Home", action = "Index", id = "32" };
			RouteAssert.HasRoute(routes, "/home", expectedRoute);
		}
		[Fact]
		public void HasRouteWithAllDefaults()
		{
			var expectedRoute = new { controller = "Home", action = "Index", id = "32" };
			RouteAssert.HasRoute(routes, "/", expectedRoute);
		}
	}
}
