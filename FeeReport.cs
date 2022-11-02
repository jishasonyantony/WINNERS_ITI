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
using System.IO;
using ClosedXML.Excel;

namespace Winners_ITI
{
    public partial class FeeReport : Form
    {
        ClsBLStudent_FeeDetails objStudentFeeDetails;
        ClsBLBatchDetails objBatchDetails;
        ClsBLTrade objTrade;
        ClsBLStudent objStudent;
        ClsBLPayment_Details objPymt;
        Cls_BL_TradeFeeDetails objTradeFeeDetails;
        public FeeReport()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = objStudentFeeDetails.SearchFeePaymentsDetailed(Convert.ToDateTime(dtFrom.Value), Convert.ToDateTime(dtTo.Value), Common.Institution, Convert.ToInt32(cmbBatch.SelectedValue), Convert.ToInt32(cmbTrade.SelectedValue), Convert.ToString(cmbStudent.SelectedValue), Convert.ToInt32(cmbFeeType.SelectedValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FeeReport_Load(object sender, EventArgs e)
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

                DataTable dtFeeTypes = (DataTable)objPymt.GetFeeTypes();
                cmbFeeType.DataSource = dtFeeTypes;
                cmbFeeType.ValueMember = "ID";
                cmbFeeType.DisplayMember = "Type";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbBatch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            { 
                if (cmbTrade.SelectedValue != null && cmbTrade.SelectedValue.ToString() != "-1" && cmbBatch.SelectedValue != null && cmbBatch.SelectedValue.ToString() != "-1")
                {
                    objStudent = new ClsBLStudent();
                    cmbStudent.DataSource = (DataTable)objStudent.SelectStudentDetails_Trade_Batch(Convert.ToInt32(cmbTrade.SelectedValue), Convert.ToInt32(cmbBatch.SelectedValue), Common.Institution);
                    cmbStudent.DisplayMember = "Name";
                    cmbStudent.ValueMember = "ID";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbTrade_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbTrade.SelectedValue != null && cmbTrade.SelectedValue.ToString() != "-1" && cmbBatch.SelectedValue != null && cmbBatch.SelectedValue.ToString() != "-1")
                {
                    objStudent = new ClsBLStudent();
                    cmbStudent.DataSource = (DataTable)objStudent.SelectStudentDetails_Trade_Batch(Convert.ToInt32(cmbTrade.SelectedValue), Convert.ToInt32(cmbBatch.SelectedValue), Common.Institution);
                    cmbStudent.DisplayMember = "Name";
                    cmbStudent.ValueMember = "ID";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFeeSummarySearch_Click(object sender, EventArgs e)
        {
            try
            { 
                dataGridView1.DataSource = objStudentFeeDetails.SearchDFeePaymentSummary(Convert.ToDateTime(dtFrom.Value), Convert.ToDateTime(dtTo.Value), Common.Institution, Convert.ToInt32(cmbBatch.SelectedValue), Convert.ToInt32(cmbTrade.SelectedValue), Convert.ToString(cmbStudent.SelectedValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            { 
                SaveFileDialog oFD = new SaveFileDialog();
                oFD.FileName = "Fee Report_" + DateTime.Now .Date.ToShortDateString() + ".xlsx";
                oFD.DefaultExt = "xlsx";
                oFD.ShowDialog();
          
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add((DataTable) dataGridView1.DataSource, "FeeReport");
                    wb.SaveAs(oFD.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
