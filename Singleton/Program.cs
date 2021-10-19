using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;
using NUnit.Framework;

namespace Singleton
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var db = SingletonDataBase.Instance;
        //    var db2 = SingletonDataBase.Instance;
//
        //    Console.WriteLine(ReferenceEquals(db, db2));
        //    Console.WriteLine(db.GetPopulation("Tokyo"));
        //}
    }

    public interface IDataBase
    {
        int GetPopulation(string city);
    }
    
    public class SingletonDataBase : IDataBase
    {
        public int Count => _instanceCount;
        private static int _instanceCount;
        private Dictionary<string, int> _capitals;

        private SingletonDataBase()
        {
            _capitals = GetCapitals();
        }

        private Dictionary<string, int> GetCapitals()
        {
            var directoryName = new FileInfo(typeof(SingletonDataBase).Assembly.Location)
                .DirectoryName;
            if (directoryName != null)
                return File.ReadAllLines(Path.Combine(directoryName, "CitysDB.txt"))
                    .Batch(2)
                    .ToDictionary(list => list.ElementAt(0).Trim(),
                        list => int.Parse(list.ElementAt(1)));
            
            return new Dictionary<string, int>();
        }

        public int GetPopulation(string city) => _capitals[city];

        private static Lazy<SingletonDataBase> _instance = new Lazy<SingletonDataBase>(() =>
        {
            _instanceCount++;
            return new SingletonDataBase();
        });

        public static SingletonDataBase Instance => _instance.Value;
    }

    public class SingletonRecordFinder
    {
        public int TotalPopulation(IEnumerable<string> cities)
        {
            var result = 0;
            foreach (var city in cities)
                result += SingletonDataBase.Instance.GetPopulation(city);

            return result;
        }
    }

    public class ConfigurableRecordFinder
    {
        private IDataBase _dataBase;

        public ConfigurableRecordFinder(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }
        
        public int TotalPopulation(IEnumerable<string> cities)
        {
            var result = 0;
            foreach (var city in cities)
                result += _dataBase.GetPopulation(city);

            return result;
        }
    }

    public class DummyDataBase : IDataBase
    {
        public int GetPopulation(string number)
        {
            return new Dictionary<string, int>
            {
                ["one"] = 1,
                ["two"] = 2,
                ["three"] = 3
            }[number];
        }
    }

    [TestFixture]
    public class SingletonTest
    {
        [Test] // Its not Unit Test!
        public void SingletonTotalPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new string[] {"Seoul", "Mexico City"};
            var population = rf.TotalPopulation(names);
            Assert.That(population, Is.EqualTo(17_500_000 + 17_400_000));
        }

        [Test]
        public void DependentTotalPopulation()
        {
            var db = new DummyDataBase();
            var rf = new ConfigurableRecordFinder(db);
            Assert.That(rf.TotalPopulation(new[] {"one", "two"}), Is.EqualTo(3));
        }
    }
}