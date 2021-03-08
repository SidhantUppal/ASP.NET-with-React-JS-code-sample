using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace DrinkUPServer.MachineServer.Communication.Facilitators
{
    internal class Client : IDisposable
    {
        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            MaxDepth = 64,
        };

        internal delegate void MessageEventCallback ( Client client, MessageEventArgs message );
        internal delegate void ProblemCallback ( Client client );

        internal event MessageEventCallback Message;
        internal event ProblemCallback Problem;

        private NetworkStream CommunicationStream;

        private Receiving Receiver;
        private Sending Sender;

        internal Client ( TcpClient client )
        {
            CommunicationStream = client.GetStream();

            Receiver = new Receiving( CommunicationStream );
            Receiver.Problem += WhenProblem;
            Receiver.MessageReceived += WhenMessageReceived;
            Receiver.KeepAliveReceived += WhenKeepAlive;

            Sender = new Sending( CommunicationStream );
            Sender.Problem += WhenProblem;
        }

        internal void Send ( string message )
        {
            Sender.SendData( message );
        }

        internal void Send<T> ( T sendingMessage ) where T : ServerSentMessage
        {
            Sender.SendData( JsonSerializer.Serialize<object>( sendingMessage, serializerOptions ) );
        }

        private void WhenMessageReceived ( object sender, MessageEventArgs e )
        {
            Message?.Invoke( this, e );
        }

        private void WhenKeepAlive ( object sender, bool e )
        {
        }

        private void WhenProblem ( object sender, ErrorEventArgs e )
        {
            Problem?.Invoke( this );
        }

        #region Disposition

        private bool disposed;

        ~Client ()
        {
            Dispose( disposing: false );
        }

        protected virtual void Dispose ( bool disposing )
        {
            if ( !disposed )
            {
                if ( disposing )
                {
                    Receiver.Dispose();
                    Sender.Dispose();

                    CommunicationStream.Dispose();
                }

                Receiver = null;
                Sender = null;

                CommunicationStream = null;

                disposed = true;
            }
        }

        public void Dispose ()
        {
            Dispose( disposing: true );
            GC.SuppressFinalize( this );
        }

        #endregion
    }
}
