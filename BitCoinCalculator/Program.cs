using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace BitCoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinRate currentBitcoin = GetRates();
            Console.WriteLine($"current rate: {currentBitcoin.bpi.EUR.code} {currentBitcoin.bpi.EUR.rate_float}");

            Console.WriteLine("Calculate in: EUR/USD/GBP");
            string userChoice = Console.ReadLine();
            Console.WriteLine("enter the amount of bitcoins");
            float userCoins = float.Parse(Console.ReadLine());
            float currentRate = 0;

            if(userChoice == "EUR")
            {
                currentRate = currentBitcoin.bpi.EUR.rate_float;
            }

            float result = currentRate * userCoins;
            Console.WriteLine($"your bitcoins are worth {userChoice} {result}");
        }

        public static BitcoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitcoinRate bitcoinData;

            using(var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoinData = JsonConvert.DeserializeObject<BitcoinRate>(response);
            }

            return bitcoinData;
        }
    }
}
