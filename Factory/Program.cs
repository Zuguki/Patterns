using System;

namespace Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            var pointCartesian = PointFactory.NewCartesianPoint(1, 2);
            var pointPolar = PointFactory.NewPolarPoint(1, 2);
            
            pointCartesian.Draw();
            pointPolar.Draw();
        }
    }

    public class Point
    {
        private double _x;
        private double _y;

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public void Draw()
        {
            Console.WriteLine($"X: {_x}\nY: {_y}");
        }
    }

    public static class PointFactory
    {
        public static Point NewCartesianPoint(double x, double y) => new Point(x, y);

        public static Point NewPolarPoint(double rho, double theta) => new Point(rho * Math.Cos(theta),
            rho * Math.Sin(theta));
    }
}