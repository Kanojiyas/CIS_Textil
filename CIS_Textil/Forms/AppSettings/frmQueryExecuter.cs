using CIS_DBLayer;
using CIS_Bussiness;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Data;

namespace CIS_Textil
{
    public partial class frmQueryExecuter : frmTrnsIface
    {
        private string FileName;
        private StreamReader sr;

        public frmQueryExecuter()
        {
            InitializeComponent();
        }

        # region Event
        private void frmQueryExecuter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                QueryExecute();
            }
        }

        private void frmQueryExecuter_Load(object sender, EventArgs e)
        {
            try
            {
                IniFile file = new IniFile(Application.StartupPath.ToString() + @"\Others\System.ini");
                TSp_ServerNm.Text = file.IniReadValue("DATABASESETTING", "ServerName");
                TSp_DbNm.Text = file.IniReadValue("DATABASESETTING", "DataBaseName");
                TSp_NoRec.Text = "0";
                TSp_Result.Text = "Connected";
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {
            this.QueryExecute();
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = this.OpenFileDialog;
                openFileDialog.Filter = "Sql File (.Sql)|*.sql|Text File (*.txt)|*.txt|XML File(.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = openFileDialog.FileName;
                    sr = new StreamReader(openFileDialog.OpenFile());
                    txt_Query.Text = sr.ReadToEnd();
                }
                openFileDialog = null;
            }
            catch (Exception exception1)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
                Navigate.logError(exception1.Message, exception1.StackTrace);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            StreamWriter writer = null;
            try
            {
                System.Windows.Forms.SaveFileDialog saveFileDialog = this.SaveFileDialog;
                saveFileDialog.FileName = FileName;
                saveFileDialog.Filter = "Sql File (.Sql)|*.sql|Text File (*.txt)|*.txt|XML File(.xml)|*.xml|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.FileName = saveFileDialog.FileName;
                    writer = new StreamWriter(this.FileName);
                    writer.Write(this.txt_Query.Text);
                }
                saveFileDialog = null;
            }
            catch (Exception exception1)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
                Navigate.logError(exception1.Message, exception1.StackTrace);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        private void QueryExecute()
        {
            try
            {
                grd_ResultGrid.DataSource = DB.GetDT(txt_Query.Text,false);
                grd_ResultGrid.Select();
                TSp_NoRec.Text = grd_ResultGrid.Rows.Count.ToString();
                TSp_Result.Text = "Query Executed successfully";
                //TSp_Result.Image = Image.FromFile(Application.StartupPath.ToString());
            }
            catch (Exception exception1)
            {
                this.TSp_Result.Text = "Query Completed with errors.";
                Navigate.logError(exception1.Message, exception1.StackTrace);
            }
        }

        #endregion
}
    }
