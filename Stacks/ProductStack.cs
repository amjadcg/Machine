
namespace Stacks
{
    public class ProductStack
    {
        public ProductStack(int quantity, string name, double price)
        {
            Quantity = quantity;
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
