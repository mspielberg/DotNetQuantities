using System;
using System.Collections.Generic;

namespace QuantitiesNet
{
    public class Quantity
    {
        public readonly double scalar;
        public readonly Dimension dimension;

        public Quantity(double scalar, Dimension dimension)
        {
            this.scalar = scalar;
            this.dimension = dimension;
        }

        public Quantity(double scalar, Unit unit)
        {
            this.scalar = (scalar * unit.Scalar) + unit.Offset;
            this.dimension = unit.Dimension;
        }

        public override bool Equals(object obj)
        {
            return obj is Quantity quantity &&
                   scalar == quantity.scalar &&
                   EqualityComparer<Dimension>.Default.Equals(dimension, quantity.dimension);
        }

        public override int GetHashCode()
        {
            int hashCode = -689933752;
            hashCode = hashCode * -1521134295 + scalar.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Dimension>.Default.GetHashCode(dimension);
            return hashCode;
        }

        public static bool operator ==(Quantity q1, Quantity q2)
        {
            if (q1 is null && q2 is null)
                return true;
            if (q1 is null || q2 is null)
                return false;
            return q1.Equals(q2);
        }

        public static bool operator !=(Quantity q1, Quantity q2)
        {
            return !(q1 == q2);
        }

        public double In(Unit unit)
        {
            if (dimension != unit.Dimension)
                throw new ArgumentException($"dimensions do not match: {dimension}, {unit.Dimension}");
            return (scalar - unit.Offset) / unit.Scalar;
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

        public static Quantity<D> Assert<D>(this Quantity q) where D : IDimension, new()
        {
            if (q.dimension != Dimension.ForType<D>())
                throw new ArgumentException($"dimension does not match expectation: {q.dimension}");
            return new Quantity<D>(q.scalar);
        }
    }

    public class Quantity<D> : Quantity, IDimension
        where D : IDimension, new()
    {
        public Quantity() : base (default, Dimension.ForType<D>())
        {
        }

        public Quantity(double scalar) : base(scalar, Dimension.ForType<D>())
        {
        }

        public Quantity(double scalar, Unit unit) : base(scalar, unit)
        {
            if (unit.Dimension != Dimension.ForType<D>())
                throw new ArgumentException($"dimension does not match expectation: {unit.Dimension}");
        }

        public Dimension Dimension => Dimension.ForType<D>();

        public static Quantity<D> operator +(Quantity<D> q, Quantity<D> q2)
        {
            return new Quantity<D>(q.scalar + q2.scalar);
        }

        public static Quantity<D> operator -(Quantity<D> q, Quantity<D> q2)
        {
            return new Quantity<D>(q.scalar - q2.scalar);
        }
    }
}
