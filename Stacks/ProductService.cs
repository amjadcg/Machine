
using Newtonsoft.Json;

namespace Stacks
{
    public class ProductService : IProductService
    {
        private readonly Dictionary<string, ProductStack> _productDictionary;

        public ProductService()
        {
            try
            {
                //Read Product Settings from Json File.
                _productDictionary = JsonConvert.DeserializeObject<Dictionary<string, ProductStack>>(File.ReadAllText("Products.json")) ??
                                     new Dictionary<string, ProductStack>();
            }
            catch (Exception ex)
            {
                //Something Went Wrong.
                throw new InvalidOperationException("Error in price settings file. " + ex.Message);
            }
        }

        public List<string> GetProductSymbols() => _productDictionary.Keys.ToList();

        public bool ProductExist(string symbol)
        {
            if (_productDictionary.TryGetValue(symbol, out var product))
                return product.Quantity > 0;

            return false;
        }

        public void DispenseProduct(string symbol)
        {
            _productDictionary[symbol].Quantity -= 1;
            Console.WriteLine("Please Take Your Snack! :)");
        }

        public void PrintProductNames()
        {
            Console.WriteLine("--------------------------------------------------");
            foreach (var (key, value) in _productDictionary)
                Console.WriteLine($"{key}: {value.Name}, Price: {value.Price} ({value.Quantity})");
            Console.WriteLine("--------------------------------------------------");
        }

        public ProductStack GetProduct(string symbol)
        {
            return _productDictionary[symbol];
        }
    }
}
