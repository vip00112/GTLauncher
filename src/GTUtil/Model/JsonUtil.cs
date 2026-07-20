using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// 속성을 JSON으로 직렬화해 파일에 원자적으로 저장한다.
        /// 직렬화에 실패하면(빈 결과) 기존 파일을 덮어쓰지 않아 데이터 소실을 방지한다.
        /// 임시 파일에 먼저 기록한 뒤 교체하여 쓰기 도중 중단 시에도 원본이 손상되지 않는다.
        /// </summary>
        public static bool SaveToFile(string path, Dictionary<string, object> properties)
        {
            try
            {
                string json = JsonConvert.SerializeObject(properties, Formatting.Indented);
                if (string.IsNullOrWhiteSpace(json)) return false;

                string tempPath = path + ".tmp";
                File.WriteAllText(tempPath, json);

                if (File.Exists(path))
                {
                    File.Replace(tempPath, path, null);
                }
                else
                {
                    File.Move(tempPath, path);
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
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
