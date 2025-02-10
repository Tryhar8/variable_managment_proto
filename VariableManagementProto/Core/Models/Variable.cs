namespace Core.Models
{
    public delegate void VariableChangeHandler(string identifier, string oldValue, string newValue);

    public class Variable
    {
        private string _value = string.Empty;

        public event VariableChangeHandler VariableChange;
        public int Id { get; init; }
        public string Identifier { get; init; } = string.Empty;
        public int TypeId { get; init; }
        public TypeDefinition Type { get; init; } = null!;
        public string DefaultValue { get; init; } = string.Empty;
        public string CurrentValue { get => _value; 
                                     set {
                                          if(_value != value) 
                                            {
                                                VariableChange?.Invoke(Identifier, _value, value);
                                                _value = value;
                                            }
                                         } 
                                   }

        public void Unsuscribe(VariableChangeHandler handler)
        {
            VariableChange -= handler;
        }
    }
}
