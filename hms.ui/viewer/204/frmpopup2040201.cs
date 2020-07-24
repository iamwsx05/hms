using Common.Controls;
using Hms.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace Hms.Ui
{
    public partial class frmPopup2040201 :frmBase
    {
        public frmPopup2040201(EntityDisplayPromotionPlan _displayPromotionPlan = null)
        {
            InitializeComponent();
            promotionPlan = _displayPromotionPlan;
        }

        #region var/property
        EntityDisplayPromotionPlan promotionPlan { get; set; }
        string [] weekdays = new string[] { "星期制日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
        #endregion

        #region  methods

        #region Init
        /// <summary>
        /// 初始化
        /// </summary>
        void Init()
        {
            if(promotionPlan != null)
            {
                lblClientName.Text = promotionPlan.clientName;
                lblCompany.Text = promotionPlan.company;
                lblGradName.Text = promotionPlan.gradeName;
                lblMobile.Text = promotionPlan.mobile;
                lblSex.Text = promotionPlan.sex;
                lblAge.Text = promotionPlan.age;

                dtePlan.Text = promotionPlan.planDate;
                cboPlanContent.Text = promotionPlan.planContent;
                cboPlanways.Text = promotionPlan.planWay;
                memPlanRemind.Text = promotionPlan.planRemind;
                lblDoctor.Text = promotionPlan.createName;
            }
        }
        #endregion

        #region 重要指标
        public EntityDisplayClientRpt GetRptRowsObject()
        {
            EntityDisplayClientRpt vo = null;
            if (gvTjReport.FocusedRowHandle >= 0)
            {
                vo = gvTjReport.GetRow(gvTjReport.FocusedRowHandle) as EntityDisplayClientRpt;
            }

            return vo;
        }
        #endregion

        #endregion

        #region events

        #region  汇总
        private void btnInfoCollect_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navInfoCollect;

            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> lstParms = new List<EntityParm>();
                if (promotionPlan != null)
                {
                    EntityParm parm = new EntityParm();
                    parm.key = "clientId";
                    parm.value = promotionPlan.clientId;
                    lstParms.Add(parm);
                    gcClientModel.DataSource = proxy.Service.GetDisplayClientModelAcess(lstParms);
                }
            }
        }
        #endregion

        #region 危险因素及问题
        private void btnRiskQuestion_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navRiskQuestion;
        }
        #endregion

        #region 重要指标
        private void btnImportantIdicate_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navImportantIdicate;

            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> lstParms = new List<EntityParm>();
                if(promotionPlan != null)
                {
                    EntityParm parm = new EntityParm();
                    parm.key = "clientNo";
                    parm.value = promotionPlan.clientNo;
                    lstParms.Add(parm);
                    gcTjReport.DataSource = proxy.Service.GetClientReports(lstParms);
                    gcTjReport.RefreshDataSource();
                }
            }
        }

        private void gcTjReport_Click(object sender, EventArgs e)
        {
            EntityDisplayClientRpt vo = GetRptRowsObject();

            using (ProxyHms proxy = new ProxyHms())
            {
                gcMainItemData.DataSource = proxy.Service.GetReportMainItem(vo.reportNo);
                gcMainItemData.RefreshDataSource();
            }
        }

        private void gvMainItemData_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
        #endregion

        #region 干预方案及总结
        private void btnPromotionCase_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navPromotionCase;
        }
        #endregion

        #region 健康报告 
        private void btnHmsReport_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navHmsReport;

            using (ProxyHms proxy = new ProxyHms())
            {
                if (promotionPlan != null)
                {
                    List<EntityParm> lstParms = new List<EntityParm>();
                    EntityParm param = new EntityParm();
                    param.key = "clientNo";
                    param.value = promotionPlan.clientNo;
                    lstParms.Add(param);
                    this.gcReport.DataSource = proxy.Service.GetClientReports(lstParms);
                }
            }
        }
        #endregion

        #region 服务
        private void btnService_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navService;
        }
        #endregion

        #region 干预计划
        private void btnPromotionPlan_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navPromotionPlan;
            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> lstParms = new List<EntityParm>();
                if (promotionPlan != null)
                {
                    EntityParm parm = new EntityParm();
                    parm.key = "clientNo";
                    parm.value = promotionPlan.clientNo;
                    lstParms.Add(parm);
                    gcPromotionPlan.DataSource = proxy.Service.GetPromotionPlans(lstParms);
                }
            }
        }
        private void gvPromotionPlan_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
        #endregion

        #region 干预记录
        private void btnPromotionRecord_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navPromotionRecord;
            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> lstParms = new List<EntityParm>();
                if (promotionPlan != null)
                {
                    EntityParm parm = new EntityParm();
                    parm.key = "clientNo";
                    parm.value = promotionPlan.clientNo;
                    lstParms.Add(parm);
                    gcPromotionRecord.DataSource = proxy.Service.GetPromotionPlanRecords(lstParms);
                }
            }
        }

        private void gvPromotionRecord_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
        #endregion

        #region 膳食方案
        private void btnDiet_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navDiet;
        }
        #endregion

        #region 体检报告
        private void btnPeReport_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navPeReport;
            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> lstParms = new List<EntityParm>();
                if (promotionPlan != null)
                {
                    EntityParm parm = new EntityParm();
                    parm.key = "clientNo";
                    parm.value = promotionPlan.clientNo;
                    lstParms.Add(parm);
                    gcReportItem.DataSource = proxy.Service.GetClientReports(lstParms);
                }
            }

        }

        private void gvReportItem_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            EntityDisplayClientRpt vo = gvReportItem.GetRow(e.RowHandle) as EntityDisplayClientRpt;

            if (vo == null)
                return;
            using (ProxyHms proxy = new ProxyHms())
            {
                gcReportItemData.DataSource = proxy.Service.GetReportItems(vo.reportId);
                gcReportItemData.RefreshDataSource();
            }  
        }

        #endregion

        private void btnHealthMinitor_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navHealthMinitor;
        }
        private void BtnQuestionnaire_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navQuestionnaire;
        }
        private void btnMedRecord_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navMedRecord;
        }
        private void btnGxy_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navGxy;
        }
        private void btnTnb_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navTnb;
        }

        private void btnClinicRecord_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navClinicRecord;
        }
        private void frmPopup2040201_Load(object sender, EventArgs e)
        {
            Init();
            btnInfoCollect_Click(null, null);
        }
        private void timer_Tick(object sender, EventArgs e)
        { 
            lblDateTime.Text = DateTime.Now.ToString("yyyy年MM月dd日") + "  " + weekdays[(int)DateTime.Now.DayOfWeek] + "  " +DateTime.Now.ToString("HH:mm:ss");
        }

        #endregion

    }
}
