using System;
using System.Collections.Generic;
using System.Text;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            var drawing = new GraphicObject {Name = "Main Group"};
            drawing.Children.Add(new Circle("Red"));
            drawing.Children.Add(new Square("Green"));

            var group1 = new GraphicObject {Name = "Group1"};
            group1.Children.Add(new Circle("Blue"));
            group1.Children.Add(new Square("Pink"));

            var group2 = new GraphicObject {Name = "Group2"};
            group2.Children.Add(new Circle("Purple"));
            group2.Children.Add(new Square("Black"));
            
            group1.Children.Add(group2);
            drawing.Children.Add(group1);

            Console.WriteLine(drawing);
        }
    }

    public class GraphicObject
    {
        public virtual string Name { get; set; } = "Group";
        public string Color { get; set; }

        private Lazy<List<GraphicObject>> _children = new Lazy<List<GraphicObject>>(
            () => new List<GraphicObject>());

        public GraphicObject(string color = null)
        {
            Color = color;
        }

        public List<GraphicObject> Children => _children.Value;

        public override string ToString()
        {
            var sb = new StringBuilder();
            Print(sb, 0);
            return sb.ToString();
        }

        private void Print(StringBuilder sb, int depth)
        {
            sb.Append(new string('*', depth))
                .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $" {Color}")
                .AppendLine($" {Name}");

            Children.ForEach(child => child.Print(sb, depth + 1));
        }
    }

    public class Circle : GraphicObject
    {
        public Circle(string color) : base(color)
        {
        }
        
        public override string Name { get; set; } = "Circle";
    }
    
    public class Square : GraphicObject
    {
        public Square(string color) : base(color)
        {
        }
        
        public override string Name { get; set; } = "Square";
    }
}