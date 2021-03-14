using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCoreApiApp.Controllers.Common
{
    public static class Serializer
    {
        public static Dictionary<string, string> Desirializer(string filters)
        {
            Dictionary<string, string> desirialize = null;
            try
            {
                if (!String.IsNullOrEmpty(filters))
                {
                    desirialize = JsonConvert.DeserializeObject<Dictionary<string, string>>(filters)
                        .Where(w => w.Value != null)
                        .ToDictionary(d => d.Key, d => d.Value);
                }
            }
            catch
            {
                return desirialize;
            }
            return desirialize;
        }
    }
}
