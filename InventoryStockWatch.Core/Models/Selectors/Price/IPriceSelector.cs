namespace InventoryStockWatch.Core.Models.Selectors.Price
{
    public interface IPriceSelector
    {
        public SelectorContentType Type { get; }
    }
}