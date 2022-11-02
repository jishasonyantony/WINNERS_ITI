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
    public partial class BatchMaster : Form
    {
        ClsBLBatchDetails objBatchDetails;
        public BatchMaster()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtBranch = (DataTable)objBatchDetails.GetDataPerBatch(Convert.ToInt32(cmbBatchList.SelectedValue));
                DataRow drInfo = dtBranch.Rows[0];
                if (drInfo != null)
                {
                    txtID.Text = Convert.ToString(drInfo["Batch_ID"].ToString());
                    txtDesc.Text = Convert.ToString(drInfo["Batch"].ToString());

                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    cmbBatchList.SelectedValue = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BatchMaster_Load(object sender, EventArgs e)
        {
            try
            {
                LoadBatchDetailsForFind();
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
         public void LoadBatchDetailsForFind()
        {
            objBatchDetails = new ClsBLBatchDetails();
            DataTable dtBatch = (DataTable)objBatchDetails.GetData();
            cmbBatchList.DataSource = dtBatch;
            cmbBatchList.ValueMember = "Batch_ID";
            cmbBatchList.DisplayMember = "Batch";
        }
        private bool Validate()
        {
            if (txtDesc.Text.Trim().Length == 0) return false;
            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(Validate() == false)
            {
                MessageBox.Show("Please fill required fields");
                return;
            }
            try
            {
                PropertyLayer.Batch_Details objProp = new PropertyLayer.Batch_Details();
                objProp.Batch_ID = Convert.ToInt32(txtID.Text.Trim());
                objProp.Batch = txtDesc.Text.Trim();

                objBatchDetails.InsertData(objProp);
                MessageBox.Show("Batch record registered successfully.");
                LoadBatchDetailsForFind();
                ClearData();
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
                PropertyLayer.Batch_Details objProp = new PropertyLayer.Batch_Details();
                objProp.Batch_ID = Convert.ToInt32(txtID.Text.Trim());
                objProp.Batch = txtDesc.Text.Trim();

                objBatchDetails.UpdateData(objProp);
                MessageBox.Show("Batch record updated successfully.");
                LoadBatchDetailsForFind();
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
                objBatchDetails.DeleteData(Convert.ToInt32(txtID.Text));
                MessageBox.Show("Batch record deleted successfully.");
                ClearData();
                LoadBatchDetailsForFind();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("conflicted with the REFERENCE constraint"))
                    MessageBox.Show("You cannnot delete a branch which is in use");
                else
                    MessageBox.Show(ex.Message);
            }
        }
        private void ClearData()
        {
            try
            {
                txtID.Text =Convert.ToString( BusinessLayer.Common.GetMaxID("Batch_Details", "Batch_ID"));
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }

}
