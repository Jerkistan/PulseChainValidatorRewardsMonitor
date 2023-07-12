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
            Settings settings = new Settings();

            if (!System.IO.File.Exists("appsettings.json"))
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

                System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/appsettings.json", JsonConvert.SerializeObject(settings));
            }

            string json = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/appsettings.json");

            settings = JsonConvert.DeserializeObject<Settings>(json);

            Console.Clear();

            RewardsClient client = new RewardsClient(settings);
            client.Start();
        }
    }
}