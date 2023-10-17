using System.Text;

namespace Composite;

public class Graphic
{
    public virtual string Name { get; set; } = "Group";
    public string? Color = null;

    private readonly Lazy<List<Graphic>> _children = new();
    public List<Graphic> Children => _children.Value;

    public override string ToString()
    {
        var sb = new StringBuilder();
        Print(sb, 0);
        return sb.ToString();
    }

    private void Print(StringBuilder stringBuilder, int depth)
    {
        stringBuilder.Append(new string('*', depth))
            .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
            .AppendLine($"{Name}");
        
        foreach (var child in Children)
            child.Print(stringBuilder, depth + 1);
    }
}

public class Circle : Graphic
{
    public override string Name { get; set; } = "Circle";
}

public class Square : Graphic
{
    public override string Name { get; set; } = "Square";
}