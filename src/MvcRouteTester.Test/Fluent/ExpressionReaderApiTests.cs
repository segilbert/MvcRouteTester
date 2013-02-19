//
using System;
using System.Linq.Expressions;
using System.Web.Http;
//
using FluentAssertions;
using MvcRouteTester.Fluent;
//
using Xunit;

namespace MvcRouteTester.Test.Fluent
{
	public class TestApiController : ApiController
	{
		public object Get()
		{
			return "Hello, Api.";
		}

		public object Post(int id = 12)
		{
			return "";
		}

	}

	public class ExpressionReaderApiTests
	{
		[Fact]
		public void ReadNullObjectThrowsException()
		{
			var reader = new ExpressionReader();

			Expression<Func<TestApiController, object>> args = null;
			Assert.Throws<ArgumentNullException>(() => reader.Read(args));
		}

		[Fact]
		public void ReadApiReturnsDictionary()
		{
			var reader = new ExpressionReader();

			Expression<Func<TestApiController, object>> args = c => c.Get();
			var result = reader.Read(args);

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void ReadGetsApiControllerAndAction()
		{
			var reader = new ExpressionReader();

			Expression<Func<TestApiController, object>> args = c => c.Get();
			var result = reader.Read(args);

			result["controller"].ShouldBeEquivalentTo("TestApi");
			result["action"].ShouldBeEquivalentTo("Get");
		}

		[Fact]
		public void ReadGetsMethodParam()
		{
			var reader = new ExpressionReader();

			Expression<Func<TestApiController, object>> args = c => c.Post(42);
			var result = reader.Read(args);

            result["controller"].ShouldBeEquivalentTo("TestApi");
            result["action"].ShouldBeEquivalentTo("Post");
			result["id"].ShouldBeEquivalentTo("42");
		}
	}
}
