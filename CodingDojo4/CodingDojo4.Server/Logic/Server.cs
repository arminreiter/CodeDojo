using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CodingDojo4.Server.Logic
{
    public class Server
    {
        private const int MAX_CONNECTIONS = 5;

        private Socket _socket;
        private Thread _listener;
        private List<ClientHandler> _clients = new List<ClientHandler>();

        public event EventHandler<string> MessageReceived;

        #region Properties
        
        public List<string> Messages { get; private set; }
        public List<string> Users { get; private set; }

        #endregion

        public Server(string ip, int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            _socket.Listen(MAX_CONNECTIONS);

            Messages = new List<string>();
            Users = new List<string>();
        }

        public void Start()
        {
            _listener = new Thread(new ThreadStart(ListenForMessages));
            _listener.IsBackground = true;
            _listener.Start();
        }

        private void ListenForMessages()
        {
            while(_listener.IsAlive)
            {
                try
                {
                    var handler = _socket.Accept();
                    var bytes = new byte[1024];
                    //var bytesReceived = handler.Receive(bytes);

                    var clientHandler = new ClientHandler(handler);
                    clientHandler.MessageReceived += ClientHandler_MessageReceived;
                    _clients.Add(clientHandler);

                    //string message = Encoding.UTF8.GetString(bytes,0, bytesReceived);
                    //_socket.Send(Encoding.UTF8.GetBytes(message));
                }
                catch (Exception e) { System.Diagnostics.Trace.TraceWarning(e.Message); }
            }
        }
        
        public void Stop()
        {
            _socket.Close();
            _listener.Abort();

            foreach (var c in _clients)
            {
                c.Stop();
            }
        }

        public void DropUser(string selectedUser)
        {
            var client = _clients.FirstOrDefault(x => x.UserName.Equals(selectedUser));
            if(client != null)
            {
                client.Stop();
                _clients.Remove(client);
            }
        }

        private void ClientHandler_MessageReceived(object sender, string e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (MessageReceived != null)
                    MessageReceived(sender, e);

                foreach (var c in _clients)
                {
                    if (c.ClientSocket != sender)
                        c.SendMessage(e);
                }
            });
        }
    }
}
