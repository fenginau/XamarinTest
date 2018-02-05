using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    static class GlobalVariable
    {
        public static string ServerUrl = ""; // path to signal server
        public static string NotificationServer = ServerUrl + "/notificationServer";
        public static string SignalServer = NotificationServer;
        public static string ApiBaseUrl = NotificationServer + "/api/";
        public static string HardwareId = "R52H618VM9F";
        public static string ImageDir = "Image";
    }
}
