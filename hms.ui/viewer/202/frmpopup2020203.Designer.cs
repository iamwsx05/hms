namespace Hms.Ui
{
    partial class frmPopup2020203
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
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.qnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.className = new DevExpress.XtraGrid.Columns.GridColumn();
            this.hazardFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.btnOk);
            this.pcBackGround.Controls.Add(this.btnSearch);
            this.pcBackGround.Controls.Add(this.txtSearch);
            this.pcBackGround.Controls.Add(this.labelControl2);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcBackGround.Size = new System.Drawing.Size(656, 40);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // marqueeProgressBarControl
            // 
            this.marqueeProgressBarControl.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 40);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemDateEdit1,
            this.repositoryItemImageComboBox1});
            this.gridControl.Size = new System.Drawing.Size(656, 291);
            this.gridControl.TabIndex = 15;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.GroupPanel.Font = new System.Drawing.Font("宋体", 9F);
            this.gridView.Appearance.GroupPanel.Options.UseFont = true;
            this.gridView.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("宋体", 9.5F);
            this.gridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Appearance.Row.Font = new System.Drawing.Font("宋体", 9.5F);
            this.gridView.Appearance.Row.Options.UseFont = true;
            this.gridView.ColumnPanelRowHeight = 26;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.qnName,
            this.className,
            this.hazardFlag});
            this.gridView.GridControl = this.gridControl;
            this.gridView.IndicatorWidth = 38;
            this.gridView.Name = "gridView";
            this.gridView.OptionsSelection.MultiSelect = true;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.RowAutoHeight = true;
            this.gridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowHeight = 26;
            // 
            // qnName
            // 
            this.qnName.AppearanceCell.Options.UseTextOptions = true;
            this.qnName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.qnName.Caption = "问卷名称";
            this.qnName.FieldName = "qnName";
            this.qnName.Name = "qnName";
            this.qnName.OptionsColumn.AllowEdit = false;
            this.qnName.OptionsColumn.AllowFocus = false;
            this.qnName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.qnName.OptionsFilter.AllowAutoFilter = false;
            this.qnName.OptionsFilter.AllowFilter = false;
            this.qnName.Visible = true;
            this.qnName.VisibleIndex = 0;
            this.qnName.Width = 363;
            // 
            // className
            // 
            this.className.AppearanceCell.Options.UseTextOptions = true;
            this.className.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.className.Caption = "问卷类型";
            this.className.FieldName = "className";
            this.className.Name = "className";
            this.className.OptionsColumn.AllowEdit = false;
            this.className.OptionsColumn.AllowFocus = false;
            this.className.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.className.OptionsFilter.AllowAutoFilter = false;
            this.className.OptionsFilter.AllowFilter = false;
            this.className.Visible = true;
            this.className.VisibleIndex = 1;
            this.className.Width = 140;
            // 
            // hazardFlag
            // 
            this.hazardFlag.AppearanceCell.Options.UseTextOptions = true;
            this.hazardFlag.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.hazardFlag.Caption = "配置危险因素";
            this.hazardFlag.ColumnEdit = this.repositoryItemImageComboBox1;
            this.hazardFlag.FieldName = "hazardFlag";
            this.hazardFlag.ImageAlignment = System.Drawing.StringAlignment.Center;
            this.hazardFlag.Name = "hazardFlag";
            this.hazardFlag.OptionsColumn.AllowEdit = false;
            this.hazardFlag.OptionsColumn.AllowFocus = false;
            this.hazardFlag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.hazardFlag.OptionsFilter.AllowAutoFilter = false;
            this.hazardFlag.OptionsFilter.AllowFilter = false;
            this.hazardFlag.Visible = true;
            this.hazardFlag.VisibleIndex = 2;
            this.hazardFlag.Width = 113;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 2)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit1.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemMemoEdit1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ValueChecked = 1;
            this.repositoryItemCheckEdit1.ValueUnchecked = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(41, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSearch.Properties.Appearance.Options.UseFont = true;
            this.txtSearch.Properties.NullValuePrompt = "输入姓名或客户编号";
            this.txtSearch.Size = new System.Drawing.Size(176, 20);
            this.txtSearch.TabIndex = 13;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(11, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 12);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "查询";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(236, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(339, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmPopup2020203
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 331);
            this.Controls.Add(this.gridControl);
            this.Name = "frmPopup2020203";
            this.Text = "常规问卷";
            this.Load += new System.EventHandler(this.frmPopup2020203_Load);
            this.Controls.SetChildIndex(this.marqueeProgressBarControl, 0);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.gridControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn qnName;
        private DevExpress.XtraGrid.Columns.GridColumn className;
        private DevExpress.XtraGrid.Columns.GridColumn hazardFlag;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
    }
}