using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Hms.Entity;

namespace Hms.Ui
{
    /// <summary>
    /// 慢病-糖尿病
    /// </summary>
    public partial class frm20502 : frmBaseMdi
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frm20502()
        {
            InitializeComponent();
        }
        #endregion

        #region override

        /// <summary>
        /// 添加人员
        /// </summary>
        public override void New()
        {
            frmPopup2050203 frm = new frmPopup2050203();
            frm.ShowDialog();

            if (frm.isRefresh)
            {
                Init();
            }
        }
        /// <summary>
        /// 添加计划
        /// </summary>
        public override void Copy()
        {

        }
        /// <summary>
        /// 随访
        /// </summary>
        public override void Remind()
        {
            if (mbglTab == EnumMbgltab.record)
            {
                if (this.gvTnbRecord.SelectedRowsCount > 0)
                {
                    frmPopup2050201 frm = new frmPopup2050201(this.gvTnbRecord.GetRow(this.gvTnbRecord.GetSelectedRows()[0]) as EntityTnbRecord);
                    frm.ShowDialog();
                    if (frm.IsRequireRefresh)
                    {
                        this.RefreshData();
                    }
                }
                else
                {
                    DialogBox.Msg("请选择要编辑的记录.");
                }
            }
            else if (mbglTab == EnumMbgltab.sf)
            {
                if (this.gvTnbSf.SelectedRowsCount > 0)
                {
                    frmPopup2050201 frm = new frmPopup2050201(this.gvTnbSf.GetRow(this.gvTnbSf.GetSelectedRows()[0]) as EntityTnbSf);
                    frm.ShowDialog();
                    if (frm.IsRequireRefresh)
                    {
                        this.RefreshData();
                    }
                }
                else
                {
                    DialogBox.Msg("请选择要编辑的记录.");
                }
            }
        }
        /// <summary>
        /// 评估
        /// </summary>
        public override void Capture()
        {
            if (mbglTab == EnumMbgltab.record)
            {
                if (this.gvTnbRecord.SelectedRowsCount > 0)
                {
                    frmPopup2050202 frm = new frmPopup2050202(this.gvTnbRecord.GetRow(this.gvTnbRecord.GetSelectedRows()[0]) as EntityTnbRecord);
                    frm.ShowDialog();
                    if (frm.IsRequireRefresh)
                    {
                        this.RefreshData();
                    }
                }
                else
                {
                    DialogBox.Msg("请选择要编辑的记录.");
                }
            }
            else if (mbglTab == EnumMbgltab.pg)
            {
                if (this.gvTnbPg.SelectedRowsCount > 0)
                {
                    frmPopup2050202 frm = new frmPopup2050202(this.gvTnbPg.GetRow(this.gvTnbPg.GetSelectedRows()[0]) as EntityTnbPg);
                    frm.ShowDialog();
                    if (frm.IsRequireRefresh)
                    {
                        this.RefreshData();
                    }
                }
                else
                {
                    DialogBox.Msg("请选择要编辑的记录.");
                }
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        public override void Edit()
        {

        }
        /// <summary>
        /// 删除
        /// </summary>
        public override void Delete()
        {

        }
        /// <summary>
        /// 查询
        /// </summary>
        public override void Search()
        {

        }
        /// <summary>
        /// 刷新
        /// </summary>
        public override void RefreshData()
        {
            using (ProxyHms proxy = new ProxyHms())
            {
                this.gcTnbRecord.DataSource = proxy.Service.GetTnbPatients(null);
                this.gcTnbSf.DataSource = proxy.Service.GetTnbSfRecords(null);
                this.gcTnbPg.DataSource = proxy.Service.GetTnbPgRecords(null);
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        public override void Preview()
        {

        }
        /// <summary>
        /// 导出
        /// </summary>
        public override void Export()
        {

        }

        #endregion

        #region var/property
        EnumMbgltab mbglTab;
        #endregion

        #region method

        #region ScrollRow
        /// <summary>
        /// ScrollRow
        /// </summary>
        /// <param name="flagId"></param> 
        /// <param name="id"></param>
        void ScrollRow(int flagId, string id)
        {

        }
        #endregion

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            try
            {
                uiHelper.BeginLoading(this);
                using (ProxyHms proxy = new ProxyHms())
                {
                    this.gcTnbRecord.DataSource = proxy.Service.GetTnbPatients(null);
                    this.gcTnbSf.DataSource = proxy.Service.GetTnbSfRecords(null);
                    this.gcTnbPg.DataSource = proxy.Service.GetTnbPgRecords(null);
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

        private void frm20502_Load(object sender, EventArgs e)
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

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Appearance.ForeColor = Color.Gray;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this.Remind();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            this.Capture();
        }

        #endregion

        private void tabTnb_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tabTnb.SelectedTabPageIndex == 0)
                mbglTab = EnumMbgltab.record;
            else if (tabTnb.SelectedTabPageIndex == 1)
                mbglTab = EnumMbgltab.sf;
            else if (tabTnb.SelectedTabPageIndex == 2)
                mbglTab = EnumMbgltab.pg;
        }
    }
}
