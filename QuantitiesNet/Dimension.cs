using System.Linq;
using System.Text;

namespace QuantitiesNet
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

        public static Dimension ForType<D>() where D : IDimension, new() => new D().Dimension;
    }

    public interface IDimension
    {
        Dimension Dimension { get; }
    }
}