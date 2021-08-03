using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Crocus_Bussiness;
using System.Runtime.CompilerServices;

namespace CIS_Textil
{
    public partial class frmRadView : frmIface
    {
        public frmRadView()
        {
            InitializeComponent();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            }
            catch { }
        }

        private void frmRadView_Load(object sender, EventArgs e)
        {
            CIS_GridReportTool.frmReportTool_Level frm = new CIS_GridReportTool.frmReportTool_Level();
            CIS_GridReportTool.frmReportTool_Level.iCompID = Db_Detials.CompID;
            CIS_GridReportTool.frmReportTool_Level.iYearID = Db_Detials.YrID;
            CIS_GridReportTool.frmReportTool_Level.iUserID = Db_Detials.UserID;
            CIS_GridReportTool.frmReportTool_Level.iMenuID = base.iIDentity;
            frm.Text = "Rad View";
            frm.ShowDialog();
            frm.MdiParent = this;            
            this.Close();

            
        }
    }
}
