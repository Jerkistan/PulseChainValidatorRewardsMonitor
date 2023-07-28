using Newtonsoft.Json;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace ValidatorRewardsMonitor
{
    public class Program
    {
        static void Main(string[] args)
        {
            string configPath = string.Empty;
            Settings settings = new Settings();

            if (!Debugger.IsAttached)
            {
                if (string.IsNullOrEmpty(args[0]))
                {
                    Console.WriteLine("Please specify the configuration file path.");
                    Environment.Exit(0);
                }

                settings = new Settings();

                configPath = args[0];

                if (!System.IO.File.Exists(configPath + "/appsettings.json"))
                {
                    Console.Write("Enter your validator reward wallet address: ");
                    settings.wallet = Console.ReadLine();

                    Console.Write("Enter your 10 digit phone number: ");
                    settings.phoneNumber = Console.ReadLine();

                    Console.Write("Enter your Twilio AccountSID: ");
                    settings.accountSid = Console.ReadLine();

                    Console.Write("Enter your Twilio auth token: ");
                    settings.authToken = Console.ReadLine();

                    Console.Write("Enter your Twilio phone number: ");
                    settings.twilioPhone = Console.ReadLine();

                    System.IO.File.WriteAllText(configPath + "/appsettings.json", JsonConvert.SerializeObject(settings));
                }
            }
            

            string json = System.IO.File.ReadAllText(configPath + "/appsettings.json");

            settings = JsonConvert.DeserializeObject<Settings>(json);

            Console.Clear();

            RewardsClient client = new RewardsClient(settings);
            client.Start();
        }
    }
}
