using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DrinkUPServer.MachineServer.Communication.Facilitators
{
    internal sealed class Sending : IDisposable
    {
        private static readonly string KeepAliveMessage = @"<![KEEP-ALIVE]!>";
        private static readonly int KeepAliveGap = 1000;

        private NetworkStream NetworkStream;
        private Thread SendingThread;

        internal event EventHandler<ErrorEventArgs> Problem;

        private readonly Queue<string> MessageQueue = new Queue<string>();
        private readonly object MessageQueueLock = new object();

        internal Sending ( NetworkStream networkStream )
        {
            NetworkStream = networkStream;

            SendingThread = new Thread( SendingRunner );
            SendingThread.Start();
        }

        private DateTime LastKeepAlive { get; set; } = DateTime.Now;

        internal void DisConnect ()
        {
            SendingThread.Abort();

            NetworkStream = null;
            SendingThread = null;
        }

        internal void SendData ( string message )
        {
            lock ( MessageQueueLock )
            {
                MessageQueue.Enqueue( message );
            }
        }

        private string PrepareMessage ( string message )
        {
            string guid = Convert.ToBase64String( Guid.NewGuid().ToByteArray() );

            return $"<![MESSAGE[{ guid }[{ message }]{ guid }]]>";
        }

        private void SendingRunner ()
        {
            int count = 0;

            try
            {
                while ( true )
                {
                    lock ( MessageQueueLock )
                    {
                        count = MessageQueue.Count;
                    }

                    if ( LastKeepAlive.AddMilliseconds( KeepAliveGap ) < DateTime.Now )
                    {
                        byte[] keepAlive = Encoding.ASCII.GetBytes( KeepAliveMessage );
                        NetworkStream.Write( keepAlive, 0, keepAlive.Length );
                        LastKeepAlive = DateTime.Now;
                    }

                    if ( count == 0 )
                    {
                        Thread.Sleep( 1 );
                    }
                    else
                    {
                        string message;

                        lock ( MessageQueueLock )
                        {
                            message = MessageQueue.Dequeue();
                        }

                        byte[] binaryMessage = Encoding.ASCII.GetBytes( PrepareMessage( message ) );
                        NetworkStream.Write( binaryMessage, 0, binaryMessage.Length );
                    }
                }
            }
            catch ( ThreadAbortException ) { }
            catch ( ObjectDisposedException ) { }
            catch ( IOException ex )
            {
                Problem?.Invoke( this, new ErrorEventArgs( ex ) );
            }
            finally { }
        }

        #region Disposition

        private bool disposed;

        ~Sending ()
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
