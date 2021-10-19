using System;
using System.Collections.Generic;

namespace Multiton
{
    class Program
    {
        static void Main(string[] args)
        {
            //var mainPrinter = Printer.Get(Subsystem.Main);
            //var backupPrinter = Printer.Get(Subsystem.Backup);
            //var mainPrinter2 = Printer.Get(Subsystem.Main);
//
            //Console.WriteLine(ReferenceEquals(mainPrinter, backupPrinter)); // False
            //Console.WriteLine(ReferenceEquals(mainPrinter, mainPrinter2)); // True

            var chief1 = new Chief();
            chief1.Name = "John";
            
            var chief2 = new Chief();
            chief2.Age = 45;

            Console.WriteLine(chief1);
            Console.WriteLine(chief2);
        }
    }

    public enum Subsystem
    {
        Main,
        Backup
    }

    public class Printer
    {
        private static readonly Dictionary<Subsystem, Printer> Instance =
            new Dictionary<Subsystem, Printer>();

        public Printer()
        {
            
        }

        public static Printer Get(Subsystem ss)
        {
            if (Instance.ContainsKey(ss))
                return Instance[ss];

            var instance = new Printer();
            Instance[ss] = instance;
            return instance;
        }
    }

    public class Chief
    {
        private static string _name;
        private static int _age;

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public override string ToString()
        {
            return $"Name: {Name}\t Age: {Age}";
        }
    }
}