using static QuantitiesNet.Units;
using Xunit;

namespace QuantitiesNet
{
    public class UnitTest
    {
        [Fact]
        public void SpeedTest()
        {
            Assert.Equal(3.6, new Quantity(1, Meter / Second).In(Kilometer / Hour), 10);
            Assert.Equal(96.5606, new Quantity(60, Mile / Hour).In(Kilometer / Hour), 4);
        }

        [Fact]
        public void AbsoluteZeroTest()
        {
            Assert.Equal(0, new Quantity(-273.15, Celsius).In(Kelvin));
            Assert.Equal(0, new Quantity(-459.67, Fahrenheit).In(Kelvin), 10);
            Assert.Equal(-273.15, new Quantity(0, Kelvin).In(Celsius));
            Assert.Equal(-459.67, new Quantity(0, Kelvin).In(Fahrenheit), 10);
            Assert.Equal(-273.15, new Quantity(-459.67, Fahrenheit).In(Celsius));
            Assert.Equal(-459.67, new Quantity(-273.15, Celsius).In(Fahrenheit), 10);
        }

        [Fact]
        public void WaterFreezingTest()
        {
            Assert.Equal(0, new Quantity(273.15, Kelvin).In(Celsius));
            Assert.Equal(0, new Quantity(32, Fahrenheit).In(Celsius), 10);
            Assert.Equal(273.15, new Quantity(0, Celsius).In(Kelvin));
            Assert.Equal(32, new Quantity(0, Celsius).In(Fahrenheit), 10);
        }

        [Fact]
        public void WaterBoilingTest()
        {
            Assert.Equal(100, new Quantity(373.15, Kelvin).In(Celsius));
            Assert.Equal(100, new Quantity(212, Fahrenheit).In(Celsius), 10);
        }

        [Fact]
        public void SymbolsShouldIncludeOperators()
        {
            Assert.Equal("J*K", (Joule * Kelvin).Symbol);
            Assert.Equal("km/h", (Kilometer / Hour).Symbol);
        }

        [Fact]
        public void PrefixesShouldRenderWithoutSpace()
        {
            Assert.Equal("km", Kilometer.Symbol);
        }
    }
}