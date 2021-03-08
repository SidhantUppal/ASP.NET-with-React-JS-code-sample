using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.Structures.API
{
    public class ReadinessResponse
    {
        public string Status { get; set; }
        public class Statuses
        {
            public const string Ready = "ready";
            public const string StandBy = "standby";
            public const string Fault = "fault";
        }
    }
}
