using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            var emp1 = new EmployeeFactory().NewMainOfficeEmployee("John", 1);
            var emp2 = new EmployeeFactory().NewAuxOfficeEmployee("Jeff", 23);
            var emp3 = new EmployeeFactory().NewMainOfficeEmployee("Liza", 3);
            var emp4 = new EmployeeFactory().NewAuxOfficeEmployee("Andrey", 5231);
            var emp5 = new EmployeeFactory().NewMainOfficeEmployee("Scarlet", 64);

            Console.WriteLine(emp1);
            Console.WriteLine(emp2);
            Console.WriteLine(emp3);
            Console.WriteLine(emp4);
            Console.WriteLine(emp5);

            emp5.Address.Suite = 10;
            Console.WriteLine(emp5);
        }
    }

    [Serializable]
    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int Suite { get; set; }

        public Address(string streetAddress, string city, int suite)
        {
            StreetAddress = streetAddress;
            City = city;
            Suite = suite;
        }

        public override string ToString()
        {
            return $"Street: {StreetAddress}\tCity: {City}\tSuite: {Suite}";
        }
    }
    
    [Serializable]
    public class Employee
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Employee(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"Name: {Name}\tAddress: {Address}";
        }
    }

    public class EmployeeFactory
    {
        private Employee _main = new Employee(null, new Address("321 East Dr", "LA", 0));
        private Employee _aux = new Employee(null, new Address("123 East Dr", "Chicago", 0));

        private Employee NewEmployee(Employee proto, string name, int suite)
        {
            var copy = proto.DeepCopy();
            copy.Name = name;
            copy.Address.Suite = suite;
            return copy;
        }

        public Employee NewMainOfficeEmployee(string name, int suite) => NewEmployee(_main, name, suite);
        
        public Employee NewAuxOfficeEmployee(string name, int suite) => NewEmployee(_aux, name, suite);
    }

    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                var copy = formatter.Deserialize(stream);
                return (T) copy;
            }
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T) s.Deserialize(ms);
            }
        }
    }
}