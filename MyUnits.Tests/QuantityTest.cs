using Xunit;

namespace MyUnits.Tests
{
    public class QuantityTest
    {
        [Fact]
        public void TestGasConstant()
        {
            var IdealGasConstant = new Quantity(8.31451e-3, Dimension.MolarEntropy);
            var SteamMolarMass = new Quantity(18.015257e-3, Dimension.MolarMass);
            var SpecificSteamGasConstant = IdealGasConstant / SteamMolarMass;
            Assert.Equal(Dimension.SpecificEntropy, SpecificSteamGasConstant.dimension);
            Assert.Equal(0.461526, SpecificSteamGasConstant.scalar, precision: 6);
        }

        [Fact]
        public void TestSpeed()
        {
            var distance = new Quantity(60, Dimension.Length);
            var time = new Quantity(3600, Dimension.Time);
            var speed = distance / time;
            Assert.Equal(Dimension.Speed, speed.dimension);
            Assert.Equal(0.016667, speed.scalar, 6);
        }

        [Fact]
        public void TestAssertDimension()
        {
            var distance = new Quantity(10, Dimension.Length);
            var typed = distance.Assert<Dimensions.Length>();
            Assert.IsType<Quantity<Dimensions.Length>>(typed);
            Assert.Equal(Dimension.Length, typed.dimension);
        }
    }
}
