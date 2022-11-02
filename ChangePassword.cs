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
    public partial class ChangePassword : Form
    {
        ClsBLUserLogin objBlUserLogin = new ClsBLUserLogin();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                PropertyLayer.UserLogin objULProp = new PropertyLayer.UserLogin();
                objULProp.UserID = Common.UserID;
                objULProp.UserType = 1;
                objULProp.Password = txtNewPassword.Text.Trim();
                objULProp.OldPassword = txtOldPassword.Text;
                objULProp.ErrorMessage = "";
                string result = objBlUserLogin.UpdateData(objULProp);
                if (result.Trim().Length == 0)
                {
                    txtNewPassword.Text = "";
                    txtOldPassword.Text = "";
                    MessageBox.Show("Password updated successfully. It will take on effect from the next login.");
                }
                else
                    MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in password update. Please contact your administrator: " + ex.Message);
            }
        }
    }
}
