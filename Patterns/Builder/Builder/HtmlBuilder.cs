using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder.Builder;

public class HtmlBuilder
{
    private readonly HtmlElement rootElement;

    public HtmlBuilder(HtmlElement rootElement)
    {
        this.rootElement = rootElement;
    }

    public HtmlBuilder AddChild(string elementName, string elementText = null)
    {
        rootElement.Children.Add(new HtmlElement(elementName, elementText));
        return this;
    }

    public HtmlElement Build() => rootElement;

    public override string ToString() => rootElement.ToString();
}

public class HtmlElement
{
    public readonly List<HtmlElement> Children;
    private const int IndentSize = 2;
    
    private readonly string elementName;
    private readonly string elementText;

    public HtmlElement(string elementName, string elementText = null)
    {
        this.elementName = elementName;
        this.elementText = elementText;
        Children = new List<HtmlElement>();
    }

    public override string ToString() => ToString(0);

    private string ToString(int indentLvl)
    {
        var sb = new StringBuilder();
        var indent = new string(' ', indentLvl * IndentSize);
        sb.AppendLine($"{indent}<{elementName}>");
        if (elementText is not null)
            sb.AppendLine($"{indent + ' '}{elementText}");
        
        if (Children.Count > 0)
        {
            foreach (var child in Children)
                sb.Append(child.ToString(indentLvl + 1));
        }

        sb.AppendLine($"{indent}</{elementName}>");

        return sb.ToString();
    }
}
