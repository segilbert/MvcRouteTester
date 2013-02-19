//
using System;
//
using Xunit;
//
using FluentAssertions;

namespace MvcRouteTester.Test
{
	public class PropertyReaderTests
	{
		[Fact]
		public void ShouldReadEmptyObject()
		{
			var reader = new PropertyReader();
			var properties = reader.Properties(new object());
		    
            properties.Should().NotBeNull();
            properties.Count.ShouldBeEquivalentTo(0);
		}

		[Fact]
		public void ShouldNotReadNullObject()
		{
			var reader = new PropertyReader();

			Assert.Throws<ArgumentNullException>(() => reader.Properties(null));
		}

		[Fact]
		public void ShouldReadPropertiesOfAnonObject()
		{
			var reader = new PropertyReader();
			var properties = reader.Properties(new { Foo = 1, Bar = "Two" });

			properties.Should().NotBeNull();
            properties.Count.ShouldBeEquivalentTo(2);
            properties["Foo"].ShouldBeEquivalentTo("1");
            properties["Bar"].ShouldBeEquivalentTo("Two");
		}

		[Fact]
		public void ShouldReadPropertyValueNull()
		{
			var reader = new PropertyReader();
			var properties = reader.Properties(new { Foo = 1, Bar = (string)null });

            properties.Should().NotBeNull();
            properties.Count.ShouldBeEquivalentTo(2);
            properties["Foo"].ShouldBeEquivalentTo("1");
            properties["Bar"].Should().BeNull();
		}
	}
}
