using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace DrinkUPServer.MachineServer.Communication.Facilitators
{
    internal sealed class Server
    {
        internal delegate void ClientName ( string clientName );
        internal event ClientName ClientAdded;
        internal event ClientName ClientRemoved;

        // Set the Server IP for debug server.
        ///private static IPAddress ServerIP = new IPAddress( new byte[] { 192, 168, 1, 4 } );
        private static IPAddress ServerIP = new IPAddress(new byte[] { 20, 186, 115, 164 });

        // Port number for direct machine communication.
        //private static readonly int ServerPort = 3687;
        private static readonly int ServerPort = 3687;

        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            MaxDepth = 64,
        };

        private readonly Dictionary<string, Client> MachineIdToClient = new Dictionary<string, Client>();

        private readonly Queue<TcpClient> ClientQueue = new Queue<TcpClient>();
        private readonly object ClientQueueLock = new object();

        internal event EventHandler<MessageEventArgs> MessageEvent;

        private readonly TcpListener Listener;

        internal Server ()
        {
#if DEBUG
#else
            ServerIP = Dns.GetHostEntry( Dns.GetHostName() ).AddressList.Last().MapToIPv4();
#endif
            IPEndPoint EndPoint = new IPEndPoint( ServerIP, ServerPort );

            Listener = new TcpListener( EndPoint );
            Utility.LogFile("Listener started at ", ServerIP + ":" + ServerPort);
            Thread ListeningThread = new Thread( ListeningRunner );
            ListeningThread.Start();

            Thread ServicingThread = new Thread( ServicingRunner );
            ServicingThread.Start();
        }

        private void ListeningRunner ()
        {
            try
            {
                Listener.Start();
                Utility.LogFile("ListeningRunner()", "Listener - start");

                while (true)
                {
                    TcpClient tcpClient = Listener.AcceptTcpClient();
                    // var str = $"Client {tcpClient.Client.RemoteEndPoint} connected at {ServerIP}:{ServerPort}";
                    Utility.LogFile("ListeningRunner", "Inside While");
                    lock (ClientQueueLock)
                    {
                        ClientQueue.Enqueue(tcpClient);
                        Utility.LogFile("ClientQueueLock", "Client Queue Lock");
                    }
                }
            }
            catch (InvalidOperationException ex) { Utility.LogFile(ex.Message, "Server ListeningRunner InvalidOperationException"); }
            catch (SocketException ex) { Utility.LogFile(ex.Message, "Server ListeningRunner IOException"); }
            catch (ThreadAbortException ex) { Utility.LogFile(ex.Message, "Server ListeningRunner ThreadAbortException"); }
            finally { }
        }

        private void ServicingRunner ()
        {
            int count;

            try
            {
                while ( true )
                {
                    lock ( ClientQueueLock )
                    {
                        count = ClientQueue.Count;
                    }

                    if ( count == 0 )
                    {
                        Thread.Sleep( 1 );
                    }
                    else
                    {
                        TcpClient tcpClient;

                        lock ( ClientQueueLock )
                        {
                            tcpClient = ClientQueue.Dequeue();
                        }

                        Client client = new Client( tcpClient );
                        client.Problem += WhenClientProblem;
                        client.Message += RaiseMessageEvent;

                        client.Send( new RequestAnnouncement
                        {
                            Type = "system-announce",
                        } );
                    }
                }
            }
            catch (InvalidOperationException ex) { Utility.LogFile(ex.Message, "Server ServicingRunner InvalidOperationException"); }
            catch (SocketException ex) { Utility.LogFile(ex.Message, "Server ServicingRunner IOException"); }
            catch (ThreadAbortException ex) { Utility.LogFile(ex.Message, "Server ServicingRunner ThreadAbortException"); }
            finally { }
        }

        private void WhenClientProblem ( Client client )
        {
            
            if ( MachineIdToClient.ContainsValue( client ) )
            {
                foreach ( string s in ( from x in MachineIdToClient where x.Value == client select x.Key ).ToList() )
                {
                   // Utility.LogFile("WhenClientProblem remove ", s);
                    RemoveClient( s );
                }
            }
        }

        private bool RemoveClient ( string name )
        {
            Utility.LogFile("RemoveClient ", name);
            ClientRemoved?.Invoke( name );
            return MachineIdToClient.Remove( name );
        }

        private void AddClient ( string name, Client client )
        {
            Utility.LogFile("AddClient ", name);
            if ( !MachineIdToClient.ContainsKey( name ) )
            {
                MachineIdToClient.Add( name, client );
                ClientAdded?.Invoke( name );
            }
        }

        private void RaiseMessageEvent ( Client client, MessageEventArgs e )
        {
            {
                string message = e.Response;
                Utility.LogFile("RaiseMessageEvent ", message);
                ClientSentMessage receivedMessage = JsonSerializer.Deserialize<ClientSentMessage>( message, serializerOptions );

                AddClient( receivedMessage.From, client );
            }

            MessageEvent?.Invoke( this, e );
        }

        internal void Send<T> ( T message ) where T : ServerSentMessage
        {
            if ( MachineIdToClient.ContainsKey( message.For ) )
            {
                Utility.LogFile("Send ", message.For + " message " + message);
                MachineIdToClient[ message.For ].Send( message );
            }
        }
    }
}
