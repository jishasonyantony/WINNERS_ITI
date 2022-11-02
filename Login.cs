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
    public partial class Login : Form
    {
        ClsBLUserLogin objBlUserLogin = new ClsBLUserLogin();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            { 
                int userType = objBlUserLogin.Login(txtUsername.Text.Trim(), txtPassword.Text.Trim(), out Common.UserID, out Common.Password);
                switch (userType)
                {
                    case 1:
                        this.Hide();
                        Institution clsInstitution = new Institution();
                        clsInstitution.ShowDialog();
                        this.Close();
                        break;
                    default:
                        MessageBox.Show("Invalid login attempt");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
