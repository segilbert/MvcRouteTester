//
using System.Net.Http;
using System.Web.Http;
//
using Xunit;

namespace MvcRouteTester.Test.ApiRoute
{
	public class RouteParamsTests
	{
		private HttpConfiguration config;

        public RouteParamsTests()
		{
			config = new HttpConfiguration();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional });
		}
		
		[Fact]
		public void HasRouteWithParams()
		{
			RouteAssert.HasApiRoute(config, "/api/customer/1?foo=1&bar=2", HttpMethod.Get);
		}

		[Fact]
		public void HasRouteWithParamsCapturesValues()
		{
			var expectedRoute = new { controller = "customer", action = "get", id = "1", foo = "1", bar = "2" };
			RouteAssert.HasApiRoute(config, "/api/customer/1?foo=1&bar=2", HttpMethod.Get, expectedRoute);
		}
	}
}
