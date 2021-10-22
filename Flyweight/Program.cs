using System;
using System.Collections.Generic;
using System.Linq;

namespace Flyweight
{
    class Program
    {
        static void Main(string[] args)
        {
            var user1 = new User("Jon Smith");
            var user2 = new User("John Smith");
            var user3 = new User("Rob Johnson");
            var user4 = new User("Rob Johnson");

            Console.WriteLine(user1.FullName);
            Console.WriteLine(user2.FullName);
            Console.WriteLine(user3.FullName);
            Console.WriteLine(user4.FullName);

            Console.WriteLine(User.Strings.Count);
        }
    }

    public class User
    {
        private static List<string> _strings = new List<string>();
        private int[] _nameIndexes;

        public static List<string> Strings => _strings;

        public User(string fullName)
        {
            int GetOrAdd(string str)
            {
                var ind = _strings.IndexOf(str);
                if (ind != -1)
                    return ind;
                
                _strings.Add(str);
                return _strings.Count - 1;
            }

            _nameIndexes = fullName.Split(' ')
                .Select(GetOrAdd).ToArray();
        }

        public string FullName => string.Join(" ", _nameIndexes.Select(i => _strings[i]));
    }
}