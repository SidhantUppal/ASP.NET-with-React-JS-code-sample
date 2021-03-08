using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.Structures.API
{
    public class AvailabilityResponse
    {
        public string Status { get; set; }
        public class Statuses
        {
            public const string Connected = "connected";
            public const string Problem = "problem";
            public const string Fault = "fault";
        }
    }
}
