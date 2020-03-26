﻿namespace Console.Ui
{
    partial class frmLoadDic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadDic));
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.chkDept = new DevExpress.XtraEditors.CheckEdit();
            this.chkEmp = new DevExpress.XtraEditors.CheckEdit();
            this.chkRank = new DevExpress.XtraEditors.CheckEdit();
            this.chkPat = new DevExpress.XtraEditors.CheckEdit();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.marqueeProgressBarControlLoad = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControlLoad.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.marqueeProgressBarControlLoad);
            this.pcBackGround.Controls.Add(this.btnImport);
            this.pcBackGround.Controls.Add(this.chkPat);
            this.pcBackGround.Controls.Add(this.chkRank);
            this.pcBackGround.Controls.Add(this.chkEmp);
            this.pcBackGround.Controls.Add(this.chkDept);
            this.pcBackGround.Controls.Add(this.chkAll);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcBackGround.Location = new System.Drawing.Point(0, 0);
            this.pcBackGround.Size = new System.Drawing.Size(354, 368);
            this.pcBackGround.Visible = true;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // chkAll
            // 
            this.chkAll.Location = new System.Drawing.Point(59, 49);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAll.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.chkAll.Properties.Appearance.Options.UseFont = true;
            this.chkAll.Properties.Appearance.Options.UseForeColor = true;
            this.chkAll.Properties.Caption = "全部字典";
            this.chkAll.Size = new System.Drawing.Size(112, 19);
            this.chkAll.TabIndex = 0;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // chkDept
            // 
            this.chkDept.Location = new System.Drawing.Point(59, 102);
            this.chkDept.Name = "chkDept";
            this.chkDept.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDept.Properties.Appearance.Options.UseFont = true;
            this.chkDept.Properties.Caption = "1.科室字典";
            this.chkDept.Size = new System.Drawing.Size(112, 19);
            this.chkDept.TabIndex = 1;
            this.chkDept.CheckedChanged += new System.EventHandler(this.chkDept_CheckedChanged);
            // 
            // chkEmp
            // 
            this.chkEmp.Location = new System.Drawing.Point(59, 160);
            this.chkEmp.Name = "chkEmp";
            this.chkEmp.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEmp.Properties.Appearance.Options.UseFont = true;
            this.chkEmp.Properties.Caption = "2.用户字典";
            this.chkEmp.Size = new System.Drawing.Size(112, 19);
            this.chkEmp.TabIndex = 2;
            this.chkEmp.CheckedChanged += new System.EventHandler(this.chkEmp_CheckedChanged);
            // 
            // chkRank
            // 
            this.chkRank.Location = new System.Drawing.Point(188, 102);
            this.chkRank.Name = "chkRank";
            this.chkRank.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRank.Properties.Appearance.Options.UseFont = true;
            this.chkRank.Properties.Caption = "3.职称字典";
            this.chkRank.Size = new System.Drawing.Size(112, 19);
            this.chkRank.TabIndex = 3;
            this.chkRank.CheckedChanged += new System.EventHandler(this.chkRank_CheckedChanged);
            // 
            // chkPat
            // 
            this.chkPat.Enabled = false;
            this.chkPat.Location = new System.Drawing.Point(188, 160);
            this.chkPat.Name = "chkPat";
            this.chkPat.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPat.Properties.Appearance.Options.UseFont = true;
            this.chkPat.Properties.Caption = "4.患者字典";
            this.chkPat.Size = new System.Drawing.Size(112, 19);
            this.chkPat.TabIndex = 4;
            this.chkPat.CheckedChanged += new System.EventHandler(this.chkPat_CheckedChanged);
            // 
            // btnImport
            // 
            this.btnImport.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Appearance.Options.UseFont = true;
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.Location = new System.Drawing.Point(2, 342);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(350, 24);
            this.btnImport.TabIndex = 9;
            this.btnImport.Text = "同步字典 ";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // marqueeProgressBarControlLoad
            // 
            this.marqueeProgressBarControlLoad.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.marqueeProgressBarControlLoad.EditValue = "... ...";
            this.marqueeProgressBarControlLoad.Location = new System.Drawing.Point(2, 318);
            this.marqueeProgressBarControlLoad.MenuManager = this.barManager;
            this.marqueeProgressBarControlLoad.Name = "marqueeProgressBarControlLoad";
            this.marqueeProgressBarControlLoad.Properties.ShowTitle = true;
            this.marqueeProgressBarControlLoad.Size = new System.Drawing.Size(350, 24);
            this.marqueeProgressBarControlLoad.TabIndex = 11;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // frmLoadDic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 368);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoadDic";
            this.Text = "同步字典";
            this.Load += new System.EventHandler(this.frmLoadDic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControlLoad.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.SimpleButton btnImport;
        internal DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControlLoad;
        internal DevExpress.XtraEditors.CheckEdit chkPat;
        internal DevExpress.XtraEditors.CheckEdit chkRank;
        internal DevExpress.XtraEditors.CheckEdit chkEmp;
        internal DevExpress.XtraEditors.CheckEdit chkDept;
        internal DevExpress.XtraEditors.CheckEdit chkAll;
        internal System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}