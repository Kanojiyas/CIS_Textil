using System;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using  CIS_Bussiness;using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmChat : Form
    {
        public string _UserID_To;
        public string _ipAddress_To;

        public Socket socketReceive = null;
        public Socket socketSent = null;
        public IPEndPoint ipReceive = null;
        public IPEndPoint ipSent = null;
        public Socket chat = null;
        public static Thread tBroadCast;
        private string sUserName;
        public string pack;
        public string msg;
        public bool isCreateFrm;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public frmChat()
        {
            InitializeComponent();
        }

        private void frmChat_Load(object sender, EventArgs e)
        {
            try
            {
                lblUserNM.Text = DB.GetSnglValue("SELECT EmployeeName from tbl_UserMaster WHERE UserID = " + _UserID_To);
                sUserName = DB.GetSnglValue("SELECT EmployeeName from tbl_UserMaster WHERE UserID = " + Db_Detials.UserID);

                string sMessage = "";
                using (IDataReader iDr = DB.GetRS("SELECT (EMployeeName_Sender + ': ' + Message) AS Message  FROM fn_ChatDtls() WHERE UserID_Sender IN(" + Db_Detials.UserID + "," + _UserID_To + ") AND YEAR(ChatDt)=YEAR(GETDATE()) AND MONTH(ChatDt)=MONTH(GETDATE()) AND DAY(ChatDt)=DAY(GETDATE())"))
                {
                    while (iDr.Read())
                    {
                        sMessage += iDr["Message"].ToString() + "\u2028";
                    }
                }

                txtHistory.Text += sMessage;

                if (msg != null)
                {
                    isCreateFrm = false;
                    if (msg.Length > 0)
                    {
                        RichTextBox txtmsg = new RichTextBox();
                        txtmsg.Text = lblUserNM.Text + ": " + msg.Substring(4);
                        txtmsg.SelectionFont = new Font(txtmsg.SelectionFont, FontStyle.Italic);
                        txtHistory.Text += txtmsg.Text + "\u2028";
                    }
                }
            }
            catch (Exception ex) 
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnSentMessage_Click(object sender, EventArgs e)
        {
            string msg = "MSG " + txtMessage.Text;
            try
            {
                socketSent = new Socket(AddressFamily.InterNetwork,
                               SocketType.Stream,
                               ProtocolType.Tcp);
                ipSent = new IPEndPoint(IPAddress.Parse(_ipAddress_To), 8001);

                socketSent.Connect(ipSent);
                socketSent.Send(Encoding.Default.GetBytes("0"));
                socketSent.Send(Encoding.Default.GetBytes(msg));
                socketSent.Close();

                RichTextBox txtmsg = new RichTextBox();
                txtmsg.Text = msg.Substring(4);
                txtmsg.SelectionFont = new Font(txtmsg.SelectionFont, FontStyle.Italic);

                txtHistory.Text += sUserName + ": " + txtMessage.Text + "\u2028";

                txtMessage.Text = "";
                txtMessage.Focus();

                try
                {
                    DB.ExecuteSQL(string.Format("INSERT INTO tbl_Chat VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, 1, 1, getdate(), 0, getdate())",
                        Db_Detials.UserID, CommonLogic.SQuote(CommonCls.GetIP()), CommonLogic.SQuote(System.Environment.MachineName),
                        _UserID_To, CommonLogic.SQuote(_ipAddress_To), "NULL", CommonLogic.SQuote(txtmsg.Text)
                    ));
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void ShowMsg(string sMsg)
        {
            RichTextBox txtmsg = new RichTextBox();
            txtmsg.Text = sMsg;
            txtmsg.SelectionFont = new Font(txtmsg.SelectionFont, FontStyle.Italic);

            SetText(lblUserNM.Text + ": " + txtmsg.Text.Substring(4) + "\u2028");
            //try
            //{
            //    DB.ExecuteSQL(string.Format("INSERT INTO tbl_Chat VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6} 1, 1, getdate(), 0, getdate())",
            //            Db_Detials.UserID, CommonLogic.SQuote(CommonCls.GetIP()), CommonLogic.SQuote(System.Environment.MachineName),
            //            _UserID_To, CommonLogic.SQuote(_ipAddress_To), "NULL", CommonLogic.SQuote(txtmsg.Text)
            //        ));
            //}
            //catch { }
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.txtHistory.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtHistory.Text += text;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(base.Handle, 0xa1, 2, 0);
            }
        }

        private void btnSentFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    socketSent = new Socket(AddressFamily.InterNetwork,
                                   SocketType.Stream,
                                   ProtocolType.Tcp);
                    ipSent = new IPEndPoint(IPAddress.Parse(_ipAddress_To), 8001);

                    ClassSocket socketConnet = new ClassSocket(socketSent, ipSent);
                    Thread tConnection = new Thread(new ThreadStart(socketConnet.SocketConnect));
                    tConnection.Start();

                    Thread.Sleep(100);

                    ClassSentFile sentFile = new ClassSentFile(dlg, socketSent);
                    Thread tSentFile = new Thread(new ThreadStart(sentFile.SentFile));
                    tSentFile.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            try
            {
                DB.ExecuteSQL("UPDATE tbl_Chat SET IsDeleted=1 WHERE UserID_Sender IN(" + Db_Detials.UserID + "," + _UserID_To + ") AND UserID_Receiver IN(" + Db_Detials.UserID + "," + _UserID_To + ")");
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "Success", "History Cleared Successfully...");

                txtHistory.Text = "";

                string sMessage = "";
                using (IDataReader iDr = DB.GetRS("SELECT (EMployeeName_Sender + ': ' + Message) AS Message  FROM fn_ChatDtls() WHERE UserID_Sender IN(" + Db_Detials.UserID + "," + _UserID_To + ") AND YEAR(ChatDt)=YEAR(GETDATE()) AND MONTH(ChatDt)=MONTH(GETDATE()) AND DAY(ChatDt)=DAY(GETDATE())"))
                {
                    while (iDr.Read())
                    {
                        sMessage += iDr["Message"].ToString() + "\u2028";
                    }
                }

                //txtHistory.Text += sMessage;
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Error", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void ciS_Panel1_CloseClick(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    
    }
}
