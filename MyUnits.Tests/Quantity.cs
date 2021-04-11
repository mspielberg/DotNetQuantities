using System;
using Xunit;

namespace MyUnits.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Quantity IdealGasConstant = new Quantity(8.31451e-3, Units.MolarEntropy);
            Quantity SteamMolarMass = new Quantity(18.015257e-3, Units.MolarMass);
            Quantity SpecificSteamGasConstant = IdealGasConstant / SteamMolarMass;
            Assert.Equal(Units.SpecificEntropy, SpecificSteamGasConstant.units);
            Assert.Equal(0.461526, SpecificSteamGasConstant.scalar, precision: 6);
        }
    }
}
