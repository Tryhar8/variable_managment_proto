using System.Globalization;

namespace Core.Models
{
    public delegate void VariableChangeHandler(string identifier, string oldValue, string newValue);

    public class Variable
    {
        private string _currentValue = string.Empty;

        public event VariableChangeHandler? VariableChange;
        public int Id { get; init; }
        public string Identifier { get; init; } = string.Empty;
        public int TypeId { get; init; }
        public TypeDefinition Type { get; init; } = null!;
        public string DefaultValue { get; init; } = string.Empty;
        public string CurrentValue { get => _currentValue; 
                                     set {
                                          
                                          if(IsValidType(Type, value, out string? convertedValue) && _currentValue != convertedValue) 
                                            {
                                                VariableChange?.Invoke(Identifier, _currentValue, value);
                                                _currentValue = value;
                                            }
                                         } 
                                   }

        public void Unsuscribe(VariableChangeHandler handler)
        {
            VariableChange -= handler;
        }

        // drawbacks of such implementation is that user would not get any feedback if it would return false. Approach should be rethinked.
        private static bool IsValidType(TypeDefinition type, object value, out string? convertedValue)
        {
            convertedValue = null;

            if (value == null) return false;

            try
            {
                switch (type.Name.ToLower())
                {
                    case "bool":
                        if (bool.TryParse(value.ToString(), out bool boolResult))
                        {
                            convertedValue = boolResult.ToString();
                            return true;
                        }
                        break;

                    case "int":
                        if (int.TryParse(value.ToString(), out int intResult))
                        {
                            convertedValue = intResult.ToString();
                            return true;
                        }
                        break;

                    case "float":
                    case "double":
                        if (double.TryParse(value.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out double doubleResult))
                        {
                            convertedValue = doubleResult.ToString(CultureInfo.InvariantCulture);
                            return true;
                        }
                        break;

                    case "string":
                        convertedValue = value.ToString();
                        return true;

                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}
