using System;

namespace MyUnits
{
    public readonly struct Dimension
    {
        public readonly int length;
        public readonly int mass;
        public readonly int time;
        public readonly int amountOfSubstance;
        public readonly int temperature;
        public readonly int current;

        public Dimension(
            int length = 0,
            int mass = 0,
            int time = 0,
            int current = 0,
            int amountOfSubstance = 0,
            int temperature = 0)
        {
            this.length = length;
            this.mass = mass;
            this.time = time;
            this.current = current;
            this.amountOfSubstance = amountOfSubstance;
            this.temperature = temperature;
        }

        public static Dimension operator *(Dimension u1, Dimension u2)
        {
            return new Dimension(
                length: u1.length + u2.length,
                mass: u1.mass + u2.mass,
                time: u1.time + u2.time,
                current: u1.current + u2.current,
                amountOfSubstance: u1.amountOfSubstance + u2.amountOfSubstance,
                temperature: u1.temperature + u2.temperature
            );
        }

        public static Dimension operator /(Dimension u1, Dimension u2)
        {
            return new Dimension(
                length: u1.length - u2.length,
                mass: u1.mass - u2.mass,
                time: u1.time - u2.time,
                current: u1.current - u2.current,
                amountOfSubstance: u1.amountOfSubstance - u2.amountOfSubstance,
                temperature: u1.temperature - u2.temperature
            );
        }

        public static readonly Dimension Length = new Dimension(length: 1);
        public static readonly Dimension Area = Length * Length;
        public static readonly Dimension Volume = Area * Length;

        public static readonly Dimension Mass = new Dimension(mass: 1);
        public static readonly Dimension Density = Mass / Volume;

        public static readonly Dimension Duration = new Dimension(time: 1);
        public static readonly Dimension Speed = Length / Duration;
        public static readonly Dimension Acceleration = Speed / Duration;

        public static readonly Dimension Temperature = new Dimension(temperature: 1);

        public static readonly Dimension Force = Mass * Acceleration;
        public static readonly Dimension Energy = Force * Length;
        public static readonly Dimension Power = Energy / Duration;
        public static readonly Dimension SpecificEnergy = Energy / Mass;
        public static readonly Dimension SpecificEntropy = Energy / Mass / Temperature;

        public static readonly Dimension Pressure = Force / Area;

        public static readonly Dimension AmountOfSubstance = new Dimension(amountOfSubstance: 1);
        public static readonly Dimension MolarMass = Mass / AmountOfSubstance;
        public static readonly Dimension MolarEnergy = Energy / AmountOfSubstance;
        public static readonly Dimension MolarEntropy = Energy / AmountOfSubstance / Temperature;

        public override bool Equals(object obj)
        {
            return obj is Dimension units &&
                   length == units.length &&
                   mass == units.mass &&
                   time == units.time &&
                   amountOfSubstance == units.amountOfSubstance &&
                   temperature == units.temperature &&
                   current == units.current;
        }

        public override int GetHashCode()
        {
            int hashCode = 601282475;
            hashCode = (hashCode * -1521134295) + length.GetHashCode();
            hashCode = (hashCode * -1521134295) + mass.GetHashCode();
            hashCode = (hashCode * -1521134295) + time.GetHashCode();
            hashCode = (hashCode * -1521134295) + amountOfSubstance.GetHashCode();
            hashCode = (hashCode * -1521134295) + temperature.GetHashCode();
            hashCode = (hashCode * -1521134295) + current.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Dimension u1, Dimension u2) => u1.Equals(u2);
        public static bool operator !=(Dimension u1, Dimension u2) => !u1.Equals(u2);
    }

    public struct Quantity
    {
        public readonly double scalar;
        public readonly Dimension dimension;

        public Quantity(double scalar, Dimension dimension)
        {
            this.scalar = scalar;
            this.dimension = dimension;
        }

        public static Quantity operator +(Quantity q1, Quantity q2)
        {
            if (q1.dimension != q2.dimension)
                throw new ArgumentException($"dimensions do not match: {q1.dimension}, {q2.dimension}");
            return new Quantity(q1.scalar + q2.scalar, q1.dimension);
        }

        public static Quantity operator -(Quantity q1, Quantity q2)
        {
            if (q1.dimension != q2.dimension)
                throw new ArgumentException($"dimensions do not match: {q1.dimension}, {q2.dimension}");
            return new Quantity(q1.scalar - q2.scalar, q1.dimension);
        }

        public static Quantity operator *(Quantity q1, Quantity q2)
        {
            return new Quantity(q1.scalar * q2.scalar, q1.dimension * q2.dimension);
        }

        public static Quantity operator /(Quantity q1, Quantity q2)
        {
            return new Quantity(q1.scalar / q2.scalar, q1.dimension / q2.dimension);
        }
    }

    public static class QuantityExtensions
    {
        public static Quantity Assert(this Quantity q, Dimension expected)
        {
            if (q.dimension != expected)
                throw new ArgumentException($"dimension does not match expectation: {q.dimension}");
            return q;
        }
    }
}
