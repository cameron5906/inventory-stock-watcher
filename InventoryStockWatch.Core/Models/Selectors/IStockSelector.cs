namespace InventoryStockWatch.Core.Models.Selectors
{
    public interface IStockSelector
    {
        public SelectorContentType Type { get; }
        string RegexTest { get; set; }
    }
}