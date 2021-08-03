using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using CIS_CLibrary;
using Microsoft.VisualBasic;

namespace CIS_Textil
{
    public partial class frmMenuSettings : frmMasterIface
    {
        private int mParentID;
        private ArrayList AryChangedValues;
        private bool blnIsEdited;

        public frmMenuSettings()
        {
            InitializeComponent();
        }

        private void frmMenuSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.mParentID == 0)
            {
                CommonCls.LoadMDIMenu();
            }
        }

        private void frmMenuSettings_Load(object sender, EventArgs e)
        {
            try
            {
                ComboBox cboObjProp = this.cboObjProp;
                cboObjProp.Items.Clear();
                cboObjProp.Items.Add("Add New Menu Item");

                if (this.mParentID != 0)
                {
                    cboObjProp.Items.Add("Add New Separator");
                }

                this.cboObjProp.SelectedIndex = 0;
                DataGridView dgProperty = this.dgProperty;
                dgProperty.ColumnCount = 2;
                dgProperty.RowCount = 14;
                dgProperty.ColumnHeadersHeight = 40;
                dgProperty.RowHeadersVisible = false;
                dgProperty.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgProperty.AllowUserToResizeRows = false;
                dgProperty.AllowUserToResizeColumns = false;
                dgProperty.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgProperty.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                dgProperty.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgProperty.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dgProperty.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgProperty.ColumnHeadersVisible = false;
                dgProperty.Columns[0].Width = 150;
                dgProperty.Columns[1].Width = 190;

                dgProperty.Rows[0].Cells[0].Value = "Menu ID";
                dgProperty.Rows[0].Cells[1].Value = "";
                dgProperty.Rows[0].Cells[1].ReadOnly = true;
                dgProperty.Rows[0].Cells[1].Style.BackColor = Color.LightCyan;

                dgProperty.Rows[1].Cells[0].Value = "Parent ID";
                dgProperty.Rows[1].Cells[1].Value = "";
                dgProperty.Rows[1].Cells[1].ReadOnly = true;
                dgProperty.Rows[1].Cells[1].Style.BackColor = Color.LightCyan;

                dgProperty.Rows[2].Cells[0].Value = "Order By";
                dgProperty.Rows[2].Cells[1].Value = "";
                dgProperty.Rows[2].Cells[1].ReadOnly = true;
                dgProperty.Rows[2].Cells[1].Style.BackColor = Color.LightCyan;

                dgProperty.Rows[3].Cells[0].Value = "Menu Caption";
                dgProperty.Rows[3].Cells[1].Value = "";

                dgProperty.Rows[4].Cells[0].Value = "Form Caption";
                dgProperty.Rows[4].Cells[1].Value = "";

                dgProperty.Rows[5].Cells[0].Value = "Toop Tip";
                dgProperty.Rows[5].Cells[1].Value = "";

                dgProperty.Rows[6].Cells[0].Value = "Table Main Name";
                dgProperty.Rows[6].Cells[1].Value = "";

                dgProperty.Rows[7].Cells[0].Value = "Primary Col";
                dgProperty.Rows[7].Cells[1].Value = "";

                dgProperty.Rows[8].Cells[0].Value = "Search Query";
                dgProperty.Rows[8].Cells[1].Value = "";

                dgProperty.Rows[9].Cells[0].Value = "Search Query Dtls";
                dgProperty.Rows[9].Cells[1].Value = "";

                dgProperty.Rows[10].Cells[0].Value = "Form To Call";
                dgProperty.Rows[10].Cells[1].Value = "";

                dgProperty.Rows[11].Cells[0].Value = "Form To Call Web";
                dgProperty.Rows[11].Cells[1].Value = "";

                dgProperty.Rows[12].Cells[0].Value = "Form Type";
                dgProperty.Rows[12].Cells[1].Value = "";

                dgProperty.Rows[13].Cells[0].Value = "Menu Type";
                dgProperty.Rows[13].Cells[1].Value = "";

                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                DataGridViewTextBoxCell dataGridViewCell = new DataGridViewTextBoxCell();
                dataGridViewCell.Value = "Is Form";
                dataGridViewRow.Cells.Add(dataGridViewCell);

                DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                cell.Items.AddRange(new object[] { "Yes", "No" });
                dataGridViewRow.Cells.Add(cell);

                dgProperty.Rows.Insert(14, dataGridViewRow);

                DataGridViewRow row3 = new DataGridViewRow();

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = "Is Separator";
                row3.Cells.Add(cell5);

                DataGridViewComboBoxCell cell6 = new DataGridViewComboBoxCell();
                cell6.Items.AddRange(new object[] { "Yes", "No" });
                row3.Cells.Add(cell6);

                dgProperty.Rows.Insert(15, row3);

                DataGridViewRow row2 = new DataGridViewRow();

                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = "Drop Down Item";
                row2.Cells.Add(cell2);

                DataGridViewButtonCell cell3 = new DataGridViewButtonCell();
                cell3.Value = "...";
                row2.Cells.Add(cell3);
                dgProperty.Rows.Insert(16, row2);

                for (int i = 0; i <= dgProperty.RowCount - 1; i++)
                {
                    dgProperty.Rows[i].Cells[0].Style.BackColor = Color.LightCyan;
                    dgProperty.Rows[i].Cells[0].ReadOnly = true;
                    dgProperty.Rows[i].Cells[0].Style.Font = new Font("Verdana", 8f, FontStyle.Bold, GraphicsUnit.Point, 0);
                }
                this.pro_RestoreTree();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.tvList.SelectedNode != null)
                {
                    if (Conversion.Val(this.tvList.SelectedNode.Name.ToString()) != 0.0)
                    {
                        using (DataTable table = DB.GetDT(string.Format("Select * From {0} Where MenuID = {1} Order By [OrderBy]", "tbl_MenuMaster", tvList.SelectedNode.Name.ToString()), false))
                        {
                            if (table.Rows.Count > 0)
                            {
                                dgProperty.Rows[0].Cells[1].Value = Conversion.Val(table.Rows[0]["MenuID"].ToString());
                                dgProperty.Rows[1].Cells[1].Value = Conversion.Val(table.Rows[0]["ParentID"].ToString());
                                dgProperty.Rows[2].Cells[1].Value = this.tvList.SelectedNode.Index;
                                dgProperty.Rows[3].Cells[1].Value = table.Rows[0]["Menu_Caption"].ToString();
                                this.lblGroupNm.Text = table.Rows[0]["Menu_Caption"].ToString();

                                dgProperty.Rows[4].Cells[1].Value = table.Rows[0]["Form_Caption"].ToString();
                                dgProperty.Rows[5].Cells[1].Value = table.Rows[0]["ToolTip"].ToString();
                                dgProperty.Rows[6].Cells[1].Value = table.Rows[0]["TblName_Main"].ToString();
                                dgProperty.Rows[7].Cells[1].Value = table.Rows[0]["PmryColumn"].ToString();

                                dgProperty.Rows[8].Cells[1].Value = table.Rows[0]["SearchQry"].ToString();
                                dgProperty.Rows[9].Cells[1].Value = table.Rows[0]["SearchQry_Dtls"].ToString();
                                dgProperty.Rows[10].Cells[1].Value = table.Rows[0]["FormCall"].ToString();
                                dgProperty.Rows[11].Cells[1].Value = table.Rows[0]["FormCall_Web"].ToString();
                                dgProperty.Rows[12].Cells[1].Value = table.Rows[0]["FormType"].ToString();
                                dgProperty.Rows[13].Cells[1].Value = table.Rows[0]["MenuType"].ToString();

                                DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                                cell = (DataGridViewComboBoxCell)dgProperty.Rows[14].Cells[1];
                                cell.Value = (Localization.ParseBoolean(table.Rows[0]["IsForm"].ToString()) ? "Yes" : "No");

                                DataGridViewComboBoxCell cell2 = new DataGridViewComboBoxCell();
                                cell2 = (DataGridViewComboBoxCell)dgProperty.Rows[15].Cells[1];
                                cell2.Value = (Localization.ParseBoolean(table.Rows[0]["IsSeparator"].ToString()) ? "Yes" : "No");

                                if (this.mParentID == 0)
                                {
                                    cell2.ReadOnly = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        dgProperty.Rows[0].Cells[1].Value = "<New MenuID>";
                        dgProperty.Rows[1].Cells[1].Value = this.mParentID;
                        dgProperty.Rows[2].Cells[1].Value = Conversion.Val(this.tvList.GetNodeCount(false));
                        dgProperty.Rows[3].Cells[1].Value = this.tvList.SelectedNode.Text.ToString();
                        dgProperty.Rows[4].Cells[1].Value = "-";
                        dgProperty.Rows[5].Cells[1].Value = "-";
                        dgProperty.Rows[6].Cells[1].Value = "-";
                        dgProperty.Rows[7].Cells[1].Value = "-";
                        dgProperty.Rows[8].Cells[1].Value = "-";
                        dgProperty.Rows[9].Cells[1].Value = "-";
                        dgProperty.Rows[10].Cells[1].Value = "-";
                        dgProperty.Rows[11].Cells[1].Value = "-";
                        dgProperty.Rows[12].Cells[1].Value = "-";
                        dgProperty.Rows[13].Cells[1].Value = "-";
                        DataGridViewComboBoxCell cell3 = new DataGridViewComboBoxCell();
                        cell3 = (DataGridViewComboBoxCell)dgProperty.Rows[14].Cells[1];
                        cell3.Value = "Yes";
                        DataGridViewComboBoxCell cell4 = new DataGridViewComboBoxCell();
                        cell4 = (DataGridViewComboBoxCell)dgProperty.Rows[15].Cells[1];
                        cell4.Value = "No";
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                tvList.Nodes.Add(cboObjProp.SelectedItem.ToString());
                TreeNode node = this.tvList.Nodes[this.tvList.GetNodeCount(false) - 1];
                TreeView treeView = node.TreeView;
                treeView.SelectedNode = node;
                treeView.Select();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dgProperty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == 16)
                {
                    frmMenuSettings settings2 = new frmMenuSettings();
                    settings2.mParentID = Localization.ParseNativeInt(this.dgProperty.Rows[0].Cells[1].Value.ToString());
                    settings2.iIDentity = this.iIDentity;
                    settings2.ShowInTaskbar = false;
                    settings2.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void dgProperty_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                switch (e.RowIndex)
                {
                    case 3:
                        {
                            int iCount = Localization.ParseNativeInt(dgProperty.Rows[2].Cells[1].Value.ToString()) - 1;
                            tvList.Nodes[iCount].Text = dgProperty.Rows[3].Cells[1].Value.ToString();
                            dgProperty.Rows[4].Cells[1].Value = dgProperty.Rows[3].Cells[1].Value;
                            blnIsEdited = true;
                            break;
                        }
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        this.blnIsEdited = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void dgProperty_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void pro_RestoreTree()
        {
            this.tvList.Nodes.Clear();
            using (DataTable table = DB.GetDT(string.Format("Select MenuID, Menu_Caption From {0} Where ParentID = {1} Order By [OrderBy]", "tbl_MenuMaster", this.mParentID), false))
            {
                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i <= table.Rows.Count - 1; i++)
                    {
                        this.tvList.Nodes.Add(table.Rows[i]["MenuID"].ToString(), table.Rows[i]["Menu_Caption"].ToString());
                    }
                }
            }
            int index = 0;
            do
            {
                this.dgProperty.Rows[index].Cells[1].Value = "";
                index++;
            }
            while (index <= 12);
            try
            {
                this.tvList.Nodes[0].TreeView.Select();
            }
            catch (Exception exception1)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This is Final Menu. You can Add or Remove Form.");
                Navigate.logError(exception1.Message, exception1.StackTrace);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (blnIsEdited)
                {
                    this.UpdateRecord();
                }
                TreeNode selectedNode = tvList.SelectedNode;
                this.MoveUp(ref selectedNode);
                tvList.SelectedNode = selectedNode;
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.blnIsEdited)
                {
                    this.UpdateRecord();
                }
                TreeView tvList = this.tvList;
                TreeNode selectedNode = tvList.SelectedNode;
                this.MoveDown(ref selectedNode);
                tvList.SelectedNode = selectedNode;
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void MoveUp(ref TreeNode node)
        {
            try
            {
                TreeNode parent = node.Parent;
                TreeView treeView = node.TreeView;
                if (parent != null)
                {
                    int index = parent.Nodes.IndexOf(node);
                    if (index > 0)
                    {
                        parent.Nodes.RemoveAt(index);
                        parent.Nodes.Insert(index - 1, node);
                    }
                }
                else if (node.TreeView.Nodes.Contains(node))
                {
                    int index = treeView.Nodes.IndexOf(node);
                    if (index > 0)
                    {
                        treeView.Nodes.RemoveAt(index);
                        treeView.Nodes.Insert(index - 1, node);
                        treeView.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void MoveDown(ref TreeNode node)
        {
            try
            {
                TreeNode parent = node.Parent;
                TreeView treeView = node.TreeView;
                if (parent != null)
                {
                    int index = parent.Nodes.IndexOf(node);
                    if (index != (parent.Nodes.Count - 1))
                    {
                        parent.Nodes.RemoveAt(index);
                        parent.Nodes.Insert(index + 1, node);
                    }
                }
                else if ((treeView != null) & treeView.Nodes.Contains(node))
                {
                    int index = treeView.Nodes.IndexOf(node);
                    if (index < (treeView.Nodes.Count - 1))
                    {
                        treeView.Nodes.RemoveAt(index);
                        treeView.Nodes.Insert(index + 1, node);
                        treeView.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tvList.SelectedNode != null)
                {
                    if (Localization.ParseNativeDouble(tvList.SelectedNode.Name.ToString()) != 0.0)
                    {
                        using (DataTable table = DB.GetDT(string.Format("Select * From {0} Where ParentID = {1}", "tbl_MenuMaster", tvList.SelectedNode.Name.ToString()), false))
                        {
                            if (table.Rows.Count > 0)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Can't Delete this group, it have sub item(s).");
                            }
                            else if (CIS_Utilities.CIS_Dialog.Show("Are you sure to delete this entry ?", "Crocus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                TreeNode selectedNode = this.tvList.SelectedNode;
                                this.tvList.Nodes.Remove(selectedNode);
                                DB.ExecuteSQL(string.Format("Delete  From {0} Where MenuID = {1};", "tbl_MenuMaster", selectedNode.Name.ToString()).ToString());
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Success", "Record Deleted Successfully.");
                            }
                        }
                    }
                    else
                    {
                        TreeNode node = this.tvList.SelectedNode;
                        this.tvList.Nodes.Remove(node);
                    }
                }
                this.tvList.Nodes[0].TreeView.Select();
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.UpdateRecord();
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void UpdateRecord()
        {
            if (CIS_Utilities.CIS_Dialog.Show("The current record has been change, do you want to update ?", "Crocus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    StringBuilder strQry = new StringBuilder();
                    string str = string.Empty;
                    if (Localization.ParseNativeDouble(dgProperty.Rows[0].Cells[1].Value.ToString()) != 0.0)
                    {
                        strQry.Append("OrderBy = " + tvList.SelectedNode.Index.ToString() + ", ");
                        strQry.Append("Menu_Caption = '" + dgProperty.Rows[3].Cells[1].Value + "' ,");
                        strQry.Append("Form_Caption = '" + dgProperty.Rows[4].Cells[1].Value + "',");
                        strQry.Append("ToolTip = '" + dgProperty.Rows[5].Cells[1].Value + "',");
                        strQry.Append("TblName_Main = '" + dgProperty.Rows[6].Cells[1].Value + "',");
                        strQry.Append("PmryColumn = '" + dgProperty.Rows[7].Cells[1].Value + "',");
                        strQry.Append("SearchQry = '" + dgProperty.Rows[8].Cells[1].Value + "',");
                        strQry.Append("SearchQry_Dtls = '" + dgProperty.Rows[9].Cells[1].Value + "',");
                        strQry.Append("FormCall = '" + dgProperty.Rows[10].Cells[1].Value + "',");
                        strQry.Append("FormCall_Web = '" + dgProperty.Rows[11].Cells[1].Value + "',");
                        strQry.Append("FormType = '" + dgProperty.Rows[12].Cells[1].Value + "',");
                        strQry.Append("MenuType = '" + dgProperty.Rows[13].Cells[1].Value + "',");
                        strQry.Append("IsForm = " + (dgProperty.Rows[14].Cells[1].Value.ToString().ToUpper() == "YES" ? 1 : 0) + ", ");
                        strQry.Append("IsSeparator = " + (dgProperty.Rows[15].Cells[1].Value.ToString().ToUpper() == "YES" ? 1 : 0) + " ");
                        str = string.Format("Update {0} Set {1} Where MenuID = {2} And ParentID = {3};", new object[] { "tbl_MenuMaster", strQry.ToString(), Localization.ParseNativeDouble(dgProperty.Rows[0].Cells[1].Value.ToString()), Localization.ParseNativeDouble(dgProperty.Rows[1].Cells[1].Value.ToString()) });
                    }
                    else
                    {
                        int cell_1 = 0, cell_2 = 0, cell_3 = 0;
                        cell_1 = (dgProperty.Rows[12].Cells[1].Value.ToString().ToUpper() == "YES" ? 1 : 0);
                        cell_2 = (dgProperty.Rows[13].Cells[1].Value.ToString().ToUpper() == "YES" ? 1 : 0);
                        cell_3 = (dgProperty.Rows[14].Cells[1].Value.ToString().ToUpper() == "YES" ? 1 : 0);

                        //DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                        //cell = (DataGridViewComboBoxCell)dgProperty.Rows[12].Cells[1];

                        //DataGridViewComboBoxCell cell2 = new DataGridViewComboBoxCell();
                        //cell2 = (DataGridViewComboBoxCell)dgProperty.Rows[13].Cells[1];

                        //DataGridViewComboBoxCell cell3 = new DataGridViewComboBoxCell();
                        //cell3 = (DataGridViewComboBoxCell)dgProperty.Rows[14].Cells[1];

                        //int iMenuID = Localization.ParseNativeInt(DB.GetSnglValue("Select Isnull(Max(MenuID),0)+1 From tbl_MenuMaster"));
                        str = string.Format("Insert Into {0} (ParentID,OrderBy,Menu_Caption,Form_Caption,ToolTip,TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,IsForm,IsSeparator,ApplyYear,IsVisible) Values({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, 1);",
                            new object[] { "tbl_MenuMaster", this.mParentID, dgProperty.Rows[2].Cells[1].Value, DB.SQuote(dgProperty.Rows[3].Cells[1].Value.ToString()),
                                DB.SQuote(dgProperty.Rows[4].Cells[1].Value.ToString()), DB.SQuote(dgProperty.Rows[5].Cells[1].Value.ToString()), DB.SQuote(dgProperty.Rows[6].Cells[1].Value.ToString()), 
                                DB.SQuote(dgProperty.Rows[7].Cells[1].Value.ToString()), DB.SQuote(dgProperty.Rows[8].Cells[1].Value.ToString()), DB.SQuote(dgProperty.Rows[9].Cells[1].Value.ToString()),
                                DB.SQuote(dgProperty.Rows[10].Cells[1].Value.ToString()), DB.SQuote(dgProperty.Rows[11].Cells[1].Value.ToString()), DB.SQuote(dgProperty.Rows[12].Cells[1].Value.ToString()),
                                DB.SQuote(dgProperty.Rows[13].Cells[1].Value.ToString()), cell_1, cell_2, cell_3 });
                    }
                    int iNodeCount = this.tvList.GetNodeCount(false) - 1;
                    for (int i = 0; i <= iNodeCount; i++)
                    {
                        if (Localization.ParseNativeDouble(tvList.Nodes[i].Name.ToString()) != 0.0)
                        {
                            str = str + string.Format("Update {0} Set OrderBy = {1} Where MenuID = {2} And ParentID = {3};", new object[] { "tbl_MenuMaster", i, Localization.ParseNativeDouble(tvList.Nodes[i].Name.ToString()), mParentID });
                        }
                    }
                    if (str.ToString().Length != 0)
                    {
                        DB.ExecuteSQL(str.ToString());
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Success", "Records Updated Successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
            this.blnIsEdited = false;
        }
    }
}
