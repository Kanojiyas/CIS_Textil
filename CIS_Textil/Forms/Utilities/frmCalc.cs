using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CIS_Bussiness;using CIS_DBLayer;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmCalc : Form
    {
        private double Memory;
        private string Operator = "";
        private bool period;
        private bool Status;
        private double Temp;
        private double Var1;
        private double var2;
        public object refParentControl;
        public bool isGrid;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public frmCalc()
        {
            InitializeComponent();
        }

        private void btn_Num_7_Click(object sender, EventArgs e)
        {
            if (!Status)
            {
                txtResult.Text = txtResult.Text + "7";
            }
            else
            {
                txtResult.Text = "7";
                Status = false;
            }
        }

        private void btn_Num_8_Click(object sender, EventArgs e)
        {
            if (!Status)
            {
                txtResult.Text = txtResult.Text + "8";
            }
            else
            {
                txtResult.Text = "7";
                Status = false;
            }
        }

        private void btn_Num_9_Click(object sender, EventArgs e)
        {
            if (!Status)
            {
                txtResult.Text = txtResult.Text + "9";
            }
            else
            {
                txtResult.Text = "9";
               Status = false;
            }
        }

        private void btn_Num_6_Click(object sender, EventArgs e)
        {
            if (!Status)
            {
                txtResult.Text = txtResult.Text + "6";
            }
            else
            {
                txtResult.Text = "6";
               Status = false;
            }
        }

        private void btn_Num_5_Click(object sender, EventArgs e)
        {
            if (!Status)
            {
                txtResult.Text = txtResult.Text + "5";
            }
            else
            {
                txtResult.Text = "5";
               Status = false;
            }
        }

        private void btn_Num_4_Click(object sender, EventArgs e)
        {
            if (!Status)
            {
                txtResult.Text = txtResult.Text + "4";
            }
            else
            {
                txtResult.Text = "4";
               Status = false;
            }
        }

        private void btn_Num_1_Click(object sender, EventArgs e)
        {
            if (!Status)
            {
                txtResult.Text = txtResult.Text + "1";
            }
            else
            {
                txtResult.Text = "1";
               Status = false;
            }
        }

        private void btn_Num_2_Click(object sender, EventArgs e)
        {
            if (!Status)
            {
                txtResult.Text = txtResult.Text + "2";
            }
            else
            {
                txtResult.Text = "2";
                Status = false;
            }
        }

        private void btn_Num_3_Click(object sender, EventArgs e)
        {
            if (!Status)
            {
                txtResult.Text = txtResult.Text + "3";
            }
            else
            {
                txtResult.Text = "3";
               Status = false;
            }
        }

        private void btn_Num_0_Click(object sender, EventArgs e)
        {
            if (!Status && txtResult.Text.Trim().Length > 0)
            {
                txtResult.Text = txtResult.Text + "0";
            }
        }

        private void btnNumPeriod_Click(object sender, EventArgs e)
        {
            if (!Status && !period)
            {
                if (txtResult.Text.Trim().Length > 0)
                {
                    txtResult.Text = txtResult.Text + ".";
                }
                else
                {
                    txtResult.Text = "0";
                }
                period = true;
            }
        }

        private void btnnumSign_Click(object sender, EventArgs e)
        {
            if (!Status && txtResult.Text.Trim().Length > 0)
            {
                Var1 = -1.0 * Localization.ParseNativeDouble(txtResult.Text);
                txtResult.Text = Var1.ToString();
            }
        }

        private void btn_Operator_Add_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Trim().Length != 0)
            {
                if (Operator == "")
                {
                    Var1 = Localization.ParseNativeDouble(txtResult.Text);
                    txtResult.Text = "";
                }
                else
                {
                    Calculate();
                }
                Operator = "Add";
                period = false;
                txtResult.Focus();
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Trim().Length != 0 && Var1 != 0.0)
            {
                Calculate();
                Operator = "";
                period = false;
            }
        }

        private void btn_Operator_Subt_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Trim().Length != 0)
            {
                if (Operator == "")
                {
                    Var1 = Localization.ParseNativeDouble(txtResult.Text);
                    txtResult.Text = "";
                }
                else
                {
                    Calculate();
                }
                Operator = "Sub";
                period = false;
                txtResult.Focus();
            }
        }

        private void btn_Operator_Multi_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Trim().Length != 0)
            {
                if (Operator == "")
                {
                    Var1 = Localization.ParseNativeDouble(txtResult.Text);
                    txtResult.Text = "";
                }
                else
                {
                    Calculate();
                }
                Operator = "Mult";
                period = false;
                txtResult.Focus();
            }
        }

        private void btn_Operator_div_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Trim().Length != 0)
            {
                if (Operator == "")
                {
                    Var1 = Localization.ParseNativeDouble(txtResult.Text);
                    txtResult.Text = "";
                }
                else
                {
                    Calculate();
                }
                Operator = "Div";
                period = false;
                txtResult.Focus();
            }
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Trim().Length != 0)
            {
                Temp = Localization.ParseNativeDouble(txtResult.Text);
                Temp = Math.Sqrt(Temp);
                txtResult.Text = Temp.ToString();
                period = false;
            }
        }

        private void btnPow_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Trim().Length != 0)
            {
                if (Operator == "")
                {
                    Var1 = Localization.ParseNativeDouble(txtResult.Text);
                    txtResult.Text = "";
                }
                else
                {
                    Calculate();
                }
                Operator = "Pow";
                period = false;
            }
        }

        private void btnInverse_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Trim().Length != 0)
            {
                Temp = Localization.ParseNativeDouble(txtResult.Text);
                Temp = 1.0 / Temp;
                txtResult.Text = Temp.ToString();
                period = false;
            }
        }

        private void btnMemoryPlus_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Trim().Length > 0)
            {
                Memory += Localization.ParseNativeDouble(txtResult.Text);
                btnMemStatus.Text = "M";
            }
        }

        private void btnMemoryMinus_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Trim().Length > 0)
            {
                Memory -= Localization.ParseNativeDouble(txtResult.Text);
                btnMemStatus.Text = "M";
            }
        }

        private void btnMemoryRecall_Click(object sender, EventArgs e)
        {
            if (btnMemStatus.Text == "M")
            {
                txtResult.Text = Memory.ToString();
                Status = true;
            }
        }

        private void btnCLR_Curr_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            period = false;
        }

        private void btnCLR_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            Var1 = 0.0;
            var2 = 0.0;
            Operator = "";
            period = false;
            txtResult.Focus();
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Trim().Length > 0)
            {
                char ch = txtResult.Text[txtResult.Text.Trim().Length - 1];
                if (ch.ToString() == ".")
                {
                    period = false;
                }
                short length = (short)txtResult.Text.Trim().Length;
                txtResult.Text = txtResult.Text.Remove(length - 1, 1);
            }
        }

        public void Calculate()
        {
            var2 = Localization.ParseNativeDouble(txtResult.Text);
            if (Operator == "Add")
            {
                Var1 += var2;
            }
            else if (Operator == "Sub")
            {
                Var1 -= var2;
            }
            else if (Operator == "Mult")
            {
                Var1 *= var2;
            }
            else if (Operator == "Div")
            {
                Var1 /= var2;
            }
            else
            {
                if (Operator == "Sqrt")
                {
                    return;
                }
                if (Operator == "Pow")
                {
                    Var1 = Math.Pow(Var1, var2);
                }
                else if (Operator == "Inve")
                {
                    return;
                }
            }
            txtResult.Text = Var1.ToString();
            txtResult.SelectionStart = txtResult.Text.ToCharArray().Length;
            txtResult.SelectionLength = txtResult.Text.Trim().Length;
            Status = true;
        }

        private void frmCalc_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            char ch = e.KeyChar;
            //if (char.IsNumber(ch))
            {
                if (txtResult.Text.Trim().Length > 0)
                {
                    Status = false;
                }
                else
                {
                    Status = true;
                }
                switch (e.KeyChar)
                {

                    case '+':
                        if (txtResult.Text.Trim().Length != 0)
                        {
                            if (Operator == "")
                            {
                                Var1 = Localization.ParseNativeDouble(txtResult.Text);
                                txtResult.Text = "";
                            }
                            Operator = "Add";
                            period = false;
                        }
                        return;

                    case '-':
                        if (txtResult.Text.Trim().Length != 0)
                        {
                            if (Operator == "")
                            {
                                Var1 = Localization.ParseNativeDouble(txtResult.Text);
                                txtResult.Text = "";
                            }
                            Operator = "Sub";
                            period = false;
                        }
                        return;

                    case '*':
                        if (txtResult.Text.Trim().Length != 0)
                        {
                            if (Operator == "")
                            {
                                Var1 = Localization.ParseNativeDouble(txtResult.Text);
                                txtResult.Text = "";
                            }
                            Operator = "Mult";
                            period = false;
                        }
                        return;

                    case '/':
                        if (txtResult.Text.Trim().Length != 0)
                        {
                            if (Operator == "")
                            {
                                Var1 = Localization.ParseNativeDouble(txtResult.Text);
                                txtResult.Text = "";
                            }
                            Operator = "Div";
                            period = false;
                        }
                        return;

                    case '.':
                        if (Status || period)
                        {
                            return;
                        }
                        if (txtResult.Text.Trim().Length <= 0)
                        {
                            txtResult.Text = "0.";
                            break;
                        }
                        txtResult.Text = txtResult.Text + ".";
                        break;
                    default:
                        return;
                }
                period = true;
            }


        }

        private void frmCalc_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Return) && ((txtResult.Text.Trim().Length != 0) && (Var1 != 0.0)))
            {
                Calculate();
                Operator = "";
                period = false;
            }
            else if (e.KeyData == (Keys.Control | Keys.C))
            {
                txtResult.Copy();
            }
            else if (e.KeyData == (Keys.Control | Keys.X))
            {
                txtResult.Cut();
                txtResult.Text = "";
            }
            else if (e.KeyCode == Keys.V)
            {
                txtResult.Paste();
            }
            else if (e.KeyData == (Keys.Control | Keys.A))
            {
                txtResult.SelectAll();
            }
            else if (e.KeyCode == Keys.C)
            {
                txtResult.Text = "0";
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (txtResult.Text.Trim().Length > 0)
                    txtResult.Text = "";
                else
                    this.Close();
            }
            else if (e.KeyCode==Keys.Insert)
            {
                try
                {
                    if (!isGrid)
                    {
                        TextBox text = (TextBox)refParentControl;
                        text.Text = txtResult.Text;
                    }
                    else
                    {
                        System.Windows.Forms.DataGridViewTextBoxEditingControl sCbo = (System.Windows.Forms.DataGridViewTextBoxEditingControl)refParentControl;
                        sCbo.EditingControlFormattedValue = txtResult.Text;

                    }
                    this.Close();
                }
                catch { }
            }
        }

        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            if ("-+/%*".Contains(txtResult.Text))
            {
                this.txtResult.Text = "";
            }
        }

        private void txtResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            int Ascii = Convert.ToChar(e.KeyChar);
            if (Ascii >= 48 && Ascii <= 57 || Ascii == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnMemStatus_Click(object sender, EventArgs e)
        {

        }

        private void frmCalc1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(base.Handle, 0xa1, 2, 0);
            }
        }
    }
}
