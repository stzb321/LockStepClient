using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LockStepFrameWork.NetMsg;

namespace Assets.scripts
{
    class UdpSocket : NetworkProxy
    {
        public static string IP = "127.0.0.1";
        private UdpClient udpClient;
        private IPEndPoint endPoint;

        public override void StartSocket()
        {
            endPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            try
            {
                udpClient = new UdpClient(Port);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, Port);
                udpClient.Connect(endPoint);
                StartReceive();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void StartReceive()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    UdpReceiveResult result = await udpClient.ReceiveAsync();
                    HandlerReceiveMessage(Encoding.UTF8.GetString(result.Buffer));
                }
            });
        }

        public override void SendTo(byte[] buf)
        {
            udpClient.Send(buf, buf.Length, endPoint);
        }

        public override void StopSocket()
        {
            udpClient?.Close();
            udpClient?.Dispose();
        }

    }
}
