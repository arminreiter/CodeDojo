using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodingDojo4.Server.Logic
{
    public class ClientHandler
    {
        public Socket ClientSocket { get { return _socket; } }
        private Socket _socket;
        private Thread _listener;

        public event EventHandler<string> MessageReceived; 

        public ClientHandler(Socket socket)
        {
            _socket = socket;
            _listener = new Thread(Receive);
            _listener.IsBackground = true;
            _listener.Start();
        }

        private void Receive()
        {
            while(true)
            {
                try
                {
                    var bytes = new byte[1024];
                    var bytesReceived = _socket.Receive(bytes);
                    var message = Encoding.UTF8.GetString(bytes, 0, bytesReceived);

                    if (MessageReceived != null)
                        MessageReceived(_socket, message);

                }
                catch (Exception e) { System.Diagnostics.Trace.TraceWarning(e.Message); }
            }
        }

        public void Stop()
        {
            _socket.Close();
            _listener.Abort();
        }

        public void SendMessage(string e)
        {
            _socket.Send(Encoding.UTF8.GetBytes(e));
        }
    }
}
