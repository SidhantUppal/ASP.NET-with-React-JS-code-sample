using DrinkUPServer.MachineServer.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Messages
{
    [Message( Structures.Constants.Message.UpdateWebSession )]
    public class WebSessionStatusUpdate : ServerSentMessage
    {
        internal static class Phases
        {
            public const string Start = "start";
            public const string Running = "running";

            public const string Ready = "ready";
            public const string Dispense = "dispense";

            public const string Ending = "ending";
        }


        public bool Active { get; set; }
        public string Phase { get; set; }


        public string Size { get; set; }
        public string Boost { get; set; }
    }
}
