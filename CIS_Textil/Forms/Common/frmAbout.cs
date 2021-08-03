using System;
using System.Windows.Forms;
using CIS_Bussiness;using CIS_DBLayer;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            try
            {
                this.lblversion.Text = "Version : " + Db_Detials.AppVersion;
                this.lblLicense.Text = "License : " + Db_Detials.CompanyName;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

            //FileInfo info = new FileInfo(Application.StartupPath.ToString() + ClsSecure.cRegister());
            //if (info.Exists)
            //{
            //    switch (ClsSecure.CheckForRegister())
            //    {
            //        case 0:
            //            this.lblLicense.Text = this.lblLicense.Text + " Un-Register";
            //            break;

            //        case 1:
            //            this.lblLicense.Text = "License : Full";
            //            break;

            //        case 2:
            //            this.lblLicense.Text = this.lblLicense.Text + " Un-Register";
            //            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Invalid file", "Invalid file");
            //            break;
            //    }
            //}
            //else
            //{
            //    this.lblLicense.Text = this.lblLicense.Text + " Un-Register";
            //}
        }

        private void frmAbout_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape))
            {
                try
                {
                    this.Close();
                }
                catch
                {
                }
            }
        }
    }
}
