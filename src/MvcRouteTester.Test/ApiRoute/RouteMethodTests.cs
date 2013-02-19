//
using System.Net.Http;
using System.Web.Http;
//
using Xunit;

namespace MvcRouteTester.Test.ApiRoute
{
	public class RouteMethodTests
	{
		private HttpConfiguration config;

        public RouteMethodTests()
		{
			config = new HttpConfiguration();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional });
		}

		[Fact]
		public void CustomerControllerHasGetMethod()
		{
			RouteAssert.HasApiRoute(config, "/api/customer/1", HttpMethod.Get);
		}

		[Fact]
		public void CustomerControllerDoesNotHavePostMethod()
		{
			RouteAssert.ApiRouteDoesNotHaveMethod(config, "/api/customer/1", HttpMethod.Post);
		}

		[Fact]
		public void PostOnlyControllerHasPostMethod()
		{
			RouteAssert.HasApiRoute(config, "/api/postonly/1", HttpMethod.Post);
		}

		[Fact]
		public void PostOnlyControllerDoesNotHaveGetMethod()
		{
			RouteAssert.ApiRouteDoesNotHaveMethod(config, "/api/postonly/1", HttpMethod.Get);
		}
	}
}
