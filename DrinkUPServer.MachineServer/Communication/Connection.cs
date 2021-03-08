using DrinkUPServer.MachineServer.Communication.Facilitators;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace DrinkUPServer.MachineServer.Communication
{
    public class Connection
    {
        public delegate void ClientName ( string clientName );
        public event ClientName MachineConnected;
        public event ClientName MachineDisconnected;

        private static readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            MaxDepth = 64,
        };

        private readonly Server Server;

        public Connection ()
        {
            Server = new Server();
            Server.MessageEvent += RaiseMessageEvent;

            Server.ClientAdded += WhenMachineConnected;
            Server.ClientRemoved += WhenMachineDisconnected;

            RegisterMessageListener<ReportExistence>( WhenExistenceReported );
        }

        private void WhenExistenceReported ( ReportExistence reportExistence )
        {
        }

        private void WhenMachineConnected ( string clientName )
        {
            MachineConnected?.Invoke( clientName );
        }

        private void WhenMachineDisconnected ( string clientName )
        {
            MachineDisconnected?.Invoke( clientName );
        }

        private void RaiseMessageEvent ( object sender, MessageEventArgs e )
        {
            string message = e.Response;
            string type = JsonSerializer.Deserialize<Message>( message, serializerOptions ).Type;

            if ( MessageTypeToTypeOfMessage.ContainsKey( type ) )
            {
                Type construct = MessageTypeToTypeOfMessage[ type ];
                MethodInfo method = typeof( Connection ).GetMethod( nameof( Connection.ParseMessage ), new Type[] { typeof( string ) } );
                MethodInfo generic = method.MakeGenericMethod( construct );

                object[] param = new object[] { message };

                ClientSentMessage receivedMessage = generic.Invoke( this, param ) as ClientSentMessage;
                receivedMessage.Type = type;

                Delegate callback = TypeOfMessageToCallback[ construct ];
                callback.Method.Invoke( callback.Target, new[] { receivedMessage } );
            }
        }

        public T ParseMessage<T> ( string message ) where T : ClientSentMessage
        {
            return JsonSerializer.Deserialize<T>( message, serializerOptions );
        }
        public void Send<T> ( T message ) where T : ServerSentMessage
        {
            Type type = message.GetType();
            if ( !type.IsSubclassOf( typeof( ServerSentMessage ) ) )
            {
                throw new Exception( "The message must be an instance of a class that inherits the SendingMessage class." );
            }

            foreach ( MessageAttribute attribute in Attribute.GetCustomAttributes( type, typeof( MessageAttribute ) ) )
            {
                message.Type = attribute.Type;
            }

            Server.Send( message );
        }

        private readonly Dictionary<string, Type> MessageTypeToTypeOfMessage = new Dictionary<string, Type>();
        private readonly Dictionary<Type, Delegate> TypeOfMessageToCallback = new Dictionary<Type, Delegate>();

        public void RegisterMessageListener<T> ( ReceivedMessageCallback<T> callback ) where T : ClientSentMessage
        {
            Type type = typeof( T );

            foreach ( MessageAttribute attribute in Attribute.GetCustomAttributes( type, typeof( MessageAttribute ) ) )
            {
                MessageTypeToTypeOfMessage.Add( attribute.Type, type );
                TypeOfMessageToCallback.Add( type, callback );
            }
        }
    }
}
