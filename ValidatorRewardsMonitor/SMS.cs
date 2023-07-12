using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.IdentityModel.Tokens;

namespace ValidatorRewardsMonitor
{
    public class TwilioData
    {
        private string accountSid;
        private string authToken;
        private string PhoneNumber;
        private string ToPhoneNumber;

        public TwilioData(Settings settings)
        {
            accountSid = settings.accountSid;
            authToken = settings.authToken;
            PhoneNumber = settings.twilioPhone;
            ToPhoneNumber = settings.phoneNumber;
        }

        public async Task<bool> SendSMS(string Message)
        {
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(new PhoneNumber("+1" + ToPhoneNumber.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "")));
            messageOptions.From = new PhoneNumber(PhoneNumber);
            messageOptions.Body = Message;

            MessageResource message = await MessageResource.CreateAsync(messageOptions).ConfigureAwait(false);

            if (message.ErrorCode.HasValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}