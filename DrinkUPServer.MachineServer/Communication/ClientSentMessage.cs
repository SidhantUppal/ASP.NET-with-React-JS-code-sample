using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Communication
{
    public class ClientSentMessage
    {
        public string Type { get; internal set; }
        public string From { get; set; }
    }
}
