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
using weCare.Core.Utils;

namespace Hms.Ui
{
    public partial class frmPopup2060201 : frmBasePopup
    {
        public frmPopup2060201(EntityDietRecord _dietRecord = null)
        {
            InitializeComponent();
            dietRecord = _dietRecord;
        }

        public frmPopup2060201(EntityDietTemplate _dietTemplate)
        {
            InitializeComponent();
            dietTemplate = _dietTemplate;
        }


        #region var
        List<EntityClientInfo> lstClient { get; set; }
        List<EntityDietRecord> lstDietRecord { get; set; }
        List<EntityDietDetails> lstDietDetails { get; set; }
        List<EntityDicDientIngredient> lstDicDietIngrediet { get; set; }
        List<EntityDietTemplateDetails> lstDietTemplateDetails { get; set; }
        //膳食方案记录
        EntityDietRecord dietRecord { get; set; }
        //膳食模板
        EntityDietTemplate dietTemplate { get; set; }

        //膳食原则
        List<EntityDietPrinciple> lstPrinciple { get; set; }
        List<EntityDietPrinciple> lstPrincipleAll { get; set; }

        //中医食疗
        List<EntityDietTreatment> lstDietTreatment { get; set; }
        List<EntityDietTreatment> lstDietTreatmentAll { get; set; }

        #endregion

        #region methods

        #region Init
        void Init()
        {
            this.lblCaiName.Text = "";
            this.gvDietCai.ViewCaption = "成品菜";
            if (lstDietDetails == null)
                lstDietDetails = new List<EntityDietDetails>();
            if (lstDietRecord == null)
                lstDietRecord = new List<EntityDietRecord>();
            using (ProxyHms proxy = new ProxyHms())
            {
                lstDicDietIngrediet = proxy.Service.GetDicDietIngredient();
                lstPrincipleAll = proxy.Service.GetDietPrinciple();
                lstDietTreatmentAll = proxy.Service.GetDietTreatment();
            }

            if (dietRecord != null)
            {
                InitDietDetails(dietRecord);
            }

            if (dietTemplate != null)
            {
                this.pcSearch.Visible = false;
                this.pcClient.Visible = false;
                this.btnSaveTemplate.Visible = false;
                this.btnImportTemplate.Visible = false;
                this.btnAddBreakfast.Visible = false;
                this.btnAddDietPrinciple.Visible = false;
                this.btnAddDinner.Visible = false;
                this.btnAddLunch.Visible = false;
                this.btnAddZysl.Visible = false;
                this.btnDelCai.Visible = false;
                InitTemplateDietDetails(dietTemplate);
            }
        }
        #endregion

        #region 方案食谱
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dietRecord"></param>
        /// <returns></returns>
        void InitDietDetails(EntityDietRecord dietRecord)
        {
            if (dietRecord == null)
                return;

            List<EntityClientInfo> lstClientTemp = new List<EntityClientInfo>();
            lstDietRecord.Add(dietRecord);
            List<EntityParm> parms = new List<EntityParm>();
            string search = dietRecord.clientNo;
            EntityParm vo = new EntityParm();
            vo.key = "search";
            vo.value = search;
            parms.Add(vo);
            string clientNoStr = string.Empty;

            #region 客户
            using (ProxyHms proxy = new ProxyHms())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    lstClientTemp = proxy.Service.GetClientInfoAndRpt(parms);
                }
            }
            if (lstClientTemp != null)
            {
                lstClient = lstClientTemp.FindAll(r => r.regTimes == dietRecord.regTimes);
                gcSelectClient.DataSource = lstClient;
                gcSelectClient.RefreshDataSource();
            }
            #endregion

            #region 食谱
            string dietRecId = "('" + dietRecord.recId.ToString() + "')";
            List<EntityDietDetails> lstDietDetailsTmp = null;
            using (ProxyHms proxy = new ProxyHms())
            {
                lstDietDetailsTmp = proxy.Service.GetDietDetails(dietRecId);
            }

            if (lstDietDetailsTmp == null)
                return;

            foreach (var dietDetail in lstDietDetailsTmp)
            {
                if (lstDietDetails.Any(r => r.recId == dietDetail.recId && r.day == dietDetail.day && r.mealId == dietDetail.mealId))
                {
                    EntityDietDetails voDietClone = lstDietDetailsTmp.Find(r => r.recId == dietDetail.recId && r.day == dietDetail.day && r.mealId == dietDetail.mealId);
                    if (!voDietClone.lstDetailsCai.Any((u => u.recId == dietDetail.recId && u.day == dietDetail.day && u.mealId == dietDetail.mealId && u.caiId == dietDetail.caiId)))
                    {
                        EntityDietdetailsCai voC = new EntityDietdetailsCai();
                        voC.recId = dietDetail.recId;
                        voC.day = dietDetail.day;
                        voC.mealId = dietDetail.mealId;
                        voC.caiId = dietDetail.caiId;
                        voC.caiName = dietDetail.caiName;
                        voC.weight = dietDetail.caiWeight;
                        voDietClone.lstDetailsCai.Add(voC);
                    }
                }
                else
                {
                    dietDetail.lstDetailsCai = new List<EntityDietdetailsCai>();
                    lstDietDetails.Add(dietDetail);
                }
            }

            if (lstDietDetails != null)
            {
                foreach (var temp in lstDietDetails)
                {
                    if (temp.lstDetailsCai != null)
                    {
                        foreach (var caiTemp in temp.lstDetailsCai)
                        {
                            List<EntityDietDetails> details = lstDietDetailsTmp.FindAll(r => r.recId == caiTemp.recId && r.day == caiTemp.day && r.mealId == caiTemp.mealId && r.caiId == caiTemp.caiId);
                            if (details != null)
                            {
                                caiTemp.lstDietdetailsIngrediet = new List<EntityDietDetails>();

                                foreach (var temp2 in details)
                                {
                                    EntityDietDetails Ingrediet = new EntityDietDetails();
                                    Ingrediet = temp2;
                                    caiTemp.lstDietdetailsIngrediet.Add(Ingrediet);
                                }
                            }
                        }
                    }
                }
            }
            this.gcData.DataSource = lstDietDetails;
            this.gcData.RefreshDataSource();

            if (lstDietDetails.Any(r => r.day == 1))
                chkDay1.Checked = true;
            else
                chkDay1.Checked = false;
            if (lstDietDetails.Any(r => r.day == 2))
                chkDay2.Checked = true;
            else
                chkDay2.Checked = false;
            if (lstDietDetails.Any(r => r.day == 3))
                chkDay3.Checked = true;
            else
                chkDay3.Checked = false;
            if (lstDietDetails.Any(r => r.day == 4))
                chkDay4.Checked = true;
            else
                chkDay4.Checked = false;
            if (lstDietDetails.Any(r => r.day == 5))
                chkDay5.Checked = true;
            else
                chkDay5.Checked = false;
            if (lstDietDetails.Any(r => r.day == 6))
                chkDay6.Checked = true;
            else
                chkDay6.Checked = false;
            if (lstDietDetails.Any(r => r.day == 7))
                chkDay7.Checked = true;
            else
                chkDay7.Checked = false;

            if (lstDietDetails.Any(r => r.day == 1))
            {
                cboDays.SelectedIndex = 1;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 1);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 2))
            {
                cboDays.SelectedIndex = 2;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 2);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 3))
            {
                cboDays.SelectedIndex = 3;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 3);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 4))
            {
                cboDays.SelectedIndex = 4;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 4);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 5))
            {
                cboDays.SelectedIndex = 5;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 5);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 6))
            {
                cboDays.SelectedIndex = 6;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 6);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 7))
            {
                cboDays.SelectedIndex = 7;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 7);
                gcData.RefreshDataSource();
            }
            #endregion

            #region 原则
            string principle = string.Empty;
            if (!string.IsNullOrEmpty(dietRecord.principle))
            {
                if (dietRecord.principle.Contains(";"))
                {
                    string[] arrPrinciple = dietRecord.principle.Split(';');
                    for (int i = 0; i < arrPrinciple.Length; i++)
                    {
                        EntityDietPrinciple pc = lstPrincipleAll.Find(r => r.principleId == arrPrinciple[i]);
                        if (pc != null)
                            principle += pc.principleName + " 、";
                    }
                }
                else
                {
                    EntityDietPrinciple pc = lstPrincipleAll.Find(r => r.principleId == dietRecord.principle);
                    if (pc != null)
                        principle += pc.principleName + " 、";
                }

                if (!string.IsNullOrEmpty(principle))
                {
                    principle = principle.TrimEnd('、');
                }
            }
            this.memDietPrinciple.Text = principle;
            #endregion

            #region 中医食疗
            string dietTreament = string.Empty;
            if (!string.IsNullOrEmpty(dietRecord.dietTreament))
            {
                if (dietRecord.dietTreament.Contains(";"))
                {
                    string[] arrDietTreament = dietRecord.dietTreament.Split(';');
                    for (int i = 0; i < arrDietTreament.Length; i++)
                    {
                        EntityDietTreatment pc = lstDietTreatmentAll.Find(r => r.id == arrDietTreament[i]);
                        if (pc != null)
                            dietTreament += pc.names + " 、";
                    }
                }
                else
                {
                    EntityDietTreatment pc = lstDietTreatmentAll.Find(r => r.id == dietRecord.dietTreament);
                    if (pc != null)
                        dietTreament += pc.names + " 、";
                }

                if (!string.IsNullOrEmpty(dietTreament))
                {
                    dietTreament = dietTreament.TrimEnd('、');
                }
            }
            this.memDietTreament.Text = dietTreament;
            #endregion
        }
        #endregion

        #region 模板导入方案食谱
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstDietDetailsTmp"></param>
        /// <returns></returns>
        void InitDietDetails(List<EntityDietDetails> lstDietDetailsTmp)
        {
            lstDietDetails = new List<EntityDietDetails>();
            if (lstDietDetailsTmp == null)
                return;

            #region 食谱
            foreach (var dietDetail in lstDietDetailsTmp)
            {
                if (lstDietDetails.Any(r => r.recId == dietDetail.recId && r.day == dietDetail.day && r.mealId == dietDetail.mealId))
                {
                    EntityDietDetails voDietClone = lstDietDetailsTmp.Find(r => r.recId == dietDetail.recId && r.day == dietDetail.day && r.mealId == dietDetail.mealId);
                    if (!voDietClone.lstDetailsCai.Any((u => u.recId == dietDetail.recId && u.day == dietDetail.day && u.mealId == dietDetail.mealId && u.caiId == dietDetail.caiId)))
                    {
                        EntityDietdetailsCai voC = new EntityDietdetailsCai();
                        voC.recId = dietDetail.recId;
                        voC.day = dietDetail.day;
                        voC.mealId = dietDetail.mealId;
                        voC.caiId = dietDetail.caiId;
                        voC.caiName = dietDetail.caiName;
                        voC.weight = dietDetail.caiWeight;
                        voDietClone.lstDetailsCai.Add(voC);
                    }
                }
                else
                {
                    dietDetail.lstDetailsCai = new List<EntityDietdetailsCai>();
                    lstDietDetails.Add(dietDetail);
                }
            }

            if (lstDietDetails != null)
            {
                foreach (var temp in lstDietDetails)
                {
                    if (temp.lstDetailsCai != null)
                    {
                        foreach (var caiTemp in temp.lstDetailsCai)
                        {
                            List<EntityDietDetails> details = lstDietDetailsTmp.FindAll(r => r.recId == caiTemp.recId && r.day == caiTemp.day && r.mealId == caiTemp.mealId && r.caiId == caiTemp.caiId);
                            if (details != null)
                            {
                                caiTemp.lstDietdetailsIngrediet = new List<EntityDietDetails>();

                                foreach (var temp2 in details)
                                {
                                    EntityDietDetails Ingrediet = new EntityDietDetails();
                                    Ingrediet = temp2;
                                    caiTemp.lstDietdetailsIngrediet.Add(Ingrediet);
                                }
                            }
                        }
                    }
                }
            }
            this.gcData.DataSource = lstDietDetails;
            this.gcData.RefreshDataSource();

            if (lstDietDetails.Any(r => r.day == 1))
                chkDay1.Checked = true;
            else
                chkDay1.Checked = false;
            if (lstDietDetails.Any(r => r.day == 2))
                chkDay2.Checked = true;
            else
                chkDay2.Checked = false;
            if (lstDietDetails.Any(r => r.day == 3))
                chkDay3.Checked = true;
            else
                chkDay3.Checked = false;
            if (lstDietDetails.Any(r => r.day == 4))
                chkDay4.Checked = true;
            else
                chkDay4.Checked = false;
            if (lstDietDetails.Any(r => r.day == 5))
                chkDay5.Checked = true;
            else
                chkDay5.Checked = false;
            if (lstDietDetails.Any(r => r.day == 6))
                chkDay6.Checked = true;
            else
                chkDay6.Checked = false;
            if (lstDietDetails.Any(r => r.day == 7))
                chkDay7.Checked = true;
            else
                chkDay7.Checked = false;

            if (lstDietDetails.Any(r => r.day == 1))
            {
                cboDays.SelectedIndex = 1;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 1);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 2))
            {
                cboDays.SelectedIndex = 2;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 2);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 3))
            {
                cboDays.SelectedIndex = 3;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 3);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 4))
            {
                cboDays.SelectedIndex = 4;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 4);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 5))
            {
                cboDays.SelectedIndex = 5;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 5);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 6))
            {
                cboDays.SelectedIndex = 6;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 6);
                gcData.RefreshDataSource();
            }
            else if (lstDietDetails.Any(r => r.day == 7))
            {
                cboDays.SelectedIndex = 7;
                gcData.DataSource = lstDietDetails.FindAll(r => r.day == 7);
                gcData.RefreshDataSource();
            }
            #endregion

            #region 原则
            string principle = string.Empty;
            if (dietRecord == null)
                return ;
            if (!string.IsNullOrEmpty(dietRecord.principle))
            {
                if (dietRecord.principle.Contains(";"))
                {
                    string[] arrPrinciple = dietRecord.principle.Split(';');
                    for (int i = 0; i < arrPrinciple.Length; i++)
                    {
                        EntityDietPrinciple pc = lstPrincipleAll.Find(r => r.principleId == arrPrinciple[i]);
                        if (pc != null)
                            principle += pc.principleName + " 、";
                    }
                }
                else
                {
                    EntityDietPrinciple pc = lstPrincipleAll.Find(r => r.principleId == dietRecord.principle);
                    if (pc != null)
                        principle += pc.principleName + " 、";
                }

                if (!string.IsNullOrEmpty(principle))
                {
                    principle = principle.TrimEnd('、');
                }
            }
            this.memDietPrinciple.Text = principle;
            #endregion

            #region 中医食疗
            string dietTreament = string.Empty;
            if (!string.IsNullOrEmpty(dietRecord.dietTreament))
            {
                if (dietRecord.dietTreament.Contains(";"))
                {
                    string[] arrDietTreament = dietRecord.dietTreament.Split(';');
                    for (int i = 0; i < arrDietTreament.Length; i++)
                    {
                        EntityDietTreatment pc = lstDietTreatmentAll.Find(r => r.id == arrDietTreament[i]);
                        if (pc != null)
                            dietTreament += pc.names + " 、";
                    }
                }
                else
                {
                    EntityDietTreatment pc = lstDietTreatmentAll.Find(r => r.id == dietRecord.dietTreament);
                    if (pc != null)
                        dietTreament += pc.names + " 、";
                }

                if (!string.IsNullOrEmpty(dietTreament))
                {
                    dietTreament = dietTreament.TrimEnd('、');
                }
            }
            this.memDietTreament.Text = dietTreament;
            #endregion
        }
        #endregion

        #region 模板食谱
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dietRecord"></param>
        /// <returns></returns>
        void InitTemplateDietDetails(EntityDietTemplate dietTemplate)
        {
            if (dietTemplate == null)
                return;

            if (lstDietTemplateDetails == null)
                lstDietTemplateDetails = new List<EntityDietTemplateDetails>();
            #region 食谱
            List<EntityDietTemplateDetails> lstDietDetailsTmp = null;
            using (ProxyHms proxy = new ProxyHms())
            {
                lstDietDetailsTmp = proxy.Service.GetDietTemplateDetails(dietTemplate.templateId);
            }

            if (lstDietDetailsTmp == null)
                return;

            foreach (var dietDetail in lstDietDetailsTmp)
            {
                if (lstDietTemplateDetails.Any(r => r.templateId == dietDetail.templateId && r.day == dietDetail.day && r.mealId == dietDetail.mealId))
                {
                    EntityDietTemplateDetails voDietClone = lstDietDetailsTmp.Find(r => r.templateId == dietDetail.templateId && r.day == dietDetail.day && r.mealId == dietDetail.mealId);
                    if (!voDietClone.lstDetailsCai.Any((u => u.templateId == dietDetail.templateId && u.day == dietDetail.day && u.mealId == dietDetail.mealId && u.caiId == dietDetail.caiId)))
                    {
                        EntityDietdetailsCai voC = new EntityDietdetailsCai();
                        voC.templateId = dietDetail.templateId;
                        voC.day = dietDetail.day;
                        voC.mealId = dietDetail.mealId;
                        voC.caiId = dietDetail.caiId;
                        voC.caiName = dietDetail.caiName;
                        voC.weight = dietDetail.caiWeight;
                        voDietClone.lstDetailsCai.Add(voC);
                    }
                }
                else
                {
                    dietDetail.lstDetailsCai = new List<EntityDietdetailsCai>();
                    lstDietTemplateDetails.Add(dietDetail);
                }
            }

            if (lstDietTemplateDetails != null)
            {
                foreach (var temp in lstDietTemplateDetails)
                {
                    if (temp.lstDetailsCai != null)
                    {
                        foreach (var caiTemp in temp.lstDetailsCai)
                        {
                            List<EntityDietTemplateDetails> details = lstDietDetailsTmp.FindAll(r => r.templateId == caiTemp.templateId
                            && r.day == caiTemp.day && r.mealId == caiTemp.mealId && r.caiId == caiTemp.caiId);
                            if (details != null)
                            {
                                caiTemp.lstDietTemplateDetails = new List<EntityDietTemplateDetails>();

                                foreach (var temp2 in details)
                                {
                                    EntityDietTemplateDetails Ingrediet = new EntityDietTemplateDetails();
                                    Ingrediet = temp2;
                                    caiTemp.lstDietTemplateDetails.Add(Ingrediet);
                                }
                            }
                        }
                    }
                }
            }
            this.gcData.DataSource = lstDietTemplateDetails;
            this.gcData.RefreshDataSource();

            if (lstDietTemplateDetails.Any(r => r.day == 1))
                chkDay1.Checked = true;
            else
                chkDay1.Checked = false;
            if (lstDietTemplateDetails.Any(r => r.day == 2))
                chkDay2.Checked = true;
            else
                chkDay2.Checked = false;
            if (lstDietTemplateDetails.Any(r => r.day == 3))
                chkDay3.Checked = true;
            else
                chkDay3.Checked = false;
            if (lstDietTemplateDetails.Any(r => r.day == 4))
                chkDay4.Checked = true;
            else
                chkDay4.Checked = false;
            if (lstDietTemplateDetails.Any(r => r.day == 5))
                chkDay5.Checked = true;
            else
                chkDay5.Checked = false;
            if (lstDietTemplateDetails.Any(r => r.day == 6))
                chkDay6.Checked = true;
            else
                chkDay6.Checked = false;
            if (lstDietTemplateDetails.Any(r => r.day == 7))
                chkDay7.Checked = true;
            else
                chkDay7.Checked = false;

            if (lstDietTemplateDetails.Any(r => r.day == 1))
            {
                cboDays.SelectedIndex = 1;
                gcData.DataSource = lstDietTemplateDetails.FindAll(r => r.day == 1);
                gcData.RefreshDataSource();
            }
            else if (lstDietTemplateDetails.Any(r => r.day == 2))
            {
                cboDays.SelectedIndex = 2;
                gcData.DataSource = lstDietTemplateDetails.FindAll(r => r.day == 2);
                gcData.RefreshDataSource();
            }
            else if (lstDietTemplateDetails.Any(r => r.day == 3))
            {
                cboDays.SelectedIndex = 3;
                gcData.DataSource = lstDietTemplateDetails.FindAll(r => r.day == 3);
                gcData.RefreshDataSource();
            }
            else if (lstDietTemplateDetails.Any(r => r.day == 4))
            {
                cboDays.SelectedIndex = 4;
                gcData.DataSource = lstDietTemplateDetails.FindAll(r => r.day == 4);
                gcData.RefreshDataSource();
            }
            else if (lstDietTemplateDetails.Any(r => r.day == 5))
            {
                cboDays.SelectedIndex = 5;
                gcData.DataSource = lstDietTemplateDetails.FindAll(r => r.day == 5);
                gcData.RefreshDataSource();
            }
            else if (lstDietTemplateDetails.Any(r => r.day == 6))
            {
                cboDays.SelectedIndex = 6;
                gcData.DataSource = lstDietTemplateDetails.FindAll(r => r.day == 6);
                gcData.RefreshDataSource();
            }
            else if (lstDietTemplateDetails.Any(r => r.day == 7))
            {
                cboDays.SelectedIndex = 7;
                gcData.DataSource = lstDietTemplateDetails.FindAll(r => r.day == 7);
                gcData.RefreshDataSource();
            }
            #endregion

            //#region 原则
            //string principle = string.Empty;
            //if (!string.IsNullOrEmpty(dietRecord.principle))
            //{
            //    if (dietRecord.principle.Contains(";"))
            //    {
            //        string[] arrPrinciple = dietRecord.principle.Split(';');
            //        for (int i = 0; i < arrPrinciple.Length; i++)
            //        {
            //            EntityDietPrinciple pc = lstPrincipleAll.Find(r => r.principleId == arrPrinciple[i]);
            //            if (pc != null)
            //                principle += pc.principleName + " 、";
            //        }
            //    }
            //    else
            //    {
            //        EntityDietPrinciple pc = lstPrincipleAll.Find(r => r.principleId == dietRecord.principle);
            //        if (pc != null)
            //            principle += pc.principleName + " 、";
            //    }

            //    if (!string.IsNullOrEmpty(principle))
            //    {
            //        principle = principle.TrimEnd('、');
            //    }
            //}
            //this.memDietPrinciple.Text = principle;
            //#endregion

            //#region 中医食疗
            //string dietTreament = string.Empty;
            //if (!string.IsNullOrEmpty(dietRecord.dietTreament))
            //{
            //    if (dietRecord.dietTreament.Contains(";"))
            //    {
            //        string[] arrDietTreament = dietRecord.dietTreament.Split(';');
            //        for (int i = 0; i < arrDietTreament.Length; i++)
            //        {
            //            EntityDietTreatment pc = lstDietTreatmentAll.Find(r => r.id == arrDietTreament[i]);
            //            if (pc != null)
            //                dietTreament += pc.names + " 、";
            //        }
            //    }
            //    else
            //    {
            //        EntityDietTreatment pc = lstDietTreatmentAll.Find(r => r.id == dietRecord.dietTreament);
            //        if (pc != null)
            //            dietTreament += pc.names + " 、";
            //    }

            //    if (!string.IsNullOrEmpty(dietTreament))
            //    {
            //        dietTreament = dietTreament.TrimEnd('、');
            //    }
            //}
            //this.memDietTreament.Text = dietTreament;
            //#endregion
        }
        #endregion

        #region 计算成份
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstDietDetails"></param>
        /// <returns></returns>
        List<EntityIngredietNutrition> CalcNutrition(List<EntityDietDetails> lstDietdetailsIngrediet, List<EntityDietTemplateDetails> lstDdTemplateIngrediet)
        {

            decimal recKcal = 0;
            decimal recProteint = 0;
            decimal recFat = 0;
            decimal recCho = 0;
            decimal recBrxxw = 0;
            decimal recDgc = 0;
            decimal recVitaminsA = 0;
            decimal recVitaminsD = 0;
            decimal recVitaminsE = 0;
            decimal recVitaminsB1 = 0;
            decimal recVitaminsB2 = 0;
            decimal recVitaminsC = 0;
            decimal recCa = 0;
            decimal recFe = 0;

            decimal proKcal = 0;
            decimal proProteint = 0;
            decimal proFat = 0;
            decimal proCho = 0;
            decimal proBrxxw = 0;
            decimal proDgc = 0;
            decimal proVitaminsA = 0;
            decimal proVitaminsD = 0;
            decimal proVitaminsE = 0;
            decimal proVitaminsB1 = 0;
            decimal proVitaminsB2 = 0;
            decimal proVitaminsC = 0;
            decimal proCa = 0;
            decimal proFe = 0;

            List<EntityIngredietNutrition> data = null;
            EntityIngredietNutrition idNut = null;

            if (lstDietdetailsIngrediet != null)
            {
                if (lstDietdetailsIngrediet != null && lstDietdetailsIngrediet.Count > 0)
                {
                    data = new List<EntityIngredietNutrition>();
                    foreach (var vo in lstDietDetails)
                    {
                        EntityDicDientIngredient dietIngrediet = lstDicDietIngrediet.Find(r => r.ingredientId == vo.caiIngredietId);
                        if (dietIngrediet == null)
                            continue;

                        #region 推荐
                        recKcal += dietIngrediet.kCal * vo.weight;
                        recProteint += dietIngrediet.proteint * vo.weight;
                        recFat += dietIngrediet.fat * vo.weight;
                        recCho += dietIngrediet.cho * vo.weight;
                        recBrxxw += dietIngrediet.brxxw * vo.weight;
                        recDgc += dietIngrediet.dgc * vo.weight;
                        recVitaminsA += dietIngrediet.vitaminsA * vo.weight;
                        recVitaminsD += dietIngrediet.vitaminsD * vo.weight;
                        recVitaminsE += dietIngrediet.vitaminsE * vo.weight;
                        recVitaminsB1 += dietIngrediet.thiamin * vo.weight;
                        recVitaminsB2 += dietIngrediet.riboflavin * vo.weight;
                        recVitaminsC += dietIngrediet.vitaminsC * vo.weight;
                        recCa += dietIngrediet.ca * vo.weight;
                        recFe += dietIngrediet.fe * vo.weight;
                        #endregion

                        #region 提供
                        proKcal += dietIngrediet.kCal * vo.realWeight;
                        proProteint += dietIngrediet.proteint * vo.realWeight;
                        proFat += dietIngrediet.fat * vo.realWeight;
                        proCho += dietIngrediet.cho * vo.realWeight;
                        proBrxxw += dietIngrediet.brxxw * vo.realWeight;
                        proDgc += dietIngrediet.dgc * vo.realWeight;
                        proVitaminsA += dietIngrediet.vitaminsA * vo.realWeight;
                        proVitaminsD += dietIngrediet.vitaminsD * vo.realWeight;
                        proVitaminsE += dietIngrediet.vitaminsE * vo.realWeight;
                        proVitaminsB1 += dietIngrediet.thiamin * vo.realWeight;
                        proVitaminsB2 += dietIngrediet.riboflavin * vo.realWeight;
                        proVitaminsC += dietIngrediet.vitaminsC * vo.realWeight;
                        proCa += dietIngrediet.ca * vo.realWeight;
                        proFe += dietIngrediet.fe * vo.realWeight;
                        #endregion
                    }

                    #region 
                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "能量";
                    idNut.recoJ = (recKcal / 100).ToString("0.00") + " Kcal";
                    idNut.proJ = (proKcal / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "蛋白质";
                    idNut.recoJ = (recProteint / 100).ToString("0.00") + " g";
                    idNut.proJ = (proProteint / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "脂肪";
                    idNut.recoJ = (recFat / 100).ToString("0.00") + " g";
                    idNut.proJ = (proFat / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "碳水化合物";
                    idNut.recoJ = (recCho / 100).ToString("0.00") + " g";
                    idNut.proJ = (proCho / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "膳食纤维";
                    idNut.recoJ = (recBrxxw / 100).ToString("0.00") + " g";
                    idNut.proJ = (proBrxxw / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "胆固醇";
                    idNut.recoJ = (recDgc / 100).ToString("0.00") + " g";
                    idNut.proJ = (proDgc / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素A";
                    idNut.recoJ = (recVitaminsA / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsA / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素D";
                    idNut.recoJ = (recVitaminsD / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsD / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素E";
                    idNut.recoJ = (recVitaminsE / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsE / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素B1";
                    idNut.recoJ = (recVitaminsB1 / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsB1 / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素B2";
                    idNut.recoJ = (recVitaminsB2 / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsB2 / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素C";
                    idNut.recoJ = (recVitaminsC / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsC / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "钙";
                    idNut.recoJ = (recCa / 100).ToString("0.00") + " g";
                    idNut.proJ = (proCa / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "铁";
                    idNut.recoJ = (recFe / 100).ToString("0.00") + " g";
                    idNut.proJ = (proFe / 100).ToString("0.00") + " g";
                    data.Add(idNut);
                    #endregion
                }
            }
            else
            {
                if (lstDdTemplateIngrediet != null && lstDdTemplateIngrediet.Count > 0)
                {
                    data = new List<EntityIngredietNutrition>();
                    foreach (var vo in lstDietTemplateDetails)
                    {
                        EntityDicDientIngredient dietIngrediet = lstDicDietIngrediet.Find(r => r.ingredientId == vo.caiIngredietId);
                        if (dietIngrediet == null)
                            continue;

                        #region 推荐
                        recKcal += dietIngrediet.kCal * vo.weight;
                        recProteint += dietIngrediet.proteint * vo.weight;
                        recFat += dietIngrediet.fat * vo.weight;
                        recCho += dietIngrediet.cho * vo.weight;
                        recBrxxw += dietIngrediet.brxxw * vo.weight;
                        recDgc += dietIngrediet.dgc * vo.weight;
                        recVitaminsA += dietIngrediet.vitaminsA * vo.weight;
                        recVitaminsD += dietIngrediet.vitaminsD * vo.weight;
                        recVitaminsE += dietIngrediet.vitaminsE * vo.weight;
                        recVitaminsB1 += dietIngrediet.thiamin * vo.weight;
                        recVitaminsB2 += dietIngrediet.riboflavin * vo.weight;
                        recVitaminsC += dietIngrediet.vitaminsC * vo.weight;
                        recCa += dietIngrediet.ca * vo.weight;
                        recFe += dietIngrediet.fe * vo.weight;
                        #endregion

                        #region 提供
                        proKcal += dietIngrediet.kCal * vo.realWeight;
                        proProteint += dietIngrediet.proteint * vo.realWeight;
                        proFat += dietIngrediet.fat * vo.realWeight;
                        proCho += dietIngrediet.cho * vo.realWeight;
                        proBrxxw += dietIngrediet.brxxw * vo.realWeight;
                        proDgc += dietIngrediet.dgc * vo.realWeight;
                        proVitaminsA += dietIngrediet.vitaminsA * vo.realWeight;
                        proVitaminsD += dietIngrediet.vitaminsD * vo.realWeight;
                        proVitaminsE += dietIngrediet.vitaminsE * vo.realWeight;
                        proVitaminsB1 += dietIngrediet.thiamin * vo.realWeight;
                        proVitaminsB2 += dietIngrediet.riboflavin * vo.realWeight;
                        proVitaminsC += dietIngrediet.vitaminsC * vo.realWeight;
                        proCa += dietIngrediet.ca * vo.realWeight;
                        proFe += dietIngrediet.fe * vo.realWeight;
                        #endregion
                    }

                    #region 
                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "能量";
                    idNut.recoJ = (recKcal / 100).ToString("0.00") + " Kcal";
                    idNut.proJ = (proKcal / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "蛋白质";
                    idNut.recoJ = (recProteint / 100).ToString("0.00") + " g";
                    idNut.proJ = (proProteint / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "脂肪";
                    idNut.recoJ = (recFat / 100).ToString("0.00") + " g";
                    idNut.proJ = (proFat / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "碳水化合物";
                    idNut.recoJ = (recCho / 100).ToString("0.00") + " g";
                    idNut.proJ = (proCho / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "膳食纤维";
                    idNut.recoJ = (recBrxxw / 100).ToString("0.00") + " g";
                    idNut.proJ = (proBrxxw / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "胆固醇";
                    idNut.recoJ = (recDgc / 100).ToString("0.00") + " g";
                    idNut.proJ = (proDgc / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素A";
                    idNut.recoJ = (recVitaminsA / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsA / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素D";
                    idNut.recoJ = (recVitaminsD / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsD / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素E";
                    idNut.recoJ = (recVitaminsE / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsE / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素B1";
                    idNut.recoJ = (recVitaminsB1 / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsB1 / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素B2";
                    idNut.recoJ = (recVitaminsB2 / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsB2 / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "维生素C";
                    idNut.recoJ = (recVitaminsC / 100).ToString("0.00") + " g";
                    idNut.proJ = (proVitaminsC / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "钙";
                    idNut.recoJ = (recCa / 100).ToString("0.00") + " g";
                    idNut.proJ = (proCa / 100).ToString("0.00") + " g";
                    data.Add(idNut);

                    idNut = new EntityIngredietNutrition();
                    idNut.itemName = "铁";
                    idNut.recoJ = (recFe / 100).ToString("0.00") + " g";
                    idNut.proJ = (proFe / 100).ToString("0.00") + " g";
                    data.Add(idNut);
                    #endregion
                }
            }

            return data;
        }
        #endregion

        #region GetRowDietDetail
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal EntityDietDetails GetRowDietDetail()
        {
            EntityDietDetails vo = null;
            if (this.gvData.FocusedRowHandle >= 0)
                vo = this.gvData.GetRow(this.gvData.FocusedRowHandle) as EntityDietDetails;

            return vo;
        }
        #endregion

        #region RefreshDetails
        void RefreshDetails()
        {
            if (lstDietDetails == null)
            {
                gcData.DataSource = null;
                gcData.RefreshDataSource();
                return;
            }

            if (lstDietDetails.Any(r => r.day == 1))
                chkDay1.Checked = true;
            else
                chkDay1.Checked = false;
            if (lstDietDetails.Any(r => r.day == 2))
                chkDay2.Checked = true;
            else
                chkDay2.Checked = false;
            if (lstDietDetails.Any(r => r.day == 3))
                chkDay3.Checked = true;
            else
                chkDay3.Checked = false;
            if (lstDietDetails.Any(r => r.day == 4))
                chkDay4.Checked = true;
            else
                chkDay4.Checked = false;
            if (lstDietDetails.Any(r => r.day == 5))
                chkDay5.Checked = true;
            else
                chkDay5.Checked = false;
            if (lstDietDetails.Any(r => r.day == 6))
                chkDay6.Checked = true;
            else
                chkDay6.Checked = false;
            if (lstDietDetails.Any(r => r.day == 7))
                chkDay7.Checked = true;
            else
                chkDay7.Checked = false;

            gcData.DataSource = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex);
            gcData.RefreshDataSource();
        }
        #endregion

        #region 保存方案
        /// <summary>
        /// 
        /// </summary>
        void SaveDietDetails()
        {
            int affect = 0;
            Dictionary<string, decimal> dicRecId = null;
            if (lstClient == null)
            {
                DialogBox.Msg("客户为空，请选择！");
                return;
            }
            if (lstDietRecord == null)
                lstDietRecord = new List<EntityDietRecord>();

            foreach (var client in lstClient)
            {
                EntityDietRecord tempVo = null;
                if (lstDietRecord != null)
                    tempVo = lstDietRecord.Find(r => r.clientNo == client.clientNo && r.regTimes == client.regTimes);
                if (tempVo == null || tempVo.recId <= 0)
                {
                    EntityDietRecord dietRecord = new EntityDietRecord();
                    dietRecord.clientNo = client.clientNo;
                    dietRecord.regTimes = client.regTimes;

                    lstDietRecord.Add(dietRecord);
                }
            }

            if (lstDietRecord != null)
            {
                foreach (var dietR in lstDietRecord)
                {
                    if (lstDietDetails.Any(r => r.day == 1))
                        dietR.day1 = 1;
                    if (lstDietDetails.Any(r => r.day == 2))
                        dietR.day2 = 1;
                    if (lstDietDetails.Any(r => r.day == 3))
                        dietR.day3 = 1;
                    if (lstDietDetails.Any(r => r.day == 4))
                        dietR.day4 = 1;
                    if (lstDietDetails.Any(r => r.day == 5))
                        dietR.day5 = 1;
                    if (lstDietDetails.Any(r => r.day == 6))
                        dietR.day6 = 1;
                    if (lstDietDetails.Any(r => r.day == 7))
                        dietR.day7 = 1;

                    if (lstPrinciple != null)
                    {
                        foreach (var pc in lstPrinciple)
                        {
                            dietR.principle += pc.principleId + ";";
                        }
                        if (!string.IsNullOrEmpty(dietR.principle))
                            dietR.principle = dietR.principle.TrimEnd(';');
                    }

                    if (lstDietTreatment != null)
                    {
                        foreach (var pc in lstDietTreatment)
                        {
                            dietR.dietTreament += pc.id + ";";
                        }
                        if (!string.IsNullOrEmpty(dietR.dietTreament))
                            dietR.dietTreament = dietR.dietTreament.TrimEnd(';');
                    }
                }
                using (ProxyHms proxy = new ProxyHms())
                {
                    affect = proxy.Service.SaveDietCai(lstDietRecord, lstDietDetails, out dicRecId);
                }
            }

            if (affect > 0)
            {
                foreach (var recVo in lstDietRecord)
                {
                    recVo.recId = dicRecId[recVo.clientNo];
                }
                DialogBox.Msg("保存成功 ！");
            }
            else
            {
                DialogBox.Msg("保存失败 ！");
            }
        }
        #endregion

        #endregion

        #region events
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmpopup2060202 frm = new frmpopup2060202(lstClient);
            frm.ShowDialog();
            lstClient = frm.lstClientSelect;
            gcSelectClient.DataSource = lstClient;
            gcSelectClient.RefreshDataSource();
        }

        private void btnDelPerson_Click(object sender, EventArgs e)
        {
            if (cvSelectClient.FocusedRowHandle >= 0)
            {
                EntityClientInfo vo = cvSelectClient.GetRow(cvSelectClient.FocusedRowHandle) as EntityClientInfo;
                if (vo != null)
                {
                    lstClient.Remove(vo);
                    gcSelectClient.DataSource = lstClient;
                    gcSelectClient.RefreshDataSource();
                }
            }
        }

        private void btnAddBreakfast_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDays.Text))
            {

                DialogBox.Msg("请选择天数！");
                return;
            }

            frmpopup2060203 frm = new frmpopup2060203(EnumMeals.breakfast, cboDays.SelectedIndex);
            frm.ShowDialog();

            if (frm.isSave)
            {
                if (frm.lstCaiDiet != null)
                {
                    List<EntityDietDetails> lstTemp = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex && r.mealId == (int)EnumMeals.breakfast);
                    if (lstTemp != null)
                    {
                        foreach (var tmp in lstTemp)
                            lstDietDetails.Remove(tmp);
                    }

                    lstDietDetails.AddRange(frm.lstCaiDiet);
                }
                RefreshDetails();
            }
        }

        private void btnAddLunch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDays.Text))
            {

                DialogBox.Msg("请选择天数！");
                return;
            }
            frmpopup2060203 frm = new frmpopup2060203(EnumMeals.lunch, cboDays.SelectedIndex);
            frm.ShowDialog();

            if (frm.isSave)
            {
                if (frm.lstCaiDiet != null)
                {
                    List<EntityDietDetails> lstTemp = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex && r.mealId == (int)EnumMeals.lunch);
                    if (lstTemp != null)
                    {
                        foreach (var tmp in lstTemp)
                            lstDietDetails.Remove(tmp);
                    }

                    lstDietDetails.AddRange(frm.lstCaiDiet);
                }

                RefreshDetails();
            }
        }

        private void btnAddDinner_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDays.Text))
            {
                DialogBox.Msg("请选择天数！");
                return;
            }
            frmpopup2060203 frm = new frmpopup2060203(EnumMeals.diner, cboDays.SelectedIndex);
            frm.ShowDialog();

            if (frm.isSave)
            {
                if (frm.lstCaiDiet != null)
                {
                    List<EntityDietDetails> lstTemp = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex && r.mealId == (int)EnumMeals.diner);
                    if (lstTemp != null)
                    {
                        foreach (var tmp in lstTemp)
                            lstDietDetails.Remove(tmp);
                    }

                    lstDietDetails.AddRange(frm.lstCaiDiet);
                }
                RefreshDetails();
            }
        }

        private void frmpopup2060201_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void btnSaveDietCai_Click(object sender, EventArgs e)
        {
            SaveDietDetails();
        }

        private void btnDelCai_Click(object sender, EventArgs e)
        {
            EntityDietDetails vo = GetRowDietDetail();

            if (vo != null)
            {
                List<EntityDietDetails> lstDel = lstDietDetails.FindAll(r => r.day == vo.day && r.mealId == vo.mealId && r.caiId == vo.caiId);
                if (lstDel != null)
                {
                    for (int i = 0; i < lstDel.Count; i++)
                    {
                        lstDietDetails.Remove(lstDel[i]);
                    }
                }

                RefreshDetails();
            }
        }

        private void cboDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<EntityDietDetails> lstDietDetailsTemp = null;
            List<EntityDietTemplateDetails> lstDietTemplteDetailsTmp = null;
            if (dietRecord != null || lstDietDetails.Count > 0)
            {
                if (!string.IsNullOrEmpty(cboDays.Text))
                {
                    if (lstDietDetails != null)
                    {
                        lstDietDetailsTemp = lstDietDetails.FindAll(r => r.day == cboDays.SelectedIndex);
                        this.gcData.DataSource = lstDietDetailsTemp;
                        gcData.RefreshDataSource();
                    }

                    this.gcDietNutrtion.DataSource = null;
                    gcDietNutrtion.RefreshDataSource();
                }

                if (lstDietDetailsTemp != null)
                {
                    this.gcDietNutrtion.DataSource = CalcNutrition(lstDietDetailsTemp, null);
                    gcDietNutrtion.RefreshDataSource();
                }
                else
                {
                    this.gcDietNutrtion.DataSource = null;
                    gcDietNutrtion.RefreshDataSource();
                }
            }
            else if(lstDietTemplateDetails != null)
            {
                if (!string.IsNullOrEmpty(cboDays.Text))
                {
                    if (lstDietDetails != null)
                    {
                        lstDietTemplteDetailsTmp = lstDietTemplateDetails.FindAll(r => r.day == cboDays.SelectedIndex);
                        this.gcData.DataSource = lstDietTemplteDetailsTmp;
                        gcData.RefreshDataSource();
                    }

                    this.gcDietNutrtion.DataSource = null;
                    gcDietNutrtion.RefreshDataSource();
                }

                if (lstDietTemplteDetailsTmp != null)
                {
                    this.gcDietNutrtion.DataSource = CalcNutrition(null, lstDietTemplteDetailsTmp);
                    gcDietNutrtion.RefreshDataSource();
                }
                else
                {
                    this.gcDietNutrtion.DataSource = null;
                    gcDietNutrtion.RefreshDataSource();
                }
            }
        }

        private void btnAddDietPrinciple_Click(object sender, EventArgs e)
        {
            frmPopup2060204 frm = new frmPopup2060204(lstPrinciple);
            frm.ShowDialog();
            string principle = string.Empty;
            if (frm.isRefresh)
                lstPrinciple = frm.lstChoose;

            if (lstPrinciple != null)
            {
                foreach (var vo in lstPrinciple)
                {
                    principle += vo.principleName + "  、";
                }
            }

            if (!string.IsNullOrEmpty(principle))
            {
                principle = principle.TrimEnd('、');

            }
            memDietPrinciple.Text = principle;
        }

        private void gvDietCai_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                DevExpress.XtraGrid.Views.Grid.GridView currentView = (DevExpress.XtraGrid.Views.Grid.GridView)this.gcData.FocusedView;
                var res = currentView.GetRow(e.RowHandle) as EntityDietdetailsCai;

                if (res != null)
                {
                    lblCaiName.Text = res.caiName;
                    if (dietRecord != null || lstDietDetails.Count > 0)
                        this.gcIngrediet.DataSource = res.lstDietdetailsIngrediet;
                    else
                        this.gcIngrediet.DataSource = res.lstDietTemplateDetails;
                    this.gcIngrediet.RefreshDataSource();
                }
            }
        }

        private void gvDietCai_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                DevExpress.XtraGrid.Views.Grid.GridView currentView = (DevExpress.XtraGrid.Views.Grid.GridView)this.gcData.FocusedView;
                var res = currentView.GetRow(e.RowHandle) as EntityDietdetailsCai;

                if (res != null)
                {
                    if (res.lstDietdetailsIngrediet != null)
                    {
                        foreach (var vo in res.lstDietdetailsIngrediet)
                        {
                            vo.realWeight = res.weight * vo.per;
                            vo.caiWeight = res.weight;
                        }
                    }
                }
            }
        }

        private void gvDietCai_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                DevExpress.XtraGrid.Views.Grid.GridView currentView = (DevExpress.XtraGrid.Views.Grid.GridView)this.gcData.FocusedView;
                var res = currentView.GetRow(e.FocusedRowHandle) as EntityDietdetailsCai;

                if (res != null)
                {
                    lblCaiName.Text = res.caiName;
                    if (dietRecord != null || lstDietDetails.Count > 0)
                        this.gcIngrediet.DataSource = res.lstDietdetailsIngrediet;
                    else
                        this.gcIngrediet.DataSource = res.lstDietTemplateDetails;
                    this.gcIngrediet.RefreshDataSource();
                }
            }
        }

        private void btnAddZysl_Click(object sender, EventArgs e)
        {
            frmPopup2060205 frm = new frmPopup2060205(lstDietTreatment);
            frm.ShowDialog();
            string dietTreatment = string.Empty;
            if (frm.isRefresh)
                lstDietTreatment = frm.lstChoose;

            if (lstDietTreatment != null)
            {
                foreach (var vo in lstDietTreatment)
                {
                    dietTreatment += vo.names + "  、";
                }
            }

            if (!string.IsNullOrEmpty(dietTreatment))
            {
                dietTreatment = dietTreatment.TrimEnd('、');

            }
            memDietTreament.Text = dietTreatment;
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            frmPopup2060301 frm = new frmPopup2060301(lstDietDetails);
            frm.ShowDialog();
        }

        private void btnImportTemplate_Click(object sender, EventArgs e)
        {
            List<EntityDietDetails> lstDietDetailsTemp = new List<EntityDietDetails>();
            frmPopup2060206 frm = new frmPopup2060206();
            frm.ShowDialog();

            if (frm.isRefresh)
            {
                if(frm.lstDietTemplateDetails != null)
                {
                    foreach (var temp in frm.lstDietTemplateDetails)
                    {
                        EntityDietDetails vo = new EntityDietDetails();
                        Dictionary<string, string> map = new Dictionary<string, string>() {
                            { "day","day"},
                            {  "mealId","mealId"},
                            {  "mealType","mealType"},
                            {  "caiId","caiId"},
                            {  "caiName","caiName"},
                            {  "caiIngrediet","caiIngrediet"},
                            {  "caiIngredietId","caiIngredietId"},
                            {  "weight","weight"},
                            {  "realWeight","realWeight"},
                            {  "caiWeight","caiWeight"},
                            {  "per","per"} };
                        vo.recId = 0;
                        vo = Function.MapperToModel(vo,temp,map);
                        lstDietDetailsTemp.Add(vo);
                    }
                }
                InitDietDetails(lstDietDetailsTemp);
            }
        }
        #endregion
    }
}
