using System.Globalization;

namespace Core.Models
{
    public delegate void VariableChangeHandler(string identifier, string oldValue, string newValue);

    public class Variable
    {
        public Variable(string identifier, int TypeId, string defaultValue)
        {
            this.Identifier = identifier;
            this.TypeId = TypeId;
            this.DefaultValue = defaultValue;
        }

        public Variable(string identifier, int typeId, string defaultValue, string currentValue) : this(identifier, typeId, defaultValue)
        {
            CurrentValue = currentValue;
        }

        public Variable(int id, string identifier, int typeId, TypeDefinition type, string defaultValue, string currentValue) :
            this(identifier, typeId, defaultValue, currentValue)
        {
            Id = id;
            Type = type;
        }

        private string _currentValue = string.Empty;

        public event VariableChangeHandler? VariableChange;
        public int Id { get; init; }
        public string Identifier { get; init; } = string.Empty;
        public int TypeId { get; init; }
        public TypeDefinition Type { get; init; } = null!;
        public string DefaultValue { get; init; } = string.Empty;
        public string CurrentValue { get => _currentValue; 
                                     private set {
                                                    if(IsValidType(Type, value, out string? convertedValue)) 
                                                      {
                                                          if(_currentValue != convertedValue) 
                                                          { 
                                                              VariableChange?.Invoke(Identifier, _currentValue, value);
                                                              _currentValue = value;
                                                          }
                                                      }
                                                    else throw new ArgumentException(value, nameof(value));
                                                 } 
                                   }

        public void Unsuscribe(VariableChangeHandler handler)
        {
            VariableChange -= handler;
        }

        private static bool IsValidType(TypeDefinition type, string value, out string? convertedValue)
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

        public static implicit operator bool(Variable variable)
        {
            if (variable.Type.Name.ToLower() != "bool")
            {
                throw new InvalidCastException("Cannot convert non-boolean Variable to bool.");
            }

            if (bool.TryParse(variable.CurrentValue, out bool result))
            {
                return result;
            }

            throw new InvalidCastException("Failed to convert Variable's CurrentValue to bool.");
        }

        public static implicit operator int(Variable variable)
        {
            if (variable.Type.Name.ToLower() != "int")
            {
                throw new InvalidCastException("Cannot convert non-integer Variable to int.");
            }

            if (int.TryParse(variable.CurrentValue, out int result))
            {
                return result;
            }

            throw new InvalidCastException("Failed to convert Variable's CurrentValue to int.");
        }

        public static implicit operator double(Variable variable)
        {
            if (variable.Type.Name.ToLower() != "double")
            {
                throw new InvalidCastException("Cannot convert non-double Variable to double.");
            }

            if (double.TryParse(variable.CurrentValue, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }

            throw new InvalidCastException("Failed to convert Variable's CurrentValue to double.");
        }

        public static implicit operator string(Variable variable)
        {
            if (variable.Type.Name.ToLower() != "string")
            {
                throw new InvalidCastException("Variable is not of string type.");
            }

            return variable.CurrentValue;
        }

        public void SetValue<T>(T value)
        {
            if (value is bool b)
            {
                if (Type.Name.ToLower() != "bool") throw new ArgumentException("Cannot assign bool to non-bool Variable.");
                CurrentValue = b.ToString();
            }
            else if (value is int i)
            {
                if (Type.Name.ToLower() != "int") throw new ArgumentException("Cannot assign int to non-int Variable.");
                CurrentValue = i.ToString();
            }
            else if (value is double d)
            {
                if (Type.Name.ToLower() != "double") throw new ArgumentException("Cannot assign double to non-double Variable.");
                CurrentValue = d.ToString(CultureInfo.InvariantCulture);
            }
            else if (value is string s)
            {
                if (Type.Name.ToLower() != "string") throw new ArgumentException("Cannot assign string to non-string Variable.");
                CurrentValue = s.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                throw new InvalidOperationException("Unsupported type.");
            }
        }
    }
}
