﻿using Common.Controls;
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
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class frmPopup2040201 : frmBase
    {
        public frmPopup2040201(EntityDisplayPromotionPlan _displayPromotionPlan = null)
        {
            InitializeComponent();
            promotionPlan = _displayPromotionPlan;
        }

        #region var/property
        EntityDisplayPromotionPlan promotionPlan { get; set; }
        string[] weekdays = new string[] { "星期制日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
        List<EntityTjResult> lstXjResult;
        //体检结果
        List<EntityTjResult> lstTjResult;
        //体检结论建议
        EntityTjjljy tjjljyVo;

        //干预形式
        List<EntityPromotionWayConfig> lstPromtionWays { get; set; }
        //干预内容
        List<EntityPromotionContentConfig> lstPromotionContents { get; set; }
        #endregion

        #region  methods

        #region Init
        /// <summary>
        /// 初始化
        /// </summary>
        void Init()
        {
            if (promotionPlan == null)
                return;

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

            using (ProxyHms proxy = new ProxyHms())
            {
                lstPromtionWays = proxy.Service.GetPromotionWayConfigs();
                lstPromotionContents = proxy.Service.GetPromotionContentConfigs();
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

        #region  疾病评估
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
                    gcClientModel.RefreshDataSource();
                }
            }
        }
        #endregion

        #region 危险因素及问题
        private void btnRiskQuestion_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navRiskQuestion;
            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> lstParms = new List<EntityParm>();
                if (promotionPlan != null)
                {
                    EntityParm parm = new EntityParm();
                    parm.key = "clientId";
                    parm.value = promotionPlan.clientId;
                    lstParms.Add(parm);
                    gcRiskFactors.DataSource = proxy.Service.GetRiskFactorsResult(lstParms);
                    gcRiskFactors.RefreshDataSource();
                }
            }
        }
        #endregion

        #region 重要指标
        private void btnImportantIdicate_Click(object sender, EventArgs e)
        {
            this.navigationFrame.SelectedPage = navImportantIdicate;

            using (ProxyHms proxy = new ProxyHms())
            {
                List<EntityParm> lstParms = new List<EntityParm>();
                if (promotionPlan != null)
                {
                    EntityParm parm = new EntityParm();
                    parm.key = "clientNo";
                    parm.value = promotionPlan.clientNo;
                    lstParms.Add(parm);
                    gcTjReport.DataSource = proxy.Service.GetTjReports(lstParms);
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
                proxy.Service.GetTjResult(vo.reportNo, out lstTjResult, out lstXjResult, out tjjljyVo);

                if (tjjljyVo != null)
                {
                    this.memResult.Text = tjjljyVo.results + Environment.NewLine + tjjljyVo.sumup;
                    this.memSugg.Text = tjjljyVo.suggTage;
                }
                else
                {
                    this.memResult.Text = "";
                    this.memSugg.Text = "";
                }
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
                    this.gcReport.DataSource = proxy.Service.GetClientMdAccessRecord(lstParms);
                }
            }
        }
        private void btnLoadReport_Click(object sender, EventArgs e)
        {
            List<EntityModelParamCalc> lstMdParamCalc = null;
            List<EntityRiskFactorsResult> lstRiskFactorsResults = null;
            frm20301 frm = new frm20301();
            EntitymModelAccessRecord mdAccessRecord = GetRowObject();
            if (mdAccessRecord != null)
            {
                if (mdAccessRecord.qnRecId <= 0)
                {
                    DialogBox.Msg("请在个人报告选择问卷，并生成报告！");
                    return;
                }
                frm.Init();
                EntityClientReport rpt = frm.GneralPersonalReport(mdAccessRecord, out lstMdParamCalc, out lstRiskFactorsResults);
                frmPopup2030101 frmRpt = new frmPopup2030101(rpt);
                frmRpt.ShowDialog();
            }
        }


        #region 
        EntitymModelAccessRecord GetRowObject()
        {
            if (this.gvReport.FocusedRowHandle < 0) return null;
            return this.gvReport.GetRow(this.gvReport.FocusedRowHandle) as EntitymModelAccessRecord;
        }
        #endregion

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
                    gcPromotionPlan.RefreshDataSource();
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
                    gcReportItem.DataSource = proxy.Service.GetTjReports(lstParms);
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
            lblDateTime.Text = DateTime.Now.ToString("yyyy年MM月dd日") + "  " + weekdays[(int)DateTime.Now.DayOfWeek] + "  " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (promotionPlan == null)
                return;

            EntityPromotionPlan planRecord = new EntityPromotionPlan();
            planRecord = Function.MapperToModel(planRecord, promotionPlan);
            planRecord.planVisitRecord = memVisitRecord.Text;
            planRecord.planRemind = memPlanRemind.Text;
            planRecord.planWay = lstPromtionWays.Find(r => r.planWay == planRecord.planWay).id;
            planRecord.planContent = lstPromotionContents.Find(r => r.planContent == planRecord.planContent).id;
            string recordPlanWay = cboPlanways.Text;
            planRecord.recordWay = lstPromtionWays.Find(r => r.planWay == recordPlanWay).id;
            string recordPlanContent = cboPlanContent.Text;
            planRecord.recordContent = lstPromotionContents.Find(r=>r.planContent == recordPlanContent).id;
            string planPleasedLevel = cboCooperate.Text;
            planRecord.executeTime = DateTime.Now;
            planRecord.executeUserId = "00";
            planRecord.planState = "1";
            using (ProxyHms proxy = new ProxyHms())
            {
                if (proxy.Service.SavePromotionRecord(planRecord) > 0)
                {
                    DialogBox.Msg("计划执行成功!");
                }
            }
                
        }

        #endregion
    }
}
