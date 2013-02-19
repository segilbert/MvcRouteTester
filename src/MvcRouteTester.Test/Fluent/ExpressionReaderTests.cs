//
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
//
using FluentAssertions;
using MvcRouteTester.Fluent;
//
using Xunit;

namespace MvcRouteTester.Test.Fluent
{
	public class TestController : Controller
	{
		public ActionResult Index()
		{
			return new EmptyResult();
		}

		public ActionResult GetItem(int id = 12)
		{
			return new EmptyResult();
		}

	}

	public class ExpressionReaderTests
	{
		[Fact]
		public void ReadNullActionResultThrowsException()
		{
			var reader = new ExpressionReader();

			Expression<Func<TestController, ActionResult>> args = null;
			Assert.Throws<ArgumentNullException>(() => reader.Read(args));
		}

		[Fact]
		public void ReadReturnsDictionary()
		{
			var reader = new ExpressionReader();

			Expression<Func<TestController, ActionResult>> args = c => c.Index();
			var result = reader.Read(args);

		    result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan(0);
		}

		[Fact]
		public void ReadGetsControllerAndAction()
		{
			var reader = new ExpressionReader();

			Expression<Func<TestController, ActionResult>> args = c => c.Index();
			var result = reader.Read(args);

			result["controller"].ShouldBeEquivalentTo("Test");
			result["action"].ShouldBeEquivalentTo("Index");
		}

		[Fact]
		public void ReadGetsMethodParams()
		{
			var reader = new ExpressionReader();

			Expression<Func<TestController, ActionResult>> args = c => c.GetItem(42);
			var result = reader.Read(args);

            result["controller"].ShouldBeEquivalentTo("Test");
            result["action"].ShouldBeEquivalentTo("GetItem");
			result["id"].ShouldBeEquivalentTo("42");
		}
	}
}
