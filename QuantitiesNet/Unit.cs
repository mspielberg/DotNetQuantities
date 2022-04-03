using System;
using QuantitiesNet.Dimensions;

namespace QuantitiesNet
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

        public static Unit operator * (double s, Unit u)
        {
            return new Unit(u.Dimension, s * u.Scalar);
        }

        public static Unit operator * (Unit u1, Unit u2)
        {
            if (u1.Offset != 0 || u2.Offset != 0)
                throw new ArgumentException("Can only combine non-offset units");
            return new Unit(u1.Dimension * u2.Dimension, u1.Scalar * u2.Scalar);
        }

        public static Unit operator / (Unit u1, Unit u2)
        {
            if (u1.Offset != 0 || u2.Offset != 0)
                throw new ArgumentException("Can only combine non-offset units");
            return new Unit(u1.Dimension / u2.Dimension, u1.Scalar / u2.Scalar);
        }
     }

    public static class Units
    {
        public static readonly Unit Meter = Unit.Of<Length>(1);
        public static readonly Unit Millimeter = 1e-3 * Meter;
        public static readonly Unit Centimeter = 1e-2 * Meter;
        public static readonly Unit Kilometer  = 1000 * Meter;
        public static readonly Unit Inch = 2.54 * Centimeter;
        public static readonly Unit Foot = 12 * Inch;
        public static readonly Unit Yard = 3 * Foot;
        public static readonly Unit Mile = 1760 * Yard;

        public static readonly Unit Second = Unit.Of<Time>(1);
        public static readonly Unit Minute = 60 * Second;
        public static readonly Unit Hour = 60 * Minute;

        public static readonly Unit Kelvin = Unit.Of<Temperature>(1, 0);
        public static readonly Unit Celsius = Unit.Of<Temperature>(1, 273.15);
        public static readonly Unit Fahrenheit = Unit.Of<Temperature>(5 / 9.0, 273.15 - (32 * 5 / 9.0));
    }
}