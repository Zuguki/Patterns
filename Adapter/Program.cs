using System;
using System.Collections;
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
        
        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
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
        
        protected bool Equals(Line other)
        {
            return Equals(Start, other.Start) && Equals(End, other.End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Line) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Start != null ? Start.GetHashCode() : 0) * 397) 
                       ^ (End != null ? End.GetHashCode() : 0);
            }
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

    public class LineToPointAdapter : IEnumerable<Point>
    {
        private static int _count;
        private static Dictionary<int, List<Point>> _cache = new Dictionary<int, List<Point>>();
        private int _hash;
        
        public LineToPointAdapter(Line line)
        {
            _hash = line.GetHashCode();
            if (_cache.ContainsKey(_hash))
                return;

            var points = new List<Point>();
            
            Console.WriteLine($"{++_count}: Generating points for line");
            
            var left = Math.Min(line.Start.X, line.End.X);
            var right = Math.Max(line.Start.X, line.End.X);
            var top = Math.Min(line.Start.Y, line.End.Y);
            var bottom = Math.Max(line.Start.Y, line.End.Y);

            if (right - left == 0)
            {
                for (var y = top; y <= bottom; ++y)
                    
                    points.Add(new Point(left, y));
            }
            else if (line.End.Y - line.Start.Y == 0)
            {
                for (var x = left; x <= right; ++x)
                    points.Add(new Point(x, top));
            }
            
            _cache.Add(_hash, points);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return _cache[_hash].GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}