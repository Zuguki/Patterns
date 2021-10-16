using System;
using PatternNullObject.Models;

namespace NullObject
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            foreach (var productName in ListProduct.Products)
            {
                var product = Shop.GetProduct(productName);
                Console.WriteLine(product.KcalOfEnergy);
                product.Eat();
            }
        }
    }
}