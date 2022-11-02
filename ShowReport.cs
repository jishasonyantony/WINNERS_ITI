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
    public partial class ShowReport : Form
    {
        //public string StudentID
        //{
        //    get;
        //    set;
        //}
        //public string RcptNo
        //{
        //    get;
        //    set;
        //}
        //public string Date
        //{
        //    get;
        //    set;
        //}
        //public string AmtInWords
        //{
        //    get;
        //    set;
        //}
        //public string AmtInDigits
        //{
        //    get;
        //    set;
        //}
        //public string FeeType
        //{
        //    get;
        //    set;
        //}
        internal FeeReportPrint objReportData;
        ClsBLStudent objStudent = new ClsBLStudent();
        public ShowReport()
        {
            InitializeComponent();
        }

        private void ShowReport_Load(object sender, EventArgs e)
        {
            try
            {

                DataSet dsSTudentDetails = objStudent.PrintReceipt(objReportData.StudID);
                DataRow dr = dsSTudentDetails.Tables[0].Rows[0];
                feeCrystalReport1.SetDataSource(dsSTudentDetails);


                feeCrystalReport1.SetParameterValue("pAdd1", objReportData.Address1);
                feeCrystalReport1.SetParameterValue("pAdd2", objReportData.Address2);
                feeCrystalReport1.SetParameterValue("pAdd3", objReportData.Address3);
                feeCrystalReport1.SetParameterValue("pRcptNo", objReportData.ReceiptNo);
                feeCrystalReport1.SetParameterValue("pDate", objReportData.Date.ToString("dd/MM/yyyy"));
                feeCrystalReport1.SetParameterValue("pName",objReportData.StudentName);
                feeCrystalReport1.SetParameterValue("pAmtWords", objReportData.AmountinWords);
                feeCrystalReport1.SetParameterValue("pAmtDigits", objReportData.AmountinDigits);
                feeCrystalReport1.SetParameterValue("pCourse", objReportData.Course);
                feeCrystalReport1.SetParameterValue("pFeeType", objReportData.FeeType);
                feeCrystalReport1.SetParameterValue("pRptText", objReportData.ReportText);

               
                crystalReportViewer1.ReportSource = feeCrystalReport1;
              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
