using System;

namespace Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            //var pointCartesian = PointFactory.NewCartesianPoint(1, 2);
            //var pointPolar = PointFactory.NewPolarPoint(1, 2);
            //
            //pointCartesian.Draw();
            //pointPolar.Draw();

            //var pf = new PersonFactory();
            //var person1 = pf.CreatePerson("James");
            //var person2 = pf.CreatePerson("John");
            //var person3 = pf.CreatePerson("Robert");
            //var person4 = pf.CreatePerson("Mary");
            //var person5 = pf.CreatePerson("Patricia");
            //var person6 = pf.CreatePerson("Jennifer");
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"Id: {Id}\tName: {Name}";
        }
    }

    public class PersonFactory
    {
        private int _id = 0;

        public Person CreatePerson(string name) => new Person(_id++, name);
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