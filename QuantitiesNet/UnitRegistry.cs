using System.Collections.Generic;

namespace QuantitiesNet
{
    public class UnitRegistry
    {
        public static UnitRegistry Default { get; } = new UnitRegistry();

        private readonly Dictionary<Dimension, HashSet<Unit>> units = new Dictionary<Dimension, HashSet<Unit>>();
        private readonly Dictionary<Dimension, Unit> preferredUnits = new Dictionary<Dimension, Unit>();

        public bool TryGetUnits(Dimension d, out HashSet<Unit> result) => units.TryGetValue(d, out result);
        public bool TryGetUnits<D>(out HashSet<Unit> result) where D : IDimension, new() =>
            TryGetUnits(Dimension.ForType<D>(), out result);

        public bool TryGetPreferredUnit(Dimension d, out Unit result) => preferredUnits.TryGetValue(d, out result);
        public bool TryGetPreferredUnit<D>(out Unit result) where D : IDimension, new() =>
            TryGetPreferredUnit(Dimension.ForType<D>(), out result);

        public void SetPreferredUnit(Unit unit) => preferredUnits[unit.Dimension] = unit;

        public void Add(Unit u)
        {
            if (!units.TryGetValue(u.Dimension, out var set))
                units[u.Dimension] = set = new HashSet<Unit>();
            set.Add(u);
        }
    }
}