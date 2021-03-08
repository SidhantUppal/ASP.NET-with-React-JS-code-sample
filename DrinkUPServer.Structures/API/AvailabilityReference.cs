using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.Structures.API
{
    public class AvailabilityReference
    {
        public string Machine { get; set; }
        public string TransactionId { get; set; }
        public string ContinuationKey { get; set; }
    }
}
