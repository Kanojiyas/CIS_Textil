using System;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using CIS_Textil;

static class AppStart
{
    public static int IsBool = 0;
    public static bool IsExit = false;
    public static bool isSuccessFullLogIn = false;
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Db_Detials.IsRuntime = true;
        while (!IsExit)
        {
            if (IsBool == 0)
            {
                try
                {
                    Application.Run(new frmIntro());
                }
                catch
                {

                }
            }

            if (IsBool == 5)
            {
                Application.Run(new frmDBSettings());
            }

            if (IsBool == 1)
            {
                //int icount = Localization.ParseNativeInt(DB.GetSnglValue("Select Count(0) From tbl_CompanyMaster Where IsDeleted=0"));
                //if (icount > 0)
                    Application.Run(new frmLogin());
                //else
                //{
                //    Application.Run(new frmCompCreation());
                //    //frmCompCreation frm = new frmCompCreation();
                //    //if (frm.ShowDialog() == DialogResult.Cancel)
                //    //{
                //    //    frm.Dispose();
                //    //    return;
                //    //}

                //}
            }
            if (IsBool == 2)
            {
                Application.Run(new frmCompSelection());
            }
            if (IsBool == 3)
            {
                Application.Run(new MDIMain());
            }
            if (IsBool == 4)
            {
                Application.Run(new frmCompCreation());
            }

            if (IsExit)
            {
                Environment.Exit(6);
                Environment.ExitCode = 6;
                Application.ExitThread();
                break;
            }
        }
        CIS_Utilities.CIS_Dialog.Show("This system is not register,  please contact your software dealer.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        System.Environment.Exit(Localization.ParseNativeInt(CloseReason.ApplicationExitCall.ToString()));
        return;
    }
}
