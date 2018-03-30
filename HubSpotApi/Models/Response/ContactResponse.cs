using System.Collections.Generic;
using Newtonsoft.Json;

namespace HubSpotApi.Models.Response.ContactResponse
{
    public partial class ContactListResponse
    {
        [JsonProperty("contacts")]
        public List<ContactResponse> Contacts { get; set; }

        [JsonProperty("has-more")]
        public bool HasMore { get; set; }

        [JsonProperty("vid-offset")]
        public long VidOffset { get; set; }

        [JsonProperty("time-offset")]
        public long TimeOffset { get; set; }
    }

    public partial class ContactResponse
    {
        [JsonProperty("vid")]
        public long Vid { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }

    public partial class Properties
    {
        [JsonProperty("lastmodifieddate")]
        public PropertyValueResponse Lastmodifieddate { get; set; }

        [JsonProperty("lifecyclestage")]
        public PropertyValueResponse Lifecyclestage { get; set; }

        [JsonProperty("associatedcompanyid")]
        public PropertyValueResponse Associatedcompanyid { get; set; }

        [JsonProperty("firstname")]
        public PropertyValueResponse Firstname { get; set; }

        [JsonProperty("lastname")]
        public PropertyValueResponse Lastname { get; set; }
    }

    public partial class ContactListResponse
    {
        public static ContactListResponse FromJson(string json) => JsonConvert.DeserializeObject<ContactListResponse>(json, Converter<ContactListResponse>.Settings);
    }
    
}
