using InventoryStockWatch.Core.Converters;
using InventoryStockWatch.Core.Models.Selectors.Price;
using InventoryStockWatch.Core.Models.Selectors.Stock;
using Newtonsoft.Json;

namespace InventoryStockWatch.Core.Models.Config
{
    public class ProductSourceDescriptor
    {
        public string Title { get; set; }
        public string Url { get; set; }

        [JsonConverter(typeof(ConcreteTypeConverter))]
        public IPriceSelector PriceSelector { get; set; }

        [JsonConverter(typeof(ConcreteTypeConverter))]
        public IStockSelector StockSelector { get; set; }
    }
}