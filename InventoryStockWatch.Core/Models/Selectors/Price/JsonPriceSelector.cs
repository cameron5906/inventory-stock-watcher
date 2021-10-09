using InventoryStockWatch.Core.Models.Selectors.Stock;

namespace InventoryStockWatch.Core.Models.Selectors.Price
{
    public class JsonPriceSelector : IPriceSelector
    {
        public SelectorContentType Type => SelectorContentType.Json;
        public JsonContentType JsonContentType { get; set; }
        public string PathTemplate { get; set; }
    }
}