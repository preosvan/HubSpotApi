using System.Linq;
using HubSpotApi.Models.Response.ContactResponse;
using Flurl;

namespace HubSpotApi.Controllers
{
    class ContactsController : ResponseBaseController
    {
        public ContactsController(string apiKey, string baseURL)
            : base(apiKey, baseURL) { }

        public ContactListResponse GetPageContacts(ListRequestOptions opts)
        {
            if (opts == null)
            {
                opts = new ListRequestOptions();
            }

            var fullUrl = $"{ BaseURL }/contacts/v1/lists/recently_updated/contacts/recent?"
                .SetQueryParam("hapikey", ApiKey);

            fullUrl.SetQueryParam("count", opts.Limit);           
            
            if (opts.PropertiesToInclude.Any())
            {
                fullUrl.SetQueryParam("property", opts.PropertiesToInclude);
            }
            if (opts.timeOffset.HasValue)
            {
                fullUrl = fullUrl.SetQueryParam("timeOffset", opts.timeOffset);
            }

            string response = RequestController.GetResponse(fullUrl);

            //deserialise this to a list of contacts
            var contactList = ContactListResponse.FromJson(response);

            return contactList;
        }        
    }
}
