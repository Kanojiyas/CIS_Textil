using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using  CIS_Bussiness;using CIS_DBLayer;

namespace CIS_Textil
{
    class ChatSession
    {
        private static string fileName = "";
        private Socket chat;
        private string pack;
        public ChatSession(Socket chat)
        {
            this.chat = chat;
        }

        public void StartChat()
        {
            IPEndPoint ep = (IPEndPoint)chat.RemoteEndPoint;
            string sIPAdd = ep.Address.ToString();
            byte[] buff = new byte[1024];
            int len;
            while ((len = chat.Receive(buff)) != 0)
            {
                string msg = Encoding.Default.GetString(buff, 0, len);
                RichTextBox txtFIleNM = new RichTextBox();
                txtFIleNM.Text = msg.Trim();
                pack = msg.Substring(0, 1);
                string cmd = msg.Substring(1, 3);
                if (cmd == "DAT")
                {
                    if (DialogResult.Yes == MessageBox.Show(ep.Address.ToString() + " sent", "FIle", MessageBoxButtons.YesNoCancel))
                    {
                        SaveFileDialog sfdlg = new SaveFileDialog();
                        sfdlg.Title = "select File";
                        sfdlg.Filter = "(*.*)|*.*";
                        sfdlg.InitialDirectory = @"D:\";
                        sfdlg.FileName = "save";
                        if (sfdlg.ShowDialog() == DialogResult.OK)
                        {
                            fileName = sfdlg.FileName;
                            NewMethod(ep, buff, ref len, ref msg);
                        }
                    }
                }
                else if (cmd == "MSG")
                {
                    msg = Encoding.Default.GetString(buff);
                    frmChat message = (frmChat)Navigate.GetForm_byName("frmChat");
                    if (message == null)
                    {
                        MDIMain frmMDI = (MDIMain)Navigate.GetForm_byName("MDIMain");
                        message = new frmChat();
                        message._UserID_To = DB.GetSnglValue("SELECT UserID from tbl_UserMaster WHERE IsActive=1 AND IPAddress = " + CommonLogic.SQuote(sIPAdd));
                        message._ipAddress_To = sIPAdd;
                        message.msg = msg;
                        message.pack = "1";
                        message.ShowDialog();
                    }
                    else
                    {
                        message.msg = msg;
                        message.pack = "1";
                        message.ShowMsg(msg);
                        Application.DoEvents();
                    }
                }
            }
            chat.Close();
        }

        private void NewMethod(IPEndPoint ep, byte[] buff, ref int len, ref string msg)
        {
            string extName = msg.Substring(msg.LastIndexOf('.'));
            RichTextBox txtFIleNM = new RichTextBox();
            string[] sVal = extName.Split(' ');

            txtFIleNM.Text = fileName + sVal[0];
            FileStream writer = new FileStream(txtFIleNM.Text, FileMode.OpenOrCreate, FileAccess.Write);

            while ((len = chat.Receive(buff)) != 0)
            {
                msg = Encoding.Default.GetString(buff, 0, len);
                if (msg == "END")
                    break;
                writer.Write(buff, 0, len);
            }
            writer.Write(buff, 0, len);
            writer.Close();
        }
    }
}
