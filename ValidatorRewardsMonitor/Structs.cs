using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorRewardsMonitor
{
    public class BlockResult
    {
        public string message { get; set; }
        public int status { get; set; }
        public List<Block> result { get; set; }

    }
    public class Block
    {
        public int blockNumber { get; set; }
        public DateTime timeStamp { get; set; }
    }

    public class BalanceResult
    {
        public decimal jsonrpc { get; set; }
        public string result { get; set; }
        public decimal resultDecimal { get; set; }
        public int id { get; set; }
    }

    public class Settings
    {
        public string wallet { get; set; }
        public string phoneNumber { get; set; }
        public string accountSid { get; set; }
        public string authToken { get; set; }
        public string twilioPhone { get; set; }
    }
}
