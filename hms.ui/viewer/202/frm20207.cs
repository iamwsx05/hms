using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hms.Entity;
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class frm20207 : frmBaseMdi
    {
        public frm20207()
        {
            InitializeComponent();
        }

        #region var 
        List<EntityClientInfo> lstClientInfo { get; set; }
        List<EntityQnRecord> lstQnRecords { get; set; }
       
        EntityParm voParm = null;
        #endregion


        #region override
        public override void New()
        {
            frmpopup2020701 frm = new frmpopup2020701();
            frm.ShowDialog();
        }

        /// <summary>
        /// 查询
        /// </summary>
        public override void Search()
        {
            string search = this.txtSearch.Text;

            List<EntityParm> parms = new List<EntityParm>();
            voParm = new EntityParm();
            voParm.key = "qnType";
            voParm.value = "2";
            parms.Add(voParm);
            if (string.IsNullOrEmpty(search))
            {
                voParm = new EntityParm();
                voParm.key = "search";
                voParm.value = search;
                parms.Add(voParm);
            }

            using (ProxyHms proxy = new ProxyHms())
            {
                lstQnRecords = proxy.Service.GetQnRecords(parms);
            }

            this.gcQnRecord.DataSource = lstQnRecords;
            this.gcQnRecord.RefreshDataSource();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public override void Edit()
        {
            if (this.gvQnRecord.SelectedRowsCount > 0)
            {
                EntityQnRecord vo = this.gvQnRecord.GetRow(this.gvQnRecord.GetSelectedRows()[0]) as EntityQnRecord;
                frmPopup2020702 frm = new frmPopup2020702(vo);
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

        /// <summary>
        /// 删除
        /// </summary>
        public override void Delete()
        {
            if (this.gvQnRecord.SelectedRowsCount > 0)
            {
                if (DialogBox.Msg("是否删除所选行记录？", MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                bool isRequireRefresh = false;
                int affect = -1;
                List<EntityQnRecord> delData = new List<EntityQnRecord>();
                for (int i = this.gvQnRecord.RowCount - 1; i >= 0; i--)
                {
                    if (this.gvQnRecord.IsRowSelected(i))
                    {
                        delData.Add((this.gvQnRecord.GetRow(i) as EntityQnRecord));
                    }
                }

                if (delData.Count > 0)
                {
                    using (ProxyHms proxy = new ProxyHms())
                    {
                        affect = proxy.Service.DelQnRecord(delData);
                    }

                    if (affect < 0)
                    {
                        DialogBox.Msg("删除记录失败。");
                    }
                    else
                    {
                        isRequireRefresh = true;
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


        #region methods
        public void Init()
        {
            List<EntityParm> parms = new List<EntityParm>();
            voParm = new EntityParm();
            voParm.key = "qnType";
            voParm.value = "2";
            parms.Add(voParm);
            using (ProxyHms proxy = new ProxyHms())
            { 
                lstQnRecords = proxy.Service.GetQnRecords(parms);
            }

            this.gcQnRecord.DataSource = lstQnRecords;
            this.gcQnRecord.RefreshDataSource();
        }


        #region RefreshData
        /// <summary>
        /// RefreshData
        /// </summary>
        public override void RefreshData()
        {
            uiHelper.BeginLoading(this);

            using (ProxyHms proxy = new ProxyHms())
            {
                lstQnRecords = proxy.Service.GetQnRecords(null);
            }
            this.gcQnRecord.DataSource = lstQnRecords;
            this.gcQnRecord.RefreshDataSource();
            uiHelper.CloseLoading(this);
        }
        #endregion

        #endregion


        #region events

        private void frm20207_Load(object sender, EventArgs e)
        {
            Init();
        }
       
        private void gvQnRecord_DoubleClick(object sender, EventArgs e)
        {
            this.Edit();
        }

        private void gvQnRecord_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Info.IsRowIndicator)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = Convert.ToInt32(e.RowHandle + 1).ToString();
            }
        }

        #endregion
    }
}
