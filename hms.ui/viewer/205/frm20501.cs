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
    /// 慢病-高血压
    /// </summary>
    public partial class frm20501 : frmBaseMdi
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frm20501()
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
            frmPopup2050103 frm = new frmPopup2050103();
            frm.ShowDialog();

            if(frm.isRefresh)
            {
                Init();
            }
        }
        /// <summary>
        /// 添加计划
        /// </summary>
        public override void Copy()
        {
            frmPopup2050104 frm = new frmPopup2050104();
            frm.ShowDialog();
        }
        /// <summary>
        /// 随访
        /// </summary>
        public override void Remind()
        {
            EnumGxytab gxyTab = EnumGxytab.gxyRecord;
            if (tabGxy.SelectedTabPageIndex == 0)
                gxyTab = EnumGxytab.gxyRecord;
            else if(tabGxy.SelectedTabPageIndex == 1)
                gxyTab = EnumGxytab.gxySf;
            else if (tabGxy.SelectedTabPageIndex == 2)
                gxyTab = EnumGxytab.gxyPg;

            if(gxyTab == EnumGxytab.gxyRecord)
            {
                if (this.gvGxyRecord.SelectedRowsCount > 0)
                {
                    frmPopup2050101 frm = new frmPopup2050101(this.gvGxyRecord.GetRow(this.gvGxyRecord.GetSelectedRows()[0]) as EntityGxyRecord);
                    frm.ShowDialog();
                    if (frm.IsRequireRefresh)
                    {
                        this.RefreshData();
                        //this.ScrollRow(1, frm.sfVo.sfId);
                    }
                }
                else
                {
                    DialogBox.Msg("请选择要编辑的记录.");
                }
            }
            else if(gxyTab == EnumGxytab.gxySf)
            {
                if (this.gvGxySf.SelectedRowsCount > 0)
                {
                    frmPopup2050101 frm = new frmPopup2050101(this.gvGxySf.GetRow(this.gvGxySf.GetSelectedRows()[0]) as EntityGxySf);
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
            EnumGxytab gxyTab = EnumGxytab.gxyRecord;
            if (tabGxy.SelectedTabPageIndex == 0)
                gxyTab = EnumGxytab.gxyRecord;
            else if (tabGxy.SelectedTabPageIndex == 1)
                gxyTab = EnumGxytab.gxySf;
            else if (tabGxy.SelectedTabPageIndex == 2)
                gxyTab = EnumGxytab.gxyPg;

            if (gxyTab == EnumGxytab.gxyRecord)
            {
                if (this.gvGxyRecord.SelectedRowsCount > 0)
                {
                    frmPopup2050102 frm = new frmPopup2050102(this.gvGxyRecord.GetRow(this.gvGxyRecord.GetSelectedRows()[0]) as EntityGxyRecord);
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
            else if (gxyTab == EnumGxytab.gxyPg)
            {
                if (this.gvGxyPg.SelectedRowsCount > 0)
                {
                    frmPopup2050102 frm = new frmPopup2050102(this.gvGxyPg.GetRow(this.gvGxyPg.GetSelectedRows()[0]) as EntityGxyPg);
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
            try
            {
                this.dteStart.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                this.dteEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                List<EntityParm> lstParams = new List<EntityParm>();
                EntityParm vo = new EntityParm();
                vo.key = "queryDate";
                vo.value = this.dteStart.Text + "|" + this.dteEnd.Text;
                lstParams.Add(vo);

                uiHelper.BeginLoading(this);
                using (ProxyHms proxy = new ProxyHms())
                {
                    this.gcGxyRecord.DataSource = proxy.Service.GetGxyPatients(lstParams);
                    this.gcGxySf.DataSource = proxy.Service.GetGxySfRecords(null);
                    this.gcGxyPg.DataSource = proxy.Service.GetGxyPgRecords(null);
                    this.gcGxyRecord.RefreshDataSource();
                    this.gcGxySf.RefreshDataSource();
                    this.gcGxyPg.RefreshDataSource();
                }

            }
            finally
            {
                uiHelper.CloseLoading(this);
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
            for (int i = 0; i < this.gvGxyRecord.RowCount; i++)
            {
                this.gvGxyRecord.UnselectRow(i);
            }
            for (int i = 0; i < this.gvGxyRecord.RowCount; i++)
            {
                //if ((this.gridView.GetRow(i) as EntityDicSportItem).sId == sId)
                //{
                //    this.gridView.FocusedRowHandle = i;
                //    this.gridView.SelectRow(i);
                //    break;
                //}
            }
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
                this.dteStart.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                this.dteEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                List<EntityParm> lstParams = new List<EntityParm>();
                EntityParm vo = new EntityParm();
                vo.key = "queryDate";
                vo.value = this.dteStart.Text + "|" + this.dteEnd.Text;
                lstParams.Add(vo);

                uiHelper.BeginLoading(this);
                using (ProxyHms proxy = new ProxyHms())
                {
                    this.gcGxyRecord.DataSource = proxy.Service.GetGxyPatients(lstParams);
                    this.gcGxySf.DataSource = proxy.Service.GetGxySfRecords(null);
                    this.gcGxyPg.DataSource = proxy.Service.GetGxyPgRecords(null);
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

        private void frm20501_Load(object sender, EventArgs e)
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


    }
}
