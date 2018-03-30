using System;
using HubSpotApi.Models.Response.CompanyResponse;
using Flurl;

namespace HubSpotApi.Controllers
{
    class CompaniesController : ResponseBaseController
    {
        public CompaniesController(string apiKey, string baseURL)
            : base(apiKey, baseURL) { }

        public CompanyResponse GetCompany(string companyId)
        {
            if (string.IsNullOrWhiteSpace(companyId)) 
                return null;

            var fullUrl = $"{ BaseURL }/companies/v2/companies/{companyId}"
                .SetQueryParam("hapikey", ApiKey);

            string response = RequestController.GetResponse(fullUrl);
         
            var company = CompanyResponse.FromJson(response);

            return company;
        }
    }

}
