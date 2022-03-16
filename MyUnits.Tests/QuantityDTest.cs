using System;
using MyUnits.Dimensions;
using Xunit;

namespace MyUnits.Tests
{
    public class QuantityDTest
    {
        [Fact]
        public void TestConstruct()
        {
            var typed = new Quantity<Length>(4);
            Assert.IsType<Quantity<Length>>(typed);
            Assert.Equal(Dimension.Length, typed.dimension);
        }

        [Fact]
        public void TestAdd()
        {
            var q1 = new Quantity<Length>(1);
            var q2 = new Quantity<Length>(2);
            var q3 = q1 + q2;
            Assert.IsType<Quantity<Length>>(q3);
            Assert.Equal(new Quantity<Length>(3), q3);
        }

        [Fact]
        public void TestAddIncompatible()
        {
            var q1 = new Quantity<Length>(1);
            var q2 = new Quantity<Time>(2);
            Assert.Throws<ArgumentException>(() => q1 + q2);
        }

        [Fact]
        public void TestSubtract()
        {
            var q1 = new Quantity<Length>(1);
            var q2 = new Quantity<Length>(2);
            var q3 = q1 - q2;
            Assert.Equal(new Quantity<Length>(-1), q3);
        }

        [Fact]
        public void TestMultiply()
        {
            var q1 = new Quantity<Length>(2);
            var q2 = new Quantity<Length>(3);
            var q3 = (q1 * q2).Assert<Area>();
            Assert.IsType<Quantity<Area>>(q3);
            Assert.Equal(new Quantity<Area>(6), q3);
        }
    }
}
