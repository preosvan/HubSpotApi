using System;
using System.Collections.Generic;
using HubSpotApi.Models.Response.ContactResponse;
using HubSpotApi.Models.Response.CompanyResponse;

namespace HubSpotApi.Models. DTO
{
    internal static class ContactMapper
    {

        public static void ToContactList(ContactListResponse contactListResponse, ref List<ContactDTO> contactList, DateTime modifiedOnOrAfter, out bool hasMore)        
        {
            hasMore = true;

            if (contactList == null)
            {
                contactList = new List<ContactDTO>();
            }

            foreach (var contactResponse in contactListResponse.Contacts)
            {
                long modifiedOnOrAfterUnix = new DateTimeOffset(modifiedOnOrAfter).ToUnixTimeMilliseconds();

                long lastmodifieddate = 0;
                if (long.TryParse(contactResponse.Properties.Lastmodifieddate.Value, out lastmodifieddate))
                {
                    if (lastmodifieddate >= modifiedOnOrAfterUnix)
                    {
                        contactList.Add(new ContactDTO
                        {
                            Id = contactResponse.Vid,
                            Firstname = contactResponse.Properties.Firstname?.Value,
                            Lastname = contactResponse.Properties.Lastname?.Value,
                            LifeCycleStage = contactResponse.Properties.Lifecyclestage?.Value,
                            Company_Id = contactResponse.Properties.Associatedcompanyid?.Value
                        });
                    }                
                    else
                    {
                        hasMore = false;
                        return;
                    }                        
                }
            }
        }

        public static void Map(ref List<ContactDTO> contactList, List<CompanyResponse> companyList)
        {
            if (contactList != null & companyList != null) 
            {
                foreach (var contact in contactList)
                {
                    foreach (var company in companyList)
                    {
                        long companyId = 0;
                        if (long.TryParse(contact.Company_Id, out companyId))
                        {
                            if (company.CompanyId == companyId)
                            {
                                contact.Company_Name = company.Properties.Name?.Value;
                                contact.Company_Website = company.Properties.Website?.Value;
                                contact.Company_City = company.Properties.City?.Value;
                                contact.Company_State = company.Properties.State?.Value;
                                contact.Company_Zip = company.Properties.Zip?.Value;
                                contact.Company_NumberPhone = company.Properties.NumberPhone?.Value;
                                break;
                            }
                        }                           
                    }
                }
            }
        }
    }
}
