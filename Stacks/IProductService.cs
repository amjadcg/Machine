namespace Stacks
{
    public interface IProductService
    {
        List<string> GetProductSymbols();
        bool ProductExist(string symbol);
        ProductStack GetProduct(string symbol);
        void DispenseProduct(string symbol);
        void PrintProductNames();
    }
}