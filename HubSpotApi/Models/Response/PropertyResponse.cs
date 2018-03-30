using Newtonsoft.Json;

namespace HubSpotApi.Models.Response
{
    public class PropertyResponse
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("versions")]
        public PropertyValueResponse[] PropertiesValue { get; set; }
    }

    public class PropertyValueResponse
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("source-type")]
        public string SourceType { get; set; }
    }
}
