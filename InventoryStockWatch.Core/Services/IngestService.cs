using InventoryStockWatch.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockWatch.Core.Services
{
    public class IngestService
    {
        public async Task<IEnumerable<ProductSourceDescriptor>> GetAllProductsAsync()
        {
            await Task.CompletedTask;
            var content = File.ReadAllText(Environment.GetEnvironmentVariable("PRODUCT_JSON_PATH"));
            return JsonConvert.DeserializeObject<List<ProductSourceDescriptor>>(content);
        }
    }
}
