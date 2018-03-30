using Newtonsoft.Json;

namespace HubSpotApi.Models.Response.CompanyResponse
{
    public partial class CompanyResponse
    {
        [JsonProperty("companyId")]
        public long CompanyId { get; set; }

        [JsonProperty("properties")]
        public PropertiesCompany Properties { get; set; }
    }

    public class PropertiesCompany
    {
        [JsonProperty("name")]
        public PropertyResponse Name { get; set; }

        [JsonProperty("website")]
        public PropertyResponse Website { get; set; }

        [JsonProperty("city")]
        public PropertyResponse City { get; set; }

        [JsonProperty("state")]
        public PropertyResponse State { get; set; }

        [JsonProperty("zip")]
        public PropertyResponse Zip { get; set; }

        [JsonProperty("phone")]
        public PropertyResponse NumberPhone { get; set; }
    }

    public partial class CompanyResponse
    {
        public static CompanyResponse FromJson(string json) => JsonConvert.DeserializeObject<CompanyResponse>(json, Converter<CompanyResponse>.Settings);
    }

}
