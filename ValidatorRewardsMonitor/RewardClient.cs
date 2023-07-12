using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorRewardsMonitor
{
    public class RewardsClient
    {
        private HttpClient client = new HttpClient();
        private TwilioData twilio;

        private Settings settings;
        private bool running;
        private decimal Balance;
        private int Blocks;
        private int index = 0;

        public RewardsClient(Settings settings)
        {
            this.settings = settings;
            twilio = new TwilioData(settings);

            client = new HttpClient();
            client.BaseAddress = new Uri("https://scan.pulsechain.com/api");
        }

        public void Start()
        {
            running = true;

            BalanceResult balanceResult = GetBalance();
            BlockResult blockResult = GetBlocks();

            Blocks = blockResult.result.Count;
            Balance = balanceResult.resultDecimal;

            while (running)
            {
                Console.WriteLine("Checking for changes...");

                balanceResult = GetBalance();
                blockResult = GetBlocks();

                if (balanceResult.resultDecimal > Balance)
                {
                    decimal difference = balanceResult.resultDecimal - Balance;

                    string message = "Balance increased from " + Math.Round(Balance, 4).ToString() + " to " + Math.Round(balanceResult.resultDecimal, 4).ToString() + " (+" + Math.Round(difference, 4).ToString() + ").";
                    bool result = twilio.SendSMS(message).Result;

                    Console.WriteLine(message);
                }

                if (blockResult.result.Count > Blocks)
                {
                    string message = "A new block was proposed (" + blockResult.result.Count.ToString() + ").";
                    bool result = twilio.SendSMS(message).Result;

                    Console.WriteLine(message);
                }

                Blocks = blockResult.result.Count;
                Balance = balanceResult.resultDecimal;

                
                Thread.Sleep(60000);

                
                index++;
            }
        }

        public void Stop()
        {
            running = false;
        }
        
        private BlockResult GetBlocks()
        {
            string json = Get("?module=account&action=getminedblocks&address=" + settings.wallet);
            BlockResult result = JsonConvert.DeserializeObject<BlockResult>(json);

           
            return result;
        }

        private BalanceResult GetBalance()
        {
            string json =  Get("?module=account&action=eth_get_balance&address=" + settings.wallet);
            BalanceResult result = JsonConvert.DeserializeObject<BalanceResult>(json);

            BigInteger decValue = BigInteger.Parse(result.result.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
            decimal value = (decimal)decValue / 1000000000000000000;
            result.resultDecimal = Convert.ToDecimal(value);

            return result;
        }

        private string Get(string EndPoint)
        {
            HttpResponseMessage response = client.GetAsync(EndPoint).Result;
            Stream data = response.Content.ReadAsStreamAsync().Result;
            StreamReader reader = new StreamReader(data);

            string json = reader.ReadToEndAsync().Result;

            response.Dispose();
            data.Dispose();
            reader.Dispose();

            return json;
        }

    }
}
