using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace Winners_ITI
{
    public partial class TradeMaster : Form
    {
        ClsBLInstitution objInstitution;
        ClsBLTrade objTrade;
        public TradeMaster()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTemp = (DataTable)objTrade.GetDataPerTrade(Convert.ToInt32(cmbTradeList.SelectedValue));
                DataRow drInfo = dtTemp.Rows[0];
                if (drInfo != null)
                {
                    txtID.Text = Convert.ToString(drInfo["Trade_ID"].ToString());
                    txtDesc.Text = Convert.ToString(drInfo["Name"].ToString());
                    cmbInstitute.SelectedValue = Convert.ToString(drInfo["Institution_ID"].ToString());

                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    cmbTradeList.SelectedValue = -1;
                }
             
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void TradeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                objInstitution = new ClsBLInstitution();
                DataTable dtUnivList = (DataTable)objInstitution.GetData();
                cmbInstitute.DataSource = dtUnivList;
                cmbInstitute.ValueMember = "Inst_ID";
                cmbInstitute.DisplayMember = "Name";
                cmbInstitute.SelectedValue = Common.Institution;
                cmbInstitute.Enabled = false;
                FillTrades();

                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillTrades()
        {
            objTrade = new ClsBLTrade();
            DataTable dtTradeList = (DataTable)objTrade.GetDataPerInstitution(Common.Institution);
            cmbTradeList.DataSource = dtTradeList;
            cmbTradeList.ValueMember = "Trade_ID";
            cmbTradeList.DisplayMember = "Name";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Validate() == false)
            {
                MessageBox.Show("Please fill required fields");
                return;
            }
            try
            {
                PropertyLayer.Trade objProp = new PropertyLayer.Trade();
                objProp.Trade_ID = Convert.ToInt32(txtID.Text.Trim());
                objProp.Name = txtDesc.Text.Trim();
                objProp.Institution_ID = Convert.ToInt32(cmbInstitute.SelectedValue);

                objTrade.InsertData(objProp);
                MessageBox.Show("Trade record registered successfully.");
                ClearData();
                FillTrades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Validate() == false)
            {
                MessageBox.Show("Please fill required fields");
                return;
            }
            try
            {
                PropertyLayer.Trade objProp = new PropertyLayer.Trade();
                objProp.Trade_ID = Convert.ToInt32(txtID.Text.Trim());
                objProp.Name = txtDesc.Text.Trim();
                objProp.Institution_ID = Convert.ToInt32(cmbInstitute.SelectedValue);

                objTrade.UpdateData(objProp);
                MessageBox.Show("Trade record updated successfully.");
                FillTrades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                objTrade.DeleteData(Convert.ToInt32(txtID.Text));
                MessageBox.Show("Trade record deleted successfully.");
                ClearData();
                FillTrades();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("conflicted with the REFERENCE constraint"))
                    MessageBox.Show("You cannnot delete a trade which is in use");
                else
                    MessageBox.Show(ex.Message);
            }
        }
        private void ClearData()
        {
            try
            {
                txtID.Text = Convert.ToString(BusinessLayer.Common.GetMaxID("Trade", "Trade_ID"));
                txtDesc.Text = "";

                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private bool Validate()
        {
            if (txtDesc.Text.Trim().Length == 0) return false;
            return true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }

}
