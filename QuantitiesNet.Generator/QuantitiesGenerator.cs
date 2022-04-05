using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace QuantitiesNet.Generator
{
    [Generator]
    public class QuantitiesGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var classes = NamedDimensions.dimensions.Keys.Select(GenerateSingle);

            string source = $@"
using System;
using System.Collections.Generic;

namespace QuantitiesNet
{{
    public static class Quantities
    {{
        public static string GetName(Dimension d)
        {{
            if (TypeForDimension.TryGetValue(d, out var dimensionType))
                return dimensionType.Name;
            return null;
        }}
{GenerateLookup()}
{string.Join("\n", classes)}
    }}
}}
";
            context.AddSource("Quantities.g.cs", source);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public string GenerateSingle(string name)
        {
            var dim = NamedDimensions.dimensions[name];

            var sb = new StringBuilder();
            sb
                .Append(@"
        public sealed class ")
                .Append(name)
                .Append(" : Quantity<Dimensions.")
                .Append(name)
                .Append(@">
        {
            public ")
                .Append(name)
                .Append(@"() : base() { }
            public ")
                .Append(name)
                .Append(@"(double scalar) : base(scalar) { }
            public ")
                .Append(name)
                .Append(@"(double scalar, Unit unit) : base(scalar, unit) { }
");

            foreach (var otherName in NamedDimensions.dimensions.Keys.OrderBy(x => x))
            {
                var otherDim = NamedDimensions.dimensions[otherName];
                var productDim = dim * otherDim;
                var quotientDim = dim / otherDim;
                if (NamedDimensions.names.TryGetValue(productDim, out var productName))
                    sb.Append(MultiplyOperator(name, otherName, productName));
                else if (NamedDimensions.names.TryGetValue(quotientDim, out var quotientName))
                    sb.Append(DivideOperator(name, otherName, quotientName));
            }

            sb.Append("\n        }");
            return sb.ToString();
        }

        public string MultiplyOperator(string leftName, string rightName, string productName)
        {
            return $@"
            public static {productName} operator * ({leftName} lhs, {rightName} rhs) => new {productName}(lhs.scalar * rhs.scalar);";
        }

        public string DivideOperator(string leftName, string rightName, string quotientName)
        {
            return $@"
            public static {quotientName} operator / ({leftName} lhs, {rightName} rhs) => new {quotientName}(lhs.scalar / rhs.scalar);";
        }

        private string GenerateLookup()
        {
            var sb = new StringBuilder();
            sb.Append(@"
        public static readonly Dictionary<Dimension, Type> TypeForDimension = new Dictionary<Dimension, Type>()
        {
");
            foreach (var name in NamedDimensions.dimensions.Keys)
                sb.Append("            { Dimension.ForType<").Append(name).Append(">(), typeof(").Append(name).AppendLine(") },");
            sb.Append(@"
        };");
            return sb.ToString();
        }
    }
}
