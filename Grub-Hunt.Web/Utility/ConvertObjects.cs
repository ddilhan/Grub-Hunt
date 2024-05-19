using Newtonsoft.Json;

namespace Grub_Hunt.Web.Utility
{
    public class ConvertObjects
    {
        public string TypeToJsonString(object source)
        {
            return JsonConvert.SerializeObject(source, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        public T? JsonStringToType<T>(string source)
        {
            return JsonConvert.DeserializeObject<T>(source, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
