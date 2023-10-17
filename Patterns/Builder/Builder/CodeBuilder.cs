using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder.Builder;

public class CodeBuilder
{
    protected Class currentClass;
    protected Field currentField;
    
    private const int IndentSize = 2;
    private readonly List<Class> classes;

    public CodeBuilder()
    {
        classes = new List<Class>();
    }

    protected CodeBuilder(List<Class> classes, Class currentClass, Field currentField)
    {
        this.classes = classes;
        this.currentClass = currentClass;
        this.currentField = currentField;
    }

    public ClassBuilder AddClass()
    {
        currentClass = new Class();
        classes.Add(currentClass);
        return new ClassBuilder(classes, currentClass);
    }

    public FieldBuilder AddField()
    {
        currentField = new Field();
        currentClass.Fields.Add(currentField);
        return new FieldBuilder(classes, currentClass, currentField);
    }

    public override string ToString() => ToString(0);

    private string ToString(int indent)
    {
        var sb = new StringBuilder();
        
        var spaces = new string(' ', indent * IndentSize);
        foreach (var current in classes)
        {
            sb.AppendLine($"{spaces}{current}");
            sb.Append(spaces);
            sb.AppendLine("{");

            foreach (var field in current.Fields)
            {
                var fieldSpaces = new string(' ', (indent + 1) * IndentSize);
                sb.AppendLine($"{fieldSpaces}{field}");
            }
            
            sb.Append(spaces);
            sb.AppendLine("}\n");
        }

        return sb.ToString();
    }
}

public class ClassBuilder : CodeBuilder
{
    public ClassBuilder(List<Class> classes, Class currentClass) : base(classes, currentClass, null)
    {
    }

    public ClassBuilder SetName(string name)
    {
        currentClass.Name = name;
        return this;
    }

    public ClassBuilder SetAccessModifier(AccessModifier accessModifier)
    {
        currentClass.AccessModifier = accessModifier;
        return this;
    }
}

public class FieldBuilder : CodeBuilder
{
    public FieldBuilder(List<Class> classes, Class currentClass, Field field) : base(classes, currentClass, field)
    {
    }

    public FieldBuilder AddName(string name)
    {
        currentField.Name = name;
        return this;
    }

    public FieldBuilder AddAccessModifier(AccessModifier accessModifier)
    {
        currentField.AccessModifier = accessModifier;
        return this;
    }

    public FieldBuilder AddType(string type)
    {
        currentField.Type = type;
        return this;
    }
}

public enum AccessModifier
{
    Public, Private, Protected, Internal, ProtectedInternal, PrivateProtected
}

public class Class
{
    public AccessModifier AccessModifier;
    public string Name;

    public List<Field> Fields = new();

    public override string ToString() => $"{AccessModifier.ToCustomString()} class {Name}";
}

public class Field
{
    public string Name;
    public string Type;
    public AccessModifier AccessModifier;

    public override string ToString() => $"{AccessModifier.ToCustomString()} {Type} {Name};";
}

public static class AccessModifierExtent
{
    public static string ToCustomString(this AccessModifier modifier) =>
        modifier switch
        {
            AccessModifier.Public => "public",
            AccessModifier.Private => "private",
            AccessModifier.Internal => "internal",
            AccessModifier.Protected => "protected",
            AccessModifier.PrivateProtected => "private protected",
            AccessModifier.ProtectedInternal => "protected internal",
            _ => throw new ArgumentOutOfRangeException(nameof(modifier), modifier, null)
        };
}
