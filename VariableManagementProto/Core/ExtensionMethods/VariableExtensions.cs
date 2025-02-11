using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.ExtensionMethods
{
    public static class VariableExtensions
    {
        public static object ConvertTo(this Variable variable)
        {
            ArgumentNullException.ThrowIfNull(variable);
            ArgumentNullException.ThrowIfNull(variable.Type);

            string typeName = variable.Type.Name?.ToLower()!;
            
            ArgumentNullException.ThrowIfNull(typeName);

            // @todo: this implementation works just for CurrentValue but what is with default value. This has to be taken in consideration. When do we fallback to default value?
            // Do we fallback just in case if current value is null?
            string value = variable.CurrentValue;

            return typeName switch
            {
                "string" => variable.CurrentValue,
                "bool" => bool.TryParse(value, out bool boolResult) ? boolResult
                          : throw new FormatException($"Cannot convert '{value}' to Boolean."),
                "int" => int.TryParse(value, out int intResult) ? intResult
                          : throw new FormatException($"Cannot convert '{value}' to Integer."),
                "float" => float.TryParse(value, out float floatResult) ? floatResult
                              : throw new FormatException($"Cannot convert '{value}' to Float."),
                "double" => double.TryParse(value, out double doubleResult) ? doubleResult
                              : throw new FormatException($"Cannot convert '{value}' to Double."),
                _ => throw new NotSupportedException($"Conversion for type '{variable.Type.Name}' is not supported.")
            };
        }
    }
}
