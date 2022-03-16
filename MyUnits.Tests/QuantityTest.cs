using MyUnits.Dimensions;
using Xunit;

namespace MyUnits.Tests
{
    public class QuantityTest
    {
        [Fact]
        public void TestGasConstant()
        {
            var IdealGasConstant = new Quantity(8.31451e-3, MolarEntropy.dimension);
            var SteamMolarMass = new Quantity(18.015257e-3, MolarMass.dimension);
            var SpecificSteamGasConstant = IdealGasConstant / SteamMolarMass;
            Assert.Equal(SpecificEntropy.dimension, SpecificSteamGasConstant.dimension);
            Assert.Equal(0.461526, SpecificSteamGasConstant.scalar, precision: 6);
        }

        [Fact]
        public void TestSpeed()
        {
            var distance = new Quantity(60, Length.dimension);
            var time = new Quantity(3600, Time.dimension);
            var speed = distance / time;
            Assert.Equal(Speed.dimension, speed.dimension);
            Assert.Equal(0.016667, speed.scalar, 6);
        }

        [Fact]
        public void TestAssertDimension()
        {
            var distance = new Quantity(10, Length.dimension);
            var typed = distance.Assert<Length>();
            Assert.IsType<Quantity<Length>>(typed);
            Assert.Equal(Length.dimension, typed.dimension);
        }
    }
}
