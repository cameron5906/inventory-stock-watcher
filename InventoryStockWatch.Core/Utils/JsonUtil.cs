using Newtonsoft.Json.Linq;

namespace InventoryStockWatch.Core.Utils
{
    public static class JsonUtil
    {
        public static T GetValueFromArrayPath<T>(JArray array, string pathTemplate)
        {
            var parts = GetParts(pathTemplate);
            JToken token = array;

            foreach (var part in parts)
            {
                if (IsArrayPointer(part))
                {
                    token = token[GetArrayPointerIndex(part)];
                }
                else
                {
                    token = token[part];
                }
            }

            return token.Value<T>();
        }

        public static T GetValueFromObjectPath<T>(JObject obj, string pathTemplate)
        {
            var parts = GetParts(pathTemplate);
            
            JToken token = obj;

            foreach (var part in parts)
            {
                if (IsArrayPointer(part))
                {
                    token = token[GetArrayPointerIndex(part)];
                }
                else
                {
                    token = token[part];
                }
            }

            return token.Value<T>();
        }

        private static string[] GetParts(string pathTemplate)
        {
            return pathTemplate.Split('.');
        }

        private static bool IsArrayPointer(string part)
        {
            return part.Contains("[") && part.Contains("]");
        }

        private static int GetArrayPointerIndex(string part)
        {
            var indexStr = part.Split("[")[1].Split("]")[0];
            return int.Parse(indexStr);
        }
    }
}