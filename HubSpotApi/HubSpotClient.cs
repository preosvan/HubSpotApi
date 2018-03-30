using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using HubSpotApi.Controllers;
using HubSpotApi.Models.DTO;
using HubSpotApi.Models.Response.ContactResponse;
using HubSpotApi.Models.Response.CompanyResponse;
using HubSpotApi.Views;
using Excel = Microsoft.Office.Interop.Excel;

namespace HubSpotApi
{
    public class HubSpotClient
    {
        private readonly string _apiKey;
        private readonly string _baseURL = "https://api.hubapi.com";
        private GetMessage _getMessage;

        public HubSpotClient(string apiKey, GetMessage getMessage = null)
        {
            _apiKey = apiKey;
            _getMessage = getMessage;
        }

        public List<ContactDTO> GetAllContacts(DateTime modifiedOnOrAfter)
        {
            _getMessage?.Invoke("Loading contacts started");
            ContactsController contactController = new ContactsController(_apiKey, _baseURL);

            ListRequestOptions opts = new ListRequestOptions { PropertiesToInclude = new List<string> { "firstname", "lastname", "lifecyclestage", "associatedcompanyid" } };
            opts.Limit = 100;
            opts.timeOffset = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            
            List<ContactDTO> contactList = new List<ContactDTO>();

            bool hasMore = true;
            while (hasMore)
            {
                ContactListResponse pageContacts = contactController.GetPageContacts(opts);
                ContactMapper.ToContactList(pageContacts, ref contactList, modifiedOnOrAfter, out hasMore);

                _getMessage?.Invoke($"Loaded {contactList.Count} contacts");

                hasMore = hasMore & pageContacts.HasMore;
                if (hasMore)
                    opts.timeOffset = pageContacts.TimeOffset;
            }

            List<CompanyResponse> companyResponseList = GetAllCompanies(contactList);
            ContactMapper.Map(ref contactList, companyResponseList);

            return contactList;
        }

        public List<CompanyResponse> GetAllCompanies(List<ContactDTO> contactList)
        {
            _getMessage?.Invoke("Loading companies started");
            CompaniesController companiesController = new CompaniesController(_apiKey, _baseURL);

            List<CompanyResponse> companyList = new List<CompanyResponse>();
            CompanyResponse companyResponse;

            int tmp = 0;
            foreach (var contact in contactList)
            {
                companyResponse = companiesController.GetCompany(contact.Company_Id);
                if (companyResponse != null)
                {
                    companyList.Add(companyResponse);
                    tmp = companyList.Count % 10;
                    if (tmp == 0)
                        _getMessage?.Invoke($"Loaded {companyList.Count} companies");
                }                    
            }
                  
            return companyList;
        }
        
        public void ExportToExcel(List<ContactDTO> contactList)
        {
            _getMessage?.Invoke($"Export to Excel started");

            if (contactList == null)
                throw new Exception("Contact list not found.");

            Excel.Application xlApp = new Excel.Application();

            if (xlApp == null)            
                throw new Exception("Excel is not installed in the system.");

            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(Missing.Value);
            if (xlWorkBook == null)
                throw new Exception("Could not create workbook.");


            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            if (xlWorkSheet == null)
                throw new Exception("Could not create worksheet.");

            xlWorkSheet.Name = "Contacts";

            var propertiesContact = typeof(ContactDTO).GetProperties();

            int rowCount = contactList.Count + 1;
            int colCount = propertiesContact.Length;

            Excel.Range xlRange = xlWorkSheet.get_Range("A1", $"{((char)(colCount + 64))}{rowCount}");
            if (xlRange == null)
                throw new Exception("Could not get a range.");

            string[,] dataExport = new string[rowCount, colCount];

            for (int i = 0; i < colCount; i++)
                dataExport[0, i] = propertiesContact[i].Name;

            int rowNumber = 1;
            foreach (ContactDTO contact in contactList)
            {
                dataExport[rowNumber, 0] = contact.Id.ToString();
                dataExport[rowNumber, 1] = contact.Firstname;
                dataExport[rowNumber, 2] = contact.Lastname;
                dataExport[rowNumber, 3] = contact.LifeCycleStage;
                dataExport[rowNumber, 4] = contact.Company_Id;
                dataExport[rowNumber, 5] = contact.Company_Name;
                dataExport[rowNumber, 6] = contact.Company_Website;
                dataExport[rowNumber, 7] = contact.Company_City;
                dataExport[rowNumber, 8] = contact.Company_State;
                dataExport[rowNumber, 9] = contact.Company_Zip;
                dataExport[rowNumber, 10] = contact.Company_NumberPhone;
                rowNumber++;
    }

            xlRange.set_Value(Missing.Value, dataExport);
            xlRange.Columns.AutoFit();

            string filePath = 
                $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{Path.DirectorySeparatorChar}Contacts_{DateTime.Now.ToString("yyyyMMddhhmmss")}";

            xlApp.DisplayAlerts = false;
            xlWorkBook.SaveAs(filePath, Excel.XlFileFormat.xlOpenXMLWorkbook,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                Excel.XlSaveConflictResolution.xlLocalSessionChanges, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            filePath = xlWorkBook.FullName;

            xlWorkBook.Close();
            xlApp.Quit();

            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            
            Process.Start(filePath);
            _getMessage?.Invoke($"{contactList.Count} contacts exported to Excel");
        }
    }
}
