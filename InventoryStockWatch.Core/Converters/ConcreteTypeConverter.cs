using InventoryStockWatch.Core.Models;
using InventoryStockWatch.Core.Models.Selectors.Price;
using InventoryStockWatch.Core.Models.Selectors.Stock;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockWatch.Core.Converters
{
    public class ConcreteTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            var contentType = (SelectorContentType)Enum.Parse(typeof(SelectorContentType), jObject["Type"].ToString());
            object shell = null;

            if(objectType.Name == nameof(IPriceSelector))
            {
                shell = contentType switch
                {
                    SelectorContentType.Html => new HtmlPriceSelector(),
                    SelectorContentType.Json => new JsonPriceSelector(),
                    SelectorContentType.LinkedJson => new LinkedJsonPriceSelector(),
                    _ => throw new NotImplementedException()
                };
            }
            else if(objectType.Name == nameof(IStockSelector))
            {
                shell = contentType switch
                {
                    SelectorContentType.Html => new HtmlStockSelector(),
                    SelectorContentType.Json => new JsonStockSelector(),
                    SelectorContentType.LinkedJson => new LinkedJsonStockSelector(),
                    _ => throw new NotImplementedException()
                };
            }
            
            serializer.Populate(jObject.CreateReader(), shell);
            return shell;
        }

        public override bool CanWrite => false;
        public override bool CanConvert(Type objectType) => true;
    }
}
