using GetDataEveryDefaultPollingInterval.Providers;
using System;

namespace GetDataEveryDefaultPollingInterval
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var data = new DataProvider();
            data.Start();

            Console.ReadLine();
        }
    }
}