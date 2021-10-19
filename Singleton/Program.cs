using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDataBase.Instance();
            var db2 = SingletonDataBase.Instance();

            Console.WriteLine(ReferenceEquals(db, db2));
            Console.WriteLine(db.GetPopulation("Tokyo"));
        }
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

        public static SingletonDataBase Instance() => _instance.Value;
    }
}