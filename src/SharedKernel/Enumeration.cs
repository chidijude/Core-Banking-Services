using System.Reflection;

namespace SharedKernel;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>> 
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> _enumerations = CreateEnumerations();

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enuerationType = typeof(TEnum);

        var fieldsForType = enuerationType
            .GetFields(
                BindingFlags.Public | 
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => 
                enuerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => 
                (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Id);
    }

    public static IReadOnlyCollection<TEnum> GetValues()
    {
        return _enumerations.Values.ToList();
    }

    public int Id { get; protected init; }
    public string Name { get; protected init; } = string.Empty;

    protected Enumeration(int value, string name)
    {
        Id = value;
        Name = name;
    }
    public static TEnum? FromValue(int value)
    {
        return _enumerations.TryGetValue(value, out TEnum? enumeration)? enumeration : default;
    }

    public static TEnum? FromName(string name)
    {
        return _enumerations.Values.SingleOrDefault(e => e.Name == name);        
    }

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }
        return GetType() == other.GetType() && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }
        
        return obj is Enumeration<TEnum> other && Equals(other);
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }
}
