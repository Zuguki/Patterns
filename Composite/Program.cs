using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            //var drawing = new GraphicObject {Name = "Main Group"};
            //drawing.Children.Add(new Circle("Red"));
            //drawing.Children.Add(new Square("Green"));
//
            //var group1 = new GraphicObject {Name = "Group1"};
            //group1.Children.Add(new Circle("Blue"));
            //group1.Children.Add(new Square("Pink"));
//
            //var group2 = new GraphicObject {Name = "Group2"};
            //group2.Children.Add(new Circle("Purple"));
            //group2.Children.Add(new Square("Black"));
            //
            //group1.Children.Add(group2);
            //drawing.Children.Add(group1);
//
            //Console.WriteLine(drawing);

            var neuron1 = new Neuron();
            var neuron2 = new Neuron();
            var layer1 = new NeuronLayer(3);
            var layer2 = new NeuronLayer(4);
            
            neuron1.ConnectTo(neuron2);
            neuron2.ConnectTo(layer1);
            layer1.ConnectTo(layer2);
            layer2.ConnectTo(neuron1);
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
    
    // ----------------------

    public class Neuron : IEnumerable<Neuron>
    {
        public List<Neuron> In, Out;

        public void ConnectTo(Neuron other)
        {
            Out.Add(other);
            other.In.Add(this);
        }

        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class NeuronLayer : Collection<Neuron>
    {
        public NeuronLayer(int count)
        {
            while (count-- > 0)
            {
                Add(new Neuron());
            }
        }
    }

    public static class Extension
    {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other)) return;
            
            foreach (var from in self)
            {
                foreach (var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }
}