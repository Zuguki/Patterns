using PatternNullObject.Models.Products;

namespace PatternNullObject.Models
{
    public class Shop
    {
        public static Product GetProduct(string productName)
        {
            switch (productName.ToLower())
            {
                case "milk": return new Milk();
                case "cream": return new Milk();
                case "cakes": return new Cake();
                case "coffee": return new Coffee();
                default: return new UnknownProduct();
            }
        }
    }
}