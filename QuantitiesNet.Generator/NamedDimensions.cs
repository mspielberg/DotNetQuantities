using System.Collections.Generic;
using System.Text;

namespace QuantitiesNet.Generator
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

        public string CodeGen()
        {
            var sb = new StringBuilder();
            if (length != 0)
              sb.Append("length: ").Append(length).Append(", ");
            if (mass != 0)
              sb.Append("mass: ").Append(mass).Append(", ");
            if (time != 0)
              sb.Append("time: ").Append(time).Append(", ");
            if (current != 0)
              sb.Append("current: ").Append(current).Append(", ");
            if (amountOfSubstance != 0)
              sb.Append("amountOfSubstance: ").Append(amountOfSubstance).Append(", ");
            if (temperature != 0)
              sb.Append("temperature: ").Append(temperature).Append(", ");
            sb.Remove(sb.Length - 2, 2);
            return $"new Dimension({sb})";
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
    }

    public static class NamedDimensions
    {
        public static readonly Dictionary<string, Dimension> dimensions = new Dictionary<string, Dimension>();
        public static readonly Dictionary<Dimension, string> names = new Dictionary<Dimension, string>();

        static NamedDimensions()
        {
            var d = dimensions;

            d["Length"] = new Dimension(length: 1);
            d["Area"] = d["Length"] * d["Length"];
            d["Volume"] = d["Area"] * d["Length"];

            d["Mass"] = new Dimension(mass: 1);
            d["Density"] = d["Mass"] / d["Volume"];

            d["Time"] = new Dimension(time: 1);
            d["Velocity"] = d["Length"] / d["Time"];
            d["Acceleration"] = d["Velocity"] / d["Time"];

            d["Temperature"] = new Dimension(temperature: 1);

            d["Force"] = d["Mass"] * d["Acceleration"];
            d["Pressure"] = d["Force"] / d["Area"];
            d["Energy"] = d["Force"] * d["Length"];
            d["Power"] = d["Energy"] / d["Time"];
            d["SpecificEnergy"] = d["Energy"] / d["Mass"];
            d["SpecificHeatCapacity"] = d["SpecificEnergy"] / d["Temperature"];
            d["AmountOfSubstance"] = new Dimension(amountOfSubstance: 1);
            d["MolarMass"] = d["Mass"] / d["AmountOfSubstance"];
            d["MolarEnergy"] = d["Energy"] / d["AmountOfSubstance"];
            d["MolarHeatCapacity"] = d["MolarEnergy"] / d["Temperature"];

            foreach (var kvp in d)
                names[kvp.Value] = kvp.Key;
        }
    }
}