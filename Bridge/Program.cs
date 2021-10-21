using System;

namespace Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            var vectorRend = new VectorRender();
            var rasterRend = new RaserRender();
            
            var circle1 = new Circle(vectorRend, 5f);
            var circle2 = new Circle(rasterRend, 7f);

            var square1 = new Square(vectorRend, 5f);
            var square2 = new Square(rasterRend, 7f);

            circle1.Draw();
            circle1.Resize(2);
            circle1.Draw();
            
            circle2.Draw();

            square1.Draw();
            
            square2.Draw();
            square2.Resize(2);
            square2.Draw();
        }
    }

    public abstract class Shape
    {
        public Shape(IRenderer renderer)
        {
            Renderer = renderer;
        }
        
        protected readonly IRenderer Renderer;

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape
    {
        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            _radius = radius;
        }

        private float _radius;

        public override void Draw()
        {
            Renderer.RendererCircle(_radius);
        }

        public override void Resize(float factor)
        {
            _radius *= factor;
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer, float side) : base(renderer)
        {
            _side = side;
        }

        private float _side;
        
        public override void Draw()
        {
            Renderer.RendererSquare(_side);
        }

        public override void Resize(float factor)
        {
            _side *= factor;
        }
    }

    public interface IRenderer
    {
        public void RendererCircle(float radius);

        public void RendererSquare(float side);
    }

    public class VectorRender : IRenderer
    {
        public void RendererCircle(float radius)
        {
            Console.WriteLine($"Drawing circle with radius: {radius}");
        }

        public void RendererSquare(float side)
        {
            Console.WriteLine($"Drawing square with side: {side}");
        }
    }

    public class RaserRender : IRenderer
    {
        public void RendererCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle with radius: {radius}");
        }

        public void RendererSquare(float side)
        {
            Console.WriteLine($"Drawing pixels for square with side: {side}");
        }
    }
}