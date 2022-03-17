using static MyUnits.Units;
using Xunit;

namespace MyUnits
{
    public class UnitTest
    {
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
    }
}