using System;

namespace MyUnits
{
    public struct Units
    {
        public readonly int length;
        public readonly int mass;
        public readonly int time;
        public readonly int amountOfSubstance;
        public readonly int temperature;
        public readonly int current;

        public Units(
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

        public static Units operator *(Units u1, Units u2)
        {
            return new Units(
                length: u1.length + u2.length,
                mass: u1.mass + u2.mass,
                time: u1.time + u2.time,
                current: u1.current + u2.current,
                amountOfSubstance: u1.amountOfSubstance + u2.amountOfSubstance,
                temperature: u1.temperature + u2.temperature
            );
        }

        public static Units operator /(Units u1, Units u2)
        {
            return new Units(
                length: u1.length - u2.length,
                mass: u1.mass - u2.mass,
                time: u1.time - u2.time,
                current: u1.current - u2.current,
                amountOfSubstance: u1.amountOfSubstance - u2.amountOfSubstance,
                temperature: u1.temperature - u2.temperature
            );
        }

        public static readonly Units Length = new Units(length: 1);
        public static readonly Units Area = Length * Length;
        public static readonly Units Volume = Area * Length;

        public static readonly Units Mass = new Units(mass: 1);
        public static readonly Units Density = Mass / Volume;

        public static readonly Units Duration = new Units(time: 1);
        public static readonly Units Speed = Length / Duration;
        public static readonly Units Acceleration = Speed / Duration;

        public static readonly Units Temperature = new Units(temperature: 1);

        public static readonly Units Force = Mass * Acceleration;
        public static readonly Units Energy = Force * Length;
        public static readonly Units Power = Energy / Duration;
        public static readonly Units SpecificEnergy = Energy / Mass;
        public static readonly Units SpecificEntropy = Energy / Mass / Temperature;

        public static readonly Units Pressure = Force / Area;

        public static readonly Units AmountOfSubstance = new Units(amountOfSubstance: 1);
        public static readonly Units MolarMass = Mass / AmountOfSubstance;
        public static readonly Units MolarEnergy = Energy / AmountOfSubstance;
        public static readonly Units MolarEntropy = Energy / AmountOfSubstance / Temperature;

        public override bool Equals(object obj)
        {
            return obj is Units units &&
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

        public static bool operator ==(Units u1, Units u2) => u1.Equals(u2);
        public static bool operator !=(Units u1, Units u2) => !u1.Equals(u2);
    }

    public struct Quantity
    {
        public readonly double scalar;
        public readonly Units units;

        public Quantity(double scalar, Units units)
        {
            this.scalar = scalar;
            this.units = units;
        }

        public static Quantity operator +(Quantity q1, Quantity q2)
        {
            if (q1.units != q2.units)
                throw new ArgumentException($"units do not match: {q1.units}, {q2.units}");
            return new Quantity(q1.scalar + q2.scalar, q1.units);
        }

        public static Quantity operator -(Quantity q1, Quantity q2)
        {
            if (q1.units != q2.units)
                throw new ArgumentException($"units do not match: {q1.units}, {q2.units}");
            return new Quantity(q1.scalar - q2.scalar, q1.units);
        }

        public static Quantity operator *(Quantity q1, Quantity q2)
        {
            return new Quantity(q1.scalar * q2.scalar, q1.units * q2.units);
        }

        public static Quantity operator /(Quantity q1, Quantity q2)
        {
            return new Quantity(q1.scalar / q2.scalar, q1.units / q2.units);
        }
    }

    public static class QuantityExtensions
    {
        public static Quantity Assert(this Quantity q, Units expected)
        {
            if (q.units != expected)
                throw new ArgumentException($"units do not match expectation: {q.units}");
            return q;
        }
    }
}
