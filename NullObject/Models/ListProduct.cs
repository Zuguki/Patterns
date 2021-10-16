namespace PatternNullObject.Models
{
    public static class ListProduct
    {
        public static string[] Products { get; }

        static ListProduct()
        {
            Products = new string[]
            {
                "Milk",
                "Cakes",
                "Choco",
                "Cream",
                "Coffee",
                "Latte",
                "Capuchin",
                "Cacao"
            };
        }
    }
}