using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTUtil
{
    public static class JsonUtil
    {
        public static string FromProperties(Dictionary<string, object> properties)
        {
            try
            {
                return JsonConvert.SerializeObject(properties, Formatting.Indented);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return null;
        }

        public static Dictionary<string, object> FromJson(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return null;
        }

        public static List<Dictionary<string, object>> FromJArray(object propertiesList)
        {
            try
            {
                if (propertiesList == null) return null;

                var array = propertiesList as JArray;
                if (array == null) return null;
                return array.ToObject<List<Dictionary<string, object>>>();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return null;
        }

        public static T GetValue<T>(Dictionary<string, object> properties, string key)
        {
            if (properties == null) return default(T);
            if (!properties.ContainsKey(key)) return default(T);

            try
            {
                var value = properties[key];
                if (typeof(T).IsEnum)
                {
                    return (T) Enum.Parse(typeof(T), value as string);
                }

                // string, bool, doule, long
                return (T) value;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return default(T);
        }

    }
}
