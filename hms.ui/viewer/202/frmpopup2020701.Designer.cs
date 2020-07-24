namespace Hms.Ui
{
    partial class frmpopup2020701
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.cardView = new DevExpress.XtraGrid.Views.Card.CardView();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rdgQn = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdgQn.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.rdgQn);
            this.pcBackGround.Controls.Add(this.labelControl1);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcBackGround.Size = new System.Drawing.Size(501, 36);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // marqueeProgressBarControl
            // 
            this.marqueeProgressBarControl.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnAdd);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 480);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(501, 36);
            this.panelControl2.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Location = new System.Drawing.Point(170, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(107, 26);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "新增问卷";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "选择类型：";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControl);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 36);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(501, 444);
            this.panelControl1.TabIndex = 5;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(2, 2);
            this.gridControl.MainView = this.cardView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(497, 440);
            this.gridControl.TabIndex = 4;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cardView});
            // 
            // cardView
            // 
            this.cardView.CardInterval = 14;
            this.cardView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn14});
            this.cardView.FocusedCardTopFieldIndex = 0;
            this.cardView.GridControl = this.gridControl;
            this.cardView.Name = "cardView";
            this.cardView.OptionsBehavior.FieldAutoHeight = true;
            this.cardView.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent;
            this.cardView.OptionsView.ShowCardCaption = false;
            this.cardView.OptionsView.ShowFieldCaptions = false;
            this.cardView.OptionsView.ShowFieldHints = false;
            this.cardView.OptionsView.ShowLines = false;
            this.cardView.OptionsView.ShowQuickCustomizeButton = false;
            this.cardView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn14.AppearanceCell.Options.UseFont = true;
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.Caption = "gridColumn8";
            this.gridColumn14.FieldName = "qnName";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsColumn.AllowFocus = false;
            this.gridColumn14.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn14.OptionsFilter.AllowFilter = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 0;
            this.gridColumn14.Width = 200;
            // 
            // rdgQn
            // 
            this.rdgQn.Location = new System.Drawing.Point(79, 5);
            this.rdgQn.Name = "rdgQn";
            this.rdgQn.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "心理问卷"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "中医问卷"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "自定义问卷")});
            this.rdgQn.Size = new System.Drawing.Size(304, 27);
            this.rdgQn.TabIndex = 2;
            this.rdgQn.SelectedIndexChanged += new System.EventHandler(this.rdgQn_SelectedIndexChanged);
            // 
            // frmpopup2020701
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 516);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.Name = "frmpopup2020701";
            this.Text = "选择问卷类型";
            this.Load += new System.EventHandler(this.frmpopup2020701_Load);
            this.Controls.SetChildIndex(this.marqueeProgressBarControl, 0);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdgQn.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Card.CardView cardView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.RadioGroup rdgQn;
    }
}