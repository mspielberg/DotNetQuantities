using System;
using QuantitiesNet.Dimensions;
using static QuantitiesNet.Prefixes;

namespace QuantitiesNet
{
    public readonly struct Unit
    {
        public string Symbol { get; }
        public Dimension Dimension { get; }
        // <summary>Number of Dimension base units represented by one of this Unit.</summary>
        public double Scalar { get; }
        // <summary>Zero point of this unit in Dimension base units.</summary>
        public double Offset { get; }

        public Unit(string symbol, Dimension dim, double scalar, double offset = 0)
        {
            this.Symbol = symbol;
            this.Dimension = dim;
            this.Scalar = scalar;
            this.Offset = offset;
        }

        public Unit(string symbol, double scalar, Unit basis)
        : this(symbol, basis.Dimension, scalar * basis.Scalar, 0)
        {
            if (basis.Offset != 0)
                throw new ArgumentException("Cannot scale unit with offset");
        }

        public static Unit Of<D>(string symbol, double scalar, double offset = 0)
            where D : IDimension, new()
        {
            return new Unit(
                symbol,
                Dimension.ForType<D>(),
                scalar,
                offset);
        }

        public static Unit operator * (Prefix p, Unit u)
        {
            if (u.Offset != 0)
                throw new ArgumentException("Can only combine non-offset units");
            return new Unit(p.symbol + u.Symbol, u.Dimension, p.scalar * u.Scalar);
        }

        // U+22C5 DOT OPERATOR
        public static Unit operator * (Unit u1, Unit u2)
        {
            if (u1.Offset != 0 || u2.Offset != 0)
                throw new ArgumentException("Can only combine non-offset units");
            return new Unit($"{u1.Symbol}\u22C5{u2.Symbol}", u1.Dimension * u2.Dimension, u1.Scalar * u2.Scalar);
        }

        // U+2215 DIVISION OPERATOR
        public static Unit operator / (Unit u1, Unit u2)
        {
            if (u1.Offset != 0 || u2.Offset != 0)
                throw new ArgumentException("Can only combine non-offset units");
            return new Unit($"{u1.Symbol}\u2215{u2.Symbol}", u1.Dimension / u2.Dimension, u1.Scalar / u2.Scalar);
        }
     }

    public static class Units
    {
        public static readonly Unit Meter = Unit.Of<Length>("m", 1);
        public static readonly Unit Millimeter = Milli * Meter;
        public static readonly Unit Centimeter = Centi * Meter;
        public static readonly Unit Kilometer  = Kilo * Meter;
        public static readonly Unit Inch = new Unit("in", 2.54f, Centimeter);
        public static readonly Unit Foot = new Unit("ft", 12, Inch);
        public static readonly Unit Yard = new Unit("yd", 3, Foot);
        public static readonly Unit Mile = new Unit("mi", 1760, Yard);

        public static readonly Unit Second = Unit.Of<Time>("s", 1);
        public static readonly Unit Minute = new Unit("m", 60, Second);
        public static readonly Unit Hour = new Unit("h", 60, Minute);

        // U+00B0 DEGREE SIGN
        public static readonly Unit Kelvin = Unit.Of<Temperature>("K", 1, 0);
        public static readonly Unit Celsius = Unit.Of<Temperature>("\u00B0C", 1, 273.15);
        public static readonly Unit Fahrenheit = Unit.Of<Temperature>("\u00B0F", 5 / 9.0, 273.15 - (32 * 5 / 9.0));
    }
}
