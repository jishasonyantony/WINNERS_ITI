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
   
    public partial class TradeChange : Form
    {
        ClsBLTrade objTrade;
        ClsBLStudent objStudent;
        ClsBLInstitution objInstitution;
        ClsBLStudent_FeeDetails objStudentFeeDetails;
        ClsBLBatchDetails objBatchDetails;
        public TradeChange()
        {
            InitializeComponent();
        }

        private void TradeChange_Load(object sender, EventArgs e)
        {
            try
            { 
                objStudent = new ClsBLStudent();

                objTrade = new ClsBLTrade();
                DataTable dtTradeListSource = (DataTable)objTrade.GetDataPerInstitution(Common.Institution);
                cmbSourceTrade.DataSource = dtTradeListSource;
                cmbSourceTrade.ValueMember = "Trade_ID";
                cmbSourceTrade.DisplayMember = "Name";


                objInstitution = new ClsBLInstitution();
                DataTable dtUnivListSouce = (DataTable)objInstitution.GetData();
                cmbSourceInstitution.DataSource = dtUnivListSouce;
                cmbSourceInstitution.ValueMember = "Inst_ID";
                cmbSourceInstitution.DisplayMember = "Name";

                DataTable dtUnivListDest = (DataTable)objInstitution.GetData();
                cmbDestInstitution.DataSource = dtUnivListDest;
                cmbDestInstitution.ValueMember = "Inst_ID";
                cmbDestInstitution.DisplayMember = "Name";

                cmbSourceInstitution.SelectedValue = Common.Institution;

                objBatchDetails = new ClsBLBatchDetails();
                DataTable dtBatch = (DataTable)objBatchDetails.GetData();
                cmbSourceBatch.DataSource = dtBatch;
                cmbSourceBatch.ValueMember = "Batch_ID";
                cmbSourceBatch.DisplayMember = "Batch";

                DataTable dtBatchDest = (DataTable)objBatchDetails.GetData();
                cmbDestBatch.DataSource = dtBatchDest;
                cmbDestBatch.ValueMember = "Batch_ID";
                cmbDestBatch.DisplayMember = "Batch";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbSourceTrade_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnApplyChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (validate() == true)
                {
                    DialogResult rslt = MessageBox.Show("Are you sure you want to process the migration?", "Migration", MessageBoxButtons.YesNo);
                    if (rslt == DialogResult.Yes)
                    { 
                        objStudent.ProcessMigration(Convert.ToInt32(cmbDestInstitution.SelectedValue), Convert.ToInt32(cmbDestBatch.SelectedValue), Convert.ToInt32(cmbDestTrade.SelectedValue), Convert.ToString(cmbSTudent.SelectedValue));
                        objStudent.DeleteStudentRecord(Convert.ToString(cmbSTudent.SelectedValue));
                        MessageBox.Show("Operation completed successfully. Please recheck the Fee Payment and confirm.");

                        //frmHome currInstance = (frmHome)this.Parent;
                        //FeePaymentInfo objPymtInfo = new FeePaymentInfo();
                        //currInstance.OpenChildForm(objPymtInfo);
                        ClearData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearData()
        {
            cmbSourceInstitution.SelectedValue = -1;
            cmbSourceBatch.SelectedValue = -1;
            cmbSourceTrade.SelectedValue = -1;
            cmbSTudent.SelectedValue = -1;
            cmbDestInstitution.SelectedValue = -1;
            cmbDestBatch.SelectedValue = -1;
            cmbDestTrade.SelectedValue = -1;
        }
        private void cmbSTudent_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //objStudentFeeDetails = new ClsBLStudent_FeeDetails();
            //DataTable dtStudentPaidFee = (DataTable)objStudentFeeDetails.GetFeePaidInDetail((string)cmbSTudent.SelectedValue);
            //if (dtStudentPaidFee != null && dtStudentPaidFee.Rows.Count > 0)
            //{
            //    dataGridView1.DataSource = dtStudentPaidFee;
            //    //DataRow dr = dtStudentPaidFee.Rows[0];

            //    //txtegFeePaid.Text = Convert.ToString(dr["Registration_Fee"]);
            //    //txtSem1FeePaid.Text = Convert.ToString(dr["Sem1_Fee"]);
            //    //txtSem2FeePaid.Text = Convert.ToString(dr["Sem2_Fee"]);
            //    //txtSem3FeePaid.Text = Convert.ToString(dr["Sem3_Fee"]);
            //    //txtSem4FeePaid.Text = Convert.ToString(dr["Sem4_Fee"]);
            //    //txtMonthlyFeePaid.Text = Convert.ToString(dr["Installment_24"]);
            //    //txtRecordFeePaid.Text = Convert.ToString(dr["Record_Fee"]);
            //    //txtUniformFeePaid.Text = Convert.ToString(dr["Uniform_Fee"]);
            //}
        }

        private void cmbSourceTrade_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            { 
                if (cmbSourceTrade.SelectedValue != null && cmbSourceTrade.SelectedValue.ToString() != "-1" && cmbSourceBatch.SelectedValue != null && cmbSourceBatch.SelectedValue.ToString() != "-1")
                {
                    cmbSTudent.DataSource = (DataTable)objStudent.SelectStudentDetails_Trade_Batch(Convert.ToInt32(cmbSourceTrade.SelectedValue), Convert.ToInt32(cmbSourceBatch.SelectedValue), Convert.ToInt32(cmbSourceInstitution.SelectedValue));
                    cmbSTudent.DisplayMember = "Name";
                    cmbSTudent.ValueMember = "ID";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbDestInstitution_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTradeListDest = (DataTable)objTrade.GetDataPerInstitution(Convert.ToInt32(cmbDestInstitution.SelectedValue));
                cmbDestTrade.DataSource = dtTradeListDest;
                cmbDestTrade.ValueMember = "Trade_ID";
                cmbDestTrade.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}
        private bool validate()
        {
            if (Convert.ToInt32(cmbSourceInstitution.SelectedValue) == -1 || Convert.ToInt32(cmbSourceBatch.SelectedValue) == -1 || Convert.ToInt32(cmbSourceTrade.SelectedValue) == -1 || Convert.ToString(cmbSTudent.SelectedValue) == "-1"
                || Convert.ToInt32(cmbDestInstitution.SelectedValue) == -1 || Convert.ToInt32(cmbDestBatch.SelectedValue) == -1 || Convert.ToInt32(cmbDestTrade.SelectedValue) == -1) return false;
            return true;
        }
    }
}
