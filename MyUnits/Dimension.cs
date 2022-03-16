using System.Text;

namespace MyUnits
{
    public class Dimension
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (length != 0)
              sb.Append("L^").Append(length);
            if (mass != 0)
              sb.Append("M^").Append(mass);
            if (time != 0)
              sb.Append("T^").Append(time);
            if (current != 0)
              sb.Append("I^").Append(current);
            if (amountOfSubstance != 0)
              sb.Append("N^").Append(amountOfSubstance);
            if (temperature != 0)
              sb.Append("Θ^").Append(temperature);
            return sb.ToString();
        }

        public static readonly Dimension Length = new Dimension(length: 1);
        public static readonly Dimension Area = Length * Length;
        public static readonly Dimension Volume = Area * Length;

        public static readonly Dimension Mass = new Dimension(mass: 1);
        public static readonly Dimension Density = Mass / Volume;

        public static readonly Dimension Time = new Dimension(time: 1);
        public static readonly Dimension Speed = Length / Time;
        public static readonly Dimension Acceleration = Speed / Time;

        public static readonly Dimension Temperature = new Dimension(temperature: 1);

        public static readonly Dimension Force = Mass * Acceleration;
        public static readonly Dimension Energy = Force * Length;
        public static readonly Dimension Power = Energy / Time;
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

    public interface IDimension
    {
        Dimension Dimension { get; }
    }

    namespace Dimensions
    {
        public sealed class Length : IDimension
        {
            public Dimension Dimension => Dimension.Length;
        }
        public sealed class Area : IDimension
        {
            public Dimension Dimension => Dimension.Area;
        }
        public sealed class Volume : IDimension
        {
            public Dimension Dimension => Dimension.Volume;
        }
        public sealed class Mass : IDimension
        {
            public Dimension Dimension => Dimension.Mass;
        }
        public sealed class Density : IDimension
        {
            public Dimension Dimension => Dimension.Density;
        }
        public sealed class Time : IDimension
        {
            public Dimension Dimension => Dimension.Time;
        }
        public sealed class Speed : IDimension
        {
            public Dimension Dimension => Dimension.Speed;
        }
        public sealed class Acceleration : IDimension
        {
            public Dimension Dimension => Dimension.Acceleration;
        }
        public sealed class Temperature : IDimension
        {
            public Dimension Dimension => Dimension.Temperature;
        }
        public sealed class Force : IDimension
        {
            public Dimension Dimension => Dimension.Force;
        }
        public sealed class Energy : IDimension
        {
            public Dimension Dimension => Dimension.Energy;
        }
        public sealed class Power : IDimension
        {
            public Dimension Dimension => Dimension.Power;
        }
        public sealed class SpecificEnergy : IDimension
        {
            public Dimension Dimension => Dimension.SpecificEnergy;
        }
        public sealed class SpecificEntropy : IDimension
        {
            public Dimension Dimension => Dimension.SpecificEntropy;
        }
        public sealed class Pressure : IDimension
        {
            public Dimension Dimension => Dimension.Pressure;
        }
        public sealed class AmountOfSubstance : IDimension
        {
            public Dimension Dimension => Dimension.AmountOfSubstance;
        }
        public sealed class MolarMass : IDimension
        {
            public Dimension Dimension => Dimension.MolarMass;
        }
        public sealed class MolarEnergy : IDimension
        {
            public Dimension Dimension => Dimension.MolarEnergy;
        }
        public sealed class MolarEntropy : IDimension
        {
            public Dimension Dimension => Dimension.MolarEntropy;
        }
    }
}