using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InventoryStockWatch.Core.Utils
{
    public class BrowserEmulator : IDisposable
    {
        private const string UserAgent =
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Safari/537.36";
        
        private readonly HttpClient _httpClient;

        public BrowserEmulator()
        {
            _httpClient = new HttpClient(new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                UseCookies = true
            });
            _httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}