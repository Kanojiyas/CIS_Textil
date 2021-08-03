using System;
using System.Data;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmChildForm : Form
    {
        public int iMenuID = 0;
        public int iRemMenuID = 0;
        public frmChildForm()
        {
            InitializeComponent();
        }

        private void frmChildForm_Load(object sender, EventArgs e)
        {
            string MenuID = iMenuID + ",";
            using (IDataReader idr = DB.GetRS(string.Format("Select MenuID from  fn_MenuMaster_Comp() Where RefMenuID=" + iMenuID + "")))
            {
                while (idr.Read())
                {
                    MenuID += idr["MenuID"].ToString() + ",";
                }
            }
            MenuID = MenuID.Remove(MenuID.Length - 1);

            DataTable dt_Send = DB.GetDT(string.Format("Select MenuID,Form_Caption From fn_MenuMaster_tbl() Where MenuID in (" + MenuID + ")"), false);
            ((ListBox)ListChildMenu).DataSource = dt_Send;
            ((ListBox)ListChildMenu).DisplayMember = "Form_Caption";
            ((ListBox)ListChildMenu).ValueMember = "MenuID";
        }

        private void ListChildMenu_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                iRemMenuID = Localization.ParseNativeInt(ListChildMenu.SelectedValue.ToString());
                this.Close();
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace, iMenuID.ToString()); }
        }

        private void frmChildForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
                else if ((e.KeyCode == Keys.Enter))
                {
                    ListChildMenu_DoubleClick(null, null);
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }


    }
}
