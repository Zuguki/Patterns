using System;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            var circle = new Circle();
            Console.WriteLine(circle.AsString());
            
            var redCircle = new ShapeColor(circle, "red");
            Console.WriteLine(redCircle.AsString());
            
            var redTrCircle = new ShapeTransparency(redCircle, 0.32f);
            Console.WriteLine(redTrCircle.AsString());
        }
    }

    public abstract class Shape
    {
        public virtual string Name { get; }
        
        public virtual string AsString() => string.Empty;
    }

    public sealed class Circle : Shape
    {
        public override string Name { get; } = "Circle";

        public override string AsString()
        {
            return base.AsString() + $" {Name}";
        }
    }

    public class ShapeColor : Shape
    {
        private readonly Shape _shape;
        private readonly string _color;

        public ShapeColor(Shape shape, string color)
        {
            _shape = shape;
            _color = color;
        }

        public override string AsString()
        {
            return _shape.AsString() + $" has a {_color} color";
        }
    }

    public class ShapeTransparency : Shape
    {
        private readonly Shape _shape;
        private readonly float _transparency;

        public ShapeTransparency(Shape shape, float transparency)
        {
            _shape = shape;
            _transparency = transparency;
        }

        public override string AsString()
        {
            return _shape.AsString() + $" has a {_transparency * 100}% transparency";
        }
    }
}