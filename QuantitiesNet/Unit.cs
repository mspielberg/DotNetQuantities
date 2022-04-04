using System;
using QuantitiesNet.Dimensions;
using static QuantitiesNet.Prefixes;

namespace QuantitiesNet
{
    public readonly struct Unit : IDimension
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

        public Unit WithSymbol(string symbol)
        {
            return new Unit(symbol, Dimension, Scalar, Offset);
        }

        public override string ToString()
        {
            return $"{Symbol} ({Dimension})";
        }

        public static Unit operator * (Prefix p, Unit baseUnit)
        {
            if (baseUnit.Offset != 0)
                throw new ArgumentException("Can only combine non-offset units");
            return new Unit(p.symbol + baseUnit.Symbol, baseUnit.Dimension, p.scalar * baseUnit.Scalar);
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
        // static constructor ensures that all static fields are initialized before Initialize() completes.
        static Units() { }
        public static void Initialize() { }

        private static Unit Register(Unit u)
        {
            UnitRegistry.Default.Add(u);
            return u;
        }

        // Length
        public static readonly Unit Meter = Register(Unit.Of<Length>("m", 1));
        public static readonly Unit Millimeter = Register(Milli * Meter);
        public static readonly Unit Centimeter = Register(Centi * Meter);
        public static readonly Unit Kilometer  = Register(Kilo * Meter);
        public static readonly Unit Inch = Register(new Unit("in", 2.54f, Centimeter));
        public static readonly Unit Foot = Register(new Unit("ft", 12, Inch));
        public static readonly Unit Yard = Register(new Unit("yd", 3, Foot));
        public static readonly Unit Mile = Register(new Unit("mi", 1760, Yard));

        // Mass
        public static readonly Unit Gram = Register(Unit.Of<Mass>("g", 1));
        public static readonly Unit Milligram = Register(Milli * Gram);
        public static readonly Unit Kilogram = Register(Kilo * Gram);
        public static readonly Unit Pound = Register(new Unit("lb", 453.59237, Gram));

        // Time
        public static readonly Unit Second = Register(Unit.Of<Time>("s", 1));
        public static readonly Unit Minute = Register(new Unit("m", 60, Second));
        public static readonly Unit Hour = Register(new Unit("h", 60, Minute));

        // Temperature
        // U+00B0 DEGREE SIGN
        public static readonly Unit Kelvin = Register(Unit.Of<Temperature>("K", 1, 0));
        public static readonly Unit Celsius = Register(Unit.Of<Temperature>("\u00B0C", 1, 273.15));
        public static readonly Unit Fahrenheit = Register(Unit.Of<Temperature>("\u00B0F", 5 / 9.0, 273.15 - (32 * 5 / 9.0)));

        // Energy
        public static readonly Unit Joule = Register(Unit.Of<Energy>("J", 1));

        // Force
        public static readonly Unit Newton = Register((Kilogram * Meter / Second / Second).WithSymbol("N"));

        // Power
        public static readonly Unit Watt = Register((Joule / Second).WithSymbol("W"));
        public static readonly Unit MechanicalHorsepower = Register(new Unit("hp", 550, Pound * Foot / Second));

        // Velocity
        public static readonly Unit KilometerPerHour = Register((Kilometer / Hour));
        public static readonly Unit MilePerHour = Register((Mile / Hour).WithSymbol("mph"));
    }
}
