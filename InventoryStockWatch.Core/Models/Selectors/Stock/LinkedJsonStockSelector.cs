namespace InventoryStockWatch.Core.Models.Selectors.Stock
{
    public class LinkedJsonStockSelector : IStockSelector
    {
        public SelectorContentType Type => SelectorContentType.LinkedJson;
        public string RegexTest { get; set; }
        public string PathTemplate { get; set; }
    }
}