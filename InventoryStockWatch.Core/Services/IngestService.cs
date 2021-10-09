using InventoryStockWatch.Core.Interfaces.Services;
using InventoryStockWatch.Core.Models.Config;
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
        private readonly IConfigurationService _configuration;

        public IngestService(IConfigurationService configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<ProductSourceDescriptor>> GetAllProductsAsync()
        {
            await Task.CompletedTask;
            var content = File.ReadAllText(_configuration.GetProductDataPath());
            return JsonConvert.DeserializeObject<List<ProductSourceDescriptor>>(content);
        }
    }
}
