using System;

namespace PatternNullObject.Models.Products
{
    public class Cake : Product
    {
        public Cake()
            : base(energy: 1888, "Three choco")
        {}

        public override void Eat() => Console.WriteLine("That's Choco Party");
    }
}