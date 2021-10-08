namespace InventoryStockWatch.Core.Models.Selectors
{
    public enum JsonContentType
    {
        Object,
        Array
    }
    
    public class JsonStockSelector : IStockSelector
    {
        public SelectorContentType Type => SelectorContentType.Json;
        public JsonContentType JsonContentType { get; set; }
        public string RegexTest { get; set; }
        public string PathTemplate { get; set; }
    }
}