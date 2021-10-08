using InventoryStockWatch.Core.Models.Selectors;

namespace InventoryStockWatch.Core.Models
{
    public class SourceDescriptor
    {
        public string Url { get; set; }
        public SourceType Type { get; set; }
        public IPriceSelector PriceSelector { get; set; }
        public IStockSelector StockSelector { get; set; }
    }
}