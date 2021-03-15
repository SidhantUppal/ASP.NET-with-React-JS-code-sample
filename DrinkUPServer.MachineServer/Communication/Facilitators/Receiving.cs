using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DrinkUPServer.MachineServer.Communication.Facilitators
{
    internal sealed class Receiving : IDisposable
    {
        private static readonly Regex KeepAliveRegex = new Regex( @"\<\!\[KEEP-ALIVE\]\!\>", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        private static readonly Regex MessageRegex = new Regex( @"\<\!\[MESSAGE\[(?<guid>[0-9A-Za-z+/]{22}==)\[(?<message>.+)\](\k<guid>)\]\]\>", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        private NetworkStream NetworkStream;
        private Thread ReceivingThread;
        private Thread DispatchingThread;

        internal event EventHandler<MessageEventArgs> MessageReceived;
        internal event EventHandler<bool> KeepAliveReceived;

        internal event EventHandler<ErrorEventArgs> Problem;

        private readonly StringBuilder MessageBuilder = new StringBuilder();
        private readonly object MessageBuilderLock = new object();

        internal Receiving ( NetworkStream networkStream )
        {
            NetworkStream = networkStream;

            DispatchingThread = new Thread( DispatchingRunner );
            DispatchingThread.Start();

            ReceivingThread = new Thread( ReceivingRunner );
            ReceivingThread.Start();
        }

        internal void DisConnect ()
        {
            ReceivingThread.Abort();
            DispatchingThread.Abort();

            NetworkStream = null;
            ReceivingThread = null;
            DispatchingThread = null;
        }

        private void ReceivingRunner ()
        {
            try
            {
                while ( true )
                {
                    if ( !NetworkStream.DataAvailable )
                    {
                        Thread.Sleep( 1 );
                    }
                    else
                    {
                        byte[] Buffer = new byte[ 256 ];

                        do
                        {
                            int bytesRead = NetworkStream.Read( Buffer, 0, Buffer.Length );
                            string appendable = Encoding.ASCII.GetString( Buffer, 0, bytesRead );

                            lock ( MessageBuilderLock )
                            {
                                MessageBuilder.Append( appendable );
                            }

                        }
                        while ( NetworkStream.DataAvailable );

                    }
                }
            }
            catch ( ThreadAbortException ex) {
                  Utility.LogFile(ex.Message, "Receiving ThreadAbortException");
            }
            catch ( ObjectDisposedException ex) { Utility.LogFile(ex.Message, "Receiving ObjectDisposedException"); }
            catch ( IOException ex )
            {
                Utility.LogFile(ex.Message, "Receiving IOException");
                Problem?.Invoke( this, new ErrorEventArgs( ex ) );
            }
            finally { }
        }

        private void DispatchingRunner ()
        {
            int previous = 0;
            int current = 0;

            while ( true )
            {
                lock ( MessageBuilderLock )
                {
                    current = MessageBuilder.Length;
                }

                if ( current <= previous )
                {
                    Thread.Sleep( 1 );
                }
                else
                {
                    string leftover;

                    lock ( MessageBuilderLock )
                    {
                        leftover = MessageBuilder.ToString();
                    }

                    current = leftover.Length;

                    if ( KeepAliveRegex.IsMatch( leftover ) )
                    {
                        foreach ( Match keepAlive in KeepAliveRegex.Matches( leftover ) )
                        {
                            string keepAliveString = keepAlive.ToString();

                            leftover = leftover.Replace( keepAliveString, "" );

                            RaiseKeepAliveEvent();
                        }

                        lock ( MessageBuilderLock )
                        {
                            MessageBuilder.Remove( 0, current ).Insert( 0, leftover );
                        }
                    }

                    current = leftover.Length;

                    if ( MessageRegex.IsMatch( leftover ) )
                    {
                        MatchCollection messageCollection = MessageRegex.Matches( leftover );

                        foreach ( Match composedMessage in messageCollection )
                        {
                            string messageString = composedMessage.ToString();

                            leftover = leftover.Replace( messageString, "" );

                            string message = composedMessage.Groups[ "message" ].Value;
                            RaiseReadEvent( message );
                        }

                        lock ( MessageBuilderLock )
                        {
                            MessageBuilder.Remove( 0, current ).Insert( 0, leftover );
                        }
                    }
                }

                previous = current;
            }
        }

        private async void RaiseReadEvent ( string message )
        {
            await Task.Run( () =>
            {
                MessageReceived?.Invoke( this, new MessageEventArgs( message ) );
            } );
        }

        private async void RaiseKeepAliveEvent ()
        {
            await Task.Run( () =>
            {
                KeepAliveReceived?.Invoke( this, true );
            } );
        }

        #region Disposition

        private bool disposed;

        ~Receiving ()
        {
            Dispose( disposing: false );
        }

        private void Dispose ( bool disposing )
        {
            if ( !disposed )
            {
                DisConnect();

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
