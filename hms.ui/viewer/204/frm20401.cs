using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Hms.Entity;
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class frm20401 : frmBaseMdi
    {
        public frm20401()
        {
            InitializeComponent();
        }

        #region var/property
        List<EntityClientInfo> lstClientInfo { get; set; }
        List<EntityClientInfo> lstSelectClient { get; set; }
        List<EntityPromotionTemplate> lstPromotionTemplate { get; set; }
        List<EntityPromotionTemplateConfig> lstPromotionTemplateConfig { get; set; }
        List<EntityPromotionTemplateConfig> lstPromotionSelect { get; set; }


        List<EntityPromotionContentConfig> dicPromotionContentConfig { get; set; }
        List<EntityPromotionWayConfig> dicPromotionWayConfig { get; set; }
        #endregion

        #region override

        #region new
        /// <summary>
        /// 
        /// </summary>
        public override void New()
        {
            frmPopup2040101 frm = new frmPopup2040101();
            frm.ShowDialog();

            if (frm.IsRequireRefresh)
            {
                EntityPromotionTemplateConfig vo = frm.promotionTemplateConfig;

                if (!lstPromotionSelect.Exists(r => r.planPeriod == vo.planPeriod && r.planWay == vo.planWay && r.planContent == vo.planContent))
                {
                    lstPromotionSelect.Add(vo);
                    gcPlan.DataSource = lstPromotionSelect;
                    gcPlan.RefreshDataSource();
                }
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// 
        /// </summary>
        public override void Delete()
        {
            if (gvPlan.SelectedRowsCount > 0)
            {
                List<EntityPromotionTemplateConfig> lstTmp = new List<EntityPromotionTemplateConfig>();
                for (int i = 0; i <= gvPlan.RowCount; i++)
                {
                    if (gvPlan.IsRowSelected(i))
                    {
                        EntityPromotionTemplateConfig ptPlan = gvPlan.GetRow(i) as EntityPromotionTemplateConfig;
                        lstTmp.Add(ptPlan);
                    }
                }

                foreach (var vo in lstTmp)
                {
                    lstPromotionSelect.Remove(vo);
                }

                gcPlan.DataSource = lstPromotionSelect;
                gcPlan.RefreshDataSource();
            }
        }
        #endregion

        #region  保存
        /// <summary>
        /// 
        /// </summary>
        public override void Complete()
        {
            int affect = -1;
            List<EntityPromotionPlan> data = new List<EntityPromotionPlan>();
            if (lstSelectClient.Count <= 0)
            {
                DialogBox.Msg("请选择客户！");
                return;
            }
                
            if (lstPromotionSelect.Count <= 0)
            {
                DialogBox.Msg("请选择干预模板");
                return;
            }
               
            foreach (var client in lstSelectClient)
            {
                if (lstPromotionSelect.Count > 0)
                {
                    foreach (var promotion in lstPromotionSelect)
                    {
                        EntityPromotionPlan plan = new EntityPromotionPlan();
                        plan.clientId = client.clientNo;
                        plan.planType = "4";
                        plan.planDate = Function.Datetime(promotion.planPeriod) ;
                        plan.planState = "2";                       
                        plan.auditState = chkConfirm.Checked ? "3" : "1";
                        string planWay = dicPromotionWayConfig.Find(r=>r.planWay == promotion.planWay).id;
                        string planContent = dicPromotionContentConfig.Find(r=>r.planContent == promotion.planContent).id;
                        plan.planWay = planWay;
                        plan.planContent = planContent;
                        plan.planRemind = promotion.planRemind;
                        plan.ignorPlan = "2";
                        plan.planState = "2";
                        if(chkConfirm.Checked == true)
                        {
                            plan.auditState = "3";
                        }
                        plan.regTimes = client.regTimes;
                        plan.createId = "00";
                        plan.createDate = DateTime.Now;

                        data.Add(plan);
                    }
                }
            }

            if (data.Count > 0)
            {
                using (ProxyHms proxy = new ProxyHms())
                {
                    affect = proxy.Service.SavePromotionPan(data);
                }
            }

            if (affect > 0)
            {
                Init();
                DialogBox.Msg("保存成功！");
            } 
            else
                DialogBox.Msg("保存失败！");
        }
        #endregion

        #endregion

        #region methods
        void Init()
        {
            try
            {
                uiHelper.BeginLoading(this);
                LoadQnDataSource();
                lstPromotionSelect = new List<EntityPromotionTemplateConfig>();
                lstSelectClient = new List<EntityClientInfo>();

                gridControl.DataSource = lstSelectClient;
                gridControl.RefreshDataSource();
                gcPlan.DataSource = lstPromotionSelect;
                gcPlan.RefreshDataSource();
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }

        public void QueryClinets()
        {
            string name = this.txtQnName.Text;
            string dw = this.txtDw.Text;
            List<EntityParm> dicParm = new List<EntityParm>();

            if (!string.IsNullOrEmpty(name))
            {
                dicParm.Add(Function.GetParm("search", name));
            }
            if (!string.IsNullOrEmpty(dw))
            {
                dicParm.Add(Function.GetParm("dw", dw));
            }

            if(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(dw))
                return;

            using (ProxyHms proxy = new ProxyHms())
            {
                this.gcClient.DataSource = proxy.Service.GetClientInfoAndRpt(dicParm);
                this.gcClient.RefreshDataSource();
            }
        }


        #region LoadQnDataSource
        /// <summary>
        /// LoadQnDataSource
        /// </summary>
        void LoadQnDataSource()
        {
            lstClientInfo = null;

            List<EntityParm> dicParm = new List<EntityParm>();
            string beginDate = DateTime.Now.AddDays(-7).ToString("yyyy.MM.dd");
            string endDate = DateTime.Now.ToString("yyyy.MM.dd");
            if (beginDate != string.Empty && endDate != string.Empty)
            {
                dicParm.Add(Function.GetParm("genDate", beginDate + "|" + endDate));
            }

            using (ProxyHms proxy = new ProxyHms())
            {
                lstClientInfo = proxy.Service.GetClientInfoAndRpt(dicParm);
                gcClient.DataSource = lstClientInfo;
                gcClient.RefreshDataSource();

                lstPromotionTemplate = proxy.Service.GetPromotionTemplates(null);
                gcPromotionTemplate.DataSource = lstPromotionTemplate;
                gcPromotionTemplate.RefreshDataSource();

                lstPromotionTemplateConfig = proxy.Service.GetPromotionTemplateConfigs(null);
                if (lstPromotionTemplate != null)
                {
                    EntityPromotionTemplate vo = lstPromotionTemplate[0];
                    gcPromotionTemplateConfig.DataSource = lstPromotionTemplateConfig.FindAll(r => r.templateId == vo.id);
                    gcPromotionTemplateConfig.RefreshDataSource();
                }
                
                dicPromotionWayConfig= proxy.Service.GetPromotionWayConfigs();
                dicPromotionContentConfig = proxy.Service.GetPromotionContentConfigs();
            }
        }
        #endregion

        #region GetRowObject
        /// <summary>
        /// GetRowObject
        /// </summary>
        /// <returns></returns>
        EntityClientInfo GetRowObject()
        {
            if (this.gvClient.FocusedRowHandle < 0) return null;
            return this.gvClient.GetRow(this.gvClient.FocusedRowHandle) as EntityClientInfo;
        }
        #endregion


        #region
        List<EntityClientInfo> GetSelectData()
        {
            List<EntityClientInfo> data = new List<EntityClientInfo>();

            for (int i = 0; i < this.gvClient.RowCount; i++)
            {
                if (this.gvClient.IsRowSelected(i))
                {
                    data.Add((this.gvClient.GetRow(i) as EntityClientInfo));
                }
            }

            return data;
        }
        #endregion

        #region GetRowObject
        /// <summary>
        /// GetRowObject
        /// </summary>
        /// <returns></returns>
        EntityPromotionTemplate GetRowTemplateObject()
        {
            if (this.gvPromotionTemplate.FocusedRowHandle < 0) return null;
            return this.gvPromotionTemplate.GetRow(this.gvPromotionTemplate.FocusedRowHandle) as EntityPromotionTemplate;
        }
        #endregion


        #region GetRowObject
        /// <summary>
        /// GetRowObject
        /// </summary>
        /// <returns></returns>
        EntityPromotionTemplateConfig GetRowTemplateConfigObject()
        {
            if (this.gvPromotionTemplateConfig.FocusedRowHandle < 0) return null;
            return this.gvPromotionTemplateConfig.GetRow(this.gvPromotionTemplateConfig.FocusedRowHandle) as EntityPromotionTemplateConfig;
        }
        #endregion

        #endregion

        #region event
        private void frm20401_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void lblNav_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as DevExpress.XtraEditors.LabelControl).Text);
        }

        private void gvClient_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (gvClient.IsRowSelected(e.RowHandle))
                    gvClient.UnselectRow(e.RowHandle);
                else
                    gvClient.SelectRow(e.RowHandle);
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {

        }



        private void timer_Tick(object sender, EventArgs e)
        {
            lstSelectClient = GetSelectData();
            this.gridControl.DataSource = lstSelectClient;
            this.gridControl.RefreshDataSource();
            lblSelect.Text = "共 " + lstSelectClient.Count.ToString() + " 人";
        }

        private void gvPromotionTemplate_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                EntityPromotionTemplate vo = GetRowTemplateObject();
                gcPromotionTemplateConfig.DataSource = lstPromotionTemplateConfig.FindAll(r => r.templateId == vo.id);
                gcPromotionTemplateConfig.RefreshDataSource();
                List<EntityPromotionTemplateConfig> lstTmp = lstPromotionSelect.FindAll(r => r.templateId == vo.id);
                if (lstTmp != null && lstTmp.Count > 0)
                {
                    foreach (var tmp in lstTmp)
                    {
                        for (int i = 0; i < gvPromotionTemplateConfig.RowCount; i++)
                        {
                            EntityPromotionTemplateConfig ptConfig = gvPromotionTemplateConfig.GetRow(i) as EntityPromotionTemplateConfig;
                            if (tmp.id == ptConfig.id)
                                gvPromotionTemplateConfig.SelectRow(i);
                        }
                    }
                }
            }
        }

        private void gvPromotionTemplateConfig_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                EntityPromotionTemplateConfig vo = GetRowTemplateConfigObject();
                if (gvPromotionTemplateConfig.IsRowSelected(e.RowHandle))
                {
                    gvPromotionTemplateConfig.UnselectRow(e.RowHandle);
                    lstPromotionSelect.Remove(vo);
                }
                else
                {
                    gvPromotionTemplateConfig.SelectRow(e.RowHandle);
                    if (!lstPromotionSelect.Exists(r => r.id == vo.id))
                        lstPromotionSelect.Add(vo);
                }

                gcPlan.DataSource = lstPromotionSelect;
                gcPlan.RefreshDataSource();
            }
        }

        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.QueryClinets();
        }
    }
}
