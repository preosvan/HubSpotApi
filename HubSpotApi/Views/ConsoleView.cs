using System;

namespace HubSpotApi.Views
{
    public delegate void GetMessage(string msg);

    public class ConsoleView
    {
        public static void ConsoleWriteLine(string msg)
        {
            Console.WriteLine($"{DateTime.Now} - {msg}");
        }
    }
}
