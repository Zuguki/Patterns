using System;
using Patterns.Builder;

var builder = new HtmlBuilder(new HtmlElement("ul"))
    .AddChild("li", "Hello")
    .AddChild("li", "World")
    .ToString();

Console.WriteLine(builder);
