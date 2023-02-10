using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PropertyLayer;
using BusinessLayer;

namespace Winners_ITI
{
    public partial class TradeFeeEntry : Form
    {
        Cls_BL_TradeFeeDetails objTradeFeeDetails;
        ClsBLBatchDetails objBatchDetails;
        ClsBLTrade objTrade;
        ClsBLInstitution objInstitution;
        public TradeFeeEntry()
        {
            InitializeComponent();
        }

        private void TradeFeeEntry_Load(object sender, EventArgs e)
        {
            try
            {
                objInstitution = new ClsBLInstitution();
                DataTable dtUnivList = (DataTable)objInstitution.GetData();
                cmbInstituion.DataSource = dtUnivList;
                cmbInstituion.ValueMember = "Inst_ID";
                cmbInstituion.DisplayMember = "Name";

                objTrade = new ClsBLTrade();
                objTradeFeeDetails = new Cls_BL_TradeFeeDetails();
                DataTable dtTradeList = (DataTable)objTrade.GetDataPerInstitution(Common.Institution);
                cmbTrade.DataSource = dtTradeList;
                cmbTrade.ValueMember = "Trade_ID";
                cmbTrade.DisplayMember = "Name";

                LoadTradeFeeList();
                objBatchDetails = new ClsBLBatchDetails();
                DataTable dtBatch = (DataTable)objBatchDetails.GetData();
                cmbBatch.DataSource = dtBatch;
                cmbBatch.ValueMember = "Batch_ID";
                cmbBatch.DisplayMember = "Batch";
                ClearData();

                cmbInstituion.SelectedValue = Common.Institution;
                cmbInstituion.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadTradeFeeList()
        {
            try
            {
                DataTable dtTradeFeeList = (DataTable)objTradeFeeDetails.SelectDataForCombo(Common.Institution);
                cmbTFList.DataSource = dtTradeFeeList;
                cmbTFList.ValueMember = "TF_ID";
                cmbTFList.DisplayMember = "Desc";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validate() == false)
                {
                    MessageBox.Show("Please select valid Batch/Trade");
                    return;
                }
                PropertyLayer.Trade_FeeDetails objProp = new PropertyLayer.Trade_FeeDetails();
                objProp.TF_ID = Convert.ToInt32(txtID.Text.Trim());
                objProp.Trade_ID = Convert.ToInt32(cmbTrade.SelectedValue);
                objProp.Batch_ID = Convert.ToInt32(cmbBatch.SelectedValue);
                objProp.Inst_ID = Common.Institution;
                objProp.Registration_Fee = txtAdmissionFee.Value;
                objProp.Caution_Fee = txtCautioinFee.Value;
                objProp.Sem1_Fee = txtSem1.Value;
                objProp.Sem2_Fee = txtSem2.Value;
                objProp.Sem3_Fee = txtSem3.Value;
                objProp.Sem4_Fee = txtSem4.Value;
                objProp.Monthly_Fee = txtMonthlyFee .Value;
                objProp.No_OfInstallments = Convert.ToInt32( txtNumOfInstal.Value);
                objProp.Record_Fee = txtRecFee.Value;
                objProp.Uniform_Fee = txtUniformFee.Value;

                objProp.Registration_Fee_Dt = dtAdmissionFeeDate.Value;
                objProp.Caution_Fee_Dt = dtCautionFeeDate.Value;
                objProp.Sem1_Fee_Dt = dtSem1FeeDate.Value;
                objProp.Sem2_Fee_Dt = dtSem2FeeDate.Value;
                objProp.Sem3_Fee_Dt = dtSem3FeeDate.Value;
                objProp.Sem4_Fee_Dt = dtSem4FeeDate.Value;
                objProp.Monthly_Fee_Dt = dtMonthlyFeeDate.Value;

                objTradeFeeDetails .InsertData(objProp);
                MessageBox.Show("Trade Fee record registered successfully.");
                ClearData();
                LoadTradeFeeList();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validate() == false)
                {
                    MessageBox.Show("Please select valid Batch/Trade");
                    return;
                }
                PropertyLayer.Trade_FeeDetails objProp = new PropertyLayer.Trade_FeeDetails();
                objProp.TF_ID = Convert.ToInt32(txtID.Text.Trim());
                objProp.Trade_ID = Convert.ToInt32(cmbTrade.SelectedValue);
                objProp.Batch_ID = Convert.ToInt32(cmbBatch.SelectedValue);
                objProp.Inst_ID = Common.Institution;
                objProp.Registration_Fee = txtAdmissionFee.Value;
                objProp.Caution_Fee = txtCautioinFee.Value;
                objProp.Sem1_Fee = Convert.ToDecimal(txtSem1.Text.Trim());
                objProp.Sem2_Fee = Convert.ToDecimal(txtSem2.Text.Trim()); ;
                objProp.Sem3_Fee = Convert.ToDecimal(txtSem3.Text.Trim());
                objProp.Sem4_Fee = Convert.ToDecimal(txtSem4.Text.Trim());
                objProp.Monthly_Fee = txtMonthlyFee.Value;
                objProp.No_OfInstallments =Convert.ToInt32( txtNumOfInstal.Value);
                objProp.Record_Fee = txtRecFee.Value;
                objProp.Uniform_Fee =txtUniformFee.Value;

                objProp.Registration_Fee_Dt = dtAdmissionFeeDate.Value;
                objProp.Caution_Fee_Dt = dtCautionFeeDate.Value;
                objProp.Sem1_Fee_Dt = dtSem1FeeDate.Value;
                objProp.Sem2_Fee_Dt = dtSem2FeeDate.Value;
                objProp.Sem3_Fee_Dt = dtSem3FeeDate.Value;
                objProp.Sem4_Fee_Dt = dtSem4FeeDate.Value;
                objProp.Monthly_Fee_Dt = dtMonthlyFeeDate.Value;

                objTradeFeeDetails.UpdateData(objProp);
                MessageBox.Show("Trade Fee record updated successfully.");
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
                objTradeFeeDetails.DeleteData(Convert.ToInt16( txtID.Text.Trim()));
                ClearData();
                MessageBox.Show("Trade Fee record deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearData()
        {
            try
            {
                txtID.Text = Convert.ToString(BusinessLayer.Common.GetMaxID("Trade_FeeDetails", "TF_ID"));
                cmbTrade.SelectedValue = -1;
                cmbBatch.SelectedValue = -1;
                txtAdmissionFee.Value = 0;
                txtCautioinFee.Value = 0;
                txtSem1.Value = 0;
                txtSem2.Value = 0;
                txtSem3.Value = 0;
                txtSem4.Value = 0;
                txtMonthlyFee.Value = 0;
                txtNumOfInstal.Value = 0;
                txtRecFee.Value = 0;
                txtUniformFee.Value = 0;
                lblTotal.Text = "0";


                dtAdmissionFeeDate.Value = new DateTime(1800,1,1);
                dtCautionFeeDate.Value = new DateTime(1800, 1, 1);
                dtSem1FeeDate.Value = new DateTime(1800, 1, 1);
                dtSem2FeeDate.Value = new DateTime(1800, 1, 1);
                dtSem3FeeDate.Value = new DateTime(1800, 1, 1);
                dtSem4FeeDate.Value = new DateTime(1800, 1, 1);
                dtMonthlyFeeDate.Value = new DateTime(1800, 1, 1);

                dtAdmissionFeeDate.Checked = false;
                dtCautionFeeDate.Checked = false;
                dtSem1FeeDate.Checked = false;
                dtSem2FeeDate.Checked = false;
                dtSem3FeeDate.Checked = false;
                dtSem4FeeDate.Checked = false;
                dtMonthlyFeeDate.Checked = false ;


                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTemp = (DataTable)objTradeFeeDetails.GetDataPerID(Convert.ToInt32(cmbTFList.SelectedValue));

                DataRow drInfo = dtTemp.Rows[0];
                if (drInfo != null)
                {
                    txtID.Text = Convert.ToString(drInfo["TF_ID"].ToString());
                    cmbTrade.SelectedValue = Convert.ToString(drInfo["Trade_ID"].ToString());
                    cmbBatch.SelectedValue = Convert.ToString(drInfo["Batch_ID"].ToString());
                    txtAdmissionFee.Value = Convert.ToDecimal(drInfo["Registration_Fee"].ToString());
                    txtCautioinFee.Value = Convert.ToDecimal(drInfo["Caution_Fee"].ToString());
                    txtSem1.Value = Convert.ToDecimal(drInfo["Sem1_Fee"].ToString());
                    txtSem2.Value = Convert.ToDecimal(drInfo["Sem2_Fee"].ToString());
                    txtSem3.Value = Convert.ToDecimal(drInfo["Sem3_Fee"].ToString());
                    txtSem4.Value = Convert.ToDecimal(drInfo["Sem4_Fee"].ToString());
                    txtMonthlyFee.Value = Convert.ToDecimal(drInfo["Monthly_Fee"].ToString());
                    txtNumOfInstal.Value = Convert.ToDecimal(drInfo["No_OfInstallments"].ToString());
                    txtRecFee.Value = Convert.ToDecimal(drInfo["Record_Fee"].ToString());
                    txtUniformFee.Text = Convert.ToString(drInfo["Uniform_Fee"].ToString());

                    dtAdmissionFeeDate.Value = drInfo["Registration_Fee_Dt"] == DBNull.Value ? dtAdmissionFeeDate.MinDate : Convert.ToDateTime(drInfo["Registration_Fee_Dt"].ToString());
                    dtCautionFeeDate.Value = drInfo["Caution_Fee_Dt"] == DBNull.Value ?  dtCautionFeeDate.MinDate : Convert.ToDateTime(drInfo["Caution_Fee_Dt"].ToString());
                    dtSem1FeeDate.Value = drInfo["Sem1_Fee_Dt"] == DBNull.Value ? dtSem1FeeDate.MinDate : Convert.ToDateTime(drInfo["Sem1_Fee_Dt"].ToString());
                    dtSem2FeeDate.Value = drInfo["Sem2_Fee_Dt"] == DBNull.Value ? dtSem2FeeDate.MinDate : Convert.ToDateTime(drInfo["Sem2_Fee_Dt"].ToString());
                    dtSem3FeeDate.Value = drInfo["Sem3_Fee_Dt"] == DBNull.Value ? dtSem3FeeDate.MinDate : Convert.ToDateTime(drInfo["Sem3_Fee_Dt"].ToString());
                    dtSem4FeeDate.Value = drInfo["Sem4_Fee_Dt"] == DBNull.Value ? dtSem4FeeDate.MinDate : Convert.ToDateTime(drInfo["Sem4_Fee_Dt"].ToString());
                    dtMonthlyFeeDate.Value= drInfo["Monthly_Fee_Dt"] == DBNull.Value ? dtMonthlyFeeDate.MinDate : Convert.ToDateTime(drInfo["Monthly_Fee_Dt"].ToString());

                    lblTotal.Text = Convert.ToString(txtAdmissionFee.Value + txtCautioinFee.Value + txtSem1.Value + txtSem2.Value + txtSem3.Value + txtSem4.Value + (txtMonthlyFee.Value * txtNumOfInstal.Value) );
                    LoadTradeFeeList();

                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    cmbTFList.SelectedValue = -1;
                }
                    

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtAdmissionFee_KeyDown(object sender, KeyEventArgs e)
        {
         
        }

        private void txtAdmissionFee_Validating(object sender, CancelEventArgs e)
        {
            lblTotal.Text = Convert.ToString(txtAdmissionFee.Value + txtCautioinFee.Value + txtSem1.Value + txtSem2.Value + txtSem3.Value + txtSem4.Value + (txtMonthlyFee.Value * txtNumOfInstal.Value));
        }
        private bool Validate()
        {
            if(Convert.ToInt32( cmbBatch.SelectedValue) == -1 || Convert.ToInt32( cmbTrade.SelectedValue) == -1 )
            {
             
                return false;
            }
            return true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void txtSem2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
