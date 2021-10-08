namespace InventoryStockWatch.Core.Models.Selectors
{
    public interface IPriceSelector
    {
        public SelectorContentType Type { get; }
    }
}