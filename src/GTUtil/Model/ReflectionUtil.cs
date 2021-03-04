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
        public static List<string> GetBrowsablePropertyNames(object obj, params string[] category)
        {
            var propertyNames = new List<string>();

            PropertyInfo[] pis = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                var categorys = pi.GetCustomAttributes<CategoryAttribute>().ToList();
                if (categorys.Any(o => category.Contains(o.Category)))
                {
                    propertyNames.Add(pi.Name);
                }
            }

            return propertyNames;
        }
    }
}
