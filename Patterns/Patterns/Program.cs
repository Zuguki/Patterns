using System;
using System.Collections.Generic;
using System.Linq;

var redSpec = new ColorSpecification(Color.Red);
var mediumSpec = new SizeSpecification(Size.Medium);
var filter = new ProductFilter();

var products = new[]
{
    new Product("iPhone", Color.Green, Size.Medium),
    new Product("iPhone", Color.Red, Size.Medium),
    new Product("iPhone", Color.Blue, Size.Medium),
    new Product("iMac", Color.Green, Size.Large),
    new Product("iMac", Color.Red, Size.Large),
    new Product("iMac", Color.Blue, Size.Large),
};

Console.WriteLine("All Products: ");
foreach (var product in products)
    Console.WriteLine($" - {product}");

Console.WriteLine("Red Products: ");
foreach (var product in filter.Filter(products, redSpec))
    Console.WriteLine($"- {product}");

Console.WriteLine("Red, Medium Products:");
foreach (var product in filter.Filter(products, redSpec, mediumSpec))
    Console.WriteLine($"- {product}");

public interface IFilter<T>
{
    public IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);

    public IEnumerable<T> Filter(IEnumerable<T> items, params ISpecification<T>[] specifications);
}

public interface ISpecification<in T>
{
    public bool MakeSpecification(T item);
}

public class ProductFilter : IFilter<Product>
{
    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification) => 
        items.Where(specification.MakeSpecification);

    public IEnumerable<Product> Filter(IEnumerable<Product> items, params ISpecification<Product>[] specifications) =>
        items.Where(prod => specifications.All(spec => spec.MakeSpecification(prod)));
}

public class ColorSpecification : ISpecification<Product>
{
    private readonly Color _color;

    public ColorSpecification(Color color)
    {
        _color = color;
    }

    public bool MakeSpecification(Product item) => item.Color == _color;
}

public class SizeSpecification : ISpecification<Product>
{
    private readonly Size _size;

    public SizeSpecification(Size size)
    {
        _size = size;
    }

    public bool MakeSpecification(Product item) => item.Size == _size;
}

public class Product
{
    public Product(string name, Color color, Size size)
    {
        Name = name;
        Color = color;
        Size = size;
    }
    
    public string Name { get; set; }
    public Color Color { get; set; }
    public Size Size { get; set; }

    public override string ToString() => $"{Size} {Name} is {Color}";
}

public enum Size
{
    Small, Medium, Large
}

public enum Color
{
    Red, Green, Blue
}

