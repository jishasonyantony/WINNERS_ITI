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
    public partial class FeePaymentInfo : Form
    {
        ClsBLStudent_FeeDetails  objStudentFeeDetails;
        ClsBLBatchDetails objBatchDetails;
        ClsBLTrade objTrade;
        ClsBLStudent objStudent;
        ClsBLPayment_Details objPymt;
        Cls_BL_TradeFeeDetails objTradeFeeDetails;

        internal bool FeeCheckAfterMigration { get; set; }
        public FeePaymentInfo()
        {
            InitializeComponent();
        }

        private void FeePayment_Load(object sender, EventArgs e)
        {
            try
            { 
                objTradeFeeDetails = new Cls_BL_TradeFeeDetails();
                objPymt = new ClsBLPayment_Details();
                objTrade = new ClsBLTrade();
                objStudentFeeDetails = new ClsBLStudent_FeeDetails();
                DataTable dtTradeList = (DataTable)objTrade.GetDataPerInstitution(Common.Institution);
                cmbTrade.DataSource = dtTradeList;
                cmbTrade.ValueMember = "Trade_ID";
                cmbTrade.DisplayMember = "Name";

                objBatchDetails = new ClsBLBatchDetails();
                DataTable dtBatch = (DataTable)objBatchDetails.GetData();
                cmbBatch.DataSource = dtBatch;
                cmbBatch.ValueMember = "Batch_ID";
                cmbBatch.DisplayMember = "Batch";
                ClearData();
                ReverseEditFeeForMigratedStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearData()
        {
            cmbBatch.SelectedValue = -1;
            cmbTrade.SelectedValue = -1;
            cmbStudent.SelectedValue = -1;

        } 

       

        private void cmbTrade_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbTrade.SelectedValue != null && cmbTrade.SelectedValue.ToString() != "-1" && cmbBatch.SelectedValue != null && cmbBatch.SelectedValue.ToString() != "-1")
            {
                objStudent = new ClsBLStudent();
                cmbStudent.DataSource = (DataTable)objStudent.SelectStudentDetails_Trade_Batch(Convert.ToInt32(cmbTrade.SelectedValue), Convert.ToInt32(cmbBatch.SelectedValue), Common.Institution);
                cmbStudent.DisplayMember = "Name";
                cmbStudent.ValueMember = "ID";
                ClearDataLines();
            }
            else ClearDataLines();
        }

        private void cmbStudent_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            { 
                FillFeeDetailForTheStudent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbStudent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ClearDataLines()
        {

            txtRegFeePaid.Text = "0";
            txtCautionFeePaid.Text = "0";
            txtSem1FeePaid.Text = "0";
            txtSem2FeePaid.Text = "0";
            txtSem3FeePaid.Text = "0";
            txtSem4FeePaid.Text = "0";
            txtMonthlyFeePaid.Text = "0";
            txtRecordFeePaid.Text = "0";
            txtUniformFeePaid.Text = "0";

            txtRegFeeTot.Text = "0";
            txtCautionFeeTot.Text = "0";
            txtSem1FeeTot.Text = "0";
            txtSem2FeeTot.Text = "0";
            txtSem3FeeTot.Text = "0";
            txtSem4FeeTot.Text = "0";
            txtMonthyFeeTot.Text = "0";
            txtRecordFeeTot.Text = "0";
            txtUniformFeeTot.Text = "0";

            txtRegFeeDue.Text = "0";
            txtCautionFeeDue.Text = "0";
            txtSem1FeeDue.Text = "0";
            txtSem2FeeDue.Text = "0";
            txtSem3FeeDue.Text = "0";
            txtSem4FeeDue.Text = "0";
            txtMonthlyFeeDue.Text = "0";
            txtRecordFeeDue.Text = "0";
            txtUniformFeeDue.Text = "0";
            txtConcession.Text = "0";
        }

        private void btnReDistributeFee_Click(object sender, EventArgs e)
        {
            try
            { 
                if (Validate())
                {
                    EditFeeForMigratedStudents();
                    btnReDistributeFee.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        } 
        private bool Validate()
        {
            if ( Convert.ToInt32(cmbBatch.SelectedValue) == -1 || Convert.ToInt32(cmbTrade.SelectedValue) == -1 || Convert.ToString(cmbStudent.SelectedValue) == "-1" ) return false;
            return true;
        }
        private void FillFeeDetailForTheStudent()
        {
            try
            {
                DataTable dt = (DataTable)objTradeFeeDetails.GetTradeFeeDetails(Common.Institution, Convert.ToInt32(cmbTrade.SelectedValue), Convert.ToInt32(cmbBatch.SelectedValue));
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    txtRegFeeTot.Text = Convert.ToString(dr["Registration_Fee"]);
                    txtCautionFeeTot.Text = Convert.ToString(dr["Caution_Fee"]);
                    txtSem1FeeTot.Text = Convert.ToString(dr["Sem1_Fee"]);
                    txtSem2FeeTot.Text = Convert.ToString(dr["Sem2_Fee"]);
                    txtSem3FeeTot.Text = Convert.ToString(dr["Sem3_Fee"]);
                    txtSem4FeeTot.Text = Convert.ToString(dr["Sem4_Fee"]);
                    txtMonthyFeeTot.Text = Convert.ToString(Convert.ToSingle(dr["Monthly_Fee"]) * Convert.ToSingle(dr["No_OfInstallments"]));
                    txtRecordFeeTot.Text = Convert.ToString(dr["Record_Fee"]);
                    txtUniformFeeTot.Text = Convert.ToString(dr["Uniform_Fee"]);
                }
                else
                {
                    txtRegFeeTot.Text = "0";
                    txtCautionFeeTot.Text = "0";
                    txtSem1FeeTot.Text = "0";
                    txtSem2FeeTot.Text = "0";
                    txtSem3FeeTot.Text = "0";
                    txtSem4FeeTot.Text = "0";
                    txtMonthyFeeTot.Text = "0";
                    txtRecordFeeTot.Text = "0";
                    txtUniformFeeTot.Text = "0";
                }

                DataTable dtStudentPaidFee = (DataTable)objStudentFeeDetails.GetFeePaid((string)cmbStudent.SelectedValue);
                if (dtStudentPaidFee != null && dtStudentPaidFee.Rows.Count > 0)
                {
                    DataRow dr = dtStudentPaidFee.Rows[0];

                    txtRegFeeAdj.Text = txtRegFeePaid.Text = Convert.ToString(dr["Registration_Fee"]);
                    txtCautionFeeAdj.Text = txtCautionFeePaid.Text = Convert.ToString(dr["Caution_Fee"]);
                    txtSem1FeeAdj.Text = txtSem1FeePaid.Text = Convert.ToString(dr["Sem1_Fee"]);
                    txtSem2FeeAdj.Text = txtSem2FeePaid.Text = Convert.ToString(dr["Sem2_Fee"]);
                    txtSem3FeeAdj.Text = txtSem3FeePaid.Text = Convert.ToString(dr["Sem3_Fee"]);
                    txtSem4FeeAdj.Text = txtSem4FeePaid.Text = Convert.ToString(dr["Sem4_Fee"]);
                    txtMonthlyFeeAdj.Text = txtMonthlyFeePaid.Text = Convert.ToString(dr["Installment_24"]);
                    txtRecFeeAdj.Text = txtRecordFeePaid.Text = Convert.ToString(dr["Record_Fee"]);
                    txtUniformFeeAdj.Text = txtUniformFeePaid.Text = Convert.ToString(dr["Uniform_Fee"]);
                }
                else
                {
                    txtRegFeeAdj.Text = txtRegFeePaid.Text = "0";
                    txtSem1FeeAdj.Text = txtSem1FeePaid.Text = "0";
                    txtCautionFeeAdj.Text = "0";
                    txtSem2FeeAdj.Text = txtSem2FeePaid.Text = "0";
                    txtSem3FeeAdj.Text = txtSem3FeePaid.Text = "0";
                    txtSem4FeeAdj.Text = txtSem4FeePaid.Text = "0";
                    txtMonthlyFeeAdj.Text = txtMonthlyFeePaid.Text = "0";
                    txtRecFeeAdj.Text = txtRecordFeePaid.Text = "0";
                    txtUniformFeeAdj.Text = txtUniformFeePaid.Text = "0";
                }
                txtRegFeeDue.Text = Convert.ToString(Convert.ToDecimal(txtRegFeeTot.Text) - Convert.ToDecimal(txtRegFeePaid.Text));
                txtCautionFeeDue.Text = Convert.ToString(Convert.ToDecimal(txtCautionFeeTot.Text) - Convert.ToDecimal(txtCautionFeePaid.Text));
                txtSem1FeeDue.Text = Convert.ToString(Convert.ToDecimal(txtSem1FeeTot.Text) - Convert.ToDecimal(txtSem1FeePaid.Text));
                txtSem2FeeDue.Text = Convert.ToString(Convert.ToDecimal(txtSem2FeeTot.Text) - Convert.ToDecimal(txtSem2FeePaid.Text));
                txtSem3FeeDue.Text = Convert.ToString(Convert.ToDecimal(txtSem3FeeTot.Text) - Convert.ToDecimal(txtSem3FeePaid.Text));
                txtSem4FeeDue.Text = Convert.ToString(Convert.ToDecimal(txtSem4FeeTot.Text) - Convert.ToDecimal(txtSem4FeePaid.Text));
                txtMonthlyFeeDue.Text = Convert.ToString(Convert.ToDecimal(txtMonthyFeeTot.Text) - Convert.ToDecimal(txtMonthlyFeePaid.Text));
                txtRecordFeeDue.Text = Convert.ToString(Convert.ToDecimal(txtRecordFeeTot.Text) - Convert.ToDecimal(txtRecordFeePaid.Text));
                txtUniformFeeDue.Text = Convert.ToString(Convert.ToDecimal(txtUniformFeeTot.Text) - Convert.ToDecimal(txtUniformFeePaid.Text));


                lblTotFee.Text = Convert.ToString(Convert.ToDecimal(txtRegFeeTot.Text)  + Convert.ToDecimal(txtCautionFeeTot.Text) + Convert.ToDecimal(txtSem1FeeTot.Text) + Convert.ToDecimal(txtSem2FeeTot.Text) + Convert.ToDecimal(txtSem3FeeTot.Text) + Convert.ToDecimal(txtSem4FeeTot.Text) + Convert.ToDecimal((txtMonthyFeeTot.Text)));
                lblPaidFee.Text = Convert.ToString(Convert.ToDecimal(txtRegFeePaid.Text) + Convert.ToDecimal(txtCautionFeePaid.Text) + Convert.ToDecimal(txtSem1FeePaid.Text) + Convert.ToDecimal(txtSem2FeePaid.Text) + Convert.ToDecimal(txtSem3FeePaid.Text) + Convert.ToDecimal(txtSem4FeePaid.Text) + Convert.ToDecimal((txtMonthlyFeePaid.Text)) );
                lblDueFee.Text = Convert.ToString(Convert.ToDecimal(txtRegFeeDue.Text) +Convert.ToDecimal(txtCautionFeeDue.Text) + Convert.ToDecimal(txtSem1FeeDue.Text) + Convert.ToDecimal(txtSem2FeeDue.Text) + Convert.ToDecimal(txtSem3FeeDue.Text) + Convert.ToDecimal(txtSem4FeeDue.Text) + Convert.ToDecimal((txtMonthlyFeeDue.Text)));
                lblFeeAdjustmentTTotal.Text = Convert.ToString(Convert.ToDecimal(txtRegFeeAdj.Text) +Convert.ToDecimal(txtCautionFeeAdj.Text) + Convert.ToDecimal(txtSem1FeeAdj.Text) + Convert.ToDecimal(txtSem2FeeAdj.Text) + Convert.ToDecimal(txtSem3FeeAdj.Text) + Convert.ToDecimal(txtSem4FeeAdj.Text) + Convert.ToDecimal((txtMonthlyFeeAdj.Text)));

                txtConcession.Text = Convert.ToString(objStudent.GetConcessionFee(Convert.ToString(cmbStudent.SelectedValue)));
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        private void EditFeeForMigratedStudents()
        {
            try
            {
                txtRegFeeAdj.Visible = true;
                txtCautionFeeAdj.Visible = true;
                txtSem1FeeAdj.Visible = true;
                txtSem2FeeAdj.Visible = true;
                txtSem3FeeAdj.Visible = true;
                txtSem4FeeAdj.Visible = true;
                txtMonthlyFeeAdj.Visible = true;
                txtRecFeeAdj.Visible = true;
                txtUniformFeeAdj.Visible = true;
                btnAdjustFee.Visible = true;
                lblFeeAdjustmentEdit.Visible = true;
                lblFeeAdjustmentTTotal.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
        private void ReverseEditFeeForMigratedStudents()
        {
            try
            {
                txtRegFeeAdj.Visible = false;
                txtCautionFeeAdj.Visible = false;
                txtSem1FeeAdj.Visible = false;
                txtSem2FeeAdj.Visible = false;
                txtSem3FeeAdj.Visible = false;
                txtSem4FeeAdj.Visible = false;
                txtMonthlyFeeAdj.Visible = false;
                txtRecFeeAdj.Visible = false;
                txtUniformFeeAdj.Visible = false;
                btnAdjustFee.Visible = false;
                lblFeeAdjustmentEdit.Visible = false;
                lblFeeAdjustmentTTotal.Visible = false;
                btnReDistributeFee.Enabled = true;
            }
            catch (Exception ex)
            {
            }
        }

        private void txtRegFeeAdj_Validating(object sender, CancelEventArgs e)
        {
            try
            { 
                lblFeeAdjustmentTTotal.Text = Convert.ToString(Convert.ToDecimal(txtRegFeeAdj.Text) + Convert.ToDecimal(txtSem1FeeAdj.Text) + Convert.ToDecimal(txtSem2FeeAdj.Text) + Convert.ToDecimal(txtSem3FeeAdj.Text) + Convert.ToDecimal(txtSem4FeeAdj.Text) + Convert.ToDecimal((txtMonthlyFeeAdj.Text)) );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdjustFee_Click(object sender, EventArgs e)
        {
            try
            { 
                if (Validate() == true)
                {
                    //if (Convert.ToInt32(lblPaidFee.Text) == Convert.ToInt32(lblFeeAdjustmentTTotal.Text))
                    //{

                        PropertyLayer.Student_FeeDetails objProp = new PropertyLayer.Student_FeeDetails();
                        objProp.Stud_ID = Convert.ToString(cmbStudent.SelectedValue);
                        objProp.Registration_Fee = Convert.ToDecimal(txtRegFeeAdj.Text.Trim());
                        objProp.Caution_Fee = Convert.ToDecimal(txtCautionFeeAdj.Text.Trim());
                        objProp.Sem1_Fee = Convert.ToDecimal(txtSem1FeeAdj.Text.Trim());
                        objProp.Sem2_Fee = Convert.ToDecimal(txtSem2FeeAdj.Text.Trim()); ;
                        objProp.Sem3_Fee = Convert.ToDecimal(txtSem3FeeAdj.Text.Trim());
                        objProp.Sem4_Fee = Convert.ToDecimal(txtSem4FeeAdj.Text.Trim());
                        objProp.Installment_24 = Convert.ToDecimal(txtMonthlyFeeAdj.Text.Trim());
                        objProp.Uniform_Fee = Convert.ToDecimal(txtUniformFeeAdj.Text.Trim());
                        objProp.Record_Fee = Convert.ToDecimal(txtRecFeeAdj.Text.Trim());

                        objStudentFeeDetails.UpdateDataForMigratedStudents(objProp);
                        FillFeeDetailForTheStudent();
                        ReverseEditFeeForMigratedStudents();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Revised fee mismatch. Cannot proceed.");
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
