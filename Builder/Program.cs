using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    static class Program
    {
        static void Main(string[] args)
        {
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "Hello!");
            builder.AddChild("li", "How r u?");
            Console.WriteLine(builder.ToString());
        }
    }

    public class HtmlElement
    {
        public string Name;
        public string Text;

        public List<HtmlElement> Elements = new List<HtmlElement>();

        private const int IndentSize = 2;

        public HtmlElement()
        { }

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', IndentSize * indent);
            
            sb.Append($"{i}<{Name}>\n");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', IndentSize * (indent + 1)));
                sb.Append(Text);
                sb.Append('\n');
            }

            foreach (var element in Elements)
                sb.Append(element.ToStringImpl(indent + 1));

            sb.Append($"{i}</{Name}>\n");
            return sb.ToString();
        }
    }

    public class HtmlBuilder
    {
        protected HtmlElement Root = new HtmlElement();
        private readonly string _rootName;

        public HtmlBuilder(string rootName)
        {
            _rootName = rootName;
            Root.Name = rootName;
        }

        public void AddChild(string childName, string childText)
        {
            var element = new HtmlElement(childName, childText);
            Root.Elements.Add(element);
        }

        public override string ToString() => Root.ToString();

        public void Clear() => Root = new HtmlElement{Name = _rootName};

        public HtmlElement Build() => Root;
    }
}