using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
namespace CIS_Textil
{
    class ClassBroadCast
    {
        public void BroadCast()
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.1.255"), 7999);

            string computerInfo = ":USER" + ":" + System.Environment.MachineName + ":" + CommonCls.GetIP() + ":" +
            Dns.GetHostEntry(Dns.GetHostName()).AddressList[0] + ":ABC";

            byte[] buff = Encoding.Default.GetBytes(computerInfo);
            while (true)
            {
                try
                {
                    udpClient.Send(buff, buff.Length, ep);
                    Thread.Sleep(2000);
                }
                catch { }
            }
        }
    }
}
