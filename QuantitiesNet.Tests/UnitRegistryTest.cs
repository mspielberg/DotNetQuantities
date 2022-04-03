using QuantitiesNet.Dimensions;
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
            Assert.NotEmpty(UnitRegistry.Default.GetUnits<Length>());
        }
    }
}