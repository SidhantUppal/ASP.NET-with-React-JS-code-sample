using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.Structures.API
{
    public class DispensationResponse
    {
        public string Status { get; set; }
        public class Statuses
        {
            public const string Done = "done";
            public const string Failure = "failure";
        }
    }
}
