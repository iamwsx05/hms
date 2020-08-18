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
    public partial class frm20408 : frmBaseMdi
    {
        public frm20408()
        {
            InitializeComponent();
        }

        #region var/property
        List<EntityDisplayPromotionPlan> lstPromotionPlan { get; set; }
        #endregion

        #region override
        /// <summary>
        /// 
        /// </summary>
        public override void Confirm()
        {
            List<EntityPromotionPlan> lstRecord = null;
            if (this.gvPromotionRecord.SelectedRowsCount > 0)
            {
                lstRecord = new List<EntityPromotionPlan>();
                for (int i = this.gvPromotionRecord.RowCount - 1; i >= 0; i--)
                {
                    if (this.gvPromotionRecord.IsRowSelected(i))
                    {
                        EntityDisplayPromotionPlan displayVo = this.gvPromotionRecord.GetRow(i) as EntityDisplayPromotionPlan;
                        EntityPromotionPlan vo = new EntityPromotionPlan();
                        vo.id = displayVo.id;
                        vo.auditState = "2";
                        lstRecord.Add(vo);
                    }
                }
            }

            if(lstRecord != null)
            {
                using (ProxyHms proxy = new ProxyHms())
                {
                    if (proxy.Service.ConfirmPromotionRecord(lstRecord) > 0)
                    {
                        DialogBox.Msg("审核成功！");
                        Init();
                    }
                }
            }
        }

        public override void Refresh()
        {
            Init();
        }
        #endregion

        #region methods
        void Init()
        {
            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> dicParm = new List<EntityParm>();
                dicParm.Add(Function.GetParm("auditState", "('3')"));
                lstPromotionPlan = proxy.Service.GetPromotionPlans(dicParm);
                gcPromotionRecord.DataSource = lstPromotionPlan;
                gcPromotionRecord.RefreshDataSource();
            }
        }


        #region GetRowObject
        /// <summary>
        /// GetRowObject
        /// </summary>
        /// <returns></returns>
        EntityDisplayPromotionPlan GetRowObject()
        {
            if (this.gvPromotionRecord.FocusedRowHandle < 0) return null;
            return this.gvPromotionRecord.GetRow(this.gvPromotionRecord.FocusedRowHandle) as EntityDisplayPromotionPlan;
        }
        #endregion

        #endregion

        private void frm20408_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void gvPromotionRecord_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            
            
        }
    }
}
