//
using System.Web.Mvc;
using System.Web.Routing;
//
using Xunit;

namespace MvcRouteTester.Test.WebRoute
{
	public class IgnoredRouteTest
	{
		private RouteCollection routes;

        public IgnoredRouteTest()
		{
			routes = new RouteCollection();
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}

		[Fact]
		public void IgnoredRouteIsFound()
		{
			RouteAssert.HasRoute(routes, "fred.axd");
		}

		[Fact]
		public void NormalRouteIsFound()
		{
			RouteAssert.HasRoute(routes, "foo/bar/1");
		}

		[Fact]
		public void IgnoredRouteIsIgnored()
		{
			RouteAssert.IsIgnoredRoute(routes, "fred.axd");
		}

		[Fact]
		public void NormalRouteIsNotIgnored()
		{
			RouteAssert.IsNotIgnoredRoute(routes, "foo/bar/1");
		}
	}
}
