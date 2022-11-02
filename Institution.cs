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
using System.Data;

namespace Winners_ITI
{
    public partial class Institution : Form
    {
        ClsBLInstitution objInstitution;
        public Institution()
        {
            InitializeComponent();
        }

       

        private void Institution_Load(object sender, EventArgs e)
        {
            try
            { 
                objInstitution =   new ClsBLInstitution();
               DataTable dtUnivList= (DataTable) objInstitution.GetData();
                cmbInstitution.DataSource = dtUnivList;
                cmbInstitution.ValueMember = "Inst_ID";
                cmbInstitution.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbInstitution_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Common.Institution = Convert.ToInt16(cmbInstitution.SelectedValue);
            this.Hide();
            //Home HomePage = new Home();
            ////HomePage.lblSelectedInstitution.Text = cmbInstitution.SelectedItem.ToString();
            //HomePage.ShowDialog();
            frmHome f = new frmHome();
            f.ShowDialog();
        }

        private void cmbInstitution_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
