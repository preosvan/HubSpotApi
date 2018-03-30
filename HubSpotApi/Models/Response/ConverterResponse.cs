using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HubSpotApi.Models.Response
{
    public class Converter<T>
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
        };
        public T FromJson(string json) => JsonConvert.DeserializeObject<T>(json, Converter<T>.Settings);
    }
}
