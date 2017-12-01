using CodingDojo4.Core;
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
        private Socket _socket;
        private Thread _listener;

        public event EventHandler<string> MessageReceived;

        public Socket ClientSocket { get { return _socket; } }
        public string UserName { get; set; }

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

                    Logger.Log("MESSAGE received: " + message);

                    string messageText = message;

                    if (message.Contains(":"))
                    {
                        messageText = message.Substring(message.IndexOf(':') + 1).Trim();

                        if(String.IsNullOrEmpty(UserName))
                            UserName = message.Substring(0, message.IndexOf(':'));
                    }
                    
                    if (MessageReceived != null)
                        MessageReceived(_socket, message);
                    
                    if (messageText.Equals(Globals.QUITMESSAGE))
                    {
                        Stop();
                        return;
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }
        }

        public void Stop()
        {
            SendMessage(Globals.QUITMESSAGE);
            Logger.Log("DISCONNECT CLIENT " + UserName);
            _socket.Close(1);
            _listener.Abort();
        }

        public void SendMessage(string e)
        {
            _socket.Send(Encoding.UTF8.GetBytes(e));
        }
    }
}
