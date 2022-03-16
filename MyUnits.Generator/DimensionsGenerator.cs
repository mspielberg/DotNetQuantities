using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace MyUnits.Generator
{
    [Generator]
    public class DimensionsGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var classes = NamedDimensions.dimensions.Keys.Select(GenerateSingle);

            string source = $@"
namespace MyUnits
{{
    namespace Dimensions
    {{
{string.Join("\n", classes)}
    }}
}}
";
            context.AddSource("Dimensions.g.cs", source);
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
                .Append(@" : IDimension
        {
            public static readonly Dimension dimension = ")
                .Append(dim.CodeGen())
                .Append(@";
            public Dimension Dimension => dimension;
");

            foreach (var otherName in NamedDimensions.dimensions.Keys)
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
            public static {productName} operator * ({leftName} lhs, {rightName} rhs) => new {productName}();";
        }

        public string DivideOperator(string leftName, string rightName, string quotientName)
        {
            return $@"
            public static {quotientName} operator / ({leftName} lhs, {rightName} rhs) => new {quotientName}();";
        }
    }
}
