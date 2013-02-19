//
using System.Net.Http;
using System.Web.Http;
//
using MvcRouteTester.Test.ApiControllers;
//
using Xunit;

namespace MvcRouteTester.Test.ApiRoute
{
	public class FluentExtensionsTests
	{
		private HttpConfiguration config;

        public FluentExtensionsTests()
		{
			config = new HttpConfiguration();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional });
		}

		[Fact]
		public void SimpleTest()
		{
			config.ShouldMap("/api/customer/32").To<CustomerController>(HttpMethod.Get, x => x.Get(32));
		}

		[Fact]
		public void TestNoRouteForMethod()
		{
			config.ShouldMap("/api/customer/32").ToNoMethod<CustomerController>(HttpMethod.Post);
		}

		[Fact]
		public void TestNoRoute()
		{
			config.ShouldMap("/pai/customer/32").ToNoRoute();
		}
	}
}
