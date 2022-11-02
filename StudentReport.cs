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
using ClosedXML.Excel;

namespace Winners_ITI
{
    public partial class StudentReport : Form
    {
        ClsBLTrade objTrade;
        ClsBLBatchDetails objBatchDetails;
        ClsBLStudent objBLStudent;
        public StudentReport()
        {
            InitializeComponent();
        }

        private void StudentReport_Load(object sender, EventArgs e)
        {
            try
            { 
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

                objBLStudent = new ClsBLStudent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            { 
                dataGridView1.DataSource = objBLStudent.SearchForStudents (Convert.ToInt32( cmbBatch.SelectedValue), Convert.ToInt32( cmbTrade.SelectedValue), Common.Institution);
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
                oFD.FileName = "Student Report_" + DateTime.Now.Date.ToShortDateString() + ".xlsx";
                oFD.DefaultExt = "xlsx";
                oFD.ShowDialog();

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add((DataTable)dataGridView1.DataSource, "StudentReport");
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
