namespace InventoryStockWatch.Core.Models.Selectors
{
    public class HtmlPriceSelector : IPriceSelector
    {
        public SelectorContentType Type => SelectorContentType.Html;
        public string Selector { get; set; }
        public string Property { get; set; }
    }
}