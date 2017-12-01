using CodingDojo4.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodingDojo4.Client.Logic
{
    public class Client
    {
        private TcpClient _client;
        private Thread _listener;

        public event EventHandler<string> MessageReceived;
        public event EventHandler ClientDisconnected;

        public Client()
        {
            
        }

        public bool Connect(string ip, int port)
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

                _listener = new Thread(new ThreadStart(ListenForMesssages));
                _listener.Start();
            }
            catch(Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
            return true;
        }

        private void ListenForMesssages()
        {
            while(true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    var msgLength = _client.Client.Receive(buffer);
                    string message = Encoding.UTF8.GetString(buffer, 0, msgLength);

                    if (MessageReceived != null)
                        MessageReceived(this, message);

                    if(message.Equals(Globals.QUITMESSAGE))
                    {
                        Disconnect();
                        return;
                    }
                }
                catch(Exception ex)
                {
                    Logger.LogError(ex);
                }
            }
        }

        public void SendMessage(string message)
        {
            _client.Client.Send(Encoding.UTF8.GetBytes(message));
        }

        public void Disconnect()
        {
            _client.Client.Close();
            if (ClientDisconnected != null)
                ClientDisconnected(this, null);
        }
    }
}
