﻿namespace Hms.Ui
{
    partial class frm20202
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
            this.gcNormalQnRecord = new DevExpress.XtraGrid.GridControl();
            this.gvNormalQnRecord = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.itemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deptName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.refRange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.isCompareName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.isMainName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNormalQnRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNormalQnRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcBackGround.Location = new System.Drawing.Point(0, 0);
            this.pcBackGround.Size = new System.Drawing.Size(959, 31);
            this.pcBackGround.Visible = true;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // marqueeProgressBarControl
            // 
            this.marqueeProgressBarControl.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // gcNormalQnRecord
            // 
            this.gcNormalQnRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNormalQnRecord.Location = new System.Drawing.Point(0, 31);
            this.gcNormalQnRecord.MainView = this.gvNormalQnRecord;
            this.gcNormalQnRecord.Name = "gcNormalQnRecord";
            this.gcNormalQnRecord.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemCheckEdit1});
            this.gcNormalQnRecord.Size = new System.Drawing.Size(959, 509);
            this.gcNormalQnRecord.TabIndex = 13;
            this.gcNormalQnRecord.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNormalQnRecord});
            this.gcNormalQnRecord.DoubleClick += new System.EventHandler(this.gcNormalQnRecord_DoubleClick);
            // 
            // gvNormalQnRecord
            // 
            this.gvNormalQnRecord.Appearance.GroupPanel.Font = new System.Drawing.Font("宋体", 9F);
            this.gvNormalQnRecord.Appearance.GroupPanel.Options.UseFont = true;
            this.gvNormalQnRecord.Appearance.Preview.Font = new System.Drawing.Font("宋体", 9F);
            this.gvNormalQnRecord.Appearance.Preview.Options.UseFont = true;
            this.gvNormalQnRecord.Appearance.Row.Font = new System.Drawing.Font("宋体", 9F);
            this.gvNormalQnRecord.Appearance.Row.Options.UseFont = true;
            this.gvNormalQnRecord.Appearance.Row.Options.UseTextOptions = true;
            this.gvNormalQnRecord.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvNormalQnRecord.ColumnPanelRowHeight = 26;
            this.gvNormalQnRecord.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.itemName,
            this.sex,
            this.deptName,
            this.refRange,
            this.isCompareName,
            this.isMainName,
            this.gridColumn2,
            this.gridColumn3});
            this.gvNormalQnRecord.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gvNormalQnRecord.GridControl = this.gcNormalQnRecord;
            this.gvNormalQnRecord.GroupFormat = "[#image]{1} {2}";
            this.gvNormalQnRecord.IndicatorWidth = 40;
            this.gvNormalQnRecord.Name = "gvNormalQnRecord";
            this.gvNormalQnRecord.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.gvNormalQnRecord.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gvNormalQnRecord.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvNormalQnRecord.OptionsDetail.EnableMasterViewMode = false;
            this.gvNormalQnRecord.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office;
            this.gvNormalQnRecord.OptionsView.ShowGroupPanel = false;
            this.gvNormalQnRecord.RowHeight = 27;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "客户编号";
            this.gridColumn1.FieldName = "clientNo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 87;
            // 
            // itemName
            // 
            this.itemName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.itemName.AppearanceHeader.Options.UseFont = true;
            this.itemName.AppearanceHeader.Options.UseTextOptions = true;
            this.itemName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.itemName.Caption = "姓名";
            this.itemName.FieldName = "clientName";
            this.itemName.Name = "itemName";
            this.itemName.OptionsColumn.AllowEdit = false;
            this.itemName.OptionsColumn.AllowFocus = false;
            this.itemName.OptionsFilter.AllowAutoFilter = false;
            this.itemName.OptionsFilter.AllowFilter = false;
            this.itemName.Visible = true;
            this.itemName.VisibleIndex = 1;
            this.itemName.Width = 67;
            // 
            // sex
            // 
            this.sex.AppearanceCell.Options.UseTextOptions = true;
            this.sex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.sex.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
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
            this.sex.VisibleIndex = 2;
            this.sex.Width = 37;
            // 
            // deptName
            // 
            this.deptName.AppearanceCell.Options.UseTextOptions = true;
            this.deptName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deptName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.deptName.AppearanceHeader.Options.UseFont = true;
            this.deptName.AppearanceHeader.Options.UseTextOptions = true;
            this.deptName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.deptName.Caption = "年龄";
            this.deptName.FieldName = "age";
            this.deptName.Name = "deptName";
            this.deptName.OptionsColumn.AllowEdit = false;
            this.deptName.OptionsColumn.AllowFocus = false;
            this.deptName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.deptName.OptionsFilter.AllowAutoFilter = false;
            this.deptName.OptionsFilter.AllowFilter = false;
            this.deptName.Visible = true;
            this.deptName.VisibleIndex = 3;
            this.deptName.Width = 70;
            // 
            // refRange
            // 
            this.refRange.AppearanceCell.Options.UseTextOptions = true;
            this.refRange.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.refRange.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.refRange.AppearanceHeader.Options.UseFont = true;
            this.refRange.AppearanceHeader.Options.UseTextOptions = true;
            this.refRange.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.refRange.Caption = "人员类别";
            this.refRange.FieldName = "gradeName";
            this.refRange.Name = "refRange";
            this.refRange.OptionsColumn.AllowEdit = false;
            this.refRange.OptionsColumn.AllowFocus = false;
            this.refRange.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.refRange.OptionsFilter.AllowAutoFilter = false;
            this.refRange.OptionsFilter.AllowFilter = false;
            this.refRange.Visible = true;
            this.refRange.VisibleIndex = 4;
            this.refRange.Width = 81;
            // 
            // isCompareName
            // 
            this.isCompareName.AppearanceCell.Options.UseTextOptions = true;
            this.isCompareName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.isCompareName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.isCompareName.AppearanceHeader.Options.UseFont = true;
            this.isCompareName.AppearanceHeader.Options.UseTextOptions = true;
            this.isCompareName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.isCompareName.Caption = "问卷名称";
            this.isCompareName.FieldName = "qnName";
            this.isCompareName.Name = "isCompareName";
            this.isCompareName.OptionsColumn.AllowEdit = false;
            this.isCompareName.OptionsColumn.AllowFocus = false;
            this.isCompareName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.isCompareName.OptionsFilter.AllowAutoFilter = false;
            this.isCompareName.OptionsFilter.AllowFilter = false;
            this.isCompareName.Visible = true;
            this.isCompareName.VisibleIndex = 5;
            this.isCompareName.Width = 108;
            // 
            // isMainName
            // 
            this.isMainName.AppearanceCell.Options.UseTextOptions = true;
            this.isMainName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.isMainName.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.isMainName.AppearanceHeader.Options.UseFont = true;
            this.isMainName.AppearanceHeader.Options.UseTextOptions = true;
            this.isMainName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.isMainName.Caption = "填写日期";
            this.isMainName.FieldName = "strQnDate";
            this.isMainName.Name = "isMainName";
            this.isMainName.OptionsColumn.AllowEdit = false;
            this.isMainName.OptionsColumn.AllowFocus = false;
            this.isMainName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.isMainName.OptionsFilter.AllowAutoFilter = false;
            this.isMainName.OptionsFilter.AllowFilter = false;
            this.isMainName.Visible = true;
            this.isMainName.VisibleIndex = 6;
            this.isMainName.Width = 77;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "问卷来源";
            this.gridColumn2.FieldName = "strQnSource";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 7;
            this.gridColumn2.Width = 57;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("宋体", 9F);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "份数";
            this.gridColumn3.FieldName = "qnCount";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 8;
            this.gridColumn3.Width = 58;
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
            // frm20202
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 540);
            this.Controls.Add(this.gcNormalQnRecord);
            this.Name = "frm20202";
            this.Text = "常规问卷";
            this.Load += new System.EventHandler(this.frm20202_Load);
            this.Controls.SetChildIndex(this.marqueeProgressBarControl, 0);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.gcNormalQnRecord, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNormalQnRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNormalQnRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcNormalQnRecord;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNormalQnRecord;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn itemName;
        private DevExpress.XtraGrid.Columns.GridColumn sex;
        private DevExpress.XtraGrid.Columns.GridColumn deptName;
        private DevExpress.XtraGrid.Columns.GridColumn refRange;
        private DevExpress.XtraGrid.Columns.GridColumn isCompareName;
        private DevExpress.XtraGrid.Columns.GridColumn isMainName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}