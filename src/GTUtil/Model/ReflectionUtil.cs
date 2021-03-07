using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GTUtil
{
    public static class ReflectionUtil
    {
        public static List<string> GetBrowsablePropertyNames(object obj, params string[] categoryFilter)
        {
            var propertyNames = new List<string>();

            PropertyInfo[] pis = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (categoryFilter != null)
                {
                    var categorys = pi.GetCustomAttributes<CategoryAttribute>().ToList();
                    if (!categorys.Any(o => categoryFilter.Contains(o.Category))) continue;
                }
                propertyNames.Add(pi.Name);
            }

            return propertyNames;
        }

        public static void CopyProperties(object source, object result, string[] categoryFilter, string[] ignorePropertyFilter)
        {
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();
            PropertyInfo[] resultProperties = result.GetType().GetProperties();
            foreach (var sp in sourceProperties)
            {
                if (categoryFilter != null)
                {
                    var categorys = sp.GetCustomAttributes<CategoryAttribute>().ToList();
                    if (!categorys.Any(o => categoryFilter.Contains(o.Category))) continue;
                }

                if (ignorePropertyFilter != null && ignorePropertyFilter.Contains(sp.Name)) continue;

                var rp = resultProperties.FirstOrDefault(o => o.Name == sp.Name && o.PropertyType == sp.PropertyType && o.GetSetMethod() != null);
                if (rp == null) continue;

                rp.SetValue(result, sp.GetValue(source));
            }
        }
    }
}
