using System;
using System.Collections.Generic;
using HubSpotApi;
using HubSpotApi.Models.DTO;
using HubSpotApi.Views;

namespace HubSpotTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiKey = "demo";
            //DateTime modifiedOnOrAfter = DateTime.Now.AddDays(-1);
            DateTime modifiedOnOrAfter = DateTime.Now.AddHours(-10);

            HubSpotClient client = new HubSpotClient(apiKey, ConsoleView.ConsoleWriteLine);

            try
            {
                List<ContactDTO> contacts = client.GetAllContacts(modifiedOnOrAfter);
                client.ExportToExcel(contacts);
            }
            catch (Exception e)
            {
                ConsoleView.ConsoleWriteLine($"Error: {e.Message}");                
            }
            
            Console.ReadKey();
        }
    }
}
