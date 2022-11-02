
namespace Winners_ITI
{
    partial class TradeChange
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSTudent = new System.Windows.Forms.ComboBox();
            this.lblStudent = new System.Windows.Forms.Label();
            this.cmbSourceBatch = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbSourceTrade = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSourceInstitution = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbDestBatch = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbDestTrade = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDestInstitution = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnApplyChange = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSTudent);
            this.groupBox1.Controls.Add(this.lblStudent);
            this.groupBox1.Controls.Add(this.cmbSourceBatch);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.cmbSourceTrade);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbSourceInstitution);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 11.75F);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(110)))), ((int)(((byte)(130)))));
            this.groupBox1.Location = new System.Drawing.Point(128, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 194);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current";
            // 
            // cmbSTudent
            // 
            this.cmbSTudent.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.cmbSTudent.FormattingEnabled = true;
            this.cmbSTudent.Location = new System.Drawing.Point(140, 141);
            this.cmbSTudent.Name = "cmbSTudent";
            this.cmbSTudent.Size = new System.Drawing.Size(475, 31);
            this.cmbSTudent.TabIndex = 3;
            this.cmbSTudent.SelectionChangeCommitted += new System.EventHandler(this.cmbSTudent_SelectionChangeCommitted);
            // 
            // lblStudent
            // 
            this.lblStudent.AutoSize = true;
            this.lblStudent.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.lblStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(110)))), ((int)(((byte)(130)))));
            this.lblStudent.Location = new System.Drawing.Point(24, 149);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(69, 23);
            this.lblStudent.TabIndex = 81;
            this.lblStudent.Text = "Student";
            // 
            // cmbSourceBatch
            // 
            this.cmbSourceBatch.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.cmbSourceBatch.FormattingEnabled = true;
            this.cmbSourceBatch.Location = new System.Drawing.Point(140, 71);
            this.cmbSourceBatch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSourceBatch.Name = "cmbSourceBatch";
            this.cmbSourceBatch.Size = new System.Drawing.Size(475, 31);
            this.cmbSourceBatch.TabIndex = 1;
            this.cmbSourceBatch.SelectedIndexChanged += new System.EventHandler(this.cmbSourceTrade_SelectionChangeCommitted);
            this.cmbSourceBatch.SelectionChangeCommitted += new System.EventHandler(this.cmbSourceTrade_SelectionChangeCommitted);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.label17.Location = new System.Drawing.Point(24, 79);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 23);
            this.label17.TabIndex = 79;
            this.label17.Text = "Batch";
            // 
            // cmbSourceTrade
            // 
            this.cmbSourceTrade.FormattingEnabled = true;
            this.cmbSourceTrade.Location = new System.Drawing.Point(140, 106);
            this.cmbSourceTrade.Name = "cmbSourceTrade";
            this.cmbSourceTrade.Size = new System.Drawing.Size(475, 29);
            this.cmbSourceTrade.TabIndex = 2;
            this.cmbSourceTrade.SelectedIndexChanged += new System.EventHandler(this.cmbSourceTrade_SelectedIndexChanged);
            this.cmbSourceTrade.SelectionChangeCommitted += new System.EventHandler(this.cmbSourceTrade_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.label2.Location = new System.Drawing.Point(24, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Trade";
            // 
            // cmbSourceInstitution
            // 
            this.cmbSourceInstitution.Enabled = false;
            this.cmbSourceInstitution.FormattingEnabled = true;
            this.cmbSourceInstitution.Location = new System.Drawing.Point(140, 36);
            this.cmbSourceInstitution.Name = "cmbSourceInstitution";
            this.cmbSourceInstitution.Size = new System.Drawing.Size(475, 29);
            this.cmbSourceInstitution.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.label1.Location = new System.Drawing.Point(24, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Institution";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbDestBatch);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.cmbDestTrade);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbDestInstitution);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 11.75F);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(110)))), ((int)(((byte)(130)))));
            this.groupBox2.Location = new System.Drawing.Point(128, 296);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(650, 180);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Move To";
            // 
            // cmbDestBatch
            // 
            this.cmbDestBatch.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.cmbDestBatch.FormattingEnabled = true;
            this.cmbDestBatch.Location = new System.Drawing.Point(140, 80);
            this.cmbDestBatch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbDestBatch.Name = "cmbDestBatch";
            this.cmbDestBatch.Size = new System.Drawing.Size(475, 31);
            this.cmbDestBatch.TabIndex = 5;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.label18.Location = new System.Drawing.Point(24, 84);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 23);
            this.label18.TabIndex = 81;
            this.label18.Text = "Batch";
            // 
            // cmbDestTrade
            // 
            this.cmbDestTrade.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.cmbDestTrade.FormattingEnabled = true;
            this.cmbDestTrade.Location = new System.Drawing.Point(140, 122);
            this.cmbDestTrade.Name = "cmbDestTrade";
            this.cmbDestTrade.Size = new System.Drawing.Size(475, 31);
            this.cmbDestTrade.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.label3.Location = new System.Drawing.Point(24, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Trade";
            // 
            // cmbDestInstitution
            // 
            this.cmbDestInstitution.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.cmbDestInstitution.FormattingEnabled = true;
            this.cmbDestInstitution.Location = new System.Drawing.Point(140, 37);
            this.cmbDestInstitution.Name = "cmbDestInstitution";
            this.cmbDestInstitution.Size = new System.Drawing.Size(475, 31);
            this.cmbDestInstitution.TabIndex = 4;
            this.cmbDestInstitution.SelectionChangeCommitted += new System.EventHandler(this.cmbDestInstitution_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.label4.Location = new System.Drawing.Point(24, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "Institution";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(110)))), ((int)(((byte)(130)))));
            this.label6.Location = new System.Drawing.Point(250, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(222, 37);
            this.label6.TabIndex = 27;
            this.label6.Text = "TRADE CHANGE";
            // 
            // btnApplyChange
            // 
            this.btnApplyChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(110)))), ((int)(((byte)(130)))));
            this.btnApplyChange.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.btnApplyChange.ForeColor = System.Drawing.Color.White;
            this.btnApplyChange.Location = new System.Drawing.Point(275, 482);
            this.btnApplyChange.Name = "btnApplyChange";
            this.btnApplyChange.Size = new System.Drawing.Size(461, 44);
            this.btnApplyChange.TabIndex = 69;
            this.btnApplyChange.Text = "APPLY CHANGE";
            this.btnApplyChange.UseVisualStyleBackColor = false;
            this.btnApplyChange.Click += new System.EventHandler(this.btnApplyChange_Click);
            // 
            // TradeChange
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(227)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(1015, 665);
            this.Controls.Add(this.btnApplyChange);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(250, 100);
            this.Name = "TradeChange";
            this.Load += new System.EventHandler(this.TradeChange_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbSourceTrade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSourceInstitution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbDestTrade;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDestInstitution;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnApplyChange;
        private System.Windows.Forms.ComboBox cmbSTudent;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.ComboBox cmbSourceBatch;
        private System.Windows.Forms.ComboBox cmbDestBatch;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;

        #endregion

        //private System.Windows.Forms.ComboBox comboBox3;
        //private System.Windows.Forms.Label label22;
        //private System.Windows.Forms.Label label21;
        //private System.Windows.Forms.ComboBox comboBox2;
        //private System.Windows.Forms.TextBox textBox18;
        //private System.Windows.Forms.Button button9;
        //private System.Windows.Forms.Button button10;
        //private System.Windows.Forms.Label label29;
        //private System.Windows.Forms.Label label30;
        //public System.Windows.Forms.Label label32;
        //private System.Windows.Forms.ComboBox comboBox1;
    }
}