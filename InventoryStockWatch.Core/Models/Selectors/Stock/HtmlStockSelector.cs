namespace InventoryStockWatch.Core.Models.Selectors.Stock
{
    public class HtmlStockSelector : IStockSelector
    {
        public SelectorContentType Type => SelectorContentType.Html;
        public string Selector { get; set; }
        public string Property { get; set; }
        public string RegexTest { get; set; }
    }
}