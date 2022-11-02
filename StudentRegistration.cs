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
    public partial class StudentRegistration : Form
    {
        ClsBLInstitution objInstitution;
        ClsBLTrade objTrade;
        ClsBLBatchDetails objBatchDetails;
        ClsBLStudent objBLStudent;
        public StudentRegistration()
        {
            InitializeComponent();
        }

        private void StudentRegistration_Load(object sender, EventArgs e)
        {
            try
            {
                FillStudentListcombo();

                objInstitution = new ClsBLInstitution();
                DataTable dtUnivList = (DataTable)objInstitution.GetData();
                cmbInstituion.DataSource = dtUnivList;
                cmbInstituion.ValueMember = "Inst_ID";
                cmbInstituion.DisplayMember = "Name";

                objTrade = new ClsBLTrade();
                DataTable dtTradeList = (DataTable)objTrade.GetDataPerInstitution(Common.Institution);
                cmbTrade.DataSource = dtTradeList;
                cmbTrade.ValueMember = "Trade_ID";
                cmbTrade.DisplayMember = "Name";

                objBatchDetails = new ClsBLBatchDetails();
                DataTable dtBatch = (DataTable)objBatchDetails.GetData();
                cmbBatch.DataSource = dtBatch;
                cmbBatch.ValueMember = "Batch_ID";
                cmbBatch.DisplayMember = "Batch";
                txtStudentID.Text = objBLStudent.GetNextID_Student_Institution(Common.Institution);

                cmbInstituion.SelectedValue = Common.Institution;
                cmbInstituion.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Validate() == false)
            {
                MessageBox.Show("Missing data. Please fill required student details");
                return;
            }
            try
            {
                PropertyLayer.Student objProp = new PropertyLayer.Student();
                objProp.Stud_ID = txtStudentID.Text.Trim();
                objProp.Name = txtstudentName.Text.Trim();
                if (dpDOB.Text.Trim().Length == 0)
                {
                    DateTime? myTime = null;
                    objProp.DOB = myTime;
                }
                else objProp.DOB = Convert.ToDateTime(dpDOB.Text.Trim());
                objProp.FatherName = txtFatherName.Text;
                objProp.Address = txtAddress.Text;
                objProp.Phone = txtPhoneNo.Text.Trim();
                objProp.Batch_ID = Convert.ToInt32( cmbBatch.SelectedValue);
                objProp.Trade_ID = Convert.ToInt32(cmbTrade.SelectedValue);
                objProp.Institution_ID = Convert.ToInt32(cmbInstituion.SelectedValue);
                objProp.FeeConcessionAmount = Convert.ToDecimal(numericFeeConcession.Value);

                objBLStudent.InsertData(objProp);
                MessageBox.Show("Student record registered successfully.");
                ClearData();
                FillStudentListcombo();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        
    }
        private void FillStudentListcombo()
        {
            objBLStudent = new ClsBLStudent();
            DataTable dtStudList = (DataTable)objBLStudent.GetDataPerStudentInstitution(Common.Institution);
            cmbStudentList.DataSource = dtStudList;
            cmbStudentList.ValueMember = "Stud_ID";
            cmbStudentList.DisplayMember = "Name";
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(Validate() == false)
                {
                    MessageBox.Show("Missing data. Please fill required student details");
                    return;
                }
                PropertyLayer.Student objProp = new PropertyLayer.Student();
                objProp.Stud_ID = txtStudentID.Text.Trim();
                objProp.Name = txtstudentName.Text.Trim();
                if (dpDOB.Text.Trim().Length == 0)
                {
                    DateTime? myTime = null;
                    objProp.DOB = myTime;
                }
                else objProp.DOB = Convert.ToDateTime(dpDOB.Text.Trim());
                objProp.FatherName = txtFatherName.Text;
                objProp.Address = txtAddress.Text;
                objProp.Phone = txtPhoneNo.Text.Trim();
                objProp.Batch_ID = Convert.ToInt32(cmbBatch.SelectedValue);
                objProp.Trade_ID = Convert.ToInt32(cmbTrade.SelectedValue);
                objProp.Institution_ID = Convert.ToInt32(cmbInstituion.SelectedValue);
                objProp.FeeConcessionAmount = Convert.ToDecimal(numericFeeConcession.Value);

                objBLStudent.UpdateData(objProp);
                MessageBox.Show("Student record updated successfully.");
                FillStudentListcombo();
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
                objBLStudent.DeleteStudentRecord(txtStudentID.Text.Trim());
                ClearData();
                MessageBox.Show("Student record deleted successfully.");
                FillStudentListcombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        void ClearData()
        {
            try
            {
                txtStudentID.Text = objBLStudent.GetNextID_Student_Institution(Common.Institution);
                txtstudentName.Text = "";
                dpDOB.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtAddress.Text = "";
                txtPhoneNo.Text ="";
                numericFeeConcession.Value = 0;
                cmbBatch.SelectedValue =-1;
                cmbTrade.SelectedValue = -1;
                txtFatherName.Text = "";
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
                DataTable dtTemp= (DataTable) objBLStudent.GetDataPerStudent (Convert.ToString (cmbStudentList.SelectedValue));

                DataRow drInfo = dtTemp.Rows[0];
                if (drInfo != null)
                {
                    txtStudentID.Text = Convert.ToString(drInfo["Stud_ID"].ToString());
                    txtstudentName.Text = Convert.ToString(drInfo["Name"].ToString());
                    dpDOB.Text = drInfo["DOB"] != DBNull.Value ? Convert.ToDateTime(drInfo["DOB"]).ToString("yyyy-MM-dd") : "";
                    txtAddress.Text = Convert.ToString(drInfo["Address"].ToString());
                    txtPhoneNo.Text = Convert.ToString(drInfo["Phone"].ToString());
                    cmbBatch.SelectedValue = Convert.ToString(drInfo["Batch_ID"].ToString());
                    cmbTrade.SelectedValue = Convert.ToString(drInfo["Trade_ID"].ToString());
                    cmbInstituion.SelectedValue = Convert.ToString(drInfo["Institution_ID"].ToString());
                    txtFatherName.Text = Convert.ToString(drInfo["FatherName"].ToString());
                    numericFeeConcession.Value = Convert.ToDecimal(drInfo["FeeConcessionAmount"].ToString());
                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    cmbStudentList.SelectedValue = -1;
                }
                    
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private bool Validate()
        {
            if (txtstudentName.Text.Trim().Length == 0 || txtFatherName.Text.Trim().Length == 0 || txtAddress.Text.Trim().Length == 0 || txtPhoneNo.Text.Trim().Length == 0 || Convert.ToInt32(cmbBatch.SelectedValue) == -1 || Convert.ToInt32(cmbTrade.SelectedValue) == -1) return false;
            return true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}
