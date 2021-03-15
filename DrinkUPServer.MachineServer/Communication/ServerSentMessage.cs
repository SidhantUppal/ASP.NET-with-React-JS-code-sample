using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DrinkUPServer.MachineServer.Communication
{
    public class ServerSentMessage
    {
        private static JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            MaxDepth = 64,
        };

        public string Type { get; internal set; }
        public string For { get; set; }

        public override string ToString ()
        {
            Utility.LogFile(Type + " " + For, "ServerSentMessage");
            return JsonSerializer.Serialize( this, serializerOptions );
        }
    }
}
