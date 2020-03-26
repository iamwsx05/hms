﻿using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Hms.Ui
{
    /// <summary>
    /// 体检项目库
    /// </summary>
    public partial class frm20802 : frmBaseMdi
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frm20802()
        {
            InitializeComponent();
        }
        #endregion

        #region var/property

        /// <summary>
        /// 项目数据源
        /// </summary>
        List<EntityDicPeItem> dataSourceItem { get; set; }

        #endregion

        #region method

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            try
            {
                uiHelper.BeginLoading(this);
                RefreshData();
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region ScrollRow
        /// <summary>
        /// ScrollRow
        /// </summary>
        /// <param name="itemId"></param>
        void ScrollRow(string itemId)
        {
            for (int i = 0; i < this.gridView.RowCount; i++)
            {
                this.gridView.UnselectRow(i);
            }
            for (int i = 0; i < this.gridView.RowCount; i++)
            {
                if ((this.gridView.GetRow(i) as EntityDicPeItem).itemId == itemId)
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
            frmPopup20802 frm = new frmPopup20802();
            frm.ShowDialog();
            if (frm.IsRequireRefresh)
            {
                this.RefreshData();
                this.ScrollRow(frm.PeItemVo.itemId);
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
                frmPopup20802 frm = new frmPopup20802(this.gridView.GetRow(this.gridView.GetSelectedRows()[0]) as EntityDicPeItem);
                frm.ShowDialog();
                if (frm.IsRequireRefresh)
                {
                    this.RefreshData();
                    this.ScrollRow(frm.PeItemVo.itemId);
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
                            if (proxy.Service.DeletePeItem((this.gridView.GetRow(i) as EntityDicPeItem).itemId) > 0)
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
            if (this.gridView.RowCount > 0)
            {
                for (int i = 0; i < this.gridView.RowCount; i++)
                {
                    this.gridView.UnselectRow(i);
                }
                EntityDicPeItem itemVo = null;
                for (int i = 0; i < this.gridView.RowCount; i++)
                {
                    itemVo = this.gridView.GetRow(i) as EntityDicPeItem;
                    if (itemVo != null)
                    {
                        if (itemVo.itemName.Contains("腹部") || weCare.Core.Utils.SpellCodeHelper.GetPyCode(itemVo.itemName).ToUpper().Contains("FBPP") || weCare.Core.Utils.SpellCodeHelper.GetWbCode(itemVo.itemName).ToUpper().Contains("FBPP"))
                        {
                            this.gridView.FocusedRowHandle = i;
                            this.gridView.SelectRow(i);
                            break;
                        }
                    }
                }
            }
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
            uiHelper.BeginLoading(this);
            this.LoadQnDataSource();
            this.gridControl.DataSource = this.dataSourceItem;
            this.gridControl.RefreshDataSource();
            uiHelper.CloseLoading(this);
        }
        #endregion

        #region LoadQnDataSource
        /// <summary>
        /// LoadQnDataSource
        /// </summary>
        void LoadQnDataSource()
        {
            dataSourceItem = null;
            using (ProxyHms proxy = new ProxyHms())
            {
                dataSourceItem = proxy.Service.GetPeItems();
            }
        }
        #endregion

        #endregion

        #region event

        private void frm20802_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Appearance.ForeColor = Color.Gray;
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
