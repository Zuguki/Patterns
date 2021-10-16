namespace PatternNullObject.Models
{
    public abstract class Product
    {
        public Product(int energy, string name)
        {
            this.energy = energy;
            this.name = name;
        }
        
        public string Price { get; set; }
        private int energy;
        private string name;
        public abstract void Eat();

        public virtual string KcalOfEnergy => 
            $"{this.name} has {energy} kcal";
    }
}