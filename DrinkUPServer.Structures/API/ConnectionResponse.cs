using DrinkUPServer.Structures.Constructs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.Structures.API
{
    public class ConnectionResponse
    {
        public string Status { get; set; }
        public class Statuses
        {
            public const string Success = "success";
            public const string Failure = "failure";
        }

        public string TransactionId { get; set; }

        public List<IApiSize> Sizes { get; set; }

        public List<IApiBoost> Boosts { get; set; }
    }
}
