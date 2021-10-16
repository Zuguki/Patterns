using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenClose
{
    static class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            var products = new Product[] {apple, tree, house};

            var colorSpec = new ColorSpecification(Color.Green);
            var sizeSpec = new SizeSpecification(Size.Large);
            var bf = new BetterFilter();
            
            foreach (var product in bf.Filter(products, colorSpec))
            {
                Console.WriteLine($"Product: {product.Name} has green color");
            }

            Console.WriteLine();
            var andSpec = new AndSpecification(colorSpec, sizeSpec);
        
            foreach (var product in bf.Filter(products, andSpec))
            {
                Console.WriteLine($"Product: {product.Name} has green color and large size");
            }
        }
    }

    public enum Color
    {
        Red,
        Green,
        Blue
    };

    public enum Size
    {
        Small,
        Medium,
        Large
    };

    public class Product
    {
        public readonly string Name;
        public readonly Color Color;
        public readonly Size Size;

        public Product(string name, Color color, Size size)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Color = color;
            Size = size;
        }
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public interface ISpecification<T>
    {
        public bool IsSatisfied(T product);
    }
    
    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }

        public bool IsSatisfied(Product product) => product.Color == _color;
    }
    
    public class SizeSpecification : ISpecification<Product>
    {
        private Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public bool IsSatisfied(Product product) => product.Size == _size;
    }
    
    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var item in items)
                if (spec.IsSatisfied(item))
                    yield return item;
        }
    }
    
    public class AndSpecification : ISpecification<Product>
    {
        private readonly ISpecification<Product> _first, _second;

        public AndSpecification(ISpecification<Product> first, ISpecification<Product> second)
        {
            _first = first;
            _second = second;
        }

        public bool IsSatisfied(Product product) => _first.IsSatisfied(product) && _second.IsSatisfied(product);
    }
}