namespace InventoryStockWatch.Core.Models.Selectors
{
    public class LinkedJsonPriceSelector : IPriceSelector
    {
        public SelectorContentType Type => SelectorContentType.LinkedJson;
        public string PathTemplate { get; set; }
    }
}