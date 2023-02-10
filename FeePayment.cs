using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace Winners_ITI
{
    public partial class FeePayment : Form
    {
        ClsBLStudent_FeeDetails  objStudentFeeDetails;
        ClsBLBatchDetails objBatchDetails;
        ClsBLTrade objTrade;
        ClsBLStudent objStudent;
        ClsBLPayment_Details objPymt;
        Cls_BL_TradeFeeDetails objTradeFeeDetails;
        public FeePayment()
        {
            InitializeComponent();
        }

        private void FeePayment_Load(object sender, EventArgs e)
        {
            try
            {
                objStudent = new ClsBLStudent();
                objTradeFeeDetails = new Cls_BL_TradeFeeDetails();
                objPymt = new ClsBLPayment_Details();
                objTrade = new ClsBLTrade();
                objStudentFeeDetails = new ClsBLStudent_FeeDetails();
                DataTable dtTradeList = (DataTable)objTrade.GetDataPerInstitution(Common.Institution);
                cmbTrade.DataSource = dtTradeList;
                cmbTrade.ValueMember = "Trade_ID";
                cmbTrade.DisplayMember = "Name";


                DataTable dtFeeTypes = (DataTable)objPymt.GetFeeTypes();
                cmbFeeType.DataSource = dtFeeTypes;
                cmbFeeType.ValueMember = "ID";
                cmbFeeType.DisplayMember = "Type";


                objBatchDetails = new ClsBLBatchDetails();
                DataTable dtBatch = (DataTable)objBatchDetails.GetData();
                cmbBatch.DataSource = dtBatch;
                cmbBatch.ValueMember = "Batch_ID";
                cmbBatch.DisplayMember = "Batch";
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearData()
        {
            txtTransactionID.Text = Convert.ToString( BusinessLayer.Common.GetMaxID("Payment_Details", "Pymt_ID"));
            cmbBatch.SelectedValue = -1;
            cmbTrade.SelectedValue = -1;
            cmbStudent.SelectedValue = -1;
            cmbFeeType.SelectedValue = -1;
            dtpPymtDt.Value = DateTime.Now;
            txtFeeAmount.Value = 0;
            lblFeeAmount.Text = "0";
            lblFeePaid.Text = "0";
            lblAmountDue.Text = "0";

            dataGridView1.DataSource = null;

            txtRegFeeTot.Text = "0";
            txtCautionFeeTot.Text = "0";
            txtSem1FeeTot.Text = "0";
            txtSem2FeeTot.Text = "0";
            txtSem3FeeTot.Text = "0";
            txtSem4FeeTot.Text = "0";
            txtMonthyFeeTot.Text = "0";
            txtRecordFeeTot.Text = "0";
            txtUniformFeeTot.Text = "0";

            txtRegFeePaid.Text = "0";
            txtSem1FeePaid.Text = "0";
            txtCautionFeePaid.Text = "0";
            txtSem2FeePaid.Text = "0";
            txtSem3FeePaid.Text = "0";
            txtSem4FeePaid.Text = "0";
            txtMonthlyFeePaid.Text = "0";
            txtRecordFeePaid.Text = "0";
            txtUniformFeePaid.Text = "0";

            txtRegFeeDue.Text = "0";
            txtCautionFeeDue.Text = "0";
            txtSem1FeeDue.Text = "0";
            txtSem2FeeDue.Text = "0";
            txtSem3FeeDue.Text = "0";
            txtSem4FeeDue.Text = "0";
            txtMonthlyFeeDue.Text = "0";
            txtRecordFeeDue.Text = "0";
            txtUniformFeeDue.Text = "0";
            btnSave.Enabled = true;
            txtRcptNoPrint.Text = "";
            txtReportText.Text = "";
            txtFeeConcession.Text = "0";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(Validate() == false)
            {
                MessageBox.Show("Please verify the details and save.");
                return;
            }
            try
            {
                PropertyLayer.Payment_Details objPaymtProp = new PropertyLayer.Payment_Details();
                objPaymtProp.Pymt_ID = Convert.ToInt32(txtTransactionID.Text);
                objPaymtProp.Batch_ID = Convert.ToInt32(cmbBatch.SelectedValue);
                objPaymtProp.Trade_ID = Convert.ToInt32(cmbTrade.SelectedValue);
                objPaymtProp.Stud_ID = Convert.ToString(cmbStudent.SelectedValue);
                objPaymtProp.Fee_Type = Convert.ToInt32(cmbFeeType.SelectedValue);
                objPaymtProp.Fee_Amount = Convert.ToDecimal(txtFeeAmount.Value);
                objPaymtProp.Date = Convert.ToDateTime(dtpPymtDt.Value);
                objPymt.InsertData(objPaymtProp);
                MessageBox.Show("Payment has been processed successfully.");
               
                txtTransactionID.Text = Convert.ToString(BusinessLayer.Common.GetMaxID("Payment_Details", "Pymt_ID"));
                PreviewPrintValues(objPaymtProp.Fee_Type, objPaymtProp.Fee_Amount, objPaymtProp.Date, objPaymtProp.Pymt_ID, "",true);
                cmbFeeType.SelectedValue = "-1";
                txtFeeAmount.Value = 0;

                try
                {
                    dataGridView1.DataSource = objPymt.GetFeePaidForPrint(Convert.ToString(cmbStudent.SelectedValue));
                    FillFeeDetailForTheStudent();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                  
                    cmbStudent.DataSource = (DataTable)objStudent.SelectStudentDetails_Trade_Batch(Convert.ToInt32(cmbTrade.SelectedValue), Convert.ToInt32(cmbBatch.SelectedValue), Common.Institution);
                    cmbStudent.DisplayMember = "Name";
                    cmbStudent.ValueMember = "ID";

                }
                lblFeeAmount.Text = "0";
                lblFeePaid.Text = "0";
                lblAmountDue.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void cmbFeeType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                lblMonthlyFee.Text = "";
                DataTable dt = (DataTable)objTradeFeeDetails.GetTradeFeeDetails(Common.Institution, Convert.ToInt32(cmbTrade.SelectedValue), Convert.ToInt32(cmbBatch.SelectedValue));
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    string sQueryFeePaid = "";
                    switch (cmbFeeType.SelectedValue)
                    {
                        case 1:
                            lblFeeAmount.Text = Convert.ToString(dr["Registration_Fee"]);
                            sQueryFeePaid = " Registration_Fee ";
                            break;
                        case 2:
                            lblFeeAmount.Text = Convert.ToString(dr["Caution_Fee"]);
                            sQueryFeePaid = " Caution_Fee ";
                            break;
                        case 3:
                            lblFeeAmount.Text = Convert.ToString(dr["Sem1_Fee"]);
                            sQueryFeePaid = " Sem1_Fee ";
                            break;
                        case 4:
                            lblFeeAmount.Text = Convert.ToString(dr["Sem2_Fee"]);
                            sQueryFeePaid = " Sem2_Fee ";
                            break;
                        case 5:
                            lblFeeAmount.Text = Convert.ToString(dr["Sem3_Fee"]);
                            sQueryFeePaid = " Sem3_Fee ";
                            break;
                        case 6:
                            lblFeeAmount.Text = Convert.ToString(dr["Sem4_Fee"]);
                            sQueryFeePaid = " Sem4_Fee ";
                            break;
                        case 7:
                            lblFeeAmount.Text = Convert.ToString(Convert.ToDecimal(dr["Monthly_Fee"]) * Convert.ToDecimal(dr["No_OfInstallments"]));
                            lblMonthlyFee.Visible = true;
                            lblMonthlyFee.Text = "(" + Convert.ToString(dr["Monthly_Fee"]) + " X " + Convert.ToString(dr["No_OfInstallments"]) + ")";
                            sQueryFeePaid = " Installment_24 ";
                            break;
                        case 8:
                            lblFeeAmount.Text = Convert.ToString(dr["Record_Fee"]);
                            sQueryFeePaid = " Record_Fee ";
                            break;
                        case 9:
                            lblFeeAmount.Text = Convert.ToString(dr["Uniform_Fee"]);
                            sQueryFeePaid = " Uniform_Fee ";
                            break;
                        default:
                            lblFeeAmount.Text = "0.00";
                            break;
                    }
                    decimal feePaid = Convert.ToDecimal(objStudentFeeDetails.GetSingleFeePaid(sQueryFeePaid, Convert.ToString(cmbStudent.SelectedValue)));
                    lblFeePaid.Text = feePaid.ToString();
                    lblAmountDue.Text = Convert.ToString(Convert.ToDecimal(lblFeeAmount.Text) - feePaid);

                    lblerror.Visible = false;
                    lblerror.Text = "";
                }
                else
                {
                    lblerror.Visible = true;
                    lblerror.Text = "Fee structure is not registered for this trade";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFeeAmount_Validating(object sender, CancelEventArgs e)
        {
            if (txtFeeAmount.Value > Convert.ToDecimal(lblAmountDue.Text))
            {
                MessageBox.Show("Amount mismatch");
                e.Cancel = true;
            }
        }
        private void PreviewPrintValues(int? FeeTypeID , decimal? amtInDigits ,DateTime? datePrint ,int pymtID , string sRecptNo, bool previewAfterSave)
        {
            try
            {
                string feeReceiptOption = "-1";
                switch (FeeTypeID)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        feeReceiptOption = "S";
                        break;
                    case 7:
                        feeReceiptOption = "M";
                        break;
                    case 8:
                    case 9:
                        feeReceiptOption = "O";
                        break;
                }

                if (sRecptNo.Trim().Length == 0)
                    txtRcptNoPrint.Text = Convert.ToString(objPymt.GetReceiptNumber(feeReceiptOption, Common.Institution));
                else
                    txtRcptNoPrint.Text = sRecptNo;
                txtRcptNoPrint.Tag = FeeTypeID;
                //feeCrystalReport1.SetDataSource(dsSTudentDetails);

               
                string feeType = "";
                switch (FeeTypeID)
                {
                    case 1:
                       feeType = "Admission Fee";
                        break;
                    case 2:
                       feeType = "Caution Fee";
                        break;
                    case 3:
                       feeType = Humanizer.OrdinalizeExtensions.Ordinalize(1) + " Semester Fee";
                        break;
                    case 4:
                       feeType = Humanizer.OrdinalizeExtensions.Ordinalize(2) + " Semester Fee";
                        break;
                    case 5:
                       feeType = Humanizer.OrdinalizeExtensions.Ordinalize(3) + " Semester Fee";
                        break;
                    case 6:
                       feeType = Humanizer.OrdinalizeExtensions.Ordinalize(4) + " Semester Fee";
                        break;
                    case 7:
                        DataRow drNumberOfInstallment =objPymt.GetInstallmentNumber(Convert.ToString(cmbStudent.SelectedValue),pymtID);
                        if(Convert.ToString(drNumberOfInstallment[0] ) == Convert.ToString(drNumberOfInstallment[1]))
                            feeType = Humanizer.OrdinalizeExtensions.Ordinalize(Convert.ToString(drNumberOfInstallment[0])) + " Installment";
                        else
                            feeType = Humanizer.OrdinalizeExtensions.Ordinalize(Convert.ToString(drNumberOfInstallment[1])) + " to " + Humanizer.OrdinalizeExtensions.Ordinalize(Convert.ToString(drNumberOfInstallment[0])) + " Installment";
                        break;
                    case 8:
                       feeType = "Record Fee";
                        break;
                    case 9:
                       feeType = "Uniform Fee";
                        break;
                    default:
                       feeType = "Invalid Fee";
                        break;
                }

                string rptText = Properties.Resources.PrintReportText;
                

              

                DataSet dsSTudentDetails = objStudent.PrintReceipt(Convert.ToString(cmbStudent.SelectedValue));
                DataRow dr = dsSTudentDetails.Tables[0].Rows[0];
                FeeReportPrint objPrintData = new FeeReportPrint();
                objPrintData.StudID = Convert.ToString(dr["ID"]).ToUpper();
                objPrintData.StudentName = Convert.ToString(dr["Name"]).ToUpper();
                objPrintData.Trade = Convert.ToString(dr["Trade"]).ToUpper();
                objPrintData.Address1 = Convert.ToString(dr["Address1"]).ToUpper();
                objPrintData.Address2 = Convert.ToString(dr["Address2"]).ToUpper();
                objPrintData.Address3 = Convert.ToString(dr["Address3"]).ToUpper();
                objPrintData.ReceiptNo = Convert.ToString(dr["Name"]).ToUpper();
                objPrintData.Course = Convert.ToString(dr["Name"]).ToUpper();
                objPrintData.Batch = Convert.ToString(dr["Batch"]).ToUpper();
                objPrintData.ReceiptNo = txtRcptNoPrint.Text.Trim();
                objPrintData.Date = Convert.ToDateTime(datePrint);
                objPrintData.FeeType = feeType;
                objPrintData.FeeTypeID =(int) FeeTypeID;
                objPrintData.FeeRceiptOption = feeReceiptOption;
                objPrintData.PymtID = pymtID;
               
               

                string course = Convert.ToString(dr["Batch"]).ToUpper();
                if (course.Contains('-'))
                    course = course.Substring(0, course.IndexOf('-'));
                objPrintData.Course = course;

                TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                string AmtInWords = myTI.ToTitleCase(Humanizer.NumberToWordsExtension.ToWords(Convert.ToInt32(amtInDigits), System.Globalization.CultureInfo.CurrentCulture));
                objPrintData.AmountinWords = AmtInWords;
                objPrintData.AmountinDigits = Convert.ToDecimal(amtInDigits);
                txtReportText.Text = rptText.Replace("<pName>", Convert.ToString(dr["Name"]).ToUpper()).Replace("<pAmtWords>", AmtInWords).Replace("<pAmtDigit>", Convert.ToString( amtInDigits)).Replace("<pFeeType>", feeType).Replace("<pCourse>", Convert.ToString(dr["Trade"]).ToUpper()).Replace("<StartYear>", course);
                objPrintData.ReportText = txtReportText.Text;
                txtReportText.Tag = objPrintData;
                if(previewAfterSave)
                {
                    BusinessLayer.ClsBLPayment_Details objPymt = new ClsBLPayment_Details();
                    if (feeReceiptOption == "S")
                        objPymt.UpdateSemesterReceiptNumber(objPrintData.ReceiptNo.Replace("S", ""), Common.Institution);
                    else if (feeReceiptOption == "M")
                        objPymt.UpdateMonthlyReceiptNumber(objPrintData.ReceiptNo.Replace("M", ""), Common.Institution);
                    else
                        objPymt.UpdateOtherReceiptNumber(objPrintData.ReceiptNo, Common.Institution);
                    objPymt.UpdateReceiptNoAfterPrint(objPrintData.PymtID, objPrintData.ReceiptNo);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private  void Print()
        {
            try
            {
               
                ShowReport frmSR = new ShowReport();
                frmSR.objReportData = (FeeReportPrint)txtReportText.Tag;
                frmSR.objReportData.ReceiptNo = txtRcptNoPrint.Text.Trim();
                frmSR.objReportData.ReportText = txtReportText.Text.Trim();
               

                //if (cmbStudent.SelectedValue != null && Convert.ToString(cmbStudent.SelectedValue) != "-1")
                //{
                //    frmSR.StudentID = Convert.ToString(cmbStudent.SelectedValue);

                //    frmSR.RcptNo = Convert.ToString(objPymt.GetReceiptNumber(feeOption, Common.Institution));
                //    if (feeOption == "S")
                //        objPymt.UpdateSemesterReceiptNumber(frmSR.RcptNo.Replace("S", ""), Common.Institution);
                //    else if (feeOption == "M")
                //        objPymt.UpdateMonthlyReceiptNumber(frmSR.RcptNo.Replace("M", ""), Common.Institution);

                //    if (feeOption == "S")
                //        objPymt.UpdateSemesterReceiptNumber(frmSR.RcptNo.Replace("S", ""), Common.Institution);
                //    else if (feeOption == "M")
                //        objPymt.UpdateMonthlyReceiptNumber(frmSR.RcptNo.Replace("M", ""), Common.Institution);
                //    frmSR.Date = Convert.ToString(dtpPymtDt.Value.ToString("dd/MM/yyyy"));

                //    frmSR.AmtInWords = Humanizer.NumberToWordsExtension.ToWords(Convert.ToInt32(txtFeeAmount.Value), System.Globalization.CultureInfo.CurrentCulture);
                //    frmSR.AmtInDigits = Convert.ToString(txtFeeAmount.Value);



                frmSR.Show();
                //    //////////////////////////////////////////
                //    ///

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
            txtRcptNoPrint.Text = "";
            txtReportText.Text = "";
            
            try
            {
                dataGridView1.DataSource = objPymt.GetFeePaidForPrint(Convert.ToString(cmbStudent.SelectedValue));
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbStudent_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lblFeeAmount.Text = "0";
            lblFeePaid.Text = "0";
            lblAmountDue.Text = "0";

            FillFeeDetailForTheStudent();
           
            tableLayoutPanel2.Visible = true;
            
            try
            {
                dataGridView1.DataSource = objPymt.GetFeePaidForPrint(Convert.ToString(cmbStudent.SelectedValue));
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;
                dataGridView1.Columns[6].ReadOnly = true;
                dataGridView1.Columns[7].ReadOnly = true;
                dataGridView1.Columns[8].ReadOnly = true;
                dataGridView1.Columns[9].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

                    txtRegFeePaid.Text = Convert.ToString(dr["Registration_Fee"]);
                    txtCautionFeePaid.Text = Convert.ToString(dr["Caution_Fee"]);
                    txtSem1FeePaid.Text = Convert.ToString(dr["Sem1_Fee"]);
                    txtSem2FeePaid.Text = Convert.ToString(dr["Sem2_Fee"]);
                    txtSem3FeePaid.Text = Convert.ToString(dr["Sem3_Fee"]);
                    txtSem4FeePaid.Text = Convert.ToString(dr["Sem4_Fee"]);
                    txtMonthlyFeePaid.Text = Convert.ToString(dr["Installment_24"]);
                    txtRecordFeePaid.Text = Convert.ToString(dr["Record_Fee"]);
                    txtUniformFeePaid.Text = Convert.ToString(dr["Uniform_Fee"]);
                }
                else
                {
                    txtRegFeePaid.Text = "0";
                    txtSem1FeePaid.Text = "0";
                    txtCautionFeePaid.Text = "0";
                    txtSem2FeePaid.Text = "0";
                    txtSem3FeePaid.Text = "0";
                    txtSem4FeePaid.Text = "0";
                    txtMonthlyFeePaid.Text = "0";
                    txtRecordFeePaid.Text = "0";
                    txtUniformFeePaid.Text = "0";
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

                txtFeeConcession.Text = Convert.ToString(objStudent.GetConcessionFee(Convert.ToString(cmbStudent.SelectedValue)));

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private bool Validate()
        {
            if (txtFeeAmount.Value <= 0 || Convert.ToInt32(cmbBatch.SelectedValue) == -1 || Convert.ToInt32(cmbTrade.SelectedValue) == -1 || Convert.ToString(cmbStudent.SelectedValue) == "-1" || Convert.ToInt32(lblFeeAmount.Text) <=0) return false;
            return true;
        }

        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    DataGridViewTextBoxCell cellFeeType = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells["FeeType ID"];
                    DataGridViewTextBoxCell cellAmount = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells["Fee Amount"];
                    DataGridViewTextBoxCell cellDate = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells["Date"];
                    DataGridViewTextBoxCell cellPymtID = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells["Payment ID"];
                    DataGridViewTextBoxCell cellRcptNo = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells["Receipt No"];
                    PreviewPrintValues(Convert.ToInt16(cellFeeType.Value), Convert.ToDecimal(cellAmount.Value), Convert.ToDateTime(cellDate.Value), Convert.ToInt32(cellPymtID.Value), Convert.ToString(cellRcptNo.Value),false);
                }
                if (e.ColumnIndex == 1 && e.RowIndex != -1)
                {
                    dataGridView1.Columns[5].ReadOnly = false;
                    dataGridView1.Columns[6].ReadOnly = false;
                    dataGridView1.Rows[e.RowIndex].Cells[5].Selected = true;
                    dataGridView1.Select();
                }
                if (e.ColumnIndex == 2 && e.RowIndex != -1)
                {
                    if( dataGridView1.Columns[5].ReadOnly== false)
                    {
                        DataGridViewTextBoxCell cellFeeType = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells["FeeType ID"];
                        DataGridViewTextBoxCell cellPymtID = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells["Payment ID"];
                        DataGridViewTextBoxCell cellFeeAmount = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells["Fee Amount"];
                        DataGridViewTextBoxCell cellFeeDate = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells["Date"];

                        PropertyLayer.Payment_Details objPaymtProp = new PropertyLayer.Payment_Details();
                        objPaymtProp.Pymt_ID = Convert.ToInt32(cellPymtID.Value);
                        objPaymtProp.Stud_ID = Convert.ToString(cmbStudent.SelectedValue);
                        objPaymtProp.Fee_Type = Convert.ToInt16(cellFeeType.Value);
                        objPaymtProp.Batch_ID = Convert.ToInt32(cmbBatch.SelectedValue);
                        objPaymtProp.Trade_ID = Convert.ToInt32(cmbTrade.SelectedValue);

                        objPaymtProp.Fee_Amount = Convert.ToDecimal(cellFeeAmount.Value);
                        objPaymtProp.Date = Convert.ToDateTime(cellFeeDate.Value);

                        objPymt.UpdateData(objPaymtProp);

                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                    }
                }

                if (e.ColumnIndex == 3 && e.RowIndex != -1)
                {
                    DialogResult rslt = MessageBox.Show("Are you sure you want to delete?", "Migration", MessageBoxButtons.YesNo);
                    if (rslt == DialogResult.Yes)
                    {
                        DataGridViewTextBoxCell cellFeeType = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells["FeeType ID"];
                        DataGridViewTextBoxCell cellPymtID = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells["Payment ID"];

                        PropertyLayer.Payment_Details objPaymtProp = new PropertyLayer.Payment_Details();
                        objPaymtProp.Pymt_ID = Convert.ToInt32(cellPymtID.Value);
                        objPaymtProp.Stud_ID = Convert.ToString(cmbStudent.SelectedValue);
                        objPaymtProp.Fee_Type = Convert.ToInt16(cellFeeType.Value);
                        objPymt.DeleteData(objPaymtProp);
                        try
                        {
                            dataGridView1.DataSource = objPymt.GetFeePaidForPrint(Convert.ToString(cmbStudent.SelectedValue));
                            FillFeeDetailForTheStudent();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
