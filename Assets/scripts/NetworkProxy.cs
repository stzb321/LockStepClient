using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LockStepFrameWork.NetMsg;

namespace Assets.scripts
{
    abstract class NetworkProxy
    {
        public static int Port = 8488;
        abstract public void StartSocket();
        abstract public void StopSocket();

        abstract public void SendTo(byte[] buf);

        public Action<string> HandlerReceiveMessage = (recData) =>
        {
            Console.WriteLine("ReceiveMessage:{0}", recData);
        };
    }
}
