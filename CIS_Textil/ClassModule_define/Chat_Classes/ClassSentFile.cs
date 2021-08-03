using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
namespace CIS_Textil
{
    class ClassSentFile
    {
        private OpenFileDialog dlg;
        private Socket socketSent;
        public ClassSentFile(OpenFileDialog dlg, Socket socketSent)
        {
            this.dlg = dlg;
            this.socketSent = socketSent;
        }
        public void SentFile()
        {
            string msg = "0DAT " + dlg.FileName;

            socketSent.Send(Encoding.Default.GetBytes(msg));
            FileStream read = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
            byte[] buff = new byte[2048];
            int len = 0;
            while ((len = read.Read(buff, 0, 1024)) != 0)
            {
                socketSent.Send(buff, 0, len, SocketFlags.None);
            }
            msg = "END";
            socketSent.Send(Encoding.Default.GetBytes(msg));
            socketSent.Close();
            read.Close();
        }
    }
}
