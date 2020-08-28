namespace Console.Ui
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.timer = new System.Windows.Forms.Timer();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.txtAccountNo = new DevExpress.XtraEditors.TextEdit();
            this.txtPwd = new DevExpress.XtraEditors.TextEdit();
            this.picLnc = new System.Windows.Forms.PictureBox();
            this.picX = new System.Windows.Forms.PictureBox();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.picLogin = new System.Windows.Forms.PictureBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLnc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 5;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.Location = new System.Drawing.Point(554, 159);
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Properties.Appearance.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountNo.Properties.Appearance.Options.UseFont = true;
            this.txtAccountNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtAccountNo.Size = new System.Drawing.Size(251, 30);
            this.txtAccountNo.TabIndex = 1;
            this.txtAccountNo.EditValueChanged += new System.EventHandler(this.txtAccountNo_EditValueChanged);
            this.txtAccountNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccountNo_KeyDown);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(554, 229);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Properties.Appearance.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPwd.Properties.Appearance.Options.UseFont = true;
            this.txtPwd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtPwd.Properties.PasswordChar = '●';
            this.txtPwd.Size = new System.Drawing.Size(251, 30);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.EditValueChanged += new System.EventHandler(this.txtPwd_EditValueChanged);
            this.txtPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPwd_KeyDown);
            // 
            // picLnc
            // 
            this.picLnc.Image = ((System.Drawing.Image)(resources.GetObject("picLnc.Image")));
            this.picLnc.Location = new System.Drawing.Point(101, 468);
            this.picLnc.Name = "picLnc";
            this.picLnc.Size = new System.Drawing.Size(153, 26);
            this.picLnc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLnc.TabIndex = 11;
            this.picLnc.TabStop = false;
            this.picLnc.Visible = false;
            this.picLnc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picLnc_MouseDown);
            // 
            // picX
            // 
            this.picX.BackColor = System.Drawing.Color.White;
            this.picX.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picX.Image = ((System.Drawing.Image)(resources.GetObject("picX.Image")));
            this.picX.Location = new System.Drawing.Point(854, 5);
            this.picX.Name = "picX";
            this.picX.Size = new System.Drawing.Size(20, 18);
            this.picX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picX.TabIndex = 6;
            this.picX.TabStop = false;
            this.picX.Click += new System.EventHandler(this.picX_Click);
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnOk.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Appearance.Options.UseForeColor = true;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnOk.Location = new System.Drawing.Point(574, 278);
            this.btnOk.LookAndFeel.SkinName = "Office 2013";
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(216, 28);
            this.btnOk.TabIndex = 3;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // picLogin
            // 
            this.picLogin.BackColor = System.Drawing.Color.White;
            this.picLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogin.Image = ((System.Drawing.Image)(resources.GetObject("picLogin.Image")));
            this.picLogin.Location = new System.Drawing.Point(0, 0);
            this.picLogin.Margin = new System.Windows.Forms.Padding(0);
            this.picLogin.Name = "picLogin";
            this.picLogin.Size = new System.Drawing.Size(879, 450);
            this.picLogin.TabIndex = 1;
            this.picLogin.TabStop = false;
            this.picLogin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picLogin_MouseDown);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.ContentImage = ((System.Drawing.Image)(resources.GetObject("panelControl1.ContentImage")));
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Location = new System.Drawing.Point(454, 29);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(413, 78);
            this.panelControl1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(97, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "健康信息管理平台";
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.ContentImage = ((System.Drawing.Image)(resources.GetObject("panelControl2.ContentImage")));
            this.panelControl2.Controls.Add(this.label2);
            this.panelControl2.Location = new System.Drawing.Point(448, 380);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(357, 67);
            this.panelControl2.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(164, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "深圳市嘉润康科技有限公司";
            // 
            // panelControl3
            // 
            this.panelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.ContentImage = ((System.Drawing.Image)(resources.GetObject("panelControl3.ContentImage")));
            this.panelControl3.Location = new System.Drawing.Point(727, 333);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(127, 117);
            this.panelControl3.TabIndex = 14;
            // 
            // panelControl4
            // 
            this.panelControl4.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl4.Appearance.Options.UseBackColor = true;
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.ContentImage = ((System.Drawing.Image)(resources.GetObject("panelControl4.ContentImage")));
            this.panelControl4.Location = new System.Drawing.Point(554, 318);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(110, 104);
            this.panelControl4.TabIndex = 15;
            // 
            // panelControl5
            // 
            this.panelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl5.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl5.Appearance.Options.UseBackColor = true;
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.ContentImage = ((System.Drawing.Image)(resources.GetObject("panelControl5.ContentImage")));
            this.panelControl5.Location = new System.Drawing.Point(476, 154);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(67, 105);
            this.panelControl5.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(549, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "账号(";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(602, 134);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(16, 16);
            this.linkLabel1.TabIndex = 18;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "U";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(622, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = ")";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(622, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = ")";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel2.LinkColor = System.Drawing.Color.Black;
            this.linkLabel2.Location = new System.Drawing.Point(601, 205);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(16, 16);
            this.linkLabel2.TabIndex = 21;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "P";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(549, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "密码(";
            // 
            // panelControl6
            // 
            this.panelControl6.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl6.Appearance.Options.UseBackColor = true;
            this.panelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl6.ContentImage = ((System.Drawing.Image)(resources.GetObject("panelControl6.ContentImage")));
            this.panelControl6.Location = new System.Drawing.Point(454, 113);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(413, 17);
            this.panelControl6.TabIndex = 23;
            // 
            // frmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(879, 450);
            this.Controls.Add(this.panelControl6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panelControl5);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtAccountNo);
            this.Controls.Add(this.picLnc);
            this.Controls.Add(this.picX);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.picLogin);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(879, 450);
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "weCare登录...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLogin_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLnc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOk;
        private System.Windows.Forms.PictureBox picLogin;
        private System.Windows.Forms.PictureBox picX;
        private System.Windows.Forms.Timer timer;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.PictureBox picLnc;
        internal DevExpress.XtraEditors.TextEdit txtAccountNo;
        internal DevExpress.XtraEditors.TextEdit txtPwd;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.PanelControl panelControl6;
    }
}