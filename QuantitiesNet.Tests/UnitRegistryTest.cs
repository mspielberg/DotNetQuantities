using static QuantitiesNet.Dimensions;
using Xunit;

namespace QuantitiesNet.Tests
{
    public class UnitRegistryTest
    {
        public UnitRegistryTest()
        {
            Units.Initialize();
        }

        [Fact]
        public void TestEnumerateUnits()
        {
            Assert.True(UnitRegistry.Default.TryGetUnits<Length>(out var units));
            Assert.NotEmpty(units);
        }
    }
}
