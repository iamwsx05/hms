using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using Hms.Entity;

namespace Hms.Ui
{
    /// <summary>
    /// 问卷题库
    /// </summary>
    public partial class frm20902 : frmBaseMdi
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frm20902()
        {
            InitializeComponent();
        }
        #endregion

        #region var/property


        #endregion

        #region mthod

        #region init
        /// <summary>
        /// init
        /// </summary>
        void Init()
        {
            this.RefreshData();
        }
        #endregion

        #region ScrollRow
        /// <summary>
        /// ScrollRow
        /// </summary>
        /// <param name="fieldId"></param>
        void ScrollRow(string fieldId)
        {
            for (int i = 0; i < this.gridView.RowCount; i++)
            {
                this.gridView.UnselectRow(i);
            }
            for (int i = 0; i < this.gridView.RowCount; i++)
            {
                if ((this.gridView.GetRow(i) as EntityDicQnSummary).fieldId == fieldId)
                {
                    this.gridView.FocusedRowHandle = i;
                    this.gridView.SelectRow(i);
                    break;
                }
            }
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        public override void New()
        {
            frmPopup20902 frm = new frmPopup20902();
            frm.ShowDialog();
            if (frm.IsRequireRefresh)
            {
                this.RefreshData();
                this.ScrollRow(frm.TopicVo.fieldId);
            }
        }
        #endregion

        #region edit
        /// <summary>
        /// edit
        /// </summary>
        public override void Edit()
        {
            if (this.gridView.SelectedRowsCount > 0)
            {
                frmPopup20902 frm = new frmPopup20902(this.gridView.GetRow(this.gridView.GetSelectedRows()[0]) as EntityDicQnSummary);
                frm.ShowDialog();
                if (frm.IsRequireRefresh)
                {
                    this.RefreshData();
                    this.ScrollRow(frm.TopicVo.fieldId);
                }
            }
            else
            {
                DialogBox.Msg("请选择要编辑的记录.");
            }
        }
        #endregion

        #region delete
        /// <summary>
        /// delete
        /// </summary>
        public override void Delete()
        {
            if (this.gridView.SelectedRowsCount > 0)
            {
                if (DialogBox.Msg("是否删除所选行记录？", MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                bool isRequireRefresh = false;
                for (int i = this.gridView.RowCount - 1; i >= 0; i--)
                {
                    if (this.gridView.IsRowSelected(i))
                    {
                        using (ProxyHms proxy = new ProxyHms())
                        {
                            if (proxy.Service.DeleteQnTopic((this.gridView.GetRow(i) as EntityDicQnSetting).fieldId) > 0)
                            {
                                this.gridView.DeleteRow(i);
                                isRequireRefresh = true;
                            }
                            else
                            {
                                DialogBox.Msg("删除记录失败。");
                                break;
                            }
                        }
                    }
                }
                if (isRequireRefresh)
                    this.RefreshData();
            }
            else
            {
                DialogBox.Msg("请选择需要删除的记录。");
            }
        }
        #endregion

        #region search
        /// <summary>
        /// search
        /// </summary>
        public override void Search()
        {

        }
        #endregion

        #region export
        /// <summary>
        /// export
        /// </summary>
        public override void Export()
        {
            uiHelper.ExportToXls(this.gridView);
        }
        #endregion

        #region print
        /// <summary>
        /// print
        /// </summary>
        public override void Preview()
        {
            uiHelper.Print(this.gridControl);
        }
        #endregion

        #region RefreshData
        /// <summary>
        /// RefreshData
        /// </summary>
        public override void RefreshData()
        {
            try
            {
                uiHelper.BeginLoading(this);
                using (ProxyHms proxy = new ProxyHms())
                {
                    this.gridControl.DataSource = proxy.Service.GetQnList();
                    this.gridControl.RefreshDataSource();
                }
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #endregion

        #region event

        private void frm20902_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Appearance.ForeColor = System.Drawing.Color.Gray;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            this.Edit();
        }

        #endregion


    }
}
