using GetDataEveryDefaultPollingInterval.Providers;
using System;

namespace GetDataEveryDefaultPollingInterval
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var dataProvider = new DataProvider();
            dataProvider.Start();
            Console.ReadLine();
        }
    }
}