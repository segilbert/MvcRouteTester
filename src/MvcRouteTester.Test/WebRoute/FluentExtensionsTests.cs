//
using System.Web.Mvc;
using System.Web.Routing;
//
using MvcRouteTester.Test.Controllers;
//
using Xunit;

namespace MvcRouteTester.Test.WebRoute
{
	public class FluentExtensionsTests
	{
		private RouteCollection routes;

		public FluentExtensionsTests()
		{
			routes = new RouteCollection();
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = 32 });
		}

		[Fact]
		public void SimpleFluentRoute()
		{
			routes.ShouldMap("/home/index/32").To<HomeController>(x => x.Index(32));
		}

		[Fact]
		public void DefaultFluentRoute()
		{
			routes.ShouldMap("/").To<HomeController>(x => x.Index(32));
		}

		[Fact]
		public void SimpleFluentRouteWithParams()
		{
			routes.ShouldMap("/home/index/32?foo=bar").To<HomeController>(x => x.Index(32));
		}

		[Fact]
		public void IgnoredRoute()
		{
			routes.ShouldMap("fred.axd").ToIgnoredRoute();
		}

		[Fact]
		public void NonIgnoredRoute()
		{
			routes.ShouldMap("/home/index/32?foo=bar").ToNonIgnoredRoute();
		}

		[Fact]
		public void NoRoute()
		{
			routes.ShouldMap("/foo/bar/fish/spon").ToNoRoute();
		}
	}
}
