using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitiesNet
{
    public class UnitRegistry
    {
        public static UnitRegistry Default { get; } = new UnitRegistry();

        private readonly Dictionary<Dimension, HashSet<Unit>> units = new Dictionary<Dimension, HashSet<Unit>>();

        public HashSet<Unit> GetUnits(Dimension d) => units[d];
        public HashSet<Unit> GetUnits<D>() where D : IDimension, new() => units[Dimension.ForType<D>()];

        public Unit GetUnit(string symbol) => units.Values.SelectMany(x => x).First(u => u.Symbol == symbol);

        public void Register(Unit u)
        {
            if (!units.TryGetValue(u.Dimension, out var set))
                units[u.Dimension] = set = new HashSet<Unit>();
            set.Add(u);
        }
    }
}