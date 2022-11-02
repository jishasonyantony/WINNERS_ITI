using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winners_ITI
{
    public partial class Home: StyledForms.GoogleTalkForm
    {
        private Form MyChild;
        public Home()
        {
            InitializeComponent();
        }

        private void studentFeeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if(MyChild != null)
            {
                MyChild.Close();
                MyChild.Dispose();
            }
            MyChild = new StudentRegistration();
            MyChild.MdiParent = this;
            MyChild.Dock = DockStyle.Fill;
            MyChild.Show();
        }

        private void tradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MyChild != null)
            {
                MyChild.Close();
                MyChild.Dispose();
            }
            MyChild = new TradeMaster();
            MyChild.MdiParent = this;
            MyChild.Dock = DockStyle.Fill;
            MyChild.Show();

            
        }

       

        private void feePaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MyChild != null)
            {
                MyChild.Close();
                MyChild.Dispose();
            }
            MyChild = new FeePayment();
            MyChild.MdiParent = this;
            MyChild.Dock = DockStyle.Fill;
            MyChild.Show();
        }

       

        private void BatchtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MyChild != null)
            {
                MyChild.Close();
                MyChild.Dispose();
            }
            MyChild = new BatchMaster();
            MyChild.MdiParent = this;
            MyChild.Dock = DockStyle.Fill;
            MyChild.Show();
        }

        private void tradeFeetoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MyChild != null)
            {
                MyChild.Close();
                MyChild.Dispose();
            }
            MyChild = new TradeFeeEntry();
            MyChild.MdiParent = this;
            MyChild.Dock = DockStyle.Fill;
            MyChild.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            Rectangle screenRectangle = this.RectangleToScreen(this.ClientRectangle);

            int titleHeight = screenRectangle.Top - this.Top;
        }
    }
}
