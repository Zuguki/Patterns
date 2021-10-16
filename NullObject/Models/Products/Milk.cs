using System;

namespace PatternNullObject.Models.Products
{
    public class Milk : Product
    {
        public Milk() 
            : base(energy: 345, "Parmalat 0.05%")
        {}

        public override void Eat() => Console.WriteLine("Ohh, Milk");
    }
}