namespace Core
{
    public class Variable
    {
        public int Id { get; init; }
        public string Identifier { get; init; } = string.Empty;
        public int TypeId { get; init; }
        public Type Type { get; init; } = null!;
        public string DefaultValue { get; init; } = string.Empty;
        public string CurrentValue {  get; init; } = string.Empty;
    }
}
