using Xamarin.Forms;
using System;

namespace Test.Utils
{
    static class Logger
    {
        public static void Info(string tag, string message)
        {
            Console.WriteLine(message);
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    Console.WriteLine(message);
                    break;
                default:
                    break;
            }
        }
    }
}
