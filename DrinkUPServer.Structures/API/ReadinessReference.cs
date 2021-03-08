using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.Structures.API
{
    public class ReadinessReference
    {
        public string Machine { get; set; }

        public string TransactionId { get; set; }

        public string TransactionSecret { get; set; }
    }
}
