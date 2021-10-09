namespace InventoryStockWatch.Core.Models.Selectors.Stock
{
    public interface IStockSelector
    {
        public SelectorContentType Type { get; }
        string RegexTest { get; set; }
    }
}