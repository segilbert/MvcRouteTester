//
using System.Web.Mvc;
using System.Web.Routing;
//
using Xunit;

namespace MvcRouteTester.Test.WebRoute
{
	public class RouteValuesTest
	{
		private RouteCollection routes;

        public RouteValuesTest()
		{
			routes = new RouteCollection();
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

			routes.MapRoute(
				name: "FooBarFishRoute",
				url: "Foo/Bar/Fish/{name}/{pony}",
				defaults: new { controller = "Foo", action = "Bar" });

		}

		[Fact]
		public void HomeRouteCapturesId()
		{
			var expectedRoute = new { controller = "Home", action = "Index", id = 42 };
			RouteAssert.HasRoute(routes, "/home/Index/42", expectedRoute);
		}

		[Fact]
		public void RouteCapturesTwoValues()
		{
			var expectedRoute = new { controller = "Foo", action = "Bar", name = "betsy", pony = "trotter" };
			RouteAssert.HasRoute(routes, "/Foo/Bar/Fish/betsy/trotter", expectedRoute);
		}
	}
}
