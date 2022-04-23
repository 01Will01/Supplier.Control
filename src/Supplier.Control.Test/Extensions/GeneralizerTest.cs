
using Supplier.Control.Domain.Extensions;
using System.Collections.Generic;
using Xunit;


namespace Supplier.Control.Test.Extensions
{
    public class GeneralizerTest
    {

        [Fact]
        public void IsNull_Test()
        {
            Assert.True(Generalizer.IsNull<string>(null));
            Assert.False(Generalizer.IsNull<string>("test"));
        }

        [Fact]
        public void IsNotNull_Test()
        {
            Assert.False(Generalizer.IsNotNull<string>(null));
            Assert.True(Generalizer.IsNotNull<string>("test"));
        }

        [Fact]
        public void IsIqual_Test()
        {
            object one = new { name = "Test" };
            object two = new { name = "Test" };

            Assert.True(Generalizer.IsIqual(one, two));
            two = null;
            Assert.False(Generalizer.IsIqual(one, two));
            one = null;
            Assert.True(Generalizer.IsIqual(one, two));
        }

        [Fact]
        public void IsNotIqual_Test()
        {
            object one = new { name = "Test" };
            object two = new { name = "Test" };

            Assert.False(Generalizer.IsNotIqual(one, two));
            two = null;
            Assert.True(Generalizer.IsNotIqual(one, two));
            one = null;
            Assert.False(Generalizer.IsNotIqual(one, two));
        }

        [Fact]
        public void TransformInList_Test()
        {
            Assert.Equal(new List<string>() { "Test_Test" }, Generalizer.TransformInList("Test_Test"));
            Assert.Equal(new List<string>() { null }, Generalizer.TransformInList<string>(null));
            Assert.Equal(new List<int>() { 30 }, Generalizer.TransformInList(30));
            Assert.Equal(new List<object>() { new { test = "test" } }, Generalizer.TransformInList(new { test = "test" }));
        }
    }
}
