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
    public partial class frm20202 : frmBaseMdi
    {
        public frm20202()
        {
            InitializeComponent();
        }

        #region var
        List<EntityQnRecord> lstQnRecords { get; set; }
        List<EntityClientInfo> lstClientInfo { get; set; }
        #endregion

        #region override
        public override void New()
        {
            frmPopup2020201 frm = new frmPopup2020201(lstClientInfo);
            frm.ShowDialog();
            if (frm.IsRequireRefresh)
            {
                this.RefreshData();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public override void Edit()
        {
            if (this.gvNormalQnRecord.SelectedRowsCount > 0)
            {
                EntityQnRecord vo = this.gvNormalQnRecord.GetRow(this.gvNormalQnRecord.GetSelectedRows()[0]) as EntityQnRecord;
                frmPopup2020201 frm = new frmPopup2020201(vo);
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

        #region RefreshData
        /// <summary>
        /// RefreshData
        /// </summary>
        public override void RefreshData()
        {
            uiHelper.BeginLoading(this);

            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> parms = new List<EntityParm>();
                EntityParm vo = new EntityParm();
                vo.key = "qnType";
                vo.value = "1";
                parms.Add(vo);
                lstQnRecords = proxy.Service.GetQnRecords(parms);
            }
            this.gcNormalQnRecord.DataSource = lstQnRecords;
            this.gcNormalQnRecord.RefreshDataSource();
            uiHelper.CloseLoading(this);
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        public override void Search()
        {
            string search = this.txtSearch.Text;

            this.gcNormalQnRecord.DataSource = lstQnRecords.FindAll(r => r.clientName.Contains(search) || r.clientNo.Contains(search));
            this.gcNormalQnRecord.RefreshDataSource();
        }

        /// <summary>
        /// 删除
        /// </summary>
        public override void Delete()
        {
            if (this.gvNormalQnRecord.SelectedRowsCount > 0)
            {
                if (DialogBox.Msg("是否删除所选行记录？", MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                bool isRequireRefresh = false;
                int affect = -1;
                List<EntityQnRecord> delData = new List<EntityQnRecord>();
                for (int i = this.gvNormalQnRecord.RowCount - 1; i >= 0; i--)
                {
                    if (this.gvNormalQnRecord.IsRowSelected(i))
                    {
                        delData.Add((this.gvNormalQnRecord.GetRow(i) as EntityQnRecord));
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

        #region Init
        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            using (ProxyHms proxy = new ProxyHms())
            {
                lstQnRecords = proxy.Service.GetQnRecords(null);

                List<EntityParm> dicParm = new List<EntityParm>();
                //string beginDate = DateTime.Now.AddDays(-7).ToString("yyyy.MM.dd");
                //string endDate = DateTime.Now.ToString("yyyy.MM.dd");
                //if (beginDate != string.Empty && endDate != string.Empty)
                //{
                //    dicParm.Add(Function.GetParm("genDate", beginDate + "|" + endDate));
                //}
                //lstClientInfo = proxy.Service.GetClientInfoAndRpt(dicParm);
            }
            this.gcNormalQnRecord.DataSource = lstQnRecords;
            this.gcNormalQnRecord.RefreshDataSource();
        }

        #endregion

        #endregion

        #region events
        private void frm20202_Load(object sender, EventArgs e)
        {
            Init();
        }
        private void gcNormalQnRecord_DoubleClick(object sender, EventArgs e)
        {
            this.Edit();
        }

        private void gvNormalQnRecord_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if(e.RowHandle>= 0 && e.Info.IsRowIndicator)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = Convert.ToInt32(e.RowHandle + 1).ToString() ;
            }
        }
        #endregion
    }
}
