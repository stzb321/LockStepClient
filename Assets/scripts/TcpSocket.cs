using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts
{
    class TcpSocket : NetworkProxy
    {
        public static string IP = "127.0.0.1";
        public TcpClient Client;
        public override void StartSocket()
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            Client = new TcpClient(AddressFamily.InterNetwork);
            Task.Run(async () =>
            {
                await Client.ConnectAsync(IPAddress.Parse(IP), Port);

                NetworkStream stream = Client.GetStream();
                int length = 1024 * 1024 * 4;
                while (true)
                {
                    byte[] buff = new byte[length];
                    int recLength = await stream.ReadAsync(buff, 0, length);
                    byte[] recByte = new byte[recLength];
                    Array.Copy(buff, recByte, recLength);

                    HandlerReceiveMessage(Encoding.UTF8.GetString(recByte));
                }
            });
        }

        public override void SendTo(string data)
        {
            if (!IsSocketConnected())
            {
                return;
            }

            byte[] message = Encoding.UTF8.GetBytes(data);
            NetworkStream stream = Client.GetStream();
            stream.BeginWrite(message, 0, message.Length, null, null);
        }

        private bool IsSocketConnected()
        {
            return Client.Client.Poll(1000, SelectMode.SelectRead) && Client.Available == 0;
        }

        public override void StopSocket()
        {
            Client?.Close();
            Client?.Dispose();
        }
    }
}
