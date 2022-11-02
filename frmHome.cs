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
    public partial class frmHome : Form
    {
        ClsBLInstitution objInstitution;
        public frmHome()
        {
            InitializeComponent();
            CustomizeDesign();
        }
        private void CustomizeDesign()
        {
            panelRegistration.Visible = false;
            panelReport.Visible = false;
            panelFeePymt.Visible = false;
        }

        private void HideSubMenu()
        {
            if (panelRegistration.Visible) panelRegistration.Visible = false;
            if (panelReport.Visible) panelReport.Visible = false;
            if (panelFeePymt.Visible) panelFeePymt.Visible = false;
        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            showSubMenu(panelRegistration);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            showSubMenu(panelReport);
        }

        private void btnStudReg_Click(object sender, EventArgs e)
        {
            OpenChildForm(new StudentRegistration());
            HideSubMenu();
        }

        private void btnBatchReg_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BatchMaster());
            HideSubMenu();
        }

        private void btnTrade_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TradeMaster());
            HideSubMenu();
        }

        private void btnTradeFee_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TradeFeeEntry());
            HideSubMenu();
        }

        private void btnStudDetails_Click(object sender, EventArgs e)
        {
            OpenChildForm(new StudentReport());
            HideSubMenu();
        }

        private void btnFeeDetails_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FeeReport());
            HideSubMenu();
        }
        private Form activeForm = null;
        public void OpenChildForm(Form childForm)
        {
            try
            { 
                if (activeForm != null) activeForm.Close();
                activeForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                panelChildForm.Controls.Add(childForm);
                panelChildForm.Tag = childForm;
                childForm.Parent = this;
                childForm.BringToFront();
                childForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (activeForm != null) activeForm.Close();
            this.Close();
            Application.Exit();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            objInstitution = new ClsBLInstitution();
            lblInstitution.Text = objInstitution.GetInstitutionName( Common.Institution).ToUpper();
        }

        private void btnTradeChange_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TradeChange());
            HideSubMenu();
        }

        private void btnFeePayment_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FeePayment());
            HideSubMenu();
        }

        private void btnPaymentInfo_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FeePaymentInfo());
            HideSubMenu();
        }

        private void btnFee_Click(object sender, EventArgs e)
        {
            showSubMenu(panelFeePymt);
        }

        private void btnChangeInstitution_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BlankForm());
            //this.Close();
            Institution clsInstitution = new Institution();
            //if (activeForm != null) activeForm.Close();
            clsInstitution.Show();
        }

        private void btnChangPassword_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ChangePassword());
            HideSubMenu();
        }
    }
}
