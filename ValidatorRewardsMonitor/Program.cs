using Newtonsoft.Json;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace ValidatorRewardsMonitor
{
    public class Program
    {
       
        // expects wallet address as an argument
        static void Main(string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                Environment.Exit(0);
            }

            Settings settings = new Settings();

            string configPath = args[0];

            Console.WriteLine(configPath);

            Console.WriteLine("??");

            Console.ReadKey();

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

            string json = System.IO.File.ReadAllText(configPath + "/appsettings.json");

            settings = JsonConvert.DeserializeObject<Settings>(json);

            Console.Clear();

            RewardsClient client = new RewardsClient(settings);
            client.Start();
        }
    }
}