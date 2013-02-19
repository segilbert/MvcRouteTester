//
using System.Web.Mvc;
using System.Web.Routing;
//
using Xunit;

namespace MvcRouteTester.Test.WebRoute
{
	public class HasRouteTests
	{
		private RouteCollection routes;

        public HasRouteTests()
		{
			routes = new RouteCollection();
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
		}

		[Fact]
		public void HasEmptyRoute()
		{
			RouteAssert.HasRoute(routes, "/");
		}

		[Fact]
		public void HasHomeRoute()
		{
			RouteAssert.HasRoute(routes, "/home");
		}

		[Fact]
		public void HasHomeIndexRoute()
		{
			RouteAssert.HasRoute(routes, "/home/index");
		}

		[Fact]
		public void HasHomeIndexWithIdRoute()
		{
			RouteAssert.HasRoute(routes, "/home/index/1");
		}

		[Fact]
		public void DoesNotHaveOtherRoute()
		{
			RouteAssert.NoRoute(routes, "/foo/bar/fish/spon");
		}
	}
}
