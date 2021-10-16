using System;

namespace PatternNullObject.Models.Products
{
    public class Coffee : Product
    {
        public Coffee() 
            : base(energy: 78, "Little's French Vanilla")
        {}

        public override void Eat() => Console.WriteLine("That's best Coffee");
    }
}