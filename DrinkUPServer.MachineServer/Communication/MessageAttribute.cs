using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkUPServer.MachineServer.Communication
{
    [AttributeUsage( AttributeTargets.Class )]
    public class MessageAttribute : Attribute
    {
        public string Type { get; private set; }

        public MessageAttribute ( string MessageType )
        {
            Type = MessageType;
        }
    }
}
