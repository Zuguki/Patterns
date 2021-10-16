using System;

namespace PatternNullObject.Models.Products
{
    public class UnknownProduct : Product
    {
        public UnknownProduct()
            : base(energy: 0, "Unknown Product")
        {}

        public override void Eat()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("I think that u r hungry! Product Unknown");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}