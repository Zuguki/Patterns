using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MoreLinq.Extensions;

namespace Adapter
{
    static class Program
    {
        private static readonly List<VectorObject> _vectorObject = new List<VectorObject>
        {
            new VectorRectangle(1, 1, 10, 10),
            new VectorRectangle(3,3, 6, 6)
        };

        public static void DrawPoint(Point p) => Console.Write(".");

        static void Main(string[] args)
        {
            DrawPoints();
            DrawPoints();
        }

        private static void DrawPoints()
        {
            foreach (var vo in _vectorObject)
            {
                foreach (var line in vo)
                {
                    var adapter = new LineToPointAdapter(line);
                    adapter.ForEach(DrawPoint);
                }
            }
        }
    }

    public class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X: {X}\tY: {Y}";
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }

    public abstract class VectorObject : Collection<Line>
    { }

    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            Add(new Line(new Point(x, y), new Point(x + width, y)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
        }
    }

    public class LineToPointAdapter : Collection<Point>
    {
        private static int _count;
        
        public LineToPointAdapter(Line line)
        {
            Console.WriteLine($"{++_count}: Generating points for line");
            
            var left = Math.Min(line.Start.X, line.End.X);
            var right = Math.Max(line.Start.X, line.End.X);
            var top = Math.Min(line.Start.Y, line.End.Y);
            var bottom = Math.Max(line.Start.Y, line.End.Y);

            if (right - left == 0)
            {
                for (var y = top; y <= bottom; ++y)
                    Add(new Point(left, y));
            }
            else if (line.End.Y - line.Start.Y == 0)
            {
                for (var x = left; x <= right; ++x)
                    Add(new Point(x, top));
            }
        }
    }
}