using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Communication.Facilitators
{
    internal class MessageEventArgs
    {
        public string Response { get; private set; }

        internal MessageEventArgs ( string response )
        {
            Response = response;
        }
    }
}
