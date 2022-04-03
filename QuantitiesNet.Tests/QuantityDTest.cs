using System;
using QuantitiesNet.Dimensions;
using Xunit;

namespace QuantitiesNet.Tests
{
    public class QuantityDTest
    {
        [Fact]
        public void TestConstruct()
        {
            var typed = new Quantity<Length>(4);
            Assert.IsType<Quantity<Length>>(typed);
            Assert.Equal(Length.dimension, typed.dimension);
        }

        [Fact]
        public void TestConstructIncompatible()
        {
            Assert.Throws<ArgumentException>(() => new Quantity<Length>(10, Units.Hour));
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

        [Fact]
        public void TestGeneratedConversions()
        {
            var d = new Quantities.Length(1, Units.Mile);
            var t = new Quantities.Time(1, Units.Minute);
            var s = d / t;
            Assert.IsAssignableFrom<Quantity<Speed>>(s);
            Assert.Equal(60, s.In(Units.Mile / Units.Hour));
        }
    }
}
