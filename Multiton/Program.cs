using System;
using System.Collections.Generic;

namespace Multiton
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainPrinter = Printer.Get(Subsystem.Main);
            var backupPrinter = Printer.Get(Subsystem.Backup);
            var mainPrinter2 = Printer.Get(Subsystem.Main);

            Console.WriteLine(ReferenceEquals(mainPrinter, backupPrinter)); // False
            Console.WriteLine(ReferenceEquals(mainPrinter, mainPrinter2)); // True
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
}