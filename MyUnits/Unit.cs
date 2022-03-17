using MyUnits.Dimensions;

namespace MyUnits
{
    public readonly struct Unit
    {
        public Dimension Dimension { get; }
        // <summary>Number of Dimension base units represented by one of this Unit.</summary>
        public double Scalar { get; }
        // <summary>Zero point of this unit in Dimension base units.</summary>
        public double Offset { get; }

        public Unit(Dimension dim, double scalar, double offset = 0)
        {
            this.Dimension = dim;
            this.Scalar = scalar;
            this.Offset = offset;
        }

        public static Unit Of<D>(double scalar, double offset = 0)
            where D : IDimension, new()
        {
            return new Unit(
                (Dimension)typeof(D).GetProperty("Dimension").GetValue(new D()),
                scalar,
                offset);
        }
     }

    public static class Units
    {
        public static readonly Unit Millimeter = Unit.Of<Length>(1e-3);
        public static readonly Unit Centimeter = Unit.Of<Length>(1e-2);
        public static readonly Unit Kilometer  = Unit.Of<Length>(1e+3);
        public static readonly Unit Inch = Unit.Of<Length>(0.0254);
        public static readonly Unit Foot = Unit.Of<Length>(0.3048);
        public static readonly Unit Yard = Unit.Of<Length>(0.9144);
        public static readonly Unit Mile = Unit.Of<Length>(1609.344);

        public static readonly Unit Kelvin = Unit.Of<Temperature>(1, 0);
        public static readonly Unit Celsius = Unit.Of<Temperature>(1, 273.15);
        public static readonly Unit Fahrenheit = Unit.Of<Temperature>(5 / 9.0, 273.15 - (32 * 5 / 9.0));
    }
}