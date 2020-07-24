namespace Hms.Ui
{
    partial class frm20602
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
            this.itemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deptName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.refRange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.isCompareName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.isMainName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.textEdit1);
            this.pcBackGround.Controls.Add(this.labelControl1);
            this.pcBackGround.Controls.Add(this.txtSearch);
            this.pcBackGround.Controls.Add(this.labelControl2);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcBackGround.Location = new System.Drawing.Point(0, 0);
            this.pcBackGround.Size = new System.Drawing.Size(800, 36);
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
            this.gridControl.Location = new System.Drawing.Point(0, 36);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemCheckEdit1});
            this.gridControl.Size = new System.Drawing.Size(800, 414);
            this.gridControl.TabIndex = 12;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.GroupPanel.Font = new System.Drawing.Font("宋体", 9F);
            this.gridView.Appearance.GroupPanel.Options.UseFont = true;
            this.gridView.Appearance.Preview.Font = new System.Drawing.Font("宋体", 9F);
            this.gridView.Appearance.Preview.Options.UseFont = true;
            this.gridView.Appearance.Row.Font = new System.Drawing.Font("宋体", 9F);
            this.gridView.Appearance.Row.Options.UseFont = true;
            this.gridView.ColumnPanelRowHeight = 26;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.itemName,
            this.deptName,
            this.sex,
            this.refRange,
            this.isCompareName,
            this.isMainName,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupCount = 1;
            this.gridView.GroupFormat = "[#image]{1} {2}";
            this.gridView.IndicatorWidth = 45;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.gridView.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView.OptionsDetail.EnableMasterViewMode = false;
            this.gridView.OptionsSelection.MultiSelect = true;
            this.gridView.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.RowHeight = 26;
            this.gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.deptName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // itemName
            // 
            this.itemName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemName.AppearanceCell.Options.UseFont = true;
            this.itemName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.itemName.AppearanceHeader.Options.UseFont = true;
            this.itemName.AppearanceHeader.Options.UseTextOptions = true;
            this.itemName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.itemName.Caption = "姓名";
            this.itemName.FieldName = "itemName";
            this.itemName.Name = "itemName";
            this.itemName.OptionsColumn.AllowEdit = false;
            this.itemName.OptionsColumn.AllowFocus = false;
            this.itemName.OptionsFilter.AllowAutoFilter = false;
            this.itemName.OptionsFilter.AllowFilter = false;
            this.itemName.Visible = true;
            this.itemName.VisibleIndex = 0;
            this.itemName.Width = 76;
            // 
            // deptName
            // 
            this.deptName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deptName.AppearanceCell.Options.UseFont = true;
            this.deptName.AppearanceCell.Options.UseTextOptions = true;
            this.deptName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deptName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deptName.AppearanceHeader.Options.UseFont = true;
            this.deptName.AppearanceHeader.Options.UseTextOptions = true;
            this.deptName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deptName.Caption = "客户编号";
            this.deptName.FieldName = "deptName";
            this.deptName.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DisplayText;
            this.deptName.Name = "deptName";
            this.deptName.OptionsColumn.AllowEdit = false;
            this.deptName.OptionsColumn.AllowFocus = false;
            this.deptName.OptionsFilter.AllowAutoFilter = false;
            this.deptName.OptionsFilter.AllowFilter = false;
            this.deptName.Visible = true;
            this.deptName.VisibleIndex = 0;
            // 
            // sex
            // 
            this.sex.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sex.AppearanceCell.Options.UseFont = true;
            this.sex.AppearanceCell.Options.UseTextOptions = true;
            this.sex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.sex.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sex.AppearanceHeader.Options.UseFont = true;
            this.sex.AppearanceHeader.Options.UseTextOptions = true;
            this.sex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.sex.Caption = "性别";
            this.sex.FieldName = "sex";
            this.sex.Name = "sex";
            this.sex.OptionsColumn.AllowEdit = false;
            this.sex.OptionsColumn.AllowFocus = false;
            this.sex.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.sex.OptionsFilter.AllowAutoFilter = false;
            this.sex.OptionsFilter.AllowFilter = false;
            this.sex.Visible = true;
            this.sex.VisibleIndex = 1;
            this.sex.Width = 39;
            // 
            // refRange
            // 
            this.refRange.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refRange.AppearanceCell.Options.UseFont = true;
            this.refRange.AppearanceCell.Options.UseTextOptions = true;
            this.refRange.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.refRange.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.refRange.AppearanceHeader.Options.UseFont = true;
            this.refRange.AppearanceHeader.Options.UseTextOptions = true;
            this.refRange.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.refRange.Caption = "年龄";
            this.refRange.FieldName = "refRange";
            this.refRange.Name = "refRange";
            this.refRange.OptionsColumn.AllowEdit = false;
            this.refRange.OptionsColumn.AllowFocus = false;
            this.refRange.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.refRange.OptionsFilter.AllowAutoFilter = false;
            this.refRange.OptionsFilter.AllowFilter = false;
            this.refRange.Visible = true;
            this.refRange.VisibleIndex = 2;
            this.refRange.Width = 64;
            // 
            // isCompareName
            // 
            this.isCompareName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isCompareName.AppearanceCell.Options.UseFont = true;
            this.isCompareName.AppearanceCell.Options.UseTextOptions = true;
            this.isCompareName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.isCompareName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.isCompareName.AppearanceHeader.Options.UseFont = true;
            this.isCompareName.AppearanceHeader.Options.UseTextOptions = true;
            this.isCompareName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.isCompareName.Caption = "单位";
            this.isCompareName.FieldName = "isCompareName";
            this.isCompareName.Name = "isCompareName";
            this.isCompareName.OptionsColumn.AllowEdit = false;
            this.isCompareName.OptionsColumn.AllowFocus = false;
            this.isCompareName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.isCompareName.OptionsFilter.AllowAutoFilter = false;
            this.isCompareName.OptionsFilter.AllowFilter = false;
            this.isCompareName.Visible = true;
            this.isCompareName.VisibleIndex = 3;
            this.isCompareName.Width = 164;
            // 
            // isMainName
            // 
            this.isMainName.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isMainName.AppearanceCell.Options.UseFont = true;
            this.isMainName.AppearanceCell.Options.UseTextOptions = true;
            this.isMainName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.isMainName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.isMainName.AppearanceHeader.Options.UseFont = true;
            this.isMainName.AppearanceHeader.Options.UseTextOptions = true;
            this.isMainName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.isMainName.Caption = "人员类别";
            this.isMainName.FieldName = "isMainName";
            this.isMainName.Name = "isMainName";
            this.isMainName.OptionsColumn.AllowEdit = false;
            this.isMainName.OptionsColumn.AllowFocus = false;
            this.isMainName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.isMainName.OptionsFilter.AllowAutoFilter = false;
            this.isMainName.OptionsFilter.AllowFilter = false;
            this.isMainName.Visible = true;
            this.isMainName.VisibleIndex = 4;
            this.isMainName.Width = 151;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "周期";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 5;
            this.gridColumn1.Width = 48;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "创建日期";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            this.gridColumn2.Width = 104;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "创建人";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 7;
            this.gridColumn3.Width = 107;
            // 
            // repositoryItemMemoEdit1
            // 
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
            this.txtSearch.Location = new System.Drawing.Point(54, 8);
            this.txtSearch.MenuManager = this.barManager;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSearch.Properties.Appearance.Options.UseFont = true;
            this.txtSearch.Size = new System.Drawing.Size(118, 20);
            this.txtSearch.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 12);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "姓名：";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(252, 8);
            this.textEdit1.MenuManager = this.barManager;
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Size = new System.Drawing.Size(118, 20);
            this.textEdit1.TabIndex = 13;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(186, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 12);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "客户编号：";
            // 
            // frm20602
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridControl);
            this.Name = "frm20602";
            this.Text = "膳食方案";
            this.Controls.SetChildIndex(this.marqueeProgressBarControl, 0);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.gridControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn itemName;
        private DevExpress.XtraGrid.Columns.GridColumn deptName;
        private DevExpress.XtraGrid.Columns.GridColumn sex;
        private DevExpress.XtraGrid.Columns.GridColumn refRange;
        private DevExpress.XtraGrid.Columns.GridColumn isCompareName;
        private DevExpress.XtraGrid.Columns.GridColumn isMainName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}